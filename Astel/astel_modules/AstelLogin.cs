using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
// TS MODULES
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel.astel_modules{
    public partial class AstelLogin : Form{
        public AstelLogin(){
            InitializeComponent();
            _lockoutTimer = new Timer{
                Interval = 1000
            };
            _lockoutTimer.Tick += LockoutTimer_Tick;
        }
        // LOGIN PRELOADER
        // ======================================================================================================
        string login_global_lang;
        private readonly TSLoginThrottle _loginThrottle = new TSLoginThrottle();
        private readonly Timer _lockoutTimer;
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
        protected override void OnDpiChanged(DpiChangedEventArgs e){
            base.OnDpiChanged(e);
            try{
                Login_system_preloader();
                this.PerformLayout();
                this.Invalidate(true);
            }catch{ }
        }
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
            if (_loginThrottle.IsLockedOut){
                int remaining = _loginThrottle.RemainingSeconds;
                if (remaining > 0){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelLogin", "al_throttle_active"), remaining));
                    return;
                }
                _loginThrottle.Reset();
            }
            if (string.IsNullOrEmpty(get_password)){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelLogin", "al_password_info"));
                return;
            }
            //
            Text = $"{string.Format(software_lang.TSReadLangs("AstelLogin", "al_title"), Application.ProductName)} - " + software_lang.TSReadLangs("AstelLogin", "al_check_login");
            TxtPassword.Enabled = false;
            BtnLogin.Enabled = false;
            //
            int login_result = 1;
            bool login_status = await Task.Run(() =>{
                // 0 = ok, 1 = wrong password, 2 = incompatible/legacy vault
                byte[] saltBytes = null;
                byte[] storedVerifier = null;
                byte[] derivedKey = null;
                byte[] verifier = null;
                try{
                    var doc = XDocument.Load(ts_data_xml_path);
                    var root = doc.Element("Datas");
                    string vaultV = root.Attribute("V")?.Value?.Trim();
                    string saltBase64 = root.Attribute("AS")?.Value?.Trim();
                    string itStr = root.Attribute("IT")?.Value?.Trim();
                    string kdf = root.Attribute("KDF")?.Value?.Trim();
                    string pvBase64 = root.Attribute("PV")?.Value?.Trim();
                    bool isLegacy = root.Attribute("EK")?.Value != null || root.Attribute("ST")?.Value != null;
                    if (isLegacy || vaultV != TSSecureModule.VaultV0x02 || kdf != TSSecureModule.VaultKDF || string.IsNullOrEmpty(saltBase64) || string.IsNullOrEmpty(itStr) || string.IsNullOrEmpty(pvBase64)){
                        login_result = 2;
                        return false;
                    }
                    if (!int.TryParse(itStr, out int iterations) || iterations <= 0){
                        login_result = 2;
                        return false;
                    }
                    saltBytes = Convert.FromBase64String(saltBase64);
                    storedVerifier = Convert.FromBase64String(pvBase64);
                    (derivedKey, verifier) = DeriveVaultKey(get_password, saltBytes, iterations);
                    if (!TS_AES_Encryption.FixedTimeEquals(verifier, storedVerifier)){
                        login_result = 1;
                        return false;
                    }
                    TS_AES_Encryption.SetKey(derivedKey);
                    login_result = 0;
                    return true;
                }catch(Exception){
                    if (login_result != 2)
                        login_result = 1;
                    return false;
                }finally{
                    if (saltBytes != null)
                        Array.Clear(saltBytes, 0, saltBytes.Length);
                    if (storedVerifier != null)
                        Array.Clear(storedVerifier, 0, storedVerifier.Length);
                    if (derivedKey != null)
                        Array.Clear(derivedKey, 0, derivedKey.Length);
                    if (verifier != null)
                        Array.Clear(verifier, 0, verifier.Length);
                }
            });
            //
            if (login_status){
                _loginThrottle.Reset();
                TxtPassword.Text = "";
                new AstelMain().Show();
                Hide();
            }else if (login_result == 2){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("AstelLogin", "al_vault_incompatible"), "\n\n", "\n\n", Application.ProductName));
                BeginInvoke(new Action(() =>{
                    TxtPassword.Focus();
                }));
            }else{
                TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelLogin", "al_password_failed"), "\n\n"));
                _loginThrottle.RecordFailure();
                TxtPassword.Text = "";
                if (_loginThrottle.ShouldStartLockout){
                    StartLoginLockout(software_lang);
                }else{
                    BeginInvoke(new Action(() =>{
                        TxtPassword.Focus();
                    }));
                }
            }
            //
            Text = string.Format(software_lang.TSReadLangs("AstelLogin", "al_title"), Application.ProductName);
            TxtPassword.Enabled = true;
            BtnLogin.Enabled = true;
        }
        // LOGIN THROTTLE (3 failed attempts -> 30 s in-memory lockout)
        // ======================================================================================================
        private void StartLoginLockout(TSGetLangs software_lang){
            _loginThrottle.StartLockout();
            BtnLogin.Enabled = false;
            TxtPassword.Enabled = false;
            BtnLogin.Text = " " + string.Format(software_lang.TSReadLangs("AstelLogin", "al_throttle_countdown"), TSLoginThrottle.LockoutSeconds);
            Text = string.Format(software_lang.TSReadLangs("AstelLogin", "al_title"), Application.ProductName) + " - " + string.Format(software_lang.TSReadLangs("AstelLogin", "al_throttle_info"), "\n\n", TSLoginThrottle.LockoutSeconds);
            _lockoutTimer.Start();
        }
        private void LockoutTimer_Tick(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(login_global_lang);
            if (_loginThrottle.Tick()){
                _lockoutTimer.Stop();
                BtnLogin.Enabled = true;
                TxtPassword.Enabled = true;
                Text = string.Format(software_lang.TSReadLangs("AstelLogin", "al_title"), Application.ProductName);
                BtnLogin.Text = " " + software_lang.TSReadLangs("AstelLogin", "al_btn");
            }else{
                BtnLogin.Text = " " + string.Format(software_lang.TSReadLangs("AstelLogin", "al_throttle_countdown"), _loginThrottle.LockoutRemaining);
            }
        }
        // RE-LOGIN AFTER AUTO-LOCK: clear the password box and re-apply UI state
        // ======================================================================================================
        public void ResetForRelogin(){
            if (TxtPassword != null)
                TxtPassword.Text = "";
            _loginThrottle.Reset();
            _lockoutTimer.Stop();
            BtnLogin.Enabled = true;
            TxtPassword.Enabled = true;
            Login_system_preloader();
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
            if (TxtPassword != null)
                TxtPassword.Text = "";
            TS_AES_Encryption.ClearKey();
            Application.Exit();
        }
    }
}