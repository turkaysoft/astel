using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
// TS MODULES
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel.astel_modules{
    public partial class AstelChangePassword : Form{
        public AstelChangePassword(){ InitializeComponent(); }
        // SIGN IN PRELOADER
        // ======================================================================================================
        public void Change_password_system_preloader(){
            try{
                TSThemeModeHelper.InitializeThemeForForm(this);
                //
                BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                Panel_BG.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor");
                //
                foreach (Control control in Panel_BG.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1");
                    }
                }
                foreach (Control control in Panel_BG.Controls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentColorHover");
                    }
                }
                //
                TSImageRenderer(BtnChangePassword, AstelMain.theme == 1 ? Properties.Resources.ct_confirm_light : Properties.Resources.ct_confirm_dark, 18, ContentAlignment.MiddleRight);
                //
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1");
                CheckPassword.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                CheckPassword.CheckMarkColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                CheckPassword.UncheckedBorderColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "CheckBoxUnCheckBorderColor");
                // ======================================================================================================
                TSGetLangs software_lang = new TSGetLangs(AstelMain.lang_path);
                // TEXTS
                Text = string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_title"), Application.ProductName);
                LabelHeader.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_header");
                LabelCurrentPassword.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_current");
                LabelNewPassword.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_new");
                LabelNewPasswordRepeat.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_label_password_new_repeat");
                CheckPassword.Text = software_lang.TSReadLangs("AstelChangePassword", "asp_visible");
                BtnChangePassword.Text = " " + software_lang.TSReadLangs("AstelChangePassword", "asp_btn");
            }catch (Exception){ }
        }
        // CHANGE PASSWORD LOAD
        // ======================================================================================================
        private void AstelChangePassword_Load(object sender, EventArgs e){
            TxtCurrentPassword.UseSystemPasswordChar = true;
            TxtNewPassword.UseSystemPasswordChar = true;
            TxtNewPasswordRepeat.UseSystemPasswordChar = true;
            AcceptButton = BtnChangePassword;
            //
            Change_password_system_preloader();
        }
        // CHANGE PASSWORD BTN
        // ======================================================================================================
        private async void BtnChangePassword_Click(object sender, EventArgs e){
            await Change_password_function();
        }
        // CHANGE PASSWORD FUNCTION
        // ======================================================================================================
        private async Task Change_password_function(){
            TSGetLangs software_lang = new TSGetLangs(AstelMain.lang_path);
            //
            string password_current = TxtCurrentPassword.Text.Trim();
            string password_new = TxtNewPassword.Text.Trim();
            string password_new_repeat = TxtNewPasswordRepeat.Text.Trim();
            //
            if (string.IsNullOrEmpty(password_current)){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelChangePassword", "asp_current_pass_info"));
                BeginInvoke(new Action(() => {
                    TxtCurrentPassword.Focus();
                }));
                return;
            }
            if (string.IsNullOrEmpty(password_new)){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_info"));
                BeginInvoke(new Action(() => {
                    TxtNewPassword.Focus();
                }));
                return;
            }
            if (string.IsNullOrEmpty(password_new_repeat)){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_repeat_info"));
                BeginInvoke(new Action(() => {
                    TxtNewPasswordRepeat.Focus();
                }));
                return;
            }
            if (password_new.Length < 6 || password_new.Length > 32){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelChangePassword", "asp_pass_req_info"));
                BeginInvoke(new Action(() => {
                    TxtNewPassword.Focus();
                }));
                return;
            }
            if (password_new != password_new_repeat){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_new_pass_compare_info"), "\n"));
                return;
            }
            //
            Text = $"{string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_title"), Application.ProductName)} - " +  software_lang.TSReadLangs("AstelChangePassword", "asp_check_cp_change");
            TxtCurrentPassword.Enabled = false;
            TxtNewPassword.Enabled = false;
            TxtNewPasswordRepeat.Enabled = false;
            BtnChangePassword.Enabled = false;
            //
            bool change_password_status = await Task.Run(() =>{
                try{
                    TSSettingsModule read = new TSSettingsModule(ts_session_file);
                    string protected_salt = read.TSReadSettings(ts_session_container, "PasswordSalt");
                    string protected_password = read.TSReadSettings(ts_session_container, "PasswordHash");
                    if (string.IsNullOrEmpty(protected_salt) || string.IsNullOrEmpty(protected_password)){
                        return false;
                    }
                    try{
                        string saved_salt = TS_SessionProtection.UnprotectSessionData(protected_salt);
                        string saved_password = TS_SessionProtection.UnprotectSessionData(protected_password);
                        string hashed_current = TSHashPassword(password_current, saved_salt, 210000);
                        if (!FixedTimeStringEquals(hashed_current, saved_password)){
                            return false;
                        }
                        string new_salt = GenerateSalt(32);
                        string new_hashed = TSHashPassword(password_new, new_salt, 210000);
                        string protected_new_salt = TS_SessionProtection.ProtectSessionData(new_salt);
                        string protected_new_hash = TS_SessionProtection.ProtectSessionData(new_hashed);
                        TSSettingsModule write = new TSSettingsModule(ts_session_file);
                        write.TSWriteSettings(ts_session_container, "PasswordSalt", protected_new_salt);
                        write.TSWriteSettings(ts_session_container, "PasswordHash", protected_new_hash);
                        return true;
                    }catch(Exception){
                        return false;
                    }
                }catch(Exception){
                    return false;
                }
            });
            //
            if (change_password_status){
                TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_pass_change_success"), "\n"));
                Hide();
            }else{
                TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_current_pass_fail_info"), "\n"));
            }
            //
            Text = string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_title"), Application.ProductName);
            TxtCurrentPassword.Enabled = true;
            TxtNewPassword.Enabled = true;
            TxtNewPasswordRepeat.Enabled = true;
            BtnChangePassword.Enabled = true;
        }
        // CHECK PASSWORD VISIBLE
        // ======================================================================================================
        private void CheckPassword_CheckedChanged(object sender, EventArgs e){
            if (CheckPassword.Checked == true){
                TxtCurrentPassword.UseSystemPasswordChar = false;
                TxtNewPassword.UseSystemPasswordChar = false;
                TxtNewPasswordRepeat.UseSystemPasswordChar = false;
            }else if (CheckPassword.Checked == false){
                TxtCurrentPassword.UseSystemPasswordChar = true;
                TxtNewPassword.UseSystemPasswordChar = true;
                TxtNewPasswordRepeat.UseSystemPasswordChar = true;
            }
        }
    }
}