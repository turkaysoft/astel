using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Linq;
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
            if (password_new.Length < 12 || password_new.Length > 128){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_pass_req_info"), 12, 128));
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
                byte[] oldSaltBytes = null;
                byte[] oldVerifier = null;
                byte[] newSaltBytes = null;
                byte[] newKey = null;
                byte[] newVerifier = null;
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
                        return false;
                    }
                    if (!int.TryParse(itStr, out int iterations) || iterations <= 0){
                        return false;
                    }
                    oldSaltBytes = Convert.FromBase64String(saltBase64);
                    oldVerifier = Convert.FromBase64String(pvBase64);
                    byte[] oldKey = null;
                    byte[] currentVerifier = null;
                    try{
                        (oldKey, currentVerifier) = DeriveVaultKey(password_current, oldSaltBytes, iterations);
                        if (!TS_AES_Encryption.FixedTimeEquals(currentVerifier, oldVerifier)){
                            return false;
                        }
                        // Derive new key/verifier
                        string newSalt = GenerateSalt(32);
                        newSaltBytes = Convert.FromBase64String(newSalt);
                        (newKey, newVerifier) = DeriveVaultKey(password_new, newSaltBytes, TSSecureModule.VaultIterations);
                        // Phase 1: decrypt every field with the OLD key
                        TS_AES_Encryption.SetKey(oldKey);
                        var plaintexts = new List<(XElement Data, string Service, string Email, string Url, string Password, string Note, string PassChangeDate)>();
                        foreach (var data in root.Elements("Data")){
                            plaintexts.Add((
                                data,
                                data.Element("Service") != null ? TS_AES_Encryption.TS_AES_Decrypt(data.Element("Service").Value) : string.Empty,
                                data.Element("Email") != null ? TS_AES_Encryption.TS_AES_Decrypt(data.Element("Email").Value) : string.Empty,
                                data.Element("Url") != null ? TS_AES_Encryption.TS_AES_Decrypt(data.Element("Url").Value) : string.Empty,
                                data.Element("Password") != null ? TS_AES_Encryption.TS_AES_Decrypt(data.Element("Password").Value) : string.Empty,
                                data.Element("Note") != null ? TS_AES_Encryption.TS_AES_Decrypt(data.Element("Note").Value) : string.Empty,
                                data.Element("PassChangeDate") != null ? TS_AES_Encryption.TS_AES_Decrypt(data.Element("PassChangeDate").Value) : string.Empty
                            ));
                        }
                        // Phase 2: re-encrypt every field with the NEW key.
                        // WithTempKey keeps the old key as master until the save succeeds.
                        TS_AES_Encryption.WithTempKey(newKey, () => {
                            foreach (var item in plaintexts){
                                item.Data.Element("Service")?.SetValue(TS_AES_Encryption.TS_AES_Encrypt(item.Service));
                                item.Data.Element("Email")?.SetValue(TS_AES_Encryption.TS_AES_Encrypt(item.Email));
                                item.Data.Element("Url")?.SetValue(TS_AES_Encryption.TS_AES_Encrypt(item.Url));
                                item.Data.Element("Password")?.SetValue(TS_AES_Encryption.TS_AES_Encrypt(item.Password));
                                item.Data.Element("Note")?.SetValue(TS_AES_Encryption.TS_AES_Encrypt(item.Note));
                                item.Data.Element("PassChangeDate")?.SetValue(TS_AES_Encryption.TS_AES_Encrypt(item.PassChangeDate));
                            }
                            return true;
                        });
                        // Update vault metadata + atomic save
                        root.SetAttributeValue("AS", Convert.ToBase64String(newSaltBytes));
                        root.SetAttributeValue("IT", TSSecureModule.VaultIterations.ToString());
                        root.SetAttributeValue("KDF", TSSecureModule.VaultKDF);
                        root.SetAttributeValue("PV", Convert.ToBase64String(newVerifier));
                        TSXmlAtomicSave(doc, ts_data_xml_path);
                        // Only now commit the new key as the running master key
                        TS_AES_Encryption.SetKey(newKey);
                        return true;
                    }finally{
                        if (oldKey != null)
                            Array.Clear(oldKey, 0, oldKey.Length);
                        if (currentVerifier != null)
                            Array.Clear(currentVerifier, 0, currentVerifier.Length);
                    }
                }catch(Exception){
                    return false;
                }finally{
                    if (oldSaltBytes != null)
                        Array.Clear(oldSaltBytes, 0, oldSaltBytes.Length);
                    if (oldVerifier != null)
                        Array.Clear(oldVerifier, 0, oldVerifier.Length);
                    if (newSaltBytes != null)
                        Array.Clear(newSaltBytes, 0, newSaltBytes.Length);
                    if (newKey != null)
                        Array.Clear(newKey, 0, newKey.Length);
                    if (newVerifier != null)
                        Array.Clear(newVerifier, 0, newVerifier.Length);
                }
            });
            //
            if (change_password_status){
                TS_MessageBoxEngine.TS_MessageBox(this, 1, string.Format(software_lang.TSReadLangs("AstelChangePassword", "asp_pass_change_success"), "\n\n", "\n\n"));
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
        // FORM CLOSING (memory safe)
        // ======================================================================================================
        private void AspChangePassword_FormClosing(object sender, FormClosingEventArgs e){
            if (TxtCurrentPassword != null)
                TxtCurrentPassword.Text = "";
            if (TxtNewPassword != null)
                TxtNewPassword.Text = "";
            if (TxtNewPasswordRepeat != null)
                TxtNewPasswordRepeat.Text = "";
        }
    }
}