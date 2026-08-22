using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography;
// TS MODULES
using static Astel.TSModules;

namespace Astel.astel_modules{
    public partial class AstelPasswordGenerator : Form{
        // ASTEL PASS GENERATOR CLASS
        // ======================================================================================================
        public class TS_AstelPasswordGenerator{
            private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
            private readonly byte[] _randomByteBuffer = new byte[1];
            // Global Standard Set
            private const string UpperSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            private const string LowerSet = "abcdefghijklmnopqrstuvwxyz";
            private const string DigitSet = "0123456789";
            private const string SpecialSet = "!@#$%^&*()-_=+";
            // Avoid Ambiguous Characters
            private const string AmbiguousChars = "Il1O0";
            public string AstelGeneratePassword(bool includeUppercase, bool includeLowercase, bool includeNumeric, bool includeSpecialChars, string mode, int passwordLength){
                if (passwordLength <= 0){
                    return string.Empty;
                }
                string modeLower = mode.ToLower();
                bool excludeAmbiguous = modeLower == "readable" || modeLower == "writable";
                return GenerateRandom(includeUppercase, includeLowercase, includeNumeric, includeSpecialChars, passwordLength, excludeAmbiguous);
            }
            private int NextIndex(int range){
                if (range <= 0){
                    throw new ArgumentOutOfRangeException(nameof(range));
                }
                int limit = 256 - (256 % range);
                while (true){
                    _rng.GetBytes(_randomByteBuffer);
                    int b = _randomByteBuffer[0];
                    if (b < limit){
                        return b % range;
                    }
                }
            }
            private string GenerateRandom( bool includeUpper, bool includeLower, bool includeNum, bool includeSpecial, int length, bool excludeAmbiguous){
                var activeCategories = new List<string>();
                string Filter(string source) => excludeAmbiguous ? new string(source.Where(c => !AmbiguousChars.Contains(c)).ToArray()) : source;
                if (includeUpper) activeCategories.Add(Filter(UpperSet));
                if (includeLower) activeCategories.Add(Filter(LowerSet));
                if (includeNum) activeCategories.Add(Filter(DigitSet));
                if (includeSpecial) activeCategories.Add(Filter(SpecialSet));
                if (activeCategories.Count == 0){
                    TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
                    throw new ArgumentException(lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_info"));
                }
                string fullCharSet = string.Concat(activeCategories);
                char[] passwordChars = new char[length];
                // Ensure at least one character from each selected category
                for (int i = 0; i < activeCategories.Count; i++){
                    passwordChars[i] = activeCategories[i][NextIndex(activeCategories[i].Length)];
                }
                for (int i = activeCategories.Count; i < length; i++){
                    passwordChars[i] = fullCharSet[NextIndex(fullCharSet.Length)];
                }
                // Fisher-Yates shuffle to avoid a predictable prefix pattern
                for (int i = length - 1; i > 0; i--){
                    int j = NextIndex(i + 1);
                    (passwordChars[i], passwordChars[j]) = (passwordChars[j], passwordChars[i]);
                }
                return new string(passwordChars);
            }
        }
        // PASSWORD STRENGTH ESTIMATOR
        // ======================================================================================================
        internal static class TS_AstelPasswordStrength{
            // Common weak passwords (compared case-insensitively)
            private static readonly HashSet<string> WeakPasswords = new HashSet<string>(StringComparer.OrdinalIgnoreCase){
                "123456", "password", "123456789", "12345678", "12345", "qwerty", "1234567890", "1234567",
                "111111", "123123", "abc123", "1234", "password1", "iloveyou", "000000", "qwerty123",
                "1q2w3e4r", "1qaz2wsx", "zaq12wsx", "qazwsx", "dragon", "sunshine", "princess", "letmein",
                "654321", "monkey", "superman", "asdfghjkl", "admin", "welcome", "login", "master",
                "hello", "freedom", "whatever", "trustno1", "batman", "football", "baseball", "starwars",
                "michael", "shadow", "ashley", "bailey", "summer", "winter", "computer", "internet",
                "samsung", "google", "facebook", "github", "parola", "sifre", "istanbul", "ankara",
                "deneme", "merhaba", "askim", "sevgilim", "hayatim", "turkiye", "izmir", "antalya",
                "hunter2", "hunter", "secret", "swordfish", "mustang", "charlie", "buster", "kangaroo"
            };
            // Number of distinct character classes used in the password (lower/upper/digit/special)
            private static int ClassCount(string password){
                bool hasLower = false, hasUpper = false, hasDigit = false, hasSpecial = false;
                foreach (char c in password){
                    if (char.IsLower(c)) hasLower = true;
                    else if (char.IsUpper(c)) hasUpper = true;
                    else if (char.IsDigit(c)) hasDigit = true;
                    else hasSpecial = true;
                }
                return (hasLower ? 1 : 0) + (hasUpper ? 1 : 0) + (hasDigit ? 1 : 0) + (hasSpecial ? 1 : 0);
            }
            public static int Score(string password){
                if (string.IsNullOrEmpty(password)) return 0;
                int len = password.Length;
                int classes = ClassCount(password);
                // Common weak passwords are always weak
                if (WeakPasswords.Contains(password)) return 0;
                // Score is determined by the number of character classes and the length.
                // More character classes lower the length required for "very strong".
                switch (classes){
                    case 1:
                        if (len < 6) return 0;
                        if (len < 9) return 1;
                        if (len < 12) return 2;
                        if (len < 20) return 3;
                        return 4;
                    case 2:
                        if (len < 6) return 0;
                        if (len < 8) return 1;
                        if (len < 10) return 2;
                        if (len < 16) return 3;
                        return 4;
                    case 3:
                        if (len < 6) return 0;
                        if (len < 8) return 1;
                        if (len < 10) return 2;
                        if (len < 14) return 3;
                        return 4;
                    default:
                        if (len < 6) return 0;
                        if (len < 8) return 1;
                        if (len < 10) return 2;
                        if (len < 12) return 3;
                        return 4;
                }
            }
        }
        // AUXILIARY METHODS
        // ======================================================================================================
        private void SetControlColors<T>(Control container, Action<T> setColors) where T : Control{
            foreach (Control control in container.Controls){
                if (control is T typedControl){
                    setColors(typedControl);
                }
            }
        }
        // FORM EVENTS & INIT
        // ======================================================================================================
        private readonly TS_AstelPasswordGenerator generator;
        private readonly Dictionary<string, (bool upperChecked, bool lowerChecked, bool numericChecked, bool specialChecked)> modes;
        public AstelPasswordGenerator(){
            InitializeComponent();
            this.FormClosing += AstelPasswordGenerator_FormClosing;
            generator = new TS_AstelPasswordGenerator();
            modes = new Dictionary<string, (bool, bool, bool, bool)>(){
                { "readable", (true, true, true, false) },  // Uppercase, Lowercase, Numbers (No symbols, no similar-looking characters)
                { "writable", (false, true, true, false) }, // Lowercase, Digit (No symbols, no uppercase letters)
                { "random", (true, true, true, true) }      // Completely Mixed Up
            };
            RadioRead.CheckedChanged += RadioButton_CheckedChanged;
            RadioWrite.CheckedChanged += RadioButton_CheckedChanged;
            RadioMixed.CheckedChanged += RadioButton_CheckedChanged;
            PassGenProFE.Width = 0;
            PassGenProLabel.Text = string.Empty;
        }
        public void Password_generator_preloader(){
            try{
                TSThemeModeHelper.InitializeThemeForForm(this);
                BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                Panel_BG.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor");
                SetControlColors<Label>(Panel_BG, lbl => lbl.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1"));
                SetControlColors<TextBox>(Panel_BG, tb => {
                    tb.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                    tb.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1");
                });
                SetControlColors<Button>(Panel_BG, btn => {
                    btn.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "DynamicThemeActiveBtnBGColor");
                    var color = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                    btn.BackColor = color;
                    btn.FlatAppearance.BorderColor = color;
                    btn.FlatAppearance.MouseDownBackColor = color;
                    btn.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentColorHover");
                });
                LabelHeader.BackColor = Panel_Feature.BackColor = Panel_Mode.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                LabelHeader.ForeColor = LabelFeature.ForeColor = LabelMode.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1");
                LabelFeature.BackColor = LabelMode.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor");
                SetControlColors<CheckBox>(Panel_Feature, cb => cb.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1"));
                SetControlColors<RadioButton>(Panel_Mode, rb => rb.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1"));
                CheckUppercase.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                CheckUppercase.CheckMarkColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                CheckUppercase.UncheckedBorderColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "CheckBoxUnCheckBorderColor");
                CheckLowercase.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                CheckLowercase.CheckMarkColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                CheckLowercase.UncheckedBorderColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "CheckBoxUnCheckBorderColor");
                CheckNumeric.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                CheckNumeric.CheckMarkColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                CheckNumeric.UncheckedBorderColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "CheckBoxUnCheckBorderColor");
                CheckSpecialChars.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                CheckSpecialChars.CheckMarkColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                CheckSpecialChars.UncheckedBorderColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "CheckBoxUnCheckBorderColor");
                RadioRead.UnCheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "CheckBoxUnCheckBorderColor");
                RadioRead.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                RadioWrite.UnCheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "CheckBoxUnCheckBorderColor");
                RadioWrite.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                RadioMixed.UnCheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "CheckBoxUnCheckBorderColor");
                RadioMixed.CheckedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                PassLenghtLabel.BackColor = PassGenLenght.BackColor = PassResultLabel.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                PassLenghtLabel.ForeColor = PassGenLenght.ForeColor = PassResultLabel.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1");
                PassGenLenght.TrackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TrackColor");
                PassGenLenght.ThumbColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                PassGenLenght.ThumbHoverColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentColorHover");
                PassGenLenght.ThumbPressedColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentColorHover");
                PassGenLenght.TrackFillColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                PassGenProBG.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                PassGenProFE.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                TSImageRenderer(BtnGenPass, AstelMain.theme == 1 ? Properties.Resources.ct_generate_light : Properties.Resources.ct_generate_dark, 18, ContentAlignment.MiddleRight);
                TSImageRenderer(BtnCopyPassword, AstelMain.theme == 1 ? Properties.Resources.ct_copy_light : Properties.Resources.ct_copy_dark, 22, ContentAlignment.MiddleCenter);
                var lang = new TSGetLangs(AstelMain.lang_path);
                Text = string.Format(lang.TSReadLangs("AstelPasswordGenerator", "apg_title"), Application.ProductName);
                LabelHeader.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_header");
                LabelFeature.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_title");
                LabelMode.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_title");
                CheckUppercase.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_uppercase");
                CheckLowercase.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_lowercase");
                CheckNumeric.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_numeric");
                CheckSpecialChars.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_feature_special_chars");
                RadioRead.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_easy_read");
                RadioWrite.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_easy_write");
                RadioMixed.Text = lang.TSReadLangs("AstelPasswordGenerator", "apg_mode_mixed");
                BtnGenPass.Text = " " + lang.TSReadLangs("AstelPasswordGenerator", "apg_gen_pass_btn");
                if (!string.IsNullOrEmpty(PassResultLabel.Text)){
                    UpdatePasswordStrength(PassResultLabel.Text);
                }
            }catch (Exception){ }
        }
        // TOOLTIP SETTINGS
        // ======================================================================================================
        private void MainToolTip_Draw(object sender, DrawToolTipEventArgs e) { e.DrawBackground(); e.DrawBorder(); e.DrawText(); }
        // LOAD
        // ======================================================================================================
        private void AstelPasswordGenerator_Load(object sender, EventArgs e){
            AcceptButton = BtnGenPass;
            TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
            PassLenghtLabel.Text = string.Format(lang.TSReadLangs("AstelPasswordGenerator", "apg_password_length"), PassGenLenght.Value);
            Password_generator_preloader();
        }
        // UI FUNCTIONS
        // ======================================================================================================
        private void RadioButton_CheckedChanged(object sender, EventArgs e){
            string mode = RadioRead.Checked ? "readable" : RadioWrite.Checked ? "writable" : "random";
            UpdateCheckboxesByMode(mode);
        }
        private void UpdateCheckboxesByMode(string mode){
            var (upperChecked, lowerChecked, numericChecked, specialChecked) = modes[mode];
            CheckUppercase.Checked = upperChecked;
            CheckLowercase.Checked = lowerChecked;
            CheckNumeric.Checked = numericChecked;
            CheckSpecialChars.Checked = specialChecked;
            bool profileMode = mode != "random";
            CheckUppercase.Enabled = !profileMode;
            CheckLowercase.Enabled = !profileMode;
            CheckNumeric.Enabled = !profileMode;
            CheckSpecialChars.Enabled = !profileMode;
        }
        private void PassGenLenght_ValueChanged(object sender, EventArgs e){
            TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
            PassLenghtLabel.Text = string.Format(lang.TSReadLangs("AstelPasswordGenerator", "apg_password_length"), PassGenLenght.Value);
        }
        private void Astel_pass_gen_engine(){
            string mode = RadioRead.Checked ? "readable" : RadioWrite.Checked ? "writable" : "random";
            try{
                string password = generator.AstelGeneratePassword(CheckUppercase.Checked, CheckLowercase.Checked, CheckNumeric.Checked, CheckSpecialChars.Checked, mode, PassGenLenght.Value);
                PassResultLabel.Text = password;
                UpdatePasswordStrength(password);
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(this, 2, ex.Message);
            }
        }
        // PASSWORD STRENGTH (score 0-4)
        // ======================================================================================================
        private void UpdatePasswordStrength(string password){
            TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
            if (string.IsNullOrEmpty(password)){
                PassGenProFE.Width = 0;
                PassGenProLabel.Text = string.Empty;
                return;
            }
            int strengthScore = TS_AstelPasswordStrength.Score(password);
            int widthPercent;
            Color barColor;
            string strengthKey;
            if (strengthScore <= 1){
                widthPercent = 25; barColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentRed"); strengthKey = "apg_strength_weak";
            }else if (strengthScore == 2){
                widthPercent = 50; barColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentOrange"); strengthKey = "apg_strength_average";
            }else if (strengthScore == 3){
                widthPercent = 75; barColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentAqua"); strengthKey = "apg_strength_good";
            }else{
                widthPercent = 100; barColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor"); strengthKey = "apg_strength_very_strong";
            }
            PassGenProFE.Width = Math.Max(1, PassGenProBG.Width * widthPercent / 100);
            PassGenProFE.BackColor = barColor;
            PassGenProLabel.Text = lang.TSReadLangs("AstelPasswordGenerator", strengthKey);
            PassGenProLabel.ForeColor = barColor;
        }
        // LAUNCHER
        // ======================================================================================================
        private void BtnGenPass_Click(object sender, EventArgs e) => Astel_pass_gen_engine();
        // COPY PASSWORD
        // ======================================================================================================
        private void BtnCopyPassword_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PassResultLabel.Text))
            {
                string copiedPassword = PassResultLabel.Text.Trim();
                Clipboard.SetText(copiedPassword);
                TSSecureModule.TSClipboardSecurity.TrackCopiedText(copiedPassword);
                TSGetLangs lang = new TSGetLangs(AstelMain.lang_path);
                TS_MessageBoxEngine.TS_MessageBox(this, 1, lang.TSReadLangs("AstelPasswordGenerator", "apg_copy_password"));
            }
        }
        // FORM CLOSING: clear the generated plaintext label + our clipboard content
        // ======================================================================================================
        private void AstelPasswordGenerator_FormClosing(object sender, FormClosingEventArgs e){
            if (PassResultLabel != null){
                PassResultLabel.Text = "";
            }
            TSSecureModule.TSClipboardSecurity.ClearOwnClipboardIfPresent();
        }
    }
}