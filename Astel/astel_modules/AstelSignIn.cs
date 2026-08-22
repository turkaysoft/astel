using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
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
                TSImageRenderer(BtnSignIn, theme_mode == 1 ? Properties.Resources.ct_confirm_light : Properties.Resources.ct_confirm_dark, 18, ContentAlignment.MiddleRight);
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
                signin_global_lang = lang_file_path;
                // TEXTS
                Text = string.Format(software_lang.TSReadLangs("AstelSignIn", "as_title"), Application.ProductName);
                LabelHeader.Text = software_lang.TSReadLangs("AstelSignIn", "as_header");
                LabelPassword.Text = software_lang.TSReadLangs("AstelSignIn", "as_label_password");
                LabelPasswordRepeat.Text = software_lang.TSReadLangs("AstelSignIn", "as_label_password_repeat");
                CheckPassword.Text = software_lang.TSReadLangs("AstelSignIn", "as_visible");
                BtnSignIn.Text = " " + software_lang.TSReadLangs("AstelSignIn", "as_btn");
                // PASS VISIBLE MODE
                string pass_vis_mode = software_read_settings.TSReadSettings(ts_settings_container, "LoginPassVisible");
                if (string.IsNullOrEmpty(pass_vis_mode)) { pass_vis_mode = "0"; }
                bool pass_vis_mode_bool = pass_vis_mode == "1";
                TxtPassword.UseSystemPasswordChar = !pass_vis_mode_bool;
                CheckPassword.Checked = pass_vis_mode_bool;
            }
            catch (Exception){ }
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
            if (password_1.Length < 12 || password_1.Length > 128){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelSignIn", "as_password_req_info"), 12, 128));
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
            DialogResult loss_warning = TS_MessageBoxEngine.TS_MessageBox(this, 6, string.Format(software_lang.TSReadLangs("AstelSignIn", "as_password_loss_warning"), "\n\n", "\n\n", "\n\n", "\n\n"));
            if (loss_warning != DialogResult.Yes){
                return;
            }
            //
            Text = $"{string.Format(software_lang.TSReadLangs("AstelSignIn", "as_title"), Application.ProductName)} - " + software_lang.TSReadLangs("AstelSignIn", "as_check_signin");
            TxtPassword.Enabled = false;
            TxtPasswordRepeat.Enabled = false;
            BtnSignIn.Enabled = false;
            //
            bool set_password_status = await Task.Run(() =>{
                byte[] saltBytes = null;
                byte[] masterKey = null;
                byte[] verifier = null;
                try{
                    string salt = GenerateSalt(32);
                    saltBytes = Convert.FromBase64String(salt);
                    (masterKey, verifier) = DeriveVaultKey(password_1, saltBytes, TSSecureModule.VaultIterations);
                    TS_AES_Encryption.SetKey(masterKey);
                    // Create v0x02 vault
                    var ts_xDoc = new XDocument(new XElement("Datas"));
                    var root = ts_xDoc.Element("Datas");
                    root.SetAttributeValue("V", TSSecureModule.VaultV0x02);
                    root.SetAttributeValue("AS", salt);
                    root.SetAttributeValue("IT", TSSecureModule.VaultIterations.ToString());
                    root.SetAttributeValue("KDF", TSSecureModule.VaultKDF);
                    root.SetAttributeValue("PV", Convert.ToBase64String(verifier));
                    root.SetAttributeValue("SV", TS_VersionEngine.TS_SoftwareVersion(1));
                    TSXmlAtomicSave(ts_xDoc, ts_data_xml_path);
                    return true;
                }catch(Exception){
                    return false;
                }finally{
                    if (saltBytes != null)
                        Array.Clear(saltBytes, 0, saltBytes.Length);
                    if (masterKey != null)
                        Array.Clear(masterKey, 0, masterKey.Length);
                    if (verifier != null)
                        Array.Clear(verifier, 0, verifier.Length);
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
            if (TxtPassword != null)
                TxtPassword.Text = "";
            if (TxtPasswordRepeat != null)
                TxtPasswordRepeat.Text = "";
            TS_AES_Encryption.ClearKey();
            Application.Exit();
        }
    }
}