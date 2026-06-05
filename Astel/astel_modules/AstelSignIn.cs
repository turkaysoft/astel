using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
// TS MODULES
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel.astel_modules{
    public partial class AstelSignIn : Form{
        public AstelSignIn(){ InitializeComponent(); }
        // SIGN IN PRELOADER
        // ======================================================================================================
        string signin_global_lang;
        public void Login_system_preloader(){
            try{
                TSSettingsModule software_read_settings = new TSSettingsModule(ts_sf);
                int theme_mode = int.TryParse(software_read_settings.TSReadSettings(ts_settings_container, "ThemeStatus"), out int the_status) && (the_status == 0 || the_status == 1 || the_status == 2) ? the_status : 1;
                theme_mode = TSThemeModeHelper.GetSystemTheme(theme_mode);
                //
                TSThemeModeHelper.SetThemeMode(theme_mode == 0);
                TSThemeModeHelper.InitializeThemeForForm(this);
                //
                BackColor = TS_ThemeEngine.ColorMode(theme_mode, "PageContainerUIBGColor");
                Panel_BG.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "HeaderBGColor2");
                //
                foreach (Control control in Panel_BG.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "ContentLabelLeftColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "TextboxBGColor");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "TextboxFEColor");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentColor");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentColor");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentColor");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentColorHover");
                    }
                }
                //
                TSImageRenderer(BtnSignIn, theme_mode == 1 ? Properties.Resources.ct_confirm_light : Properties.Resources.ct_confirm_dark, 18, ContentAlignment.MiddleRight);
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "PageContainerUIBGColor");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "ContentLabelLeftColor");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "ContentLabelLeftColor");
                CheckPassword.CheckedColor = TS_ThemeEngine.ColorMode(theme_mode, "AccentColor");
                CheckPassword.CheckMarkColor = TS_ThemeEngine.ColorMode(theme_mode, "SelectBoxBGColor");
                CheckPassword.UncheckedBorderColor = TS_ThemeEngine.ColorMode(theme_mode, "CheckBoxUnCheckBorderColor");
                // ======================================================================================================
                string lang_code = software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus");
                string selectedLangCode = TSPreloaderSetDefaultLanguage(lang_code);
                string lang_file_path = AllLanguageFiles[selectedLangCode];
                TSGetLangs software_lang = new TSGetLangs(lang_file_path);
                signin_global_lang = lang_file_path;
                // TEXTS
                Text = string.Format(software_lang.TSReadLangs("AstelSignIn", "as_title"), Application.ProductName);
                LabelHeader.Text = software_lang.TSReadLangs("AstelSignIn", "as_header");
                LabelPassword.Text = software_lang.TSReadLangs("AstelSignIn", "as_label_password");
                LabelPasswordRepeat.Text = software_lang.TSReadLangs("AstelSignIn", "as_label_password_repeat");
                CheckPassword.Text = software_lang.TSReadLangs("AstelSignIn", "as_visible");
                BtnSignIn.Text = " " + software_lang.TSReadLangs("AstelSignIn", "as_btn");
            }catch (Exception){ }
        }
        // SIGN IN LOAD
        // ======================================================================================================
        private void AstelSignIn_Load(object sender, EventArgs e){
            TxtPassword.UseSystemPasswordChar = true;
            TxtPasswordRepeat.UseSystemPasswordChar = true;
            AcceptButton = BtnSignIn;
            //
            Login_system_preloader();
        }
        // SIGN IN BTN
        // ======================================================================================================
        private async void BtnSignIn_Click(object sender, EventArgs e){
            await Sign_in_function();
        }
        // SIGN IN FUNCTION
        // ======================================================================================================
        private async Task Sign_in_function(){
            TSGetLangs software_lang = new TSGetLangs(signin_global_lang);
            string password_1 = TxtPassword.Text.Trim();
            string password_2 = TxtPasswordRepeat.Text.Trim();
            //
            if (string.IsNullOrEmpty(password_1)){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelSignIn", "as_password_info"));
                BeginInvoke(new Action(() => {
                    TxtPassword.Focus();
                }));
                return;
            }
            if (string.IsNullOrEmpty(password_2)){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelSignIn", "as_password_repeat_info"));
                BeginInvoke(new Action(() => {
                    TxtPasswordRepeat.Focus();
                }));
                return;
            }
            if (password_1.Length < 6 || password_1.Length > 32){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelSignIn", "as_password_req_info"));
                BeginInvoke(new Action(() => {
                    TxtPassword.Focus();
                }));
                return;
            }
            if (password_1 != password_2){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelSignIn", "as_password_set_failed"), "\n"));
                return;
            }
            //
            Text = $"{string.Format(software_lang.TSReadLangs("AstelSignIn", "as_title"), Application.ProductName)} - " + software_lang.TSReadLangs("AstelSignIn", "as_check_signin");
            TxtPassword.Enabled = false;
            TxtPasswordRepeat.Enabled = false;
            BtnSignIn.Enabled = false;
            //
            bool set_password_status = await Task.Run(() =>{
                try{
                    string salt = GenerateSalt(32);
                    string hashed_password = TSHashPassword(password_1, salt, 210000);
                    string crossLinker = GenerateSecureRandomString(32);
                    TSSettingsModule software_setting_save = new TSSettingsModule(ts_session_file);
                    software_setting_save.TSWriteSettings(ts_session_container, "SessionMode", "1");
                    software_setting_save.TSWriteSettings(ts_session_container, "PasswordHash", TS_SessionProtection.ProtectSessionData(hashed_password));
                    software_setting_save.TSWriteSettings(ts_session_container, "PasswordSalt", TS_SessionProtection.ProtectSessionData(salt));
                    software_setting_save.TSWriteSettings(ts_session_container, "CrossLinker", TS_SessionProtection.ProtectSessionData(crossLinker));
                    return true;
                }catch(Exception){
                    return false;
                }
            });
            //
            if (set_password_status){
                TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelSignIn", "as_password_set_success"));
                AstelMain astel = new AstelMain();
                astel.Show();
                Hide();
            }
            //
            Text = string.Format(software_lang.TSReadLangs("AstelSignIn", "as_title"), Application.ProductName);
            TxtPassword.Enabled = true;
            TxtPasswordRepeat.Enabled = true;
            BtnSignIn.Enabled = true;
        }
        // CHECK PASSWORD VISIBLE
        // ======================================================================================================
        private void CheckPassword_CheckedChanged(object sender, EventArgs e){
            if (CheckPassword.Checked == true){
                TxtPassword.UseSystemPasswordChar = false;
                TxtPasswordRepeat.UseSystemPasswordChar = false;
            }else if (CheckPassword.Checked == false){
                TxtPassword.UseSystemPasswordChar = true;
                TxtPasswordRepeat.UseSystemPasswordChar = true;
            }
        }
        // EXIT
        // ======================================================================================================
        private void AstelSignIn_FormClosing(object sender, FormClosingEventArgs e){
            Application.Exit();
        }
    }
}