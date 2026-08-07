using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
// TS MODULES
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel.astel_modules{
    public partial class AstelLogin : Form{
        public AstelLogin(){ InitializeComponent(); }
        // LOGIN PRELOADER
        // ======================================================================================================
        string login_global_lang;
        public void Login_system_preloader(){
            try{
                TSSettingsModule software_read_settings = new TSSettingsModule(ts_sf);
                int theme_mode = int.TryParse(software_read_settings.TSReadSettings(ts_settings_container, "ThemeStatus"), out int the_status) && (the_status == 0 || the_status == 1 || the_status == 2) ? the_status : 1;
                theme_mode = TSThemeModeHelper.GetSystemTheme(theme_mode);
                //
                TSThemeModeHelper.SetThemeMode(theme_mode == 0);
                TSThemeModeHelper.InitializeThemeForForm(this);
                //
                BackColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_BGColor2");
                Panel_BG.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_BGColor");
                //
                foreach (Control control in Panel_BG.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_LabelColor1");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_BGColor2");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_LabelColor1");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_AccentColor");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_AccentColor");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_AccentColor");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentColorHover");
                    }
                }
                //
                TSImageRenderer(BtnLogin, theme_mode == 1 ? Properties.Resources.ct_login_light : Properties.Resources.ct_login_dark, 18, ContentAlignment.MiddleRight);
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_BGColor2");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_LabelColor1");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_LabelColor1");
                CheckPassword.CheckedColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_AccentColor");
                CheckPassword.CheckMarkColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_BGColor2");
                CheckPassword.UncheckedBorderColor = TS_ThemeEngine.ColorMode(theme_mode, "CheckBoxUnCheckBorderColor");
                // ======================================================================================================
                string lang_code = software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus");
                string selectedLangCode = TSPreloaderSetDefaultLanguage(lang_code);
                string lang_file_path = AllLanguageFiles[selectedLangCode];
                TSGetLangs software_lang = new TSGetLangs(lang_file_path);
                login_global_lang = lang_file_path;
                // TEXTS
                Text = string.Format(software_lang.TSReadLangs("AstelLogin", "al_title"), Application.ProductName);
                LabelHeader.Text = string.Format(software_lang.TSReadLangs("AstelLogin", "al_header"), Environment.UserName);
                LabelPassword.Text = software_lang.TSReadLangs("AstelLogin", "al_label_password");
                CheckPassword.Text = software_lang.TSReadLangs("AstelLogin", "al_visible");
                BtnLogin.Text = " " + software_lang.TSReadLangs("AstelLogin", "al_btn");
                // PASS VISIBLE MODE
                string pass_vis_mode = software_read_settings.TSReadSettings(ts_settings_container, "LoginPassVisible");
                if (string.IsNullOrEmpty(pass_vis_mode)){ pass_vis_mode = "0"; }
                bool pass_vis_mode_bool = pass_vis_mode == "1";
                TxtPassword.UseSystemPasswordChar = !pass_vis_mode_bool;
                CheckPassword.Checked = pass_vis_mode_bool;
            }catch (Exception){ }
        }
        // LOGIN LOAD
        // ======================================================================================================
        private void AstelLogin_Load(object sender, EventArgs e){
            TxtPassword.UseSystemPasswordChar = true;
            AcceptButton = BtnLogin;
            //
            Login_system_preloader();
        }
        // LOGIN BTN
        // ======================================================================================================
        private async void BtnLogin_Click(object sender, EventArgs e){
            await Login_system();
        }
        // LOGIN FUNCTION
        // ======================================================================================================
        private async Task Login_system(){
            TSGetLangs software_lang = new TSGetLangs(login_global_lang);
            string get_password = TxtPassword.Text.Trim();
            if (string.IsNullOrEmpty(get_password)){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelLogin", "al_password_info"));
                return;
            }
            //
            Text = $"{string.Format(software_lang.TSReadLangs("AstelLogin", "al_title"), Application.ProductName)} - " + software_lang.TSReadLangs("AstelLogin", "al_check_login");
            TxtPassword.Enabled = false;
            BtnLogin.Enabled = false;
            //
            bool login_status = await Task.Run(() =>{
                try{
                    TSSettingsModule read = new TSSettingsModule(ts_session_file);
                    string protected_salt = read.TSReadSettings(ts_session_container, "PasswordSalt");
                    string protected_password = read.TSReadSettings(ts_session_container, "PasswordHash");
                    if (string.IsNullOrEmpty(protected_salt) || string.IsNullOrEmpty(protected_password))
                        return false;
                    try{
                        string saved_salt = TS_SessionProtection.UnprotectSessionData(protected_salt);
                        string saved_password = TS_SessionProtection.UnprotectSessionData(protected_password);
                        string hashed_input = TSHashPassword(get_password, saved_salt, TSSecureModule.PasswordHashIterations);
                        return FixedTimeStringEquals(hashed_input, saved_password);
                    }catch(Exception){
                        return false;
                    }
                }catch(Exception){
                    return false;
                }
            });
            //
            if (login_status){
                new AstelMain().Show();
                Hide();
            }else{
                TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelLogin", "al_password_failed"), "\n\n"));
                BeginInvoke(new Action(() =>{
                    TxtPassword.Focus();
                }));
            }
            //
            Text = string.Format(software_lang.TSReadLangs("AstelLogin", "al_title"), Application.ProductName);
            TxtPassword.Enabled = true;
            BtnLogin.Enabled = true;
        }
        // CHECK PASSWORD VISIBLE
        // ======================================================================================================
        private void CheckPassword_CheckedChanged(object sender, EventArgs e){
            if (CheckPassword.Checked == true){
                TxtPassword.UseSystemPasswordChar = false;
            }else if (CheckPassword.Checked == false){
                TxtPassword.UseSystemPasswordChar = true;
            }
            try{
                TSSettingsModule software_setting_save = new TSSettingsModule(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "LoginPassVisible", (CheckPassword.Checked ? 1 : 0).ToString());
            }catch (Exception) { }
        }
        // EXIT
        // ======================================================================================================
        private void AstelLogin_FormClosing(object sender, FormClosingEventArgs e) {
            Application.Exit();
        }
    }
}