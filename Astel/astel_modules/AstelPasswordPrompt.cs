using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
// TS MODULES
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel.astel_modules{
    public partial class AstelPasswordPrompt : Form{
        // SOURCE VAULT UNLOCK DIALOG (used by .astel import / drag&drop)
        // ======================================================================================================
        private readonly string sourceFilePath;
        public byte[] SourceKey { get; private set; }
        public AstelPasswordPrompt(string filePath){
            InitializeComponent();
            sourceFilePath = filePath;
            _lockoutTimer = new Timer{
                Interval = 1000
            };
            _lockoutTimer.Tick += LockoutTimer_Tick;
        }
        // IMPORT THROTTLE: 3 failed unlock attempts -> 30 s in-memory lockout
        // ======================================================================================================
        private readonly TSLoginThrottle _loginThrottle = new TSLoginThrottle();
        private readonly Timer _lockoutTimer;
        // PRELOADER
        // ======================================================================================================
        string prompt_global_lang;
        public void Prompt_system_preloader(){
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
                LabelHeader.BackColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_BGColor2");
                LabelHeader.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_LabelColor1");
                CheckPassword.ForeColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_LabelColor1");
                CheckPassword.CheckedColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_AccentColor");
                CheckPassword.CheckMarkColor = TS_ThemeEngine.ColorMode(theme_mode, "TSBT_BGColor2");
                CheckPassword.UncheckedBorderColor = TS_ThemeEngine.ColorMode(theme_mode, "CheckBoxUnCheckBorderColor");
                //
                TSImageRenderer(BtnUnlock, theme_mode == 1 ? Properties.Resources.ct_unlock_light : Properties.Resources.ct_unlock_dark, 18, ContentAlignment.MiddleRight);
                //
                // ======================================================================================================
                string lang_code = software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus");
                string selectedLangCode = TSPreloaderSetDefaultLanguage(lang_code);
                string lang_file_path = AllLanguageFiles[selectedLangCode];
                TSGetLangs software_lang = new TSGetLangs(lang_file_path);
                prompt_global_lang = lang_file_path;
                // TEXTS
                Text = string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_title"), Application.ProductName);
                LabelHeader.Text = software_lang.TSReadLangs("AstelPasswordPrompt", "ap_header");
                LabelPassword.Text = software_lang.TSReadLangs("AstelPasswordPrompt", "ap_label_password");
                CheckPassword.Text = software_lang.TSReadLangs("AstelPasswordPrompt", "ap_visible");
                BtnUnlock.Text = " " + software_lang.TSReadLangs("AstelPasswordPrompt", "ap_btn");
                // PASS VISIBLE MODE
                string pass_vis_mode = software_read_settings.TSReadSettings(ts_settings_container, "LoginPassVisible");
                if (string.IsNullOrEmpty(pass_vis_mode)){ pass_vis_mode = "0"; }
                bool pass_vis_mode_bool = pass_vis_mode == "1";
                TxtPassword.UseSystemPasswordChar = !pass_vis_mode_bool;
                CheckPassword.Checked = pass_vis_mode_bool;
            }catch (Exception){ }
        }
        // LOAD
        // ======================================================================================================
        private void AstelPasswordPrompt_Load(object sender, EventArgs e){
            TxtPassword.UseSystemPasswordChar = true;
            AcceptButton = BtnUnlock;
            //
            Prompt_system_preloader();
        }
        // UNLOCK BTN
        // ======================================================================================================
        private async void BtnUnlock_Click(object sender, EventArgs e){
            await Unlock_source_vault();
        }
        // UNLOCK FUNCTION
        // ======================================================================================================
        private async Task Unlock_source_vault(){
            TSGetLangs software_lang = new TSGetLangs(prompt_global_lang);
            string get_password = TxtPassword.Text.Trim();
            if (_loginThrottle.IsLockedOut){
                int remaining = _loginThrottle.RemainingSeconds;
                if (remaining > 0){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_throttle_active"), remaining));
                    return;
                }
                _loginThrottle.Reset();
            }
            if (string.IsNullOrEmpty(get_password)){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelPasswordPrompt", "ap_password_info"));
                return;
            }
            //
            Text = $"{string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_title"), Application.ProductName)} - " + software_lang.TSReadLangs("AstelPasswordPrompt", "ap_check");
            TxtPassword.Enabled = false;
            BtnUnlock.Enabled = false;
            //
            bool unlock_status = await Task.Run(() =>{
                byte[] saltBytes = null;
                byte[] storedVerifier = null;
                byte[] derivedKey = null;
                byte[] verifier = null;
                try{
                    var doc = XDocument.Load(sourceFilePath);
                    var root = doc.Element("Datas");
                    string vaultV = root.Attribute("V")?.Value?.Trim();
                    string saltBase64 = root.Attribute("AS")?.Value?.Trim();
                    string itStr = root.Attribute("IT")?.Value?.Trim();
                    string kdf = root.Attribute("KDF")?.Value?.Trim();
                    string pvBase64 = root.Attribute("PV")?.Value?.Trim();
                    bool isLegacy = root.Attribute("EK")?.Value != null || root.Attribute("ST")?.Value != null;
                    if (isLegacy || vaultV != TSSecureModule.VaultV0x02 || kdf != TSSecureModule.VaultKDF || string.IsNullOrEmpty(saltBase64) || string.IsNullOrEmpty(itStr) || string.IsNullOrEmpty(pvBase64)){
                        return false;
                    }
                    if (!int.TryParse(itStr, out int iterations) || iterations <= 0){
                        return false;
                    }
                    saltBytes = Convert.FromBase64String(saltBase64);
                    storedVerifier = Convert.FromBase64String(pvBase64);
                    (derivedKey, verifier) = DeriveVaultKey(get_password, saltBytes, iterations);
                    if (!TS_AES_Encryption.FixedTimeEquals(verifier, storedVerifier)){
                        return false;
                    }
                    SourceKey = derivedKey;
                    return true;
                }catch (Exception){
                    return false;
                }finally{
                    if (saltBytes != null)
                        Array.Clear(saltBytes, 0, saltBytes.Length);
                    if (storedVerifier != null)
                        Array.Clear(storedVerifier, 0, storedVerifier.Length);
                    if (verifier != null)
                        Array.Clear(verifier, 0, verifier.Length);
                }
            });
            //
            if (unlock_status){
                _loginThrottle.Reset();
                DialogResult = DialogResult.OK;
                Close();
            }else{
                TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_password_failed"), "\n\n", "\n\n"));
                _loginThrottle.RecordFailure();
                if (_loginThrottle.ShouldStartLockout){
                    StartPromptLockout(software_lang);
                }else{
                    TxtPassword.Text = "";
                    TxtPassword.Enabled = true;
                    BtnUnlock.Enabled = true;
                    Text = string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_title"), Application.ProductName);
                    TxtPassword.Focus();
                }
            }
        }
        // IMPORT THROTTLE: lock input for 30 s after 3 failed attempts
        // ======================================================================================================
        private void StartPromptLockout(TSGetLangs software_lang){
            _loginThrottle.StartLockout();
            BtnUnlock.Enabled = false;
            TxtPassword.Enabled = false;
            BtnUnlock.Text = " " + string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_throttle_countdown"), TSLoginThrottle.LockoutSeconds);
            Text = string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_title"), Application.ProductName) + " - " + string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_throttle_active"), TSLoginThrottle.LockoutSeconds);
            _lockoutTimer.Start();
        }
        private void LockoutTimer_Tick(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(prompt_global_lang);
            if (_loginThrottle.Tick()){
                _lockoutTimer.Stop();
                BtnUnlock.Enabled = true;
                TxtPassword.Enabled = true;
                Text = string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_title"), Application.ProductName);
                BtnUnlock.Text = " " + software_lang.TSReadLangs("AstelPasswordPrompt", "ap_btn");
            }else{
                BtnUnlock.Text = " " + string.Format(software_lang.TSReadLangs("AstelPasswordPrompt", "ap_throttle_countdown"), _loginThrottle.LockoutRemaining);
            }
        }
        // CHECK PASSWORD VISIBLE
        // ======================================================================================================
        private void CheckPassword_CheckedChanged(object sender, EventArgs e){
            TxtPassword.UseSystemPasswordChar = !CheckPassword.Checked;
        }
        // EXIT
        // ======================================================================================================
        private void AstelPasswordPrompt_FormClosing(object sender, FormClosingEventArgs e){
            TxtPassword.Text = "";
            if (DialogResult != DialogResult.OK && SourceKey != null){
                Array.Clear(SourceKey, 0, SourceKey.Length);
                SourceKey = null;
            }
        }
    }
}