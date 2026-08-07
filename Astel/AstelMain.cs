// ======================================================================================================
// Astel - Password Management Software
// © Copyright 2024-2026, Eray Türkay.
// Publisher: Türkaysoft
// Project Type: Open Source
// License: MIT License
// Website: https://turkaysoft.com
// GitHub: https://github.com/turkaysoft/astel
// ======================================================================================================

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
// TS MODULES
using Astel.astel_modules;
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel{
    public partial class AstelMain : Form{
        public AstelMain(){
            InitializeComponent();
            // LANGUAGE SET MODES
            // ==================
            arabicToolStripMenuItem.Tag = "ar";
            chineseToolStripMenuItem.Tag = "zh";
            englishToolStripMenuItem.Tag = "en";
            dutchToolStripMenuItem.Tag = "nl";
            frenchToolStripMenuItem.Tag = "fr";
            germanToolStripMenuItem.Tag = "de";
            hindiToolStripMenuItem.Tag = "hi";
            italianToolStripMenuItem.Tag = "it";
            japaneseToolStripMenuItem.Tag = "ja";
            koreanToolStripMenuItem.Tag = "ko";
            polishToolStripMenuItem.Tag = "pl";
            portugueseToolStripMenuItem.Tag = "pt";
            russianToolStripMenuItem.Tag = "ru";
            spanishToolStripMenuItem.Tag = "es";
            turkishToolStripMenuItem.Tag = "tr";
            // LANGUAGE SET EVENTS
            // ==================
            arabicToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            chineseToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            englishToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            dutchToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            frenchToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            germanToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            hindiToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            italianToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            japaneseToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            koreanToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            polishToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            portugueseToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            russianToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            spanishToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            turkishToolStripMenuItem.Click += LanguageToolStripMenuItem_Click;
            //
            SystemEvents.UserPreferenceChanged += (s, e) => TSUseSystemTheme();
            //
            CmbService.MouseWheel += CmbService_MouseWheel;
        }
        // GLOBAL VARIABLES
        // ======================================================================================================
        public static string lang, lang_path;
        public static int theme, themeSystem, startup_status, auto_backup_status, safety_warnings_status, password_mask_status;
        // TS PROTECTION ERROR MESSAGES
        // ======================================================================================================
        public static class TSProtectionErrorMessages{
            public static Dictionary<string, string> Messages = new Dictionary<string, string>(){
                { "AES_MasterKeyNotSet", "AES Master Key is not set." },
                { "AES_KeyNull", "Master key cannot be null." },
                { "AES_KeyLengthInvalid", "Master key must be 32 bytes (256-bit)." },
                { "AES_PlainTextNull", "Plain text cannot be null." },
                { "AES_Base64InputNull", "Base64 input cannot be null." },
                { "AES_KeyDerivationFailed", "Key derivation failed during encryption: {0}" },
                { "AES_KeyDerivationFailedDecrypt", "Key derivation failed during decryption: {0}" },
                { "AES_InvalidBase64", "Invalid base64 format in ciphertext: {0}" },
                { "AES_InvalidCipherFormat", "Invalid ciphertext format." },
                { "AES_UnsupportedVersion", "Unsupported ciphertext version: {0}" },
                { "AES_InvalidCipherLength", "Invalid ciphertext length." },
                { "AES_HMACValidationFailed", "HMAC validation failed. Data may be tampered or corrupted." },
                { "AES_InvalidUTF8", "Invalid UTF-8 decoded data: {0}" },
                { "AES_EncryptionFailed", "Encryption operation failed: {0}" },
                { "AES_DecryptionFailed", "Decryption operation failed: {0}" },
                { "HKDF_InputKeyNull", "Input key material cannot be null." },
                { "HKDF_OutputLengthInvalid", "Output length must be greater than zero." },
                { "HKDF_OutputLengthTooLarge", "Output length too large." },
                { "DeriveSubKey_MasterKeyNull", "Master key cannot be null." },
                { "DeriveSubKey_SaltNull", "Salt cannot be null." },
                { "DeriveSubKey_InfoEmpty", "Info must not be null or empty." },
                { "ExtractKey_LoadFailed", "Failed to load XML document." },
                { "ExtractKey_MissingAttributes", "Missing required attributes." },
                { "ExtractKey_InvalidBase64", "Invalid Base64 format in attributes." },
                { "TempKey_Null", "Temporary key cannot be null." },
                { "PBKDF2_PasswordNull", "Password cannot be null." },
                { "PBKDF2_SaltNull", "Salt cannot be null." },
                { "PBKDF2_IterationsInvalid", "Iterations must be greater than zero." },
                { "PBKDF2_OutputBytesInvalid", "Output bytes must be greater than zero." },
                { "Session_PlainDataNull", "Plain data cannot be null or empty." },
                { "Session_ProtectedDataNull", "Protected data cannot be null or empty." },
                { "Hash_PasswordNull", "Password cannot be null." },
                { "Hash_SaltNull", "Salt cannot be null." },
                { "Hash_SaltInvalid", "Salt must be Base64 encoded." },
                { "Salt_SizeInvalid", "Salt size must be greater than zero." },
                { "Random_LengthInvalid", "Random string length must be greater than zero." },
                { "UnknownError", "An unknown error occurred" }
            };
        }
        // LOCAL VARIABLES
        // ======================================================================================================
        Task auto_backup;
        private CancellationTokenSource cts;
        private bool suppressComboBoxEvent = false;
        private bool _isFirstKeyNavigation = true;
        // ORIGINAL VALUES FOR UPDATE CHANGE DETECTION
        private string _originalService = "";
        private string _originalEmail = "";
        private string _originalPassword = "";
        private string _originalUrl = "";
        private string _originalNote = "";
        // UI COLORS
        // ======================================================================================================
        static readonly List<Color> header_colors = new List<Color>() { Color.Transparent, Color.Transparent, Color.Transparent };
        // HEADER SETTINGS
        // ======================================================================================================
        private class HeaderMenuColors : ToolStripProfessionalRenderer{
            public HeaderMenuColors() : base(new HeaderColors()){ }
            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e){ e.ArrowColor = header_colors[1]; base.OnRenderArrow(e); }
            protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e){
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                float dpiScale = g.DpiX / 96f;
                Rectangle rect = e.ImageRectangle;
                using (Pen anti_alias_pen = new Pen(header_colors[2], 2.2f * dpiScale)){
                    anti_alias_pen.StartCap = LineCap.Round;
                    anti_alias_pen.EndCap = LineCap.Round;
                    anti_alias_pen.LineJoin = LineJoin.Round;
                    PointF p1 = new PointF(rect.Left + rect.Width * 0.18f, rect.Top + rect.Height * 0.52f);
                    PointF p2 = new PointF(rect.Left + rect.Width * 0.38f, rect.Top + rect.Height * 0.72f);
                    PointF p3 = new PointF(rect.Left + rect.Width * 0.78f, rect.Top + rect.Height * 0.28f);
                    g.DrawLines(anti_alias_pen, new[] { p1, p2, p3 });
                }
            }
        }
        private class HeaderColors : ProfessionalColorTable{
            public override Color MenuItemSelected => header_colors[0];
            public override Color ToolStripDropDownBackground => header_colors[0];
            public override Color ImageMarginGradientBegin => header_colors[0];
            public override Color ImageMarginGradientEnd => header_colors[0];
            public override Color ImageMarginGradientMiddle => header_colors[0];
            public override Color MenuItemSelectedGradientBegin => header_colors[0];
            public override Color MenuItemSelectedGradientEnd => header_colors[0];
            public override Color MenuItemPressedGradientBegin => header_colors[0];
            public override Color MenuItemPressedGradientMiddle => header_colors[0];
            public override Color MenuItemPressedGradientEnd => header_colors[0];
            public override Color MenuItemBorder => header_colors[0];
            public override Color CheckBackground => header_colors[0];
            public override Color ButtonSelectedBorder => header_colors[0];
            public override Color CheckSelectedBackground => header_colors[0];
            public override Color CheckPressedBackground => header_colors[0];
            public override Color MenuBorder => header_colors[0];
            public override Color SeparatorLight => header_colors[1];
            public override Color SeparatorDark => header_colors[1];
        }
        // LOAD SOFTWARE SETTINGS
        // ======================================================================================================
        private void RunSoftwareEngine(){
            // DOUBLE BUFFER TABLE
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, DataMainTable, new object[] { true });
            // TEMPORARY COLUMN
            DataMainTable.RowTemplate.Height = (int)(32 * this.DeviceDpi / 96f);
            for (int i = 1; i <= 7; i++){ DataMainTable.Columns.Add("x" + i, "x" + i); }
            //
            foreach (DataGridViewColumn DataTable in DataMainTable.Columns){
                DataTable.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // DPI SET
            BtnCopyEmail.Height = TxtEmail.Height + 2;
            BtnCopyPassword.Height = TxtPassword.Height + 2;
            BtnCopyUrl.Height = TxtUrl.Height + 2;
            BtnRndPssGen.Height = TxtPassword.Height + 2;
            BtnOpenUrl.Height = TxtUrl.Height + 2;
            // THEME - LANG - STARTUP - BACKUP - SAFETY WARNINGS - PASSWORD MASK MODE PRELOADER
            // ======================================================================================================
            TSSettingsModule software_read_settings = new TSSettingsModule(ts_sf);
            //
            int theme_mode = int.TryParse(software_read_settings.TSReadSettings(ts_settings_container, "ThemeStatus"), out int the_status) && (the_status == 0 || the_status == 1 || the_status == 2) ? the_status : 1;
            if (theme_mode == 2) { themeSystem = 2; Theme_engine(TSThemeModeHelper.GetSystemTheme(2)); } else Theme_engine(theme_mode);
            darkThemeToolStripMenuItem.Checked = theme_mode == 0;
            lightThemeToolStripMenuItem.Checked = theme_mode == 1;
            systemThemeToolStripMenuItem.Checked = theme_mode == 2;
            //
            string lang_mode = software_read_settings.TSReadSettings(ts_settings_container, "LanguageStatus");
            var languageFiles = new Dictionary<string, (object langResource, ToolStripMenuItem menuItem, bool fileExists)>{
                { "ar", (ts_lang_ar, arabicToolStripMenuItem, File.Exists(ts_lang_ar)) },
                { "zh", (ts_lang_zh, chineseToolStripMenuItem, File.Exists(ts_lang_zh)) },
                { "en", (ts_lang_en, englishToolStripMenuItem, File.Exists(ts_lang_en)) },
                { "nl", (ts_lang_nl, dutchToolStripMenuItem, File.Exists(ts_lang_nl)) },
                { "fr", (ts_lang_fr, frenchToolStripMenuItem, File.Exists(ts_lang_fr)) },
                { "de", (ts_lang_de, germanToolStripMenuItem, File.Exists(ts_lang_de)) },
                { "hi", (ts_lang_hi, hindiToolStripMenuItem, File.Exists(ts_lang_hi)) },
                { "it", (ts_lang_it, italianToolStripMenuItem, File.Exists(ts_lang_it)) },
                { "ja", (ts_lang_ja, japaneseToolStripMenuItem, File.Exists(ts_lang_ja)) },
                { "ko", (ts_lang_ko, koreanToolStripMenuItem, File.Exists(ts_lang_ko)) },
                { "pl", (ts_lang_pl, polishToolStripMenuItem, File.Exists(ts_lang_pl)) },
                { "pt", (ts_lang_pt, portugueseToolStripMenuItem, File.Exists(ts_lang_pt)) },
                { "ru", (ts_lang_ru, russianToolStripMenuItem, File.Exists(ts_lang_ru)) },
                { "es", (ts_lang_es, spanishToolStripMenuItem, File.Exists(ts_lang_es)) },
                { "tr", (ts_lang_tr, turkishToolStripMenuItem, File.Exists(ts_lang_tr)) },
            };
            foreach (var langLoader in languageFiles) { langLoader.Value.menuItem.Enabled = langLoader.Value.fileExists; }
            var (langResource, selectedMenuItem, _) = languageFiles.ContainsKey(lang_mode) ? languageFiles[lang_mode] : languageFiles["en"];
            Lang_engine(Convert.ToString(langResource), lang_mode);
            selectedMenuItem.Checked = true;
            //
            string startup_mode = software_read_settings.TSReadSettings(ts_settings_container, "StartupStatus");
            startup_status = int.TryParse(startup_mode, out int str_status) && (str_status == 0 || str_status == 1) ? str_status : 0;
            WindowState = startup_status == 1 ? FormWindowState.Maximized : FormWindowState.Normal;
            windowedToolStripMenuItem.Checked = startup_status == 0;
            fullScreenToolStripMenuItem.Checked = startup_status == 1;
            //
            string abackup_mode = software_read_settings.TSReadSettings(ts_settings_container, "AutoBackupStatus");
            auto_backup_status = int.TryParse(abackup_mode, out int abackup_status) && (abackup_status == 0 || abackup_status == 1) ? abackup_status : 0;
            autoDataBackupOnToolStripMenuItem.Checked = auto_backup_status == 1;
            autoDataBackupOffToolStripMenuItem.Checked = auto_backup_status == 0;
            //
            string safety_mode = software_read_settings.TSReadSettings(ts_settings_container, "SafetyWarnings");
            safety_warnings_status = int.TryParse(safety_mode, out int safetywar_status) && (safetywar_status == 0 || safetywar_status == 1) ? safetywar_status : 0;
            safetyWarningsOnToolStripMenuItem.Checked = safety_warnings_status == 1;
            safetyWarningsOffToolStripMenuItem.Checked = safety_warnings_status == 0;
            //
            string password_mask_mode = software_read_settings.TSReadSettings(ts_settings_container, "PasswordMask");
            password_mask_status = int.TryParse(password_mask_mode, out int pass_mask_status) && (pass_mask_status == 0 || pass_mask_status == 1) ? pass_mask_status : 1;
            PMaskActiveToolStripMenuItem.Checked = password_mask_status == 1;
            PMaskDisabledToolStripMenuItem.Checked = password_mask_status == 0;
        }
        // MAIN TOOLTIP SETTINGS
        // ======================================================================================================
        private void MainToolTip_Draw(object sender, DrawToolTipEventArgs e){ e.DrawBackground(); e.DrawBorder(); e.DrawText(); }
        // LOAD
        // ======================================================================================================
        private async void Astel_Load(object sender, EventArgs e){
            // PREFETCH LOAD
            ServiceListAdd();
            RunSoftwareEngine();
            //
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            Text = TS_VersionEngine.TS_SoftwareVersion(0) + " - " + software_lang.TSReadLangs("AstelHome", "ah_load");
            HeaderMenu.Cursor = Cursors.Hand;
            // TEMPORARY COLUMN CLEAR
            DataMainTable.Columns.Clear();
            // LOGIN SECURITY
            await InitializeLoaderSecurityAsync();
            // LOAD MODULE
            AstelLoadXMLData();
            DGVColumnFormatter();
            DataMainTable.ClearSelection();
            //
            Text = TS_VersionEngine.TS_SoftwareVersion(0);
            //
            float dpi = this.DeviceDpi / 96f;
            DataMainTable.Columns[0].Width = (int)(40 * dpi);
            DataMainTable.Columns[1].Width = (int)(110 * dpi);
            DataMainTable.Columns[2].Width = (int)(160 * dpi);
            DataMainTable.Columns[3].Width = (int)(140 * dpi);
            DataMainTable.Columns[4].Width = (int)(150 * dpi);
            DataMainTable.Columns[5].Width = (int)(130 * dpi);
            DataMainTable.Columns[6].Width = (int)(130 * dpi);
            foreach (DataGridViewColumn col in DataMainTable.Columns){
                int pad = (int)(3 * dpi);
                col.DefaultCellStyle.Padding = new Padding(pad, 0, 0, 0);
            }
            // EVENT: KEY NAVIGATION FOR TABLE
            DataMainTable.KeyDown += DataMainTable_KeyDown;
            // ENABLE FORM-LEVEL KEY PREVIEW FOR ARROW NAVIGATION
            this.KeyPreview = true;
            this.KeyDown += AstelMain_KeyDown;
            // RUN TASKS
            Task softwareUpdateCheck = Task.Run(() => Software_update_check(0));
            if (auto_backup_status == 1 && (auto_backup == null || auto_backup.IsCompleted)){
                cts = new CancellationTokenSource();
                auto_backup = StartAutoBackup(cts.Token);
            }
        }
        // SERVICE LIST ADD
        // ======================================================================================================
        private void ServiceListAdd(){
            string[] globalServices = new string[]{
                "-",
                // Social / Communication
                "Facebook", "Instagram", "X (Twitter)", "LinkedIn", "TikTok",
                "Snapchat", "Reddit", "Discord", "Telegram", "WhatsApp", "Signal", "Threads",
                // Entertainment / Media
                "Netflix", "Spotify", "Disney+", "Max", "Apple TV+",
                // Gaming
                "Steam", "Epic Games", "PlayStation Network", "Xbox", "Roblox", "Riot Games",
                // Shopping / Finance
                "Amazon", "eBay", "Shopify", "AliExpress", "Temu",
                "PayPal", "Wise", "Revolut", "Binance", "Coinbase",
                // AI / Productivity
                "ChatGPT", "Claude", "DeepSeek", "DeepL",
                "Google", "Microsoft", "Apple",
                "Notion", "Canva",
                // Developer
                "GitHub", "GitLab", "Stack Overflow", "Docker",
                // Travel
                "Uber", "Airbnb", "Booking.com"
            };
            string[] turkeyServices = new string[]{
                // Turkey - E-Commerce / Delivery
                "Trendyol", "Hepsiburada", "Sahibinden",
                "Yemeksepeti", "Getir",
                // Turkey - Transportation / Travel
                "Obilet", "THY", "Pegasus", "Enuygun",
                // Turkey - Finance / Banks
                "Ziraat Bankası", "İş Bankası", "Garanti BBVA", "Yapı Kredi",
                "Akbank", "DenizBank", "Halkbank", "VakıfBank",
                "Papara", "Enpara", "Midas",
                // Turkey - Government / Education
                "E-Devlet", "E-Nabız", "MHRS", "ÖSYM",
                // Turkey - Operators / Local
                "Turkcell", "Vodafone", "Türk Telekom", "Türknet"
            };
            var services_main = new List<string>(globalServices);
            var get_region = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            if (get_region.TwoLetterISORegionName == "TR"){
                services_main.AddRange(turkeyServices);
            }
            string[] content_services = services_main.ToArray();
            Array.Sort(content_services, StringComparer.CurrentCultureIgnoreCase);
            CmbService.Items.Clear();
            CmbService.Items.AddRange(content_services);
            CmbService.SelectedIndex = 0;
        }
        private async Task<XDocument> InitializeAESAsync(){
            return await Task.Run(() => {
                XDocument ts_xDoc;
                ts_xDoc = XDocument.Load(ts_data_xml_path);
                var root = ts_xDoc.Element("Datas");
                string saltBase64 = root.Attribute("ST")?.Value?.Trim();
                string keyMaterialBase64 = root.Attribute("EK")?.Value?.Trim();
                byte[] salt = null;
                byte[] keyMaterial = null;
                try{
                    if (string.IsNullOrEmpty(saltBase64) || string.IsNullOrEmpty(keyMaterialBase64)){
                        salt = new byte[16];
                        keyMaterial = new byte[32];
                        using (var rng = RandomNumberGenerator.Create()){ rng.GetBytes(salt); rng.GetBytes(keyMaterial); }
                        string keyMaterialBase64Str = Convert.ToBase64String(keyMaterial);
                        root.SetAttributeValue("EK", keyMaterialBase64Str);
                        root.SetAttributeValue("ST", Convert.ToBase64String(salt));
                        string tempPath = ts_data_xml_path + ".tmp";
                        ts_xDoc.Save(tempPath);
                        File.Replace(tempPath, ts_data_xml_path, null);
                    }else{
                        salt = Convert.FromBase64String(saltBase64);
                        keyMaterial = Convert.FromBase64String(keyMaterialBase64);
                    }
                    byte[] aesKey = TS_AES_Encryption.DeriveKeyFromMaterial(keyMaterial, salt);
                    TS_AES_Encryption.SetKey(aesKey);
                    return ts_xDoc;
                }
                finally{
                    if (keyMaterial != null)
                        Array.Clear(keyMaterial, 0, keyMaterial.Length);
                    if (salt != null)
                        Array.Clear(salt, 0, salt.Length);
                }
            });
        }
        private async Task InitializeLoaderSecurityAsync(){
            if (!File.Exists(ts_data_xml_path)){
                CreateEmptyXmlFile();
            }
            TSSettingsModule software_read_settings = new TSSettingsModule(ts_session_file);
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            var ts_xDoc = await InitializeAESAsync();
            var root = ts_xDoc.Element("Datas");
            root.SetAttributeValue("SV", TS_VersionEngine.TS_SoftwareVersion(1));
            string saved_crossLinker64 = software_read_settings.TSReadSettings(ts_session_container, "CrossLinker").Trim();
            string saved_crossLinker = string.IsNullOrEmpty(saved_crossLinker64) ? "" : TS_SessionProtection.UnprotectSessionData(saved_crossLinker64);
            string saved_cl = root.Attribute("CL")?.Value.Trim() ?? "";
            if (!string.IsNullOrEmpty(saved_cl)){
                if (saved_cl != saved_crossLinker){
                    File.Delete(ts_data_xml_path);
                    if (Directory.Exists(ts_data_backup_folder)){
                        Directory.Delete(ts_data_backup_folder, true);
                    }
                    CreateEmptyXmlFile();
                    await InitializeAESAsync();
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("CrossLinker", "cl_message"), "\n\n", "\n\n"));
                    return;
                }
            }else{
                root.SetAttributeValue("CL", saved_crossLinker);
                ts_xDoc.Save(ts_data_xml_path);
            }
        }
        // CREATES XML FILE IF IT IS EMPTY (ONLY WITH <Datas> ROOT)
        // ======================================================================================================
        private void CreateEmptyXmlFile(){
            string dir = Path.GetDirectoryName(ts_data_xml_path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var ts_xDoc = new XDocument(new XElement("Datas"));
            ts_xDoc.Save(ts_data_xml_path);
        }
        // LOADS THE XML DATA AND PASSES IT TO THE DATATABLE
        // ======================================================================================================
        private void AstelLoadXMLData(){
            if (!File.Exists(ts_data_xml_path)) CreateEmptyXmlFile();
            var ts_xDoc = XDocument.Load(ts_data_xml_path);
            var ts_xDoc_root = ts_xDoc.Element("Datas");
            // UPDATE FILE SV VERSION
            if (ts_xDoc_root != null){
                string currentVersion = ts_xDoc_root.Attribute("SV")?.Value ?? string.Empty;
                string newVersion = TS_VersionEngine.TS_SoftwareVersion(1);
                if (currentVersion != newVersion){
                    ts_xDoc_root.SetAttributeValue("SV", newVersion);
                    ts_xDoc.Save(ts_data_xml_path);
                }
            }
            //
            DataSet ts_dataSet = new DataSet();
            DataTable ts_dataTable = new DataTable("Datas");
            ts_dataTable.Columns.Add("ID", typeof(int));
            ts_dataTable.Columns.Add("Service", typeof(string));
            ts_dataTable.Columns.Add("Email", typeof(string));
            ts_dataTable.Columns.Add("Password", typeof(string));
            ts_dataTable.Columns.Add("Url", typeof(string));
            ts_dataTable.Columns.Add("Note", typeof(string));
            ts_dataTable.Columns.Add("PassChangeDate", typeof(string));
            if (ts_xDoc_root != null){
                foreach (var ts_xml_mode in ts_xDoc_root.Elements("Data")){
                    try
                    {
                        DataRow ts_xml_row = ts_dataTable.NewRow();
                        ts_xml_row["ID"] = int.Parse(ts_xml_mode.Element("ID")?.Value ?? "0");
                        ts_xml_row["Service"] = ts_xml_mode.Element("Service") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Service").Value) : string.Empty;
                        ts_xml_row["Email"] = ts_xml_mode.Element("Email") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Email").Value) : string.Empty;
                        ts_xml_row["Password"] = ts_xml_mode.Element("Password") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Password").Value) : string.Empty;
                        ts_xml_row["Url"] = ts_xml_mode.Element("Url") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Url").Value) : string.Empty;
                        ts_xml_row["Note"] = ts_xml_mode.Element("Note") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("Note").Value) : string.Empty;
                        ts_xml_row["PassChangeDate"] = ts_xml_mode.Element("PassChangeDate") != null ? TS_AES_Encryption.TS_AES_Decrypt(ts_xml_mode.Element("PassChangeDate").Value) : string.Empty;
                        ts_dataTable.Rows.Add(ts_xml_row);
                    }
                    catch
                    {
                    }
                }
            }
            ts_dataSet.Tables.Add(ts_dataTable);
            DataMainTable.DataSource = ts_dataSet.Tables[0];
            DataMainTable.CellFormatting += (s, e) => {
                if (e.ColumnIndex == 3 && e.Value != null && e.Value is string pwd && pwd != ""){
                    if (password_mask_status == 1){
                        e.Value = new string('●', Math.Min(pwd.Length, 20));
                        e.FormattingApplied = true;
                    }
                }
            };
        }
        // SECURE ID GENERATOR & REORDER ID
        // ======================================================================================================
        private static readonly object idLock = new object();
        private int TSGenerateNewID(){
            lock (idLock){
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var ts_xml_root = ts_xDoc.Element("Datas");
                int xml_max_id = ts_xml_root.Elements("Data").Select(g => (int)g.Element("ID")).DefaultIfEmpty(0).Max();
                return xml_max_id + 1;
            }
        }
        private int GetMaxIdFromXml(){
            lock (idLock){
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var ts_xml_root = ts_xDoc.Element("Datas");
                int xml_max_id = ts_xml_root.Elements("Data").Select(g => (int)g.Element("ID")).DefaultIfEmpty(0).Max();
                return xml_max_id;
            }
        }
        private void TSReorderID(XDocument xDoc){
            var root = xDoc.Element("Datas");
            var allDataElements = root.Elements("Data").ToList();
            int counter = 1;
            foreach (var element in allDataElements){
                element.SetElementValue("ID", counter++);
            }
        }
        // ADD DATA
        // ======================================================================================================
        private async void AddBtn_Click(object sender, EventArgs e){
            await ProgressData(false);
            DataMainTable.Focus();
        }
        // UPDATE DATA
        // ======================================================================================================
        private async void UpdateBtn_Click(object sender, EventArgs e){
            await ProgressData(true);
            DataMainTable.Focus();
        }
        // VALIDATE AND PROGRESS FUNCTIONS
        // ======================================================================================================
        private bool ValidateInputs(out string in_service, out string in_email, out string in_password, out string in_url, out string in_note, out string errorMsg){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            in_service = FormatServiceName(TxtService.Text);
            in_email = TxtEmail.Text.Trim();
            in_password = TxtPassword.Text.Trim();
            in_url = TxtUrl.Text.Trim();
            in_note = TxtNote.Text.Trim();
            errorMsg = "";
            if (string.IsNullOrEmpty(in_email)){
                errorMsg = string.Format(software_lang.TSReadLangs("AstelHome", "ah_add_email_info"), "\n");
                return false;
            }
            if (string.IsNullOrEmpty(in_password)){
                errorMsg = software_lang.TSReadLangs("AstelHome", "ah_add_password_info");
                return false;
            }
            if (safety_warnings_status == 1){
                var (isStrongPassword, passwordDetails) = CheckPasswordStrength(in_password);
                if (!isStrongPassword){
                    string passWeaks = string.Format(software_lang.TSReadLangs("SafetyWarningsPassword", "swp_pass_weak"), "\n\n", "\n\n");
                    foreach (var passwordDetail in passwordDetails){
                        if (!passwordDetail.Value){
                            passWeaks += "- " + passwordDetail.Key + "\n";
                        }
                    }
                    errorMsg = string.Format(software_lang.TSReadLangs("SafetyWarningsPassword", "swp_pass_weak_last"), passWeaks + "\n", "\n\n");
                    return false;
                }
            }
            return true;
        }
        // STRONG PASSWORD CHECK SYSTEM
        // ======================================================================================================
        static (bool isStrongPassword, Dictionary<string, bool> passwordDetails) CheckPasswordStrength(string password){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            var checksPasswordRequire = new Dictionary<string, bool>{
                { software_lang.TSReadLangs("SafetyWarningsPassword", "swp_pass_req_1"), password.Length >= 8 },
                { software_lang.TSReadLangs("SafetyWarningsPassword", "swp_pass_req_2"), Regex.IsMatch(password, "[A-Z]") },
                { software_lang.TSReadLangs("SafetyWarningsPassword", "swp_pass_req_3"), Regex.IsMatch(password, "[a-z]") },
                { software_lang.TSReadLangs("SafetyWarningsPassword", "swp_pass_req_4"), Regex.IsMatch(password, "[0-9]") },
                { software_lang.TSReadLangs("SafetyWarningsPassword", "swp_pass_req_5"), Regex.IsMatch(password, "[!@#$%^&*()\\-_=+?]") }
            };
            bool strongPassword = true;
            foreach (var checkPasswordReq in checksPasswordRequire.Values){
                if (!checkPasswordReq){
                    strongPassword = false;
                    break;
                }
            }
            return (strongPassword, checksPasswordRequire);
        }
        private async Task ProgressData(bool isUpdate){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (isUpdate && DataMainTable.SelectedRows.Count == 0){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelHome", "ah_update_select_info"));
                    return;
                }
                if (!ValidateInputs(out var in_service, out var in_email, out var in_password, out var in_url, out var in_note, out var errorMsg)){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, errorMsg);
                    return;
                }
                // UPDATE CHANGE CHECK
                // =====================================================================
                if (isUpdate){
                    string currentService = FormatServiceName(TxtService.Text);
                    string currentEmail = TxtEmail.Text.Trim();
                    string currentPassword = TxtPassword.Text.Trim();
                    string currentUrl = TxtUrl.Text.Trim();
                    string currentNote = TxtNote.Text.Trim();
                    if (currentService == _originalService && currentEmail == _originalEmail && currentPassword == _originalPassword && currentUrl == _originalUrl && currentNote == _originalNote){
                        TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", "ah_update_no_changes"));
                        return;
                    }
                    var confirm = TS_MessageBoxEngine.TS_MessageBox(this, 4, string.Format(software_lang.TSReadLangs("AstelHome", "ah_update_question_info"), DataMainTable.SelectedRows[0].Cells["Service"].Value?.ToString() ?? ""));
                    if (confirm != DialogResult.Yes){
                        return;
                    }
                }
                // CREATE XML IF NOT EXISTS
                // =====================================================================
                if (!File.Exists(ts_data_xml_path)){
                    await InitializeLoaderSecurityAsync();
                }
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var ts_xml_root = ts_xDoc.Element("Datas");
                // DUPLICATE CHECK
                // =====================================================================
                var duplicateData = ts_xml_root.Elements("Data").FirstOrDefault(x => {
                    int rowId = (int)x.Element("ID");
                    if (isUpdate && rowId == int.Parse(DataMainTable.SelectedRows[0].Cells["ID"].Value.ToString())){
                        return false;
                    }
                    string service = TS_AES_Encryption.TS_AES_Decrypt(x.Element("Service")?.Value ?? "");
                    string email = TS_AES_Encryption.TS_AES_Decrypt(x.Element("Email")?.Value ?? "");
                    return service.Equals(in_service, StringComparison.OrdinalIgnoreCase) && email.Equals(in_email, StringComparison.OrdinalIgnoreCase);
                });
                if (duplicateData != null){
                    string duplicateService = TS_AES_Encryption.TS_AES_Decrypt(duplicateData.Element("Service")?.Value ?? "");
                    string duplicateEmail = TS_AES_Encryption.TS_AES_Decrypt(duplicateData.Element("Email")?.Value ?? "");
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("AstelHome", "ah_duplicate_entry"), duplicateService, duplicateEmail));
                    return;
                }
                // UPDATE
                // =====================================================================
                if (isUpdate){
                    int selectedId = int.Parse(DataMainTable.SelectedRows[0].Cells["ID"].Value.ToString());
                    var elementToUpdate = ts_xml_root.Elements("Data").FirstOrDefault(x => (int)x.Element("ID") == selectedId);
                    if (elementToUpdate != null){
                        elementToUpdate.SetElementValue("Service", TS_AES_Encryption.TS_AES_Encrypt(in_service));
                        elementToUpdate.SetElementValue("Email", TS_AES_Encryption.TS_AES_Encrypt(in_email));
                        elementToUpdate.SetElementValue("Password", TS_AES_Encryption.TS_AES_Encrypt(in_password));
                        elementToUpdate.SetElementValue("Url", TS_AES_Encryption.TS_AES_Encrypt(in_url));
                        elementToUpdate.SetElementValue("Note", TS_AES_Encryption.TS_AES_Encrypt(in_note));
                        elementToUpdate.SetElementValue("PassChangeDate", TS_AES_Encryption.TS_AES_Encrypt(DateTime.Now.ToString("dd.MM.yyyy - HH:mm")));
                    }
                }else{
                    // ADD
                    // =================================================================
                    ts_xml_root.Add(
                        new XElement("Data",
                            new XElement("ID", TSGenerateNewID()),
                            new XElement("Service", TS_AES_Encryption.TS_AES_Encrypt(in_service)),
                            new XElement("Email", TS_AES_Encryption.TS_AES_Encrypt(in_email)),
                            new XElement("Password", TS_AES_Encryption.TS_AES_Encrypt(in_password)),
                            new XElement("Url", TS_AES_Encryption.TS_AES_Encrypt(in_url)),
                            new XElement("Note", TS_AES_Encryption.TS_AES_Encrypt(in_note)),
                            new XElement("PassChangeDate",TS_AES_Encryption.TS_AES_Encrypt(DateTime.Now.ToString("dd.MM.yyyy - HH:mm")))
                        )
                    );
                }
                // SAVE
                // =====================================================================
                ts_xDoc.Save(ts_data_xml_path);
                AstelLoadXMLData();
                NodeClearInput();
                _isFirstKeyNavigation = true;
                DataMainTable.Focus();
                DataMainTable.ClearSelection();
                TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", isUpdate ? "ah_update_success" : "ah_add_success"));
            }catch (Exception){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("AstelHome", isUpdate ? "ah_update_failed" : "ah_add_failed"), "\n"));
            }
        }
        // DELETE DATA
        // ======================================================================================================
        private void DeleteBtn_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (DataMainTable.SelectedRows.Count == 0){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AstelHome", "ah_delete_info"));
                    return;
                }
                //
                string selectedService = DataMainTable.SelectedRows[0].Cells["Service"].Value?.ToString() ?? "";
                string deleteMsg = string.Format(software_lang.TSReadLangs("AstelHome", "ah_delete_question_info"), selectedService);
                DialogResult checkDeleteQuery = TS_MessageBoxEngine.TS_MessageBox(this, 4, deleteMsg);
                if (checkDeleteQuery == DialogResult.Yes){
                    var ts_xDoc = XDocument.Load(ts_data_xml_path);
                    var ts_xml_root = ts_xDoc.Element("Datas");
                    //
                    int selectedId = int.Parse(DataMainTable.SelectedRows[0].Cells["ID"].Value.ToString());
                    var elementToDelete = ts_xml_root.Elements("Data").FirstOrDefault(x => (int)x.Element("ID") == selectedId);
                    //
                    elementToDelete?.Remove();
                    //
                    TSReorderID(ts_xDoc);
                    ts_xDoc.Save(ts_data_xml_path);
                    AstelLoadXMLData();
                    NodeClearInput();
                    _isFirstKeyNavigation = true;
                    //
                    DataMainTable.ClearSelection();
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", "ah_delete_success"));
                }
            }catch (Exception){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("AstelHome", "ah_delete_failed"), "\n"));
            }finally{
                DataMainTable.Focus();
                DataMainTable.ClearSelection();
            }
        }
        // CLEAR INPUT (Memory Safe)
        // ======================================================================================================
        private void NodeClearInput(){
            ClearSecureTextBox(TxtService);
            ClearSecureTextBox(TxtPassword);
            ClearSecureTextBox(TxtEmail);
            ClearSecureTextBox(TxtUrl);
            ClearSecureTextBox(TxtNote);
        }
        private static void ClearSecureTextBox(TextBox tb){
            if (tb == null || string.IsNullOrEmpty(tb.Text)) return;
            tb.Text = string.Empty;
        }
        // CLEAR SELECTION WITH ESCAPE KEY
        // ======================================================================================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData){
            if (keyData == Keys.Escape){
                DataMainTable.ClearSelection();
                DataMainTable.CurrentCell = null;
                NodeClearInput();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        // COPY DATA (with clipboard auto-clear + memory cleanup)
        // ======================================================================================================
        private void BtnCopyEmail_Click(object sender, EventArgs e){
            try{
                string copiedText = TxtEmail.Text.Trim();
                if (!string.IsNullOrEmpty(copiedText)){
                    Clipboard.SetText(copiedText);
                    ScheduleClipboardClear(copiedText);
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", "ah_copy_email"));
                }
            }catch (Exception){ }
        }
       private void BtnCopyPassword_Click(object sender, EventArgs e){
            try{
                string copiedText = TxtPassword.Text.Trim();
                if (!string.IsNullOrEmpty(copiedText)){
                    Clipboard.SetText(copiedText);
                    ScheduleClipboardClear(copiedText);
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", "ah_copy_password"));
                }
            }catch (Exception){ }
        }
        private void BtnCopyUrl_Click(object sender, EventArgs e){
            try{
                string copiedText = TxtUrl.Text.Trim();
                if (!string.IsNullOrEmpty(copiedText)){
                    Clipboard.SetText(copiedText);
                    ScheduleClipboardClear(copiedText);
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", "ah_copy_url"));
                }
            }catch (Exception){ }
        }
        private static void ScheduleClipboardClear(string copiedText){
            string captured = copiedText;
            Task.Delay(30000).ContinueWith(_ => {
                try{
                    if (Clipboard.GetText() == captured){
                        Clipboard.Clear();
                    }
                }catch { }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        // RANDOM PASSWORD GENERATOR (Cryptographically Secure)
        // ======================================================================================================
        private readonly RandomNumberGenerator _secureRng = RandomNumberGenerator.Create();
        private void BtnRndPssGen_Click(object sender, EventArgs e){
            GenerateRandomPassword();
        }
        private void GenerateRandomPassword(){
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lower = "abcdefghijklmnopqrstuvwxyz";
            string digits = "0123456789";
            string symbols = "!@#$%^&*()-_=+?";
            string allChars = upper + lower + digits + symbols;
            byte[] buffer = new byte[1];
            int passLength;
            do{
                _secureRng.GetBytes(buffer);
            } while (buffer[0] >= byte.MaxValue - (byte.MaxValue % 5));
            passLength = 12 + (buffer[0] % 5);
            char[] chars = new char[passLength];
            string[] categories = new[] { upper, lower, digits, symbols };
            for (int i = 0; i < categories.Length; i++){
                string category = categories[i];
                int catSize = category.Length;
                int catThreshold = byte.MaxValue - (byte.MaxValue % catSize);
                byte catByte;
                do{
                    _secureRng.GetBytes(buffer);
                    catByte = buffer[0];
                } while (catByte >= catThreshold);

                chars[i] = category[catByte % catSize];
            }
            int allCharsSize = allChars.Length;
            int allRejectionThreshold = byte.MaxValue - (byte.MaxValue % allCharsSize);
            for (int i = categories.Length; i < passLength; i++){
                byte randByte;
                do{
                    _secureRng.GetBytes(buffer);
                    randByte = buffer[0];
                } while (randByte >= allRejectionThreshold);
                chars[i] = allChars[randByte % allCharsSize];
            }
            for (int i = passLength - 1; i > 0; i--){
                byte swapByte;
                do{
                    _secureRng.GetBytes(buffer);
                    swapByte = buffer[0];
                } while (swapByte >= byte.MaxValue - (byte.MaxValue % (i + 1)));
                int j = swapByte % (i + 1);
                (chars[j], chars[i]) = (chars[i], chars[j]);
            }
            TxtPassword.Text = new string(chars);
        }
        // OPEN URL TO BROWSER
        // ======================================================================================================
        private void BtnOpenUrl_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtUrl.Text.Trim())){
                    Process.Start(new ProcessStartInfo(TxtUrl.Text.Trim()) { UseShellExecute = true });
                }
            }catch (Exception){ }
        }
        // TEXTBOX ROTATE DATA
        // ======================================================================================================
        private void DataMainTable_CellClick(object sender, DataGridViewCellEventArgs e){
            try{
                if (e.RowIndex >= 0){
                    _isFirstKeyNavigation = false;
                    LoadSelectedRowData(e.RowIndex);
                }
            }catch (Exception){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("AstelHome", "ah_select_failed"), "\n"));
            }
        }
        // LOAD SELECTED ROW DATA TO TEXTBOXES
        // ======================================================================================================
        private void LoadSelectedRowData(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= DataMainTable.Rows.Count) return;
            DataGridViewRow xml_select_row = DataMainTable.Rows[rowIndex];
            TxtService.Text = xml_select_row.Cells[1].Value.ToString();
            TxtEmail.Text = xml_select_row.Cells[2].Value.ToString();
            TxtPassword.Text = xml_select_row.Cells[3].Value.ToString();
            TxtUrl.Text = xml_select_row.Cells[4].Value.ToString();
            TxtNote.Text = xml_select_row.Cells[5].Value.ToString();
            // STORE ORIGINAL VALUES FOR CHANGE DETECTION
            _originalService = FormatServiceName(TxtService.Text);
            _originalEmail = TxtEmail.Text.Trim();
            _originalPassword = TxtPassword.Text.Trim();
            _originalUrl = TxtUrl.Text.Trim();
            _originalNote = TxtNote.Text.Trim();
        }
        // FORMAT SERVICE NAME (CAPITALIZE FIRST LETTER OF EACH WORD)
        // ======================================================================================================
        private string FormatServiceName(string service){
            return string.Join(" ", service.Split(' ').Select(k => string.IsNullOrWhiteSpace(k) ? "" : char.ToUpper(k[0]) + k.Substring(1).ToLower()));
        }
        // FORM-LEVEL KEY DOWN
        // ======================================================================================================
        private void AstelMain_KeyDown(object sender, KeyEventArgs e){
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) && !DataMainTable.Focused){
                e.Handled = true;
                DataMainTable.Focus();
                DataMainTable_KeyDown(DataMainTable, e);
            }
        }
        // KEY NAVIGATION FOR TABLE (UP/DOWN ARROW with WRAP)
        // ======================================================================================================
        private void DataMainTable_KeyDown(object sender, KeyEventArgs e){
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down){
                e.Handled = true;
                int rowCount = DataMainTable.Rows.Count;
                if (rowCount == 0) return;
                if (_isFirstKeyNavigation){
                    _isFirstKeyNavigation = false;
                    DataMainTable.ClearSelection();
                    DataMainTable.Rows[0].Selected = true;
                    DataMainTable.CurrentCell = DataMainTable.Rows[0].Cells[0];
                    LoadSelectedRowData(0);
                    return;
                }
                int currentIndex = (DataMainTable.CurrentCell != null) ? DataMainTable.CurrentCell.RowIndex : -1;
                if (currentIndex < 0){
                    DataMainTable.Rows[0].Selected = true;
                    DataMainTable.CurrentCell = DataMainTable.Rows[0].Cells[0];
                    LoadSelectedRowData(0);
                    return;
                }
                int newIndex;
                if (e.KeyCode == Keys.Down){
                    newIndex = (currentIndex + 1) % rowCount;
                }else{
                    newIndex = (currentIndex - 1 + rowCount) % rowCount;
                }
                DataMainTable.ClearSelection();
                DataMainTable.Rows[newIndex].Selected = true;
                DataMainTable.CurrentCell = DataMainTable.Rows[newIndex].Cells[0];
                LoadSelectedRowData(newIndex);
            }
        }
        // CMB SELECT CHANGE
        // ======================================================================================================
        private void CmbService_SelectedIndexChanged(object sender, EventArgs e){
            if (suppressComboBoxEvent) return;
            if (CmbService.SelectedIndex > 0){
                TxtService.Text = CmbService.SelectedItem.ToString();
            }else{
                TxtService.Clear();
            }
            this.ActiveControl = null;
        }
        private void CmbService_MouseWheel(object sender, MouseEventArgs e){
            ((HandledMouseEventArgs)e).Handled = true;
        }
        // TXT SERVICE SELECT CHANGE
        // ======================================================================================================
        private void TxtService_TextChanged(object sender, EventArgs e){
            string text = TxtService.Text.Trim();
            if (string.IsNullOrEmpty(text)){
                suppressComboBoxEvent = true;
                CmbService.SelectedIndex = 0;
                suppressComboBoxEvent = false;
                return;
            }
            int bestDistance = int.MaxValue;
            int bestIndex = 0;
            int threshold = 3;
            for (int i = 1; i < CmbService.Items.Count; i++){
                string item = CmbService.Items[i].ToString();
                int distance = LevenshteinDistance(text, item);
                if (distance < bestDistance){
                    bestDistance = distance;
                    bestIndex = i;
                }
            }
            suppressComboBoxEvent = true;
            if (bestDistance <= threshold)
                CmbService.SelectedIndex = bestIndex;
            else
                CmbService.SelectedIndex = 0;
            suppressComboBoxEvent = false;
        }
        // LEVENSHTEIN FUNCTION
        // ======================================================================================================
        private int LevenshteinDistance(string a, string b){
            int[,] d = new int[a.Length + 1, b.Length + 1];
            for (int i = 0; i <= a.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= b.Length; j++) d[0, j] = j;
            for (int i = 1; i <= a.Length; i++){
                for (int j = 1; j <= b.Length; j++){
                    int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            return d[a.Length, b.Length];
        }
        // ======================================================================================================
        // AUTO BACKUP DATA
        private const int MaxBackupFiles = 30;
        private readonly SemaphoreSlim _backupLock = new SemaphoreSlim(1, 1);
        private async Task StartAutoBackup(CancellationToken token){
            while (!token.IsCancellationRequested){
                if (DataMainTable.Rows.Count > 0){
                    try{
                        await Task.Delay(500, token);
                        if (token.IsCancellationRequested) break;
                        var backupFiles = Directory.Exists(ts_data_backup_folder) ? new DirectoryInfo(ts_data_backup_folder).GetFiles() : Array.Empty<FileInfo>();
                        bool shouldBackup = false;
                        if (backupFiles.Length == 0){
                            shouldBackup = true;
                        }else{
                            var lastBackupFile = backupFiles.OrderByDescending(f => f.CreationTime).First();
                            TimeSpan timeSinceLastBackup = DateTime.Now - lastBackupFile.CreationTime;
                            if (timeSinceLastBackup.TotalMinutes >= 60){
                                shouldBackup = true;
                            }
                        }
                        if (shouldBackup){
                            await _backupLock.WaitAsync(token);
                            try{
                                if (!Directory.Exists(ts_data_backup_folder)){
                                    Directory.CreateDirectory(ts_data_backup_folder);
                                }
                                string backupFileName = $"{Path.GetFileNameWithoutExtension(ts_data_xml_path)}_{DateTime.Now:ddMMyyyy_HHmmss}_{GenerateSecureRandomString(7).Substring(3)}{ts_data_backup_extension_astel}";
                                string backupFilePath = Path.Combine(ts_data_backup_folder, backupFileName);
                                File.Copy(ts_data_xml_path, backupFilePath, overwrite: false);
                                var allBackups = new DirectoryInfo(ts_data_backup_folder).GetFiles().OrderByDescending(f => f.CreationTime).ToList();
                                if (allBackups.Count > MaxBackupFiles){
                                    foreach (var oldFile in allBackups.Skip(MaxBackupFiles)){
                                        try{
                                            oldFile.Delete();
                                        }catch{ }
                                    }
                                }
                            }
                            finally{
                                _backupLock.Release();
                            }
                        }
                    }
                    catch (OperationCanceledException) { break; }
                    catch (Exception ex){
                        Debug.WriteLine(ex, "AutoBackup");
                    }
                }
                try{
                    await Task.Delay(60000, token);
                }
                catch (TaskCanceledException) { break; }
            }
        }
        // ======================================================================================================
        // THEME SETTINGS
        private ToolStripMenuItem selected_theme = null;
        private void Select_theme_active(object target_theme){
            if (target_theme == null)
                return;
            ToolStripMenuItem clicked_theme = (ToolStripMenuItem)target_theme;
            if (selected_theme == clicked_theme)
                return;
            Select_theme_deactive();
            selected_theme = clicked_theme;
            selected_theme.Checked = true;
        }
        private void Select_theme_deactive(){
            foreach (ToolStripMenuItem theme in themeToolStripMenuItem.DropDownItems){
                theme.Checked = false;
            }
        }
        private void SystemThemeToolStripMenuItem_Click(object sender, EventArgs e){
            themeSystem = 2; Theme_engine(TSThemeModeHelper.GetSystemTheme(2)); SaveTheme(2); Select_theme_active(sender);
        }
        private void LightThemeToolStripMenuItem_Click(object sender, EventArgs e){
            themeSystem = 0; Theme_engine(1); SaveTheme(1); Select_theme_active(sender);
        }
        private void DarkThemeToolStripMenuItem_Click(object sender, EventArgs e){
            themeSystem = 0; Theme_engine(0); SaveTheme(0); Select_theme_active(sender);
        }
        private void TSUseSystemTheme(){ if (themeSystem == 2) Theme_engine(TSThemeModeHelper.GetSystemTheme(2)); }
        private void SaveTheme(int ts){
            // SAVE CURRENT THEME
            try{
                TSSettingsModule software_setting_save = new TSSettingsModule(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "ThemeStatus", Convert.ToString(ts));
            }catch (Exception){ }
        }
        private void Theme_engine(int ts){
            try{
                theme = ts;
                //
                TSThemeModeHelper.SetThemeMode(ts == 0);
                TSThemeModeHelper.InitializeThemeForForm(this);
                //
                if (theme == 1){
                    TSImageRenderer(settingsToolStripMenuItem, Properties.Resources.tm_settings_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(themeToolStripMenuItem, Properties.Resources.tm_theme_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(languageToolStripMenuItem, Properties.Resources.tm_language_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(startupToolStripMenuItem, Properties.Resources.tm_startup_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(changePasswordToolStripMenuItem, Properties.Resources.tm_change_password_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(checkforUpdatesToolStripMenuItem, Properties.Resources.tm_update_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(dataTransferToolStripMenuItem, Properties.Resources.tm_data_transfer_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(exportDataToolStripMenuItem, Properties.Resources.tm_data_export_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(importDataToolStripMenuItem, Properties.Resources.tm_data_import_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(autoDataBackupToolStripMenuItem, Properties.Resources.tm_auto_backup_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(safetyWarningsToolStripMenuItem, Properties.Resources.tm_safety_warnings_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(PassMaskStatusToolStripMenuItem, Properties.Resources.tm_password_mask_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(passwordGeneratorToolStripMenuItem, Properties.Resources.tm_password_generator_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(donateToolStripMenuItem, Properties.Resources.tm_donate_light, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(aboutToolStripMenuItem, Properties.Resources.tm_about_light, 0, ContentAlignment.MiddleRight);
                    //
                    TSImageRenderer(AddBtn, Properties.Resources.ct_add_light, 23, ContentAlignment.MiddleLeft);
                    TSImageRenderer(UpdateBtn, Properties.Resources.ct_update_light, 23, ContentAlignment.MiddleLeft);
                    TSImageRenderer(DeleteBtn, Properties.Resources.ct_delete_light, 23, ContentAlignment.MiddleLeft);
                    //
                    TSImageRenderer(BtnCopyEmail, Properties.Resources.ct_copy_light, 12);
                    TSImageRenderer(BtnCopyPassword, Properties.Resources.ct_copy_light, 12);
                    TSImageRenderer(BtnCopyUrl, Properties.Resources.ct_copy_light, 12);
                    TSImageRenderer(BtnRndPssGen, Properties.Resources.ct_generate_light, 12);
                    TSImageRenderer(BtnOpenUrl, Properties.Resources.ct_link_mc_light, 12);
                }else if (theme == 0){
                    TSImageRenderer(settingsToolStripMenuItem, Properties.Resources.tm_settings_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(themeToolStripMenuItem, Properties.Resources.tm_theme_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(languageToolStripMenuItem, Properties.Resources.tm_language_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(startupToolStripMenuItem, Properties.Resources.tm_startup_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(changePasswordToolStripMenuItem, Properties.Resources.tm_change_password_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(checkforUpdatesToolStripMenuItem, Properties.Resources.tm_update_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(dataTransferToolStripMenuItem, Properties.Resources.tm_data_transfer_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(exportDataToolStripMenuItem, Properties.Resources.tm_data_export_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(importDataToolStripMenuItem, Properties.Resources.tm_data_import_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(autoDataBackupToolStripMenuItem, Properties.Resources.tm_auto_backup_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(safetyWarningsToolStripMenuItem, Properties.Resources.tm_safety_warnings_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(PassMaskStatusToolStripMenuItem, Properties.Resources.tm_password_mask_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(passwordGeneratorToolStripMenuItem, Properties.Resources.tm_password_generator_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(donateToolStripMenuItem, Properties.Resources.tm_donate_dark, 0, ContentAlignment.MiddleRight);
                    TSImageRenderer(aboutToolStripMenuItem, Properties.Resources.tm_about_dark, 0, ContentAlignment.MiddleRight);
                    //
                    TSImageRenderer(AddBtn, Properties.Resources.ct_add_dark, 23, ContentAlignment.MiddleLeft);
                    TSImageRenderer(UpdateBtn, Properties.Resources.ct_update_dark, 23, ContentAlignment.MiddleLeft);
                    TSImageRenderer(DeleteBtn, Properties.Resources.ct_delete_dark, 23, ContentAlignment.MiddleLeft);
                    //
                    TSImageRenderer(BtnCopyEmail, Properties.Resources.ct_copy_dark, 12);
                    TSImageRenderer(BtnCopyPassword, Properties.Resources.ct_copy_dark, 12);
                    TSImageRenderer(BtnCopyUrl, Properties.Resources.ct_copy_dark, 12);
                    TSImageRenderer(BtnRndPssGen, Properties.Resources.ct_generate_dark, 12);
                    TSImageRenderer(BtnOpenUrl, Properties.Resources.ct_link_mc_dark, 12);
                }
                header_colors[0] = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor2");
                header_colors[1] = TS_ThemeEngine.ColorMode(theme, "TSBT_LabelColor1");
                header_colors[2] = TS_ThemeEngine.ColorMode(theme, "TSBT_AccentColor");
                HeaderMenu.Renderer = new HeaderMenuColors();
                // TOOLTIP
                MainToolTip.ForeColor = TS_ThemeEngine.ColorMode(theme, "TSBT_LabelColor1");
                MainToolTip.BackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor");
                // HEADER MENU
                var bg = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor");
                var fg = TS_ThemeEngine.ColorMode(theme, "TSBT_LabelColor1");
                HeaderMenu.ForeColor = fg;
                HeaderMenu.BackColor = bg;
                SetMenuStripColors(HeaderMenu, bg, fg);
                // CONTENT BG
                BackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor");
                // ALL LABEL
                foreach (Control control in Panel_Footer.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(theme, "TSBT_LabelColor1");
                    }
                }
                // ALL TEXTBOX
                foreach (Control control in Panel_Footer.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor2");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(theme, "TSBT_LabelColor1");
                    }
                }
                // ALL BUTTON
                var combinedBtnsControls = FLP_Btns.Controls.Cast<Control>().Concat(Panel_Footer.Controls.Cast<Control>());
                foreach (Control control in combinedBtnsControls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(theme, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_AccentColor");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(theme, "TSBT_AccentColor");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_AccentColor");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(theme, "AccentColorHover");
                    }
                }
                CmbService.BackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor2");
                CmbService.ForeColor = TS_ThemeEngine.ColorMode(theme, "TSBT_LabelColor1");
                CmbService.HoverBackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor2");
                CmbService.ButtonColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor");
                CmbService.ArrowColor = TS_ThemeEngine.ColorMode(theme, "TSBT_LabelColor1");
                CmbService.HoverButtonColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor");
                CmbService.BorderColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBorderColor");
                CmbService.FocusedBorderColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBorderColor");
                CmbService.HoverForeColor = TS_ThemeEngine.ColorMode(theme, "TSBT_LabelColor1");
                CmbService.SelectedBackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_AccentColor");
                CmbService.SelectedForeColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor2");
                // DATA TABLE
                DataMainTable.BackgroundColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor2");
                DataMainTable.GridColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBorderColor");
                DataMainTable.DefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor2");
                DataMainTable.DefaultCellStyle.ForeColor = TS_ThemeEngine.ColorMode(theme, "TSBT_LabelColor1");
                DataMainTable.AlternatingRowsDefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_BGColor");
                DataMainTable.ColumnHeadersDefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_AccentColor");
                DataMainTable.ColumnHeadersDefaultCellStyle.SelectionBackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_AccentColor");
                DataMainTable.ColumnHeadersDefaultCellStyle.ForeColor = TS_ThemeEngine.ColorMode(theme, "DynamicThemeActiveBtnBGColor");
                DataMainTable.DefaultCellStyle.SelectionBackColor = TS_ThemeEngine.ColorMode(theme, "TSBT_AccentColor");
                DataMainTable.DefaultCellStyle.SelectionForeColor = TS_ThemeEngine.ColorMode(theme, "DynamicThemeActiveBtnBGColor");
                //
                Software_other_page_preloader();
            }catch (Exception){ }
        }
        private void SetMenuStripColors(MenuStrip menuStrip, Color bgColor, Color fgColor){
            if (menuStrip == null) return;
            foreach (ToolStripItem item in menuStrip.Items){
                if (item is ToolStripMenuItem menuItem){
                    SetMenuItemColors(menuItem, bgColor, fgColor);
                }
            }
        }
        private void SetMenuItemColors(ToolStripMenuItem menuItem, Color bgColor, Color fgColor){
            if (menuItem == null) return;
            menuItem.BackColor = bgColor;
            menuItem.ForeColor = fgColor;
            foreach (ToolStripItem item in menuItem.DropDownItems){
                if (item is ToolStripMenuItem subMenuItem){
                    SetMenuItemColors(subMenuItem, bgColor, fgColor);
                }
            }
        }
        private void SetContextMenuColors(ContextMenuStrip contextMenu, Color bgColor, Color fgColor){
            if (contextMenu == null) return;
            foreach (ToolStripItem item in contextMenu.Items){
                if (item is ToolStripMenuItem menuItem){
                    SetMenuItemColors(menuItem, bgColor, fgColor);
                }
            }
        }
        // LANGUAGES SETTINGS
        // ======================================================================================================
        private ToolStripMenuItem selected_lang = null;
        private void Select_lang_active(object target_lang){
            if (target_lang == null)
                return;
            ToolStripMenuItem clicked_lang = (ToolStripMenuItem)target_lang;
            if (selected_lang == clicked_lang)
                return;
            Select_lang_deactive();
            selected_lang = clicked_lang;
            selected_lang.Checked = true;
        }
        private void Select_lang_deactive(){
            foreach (ToolStripMenuItem disabled_lang in languageToolStripMenuItem.DropDownItems){
                disabled_lang.Checked = false;
            }
        }
        private void LanguageToolStripMenuItem_Click(object sender, EventArgs e){
            if (sender is ToolStripMenuItem menuItem && menuItem.Tag is string langCode){
                if (lang != langCode && AllLanguageFiles.ContainsKey(langCode)){
                    Lang_preload(AllLanguageFiles[langCode], langCode);
                    Select_lang_active(sender);
                }
            }
        }
        private void Lang_preload(string lang_type, string lang_code){
            Lang_engine(lang_type, lang_code);
            try{
                TSSettingsModule software_setting_save = new TSSettingsModule(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "LanguageStatus", lang_code);
            }catch (Exception){ }
            // LANG CHANGE NOTIFICATION
            // TSGetLangs software_lang = new TSGetLangs(lang_path);
            // DialogResult lang_change_message = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(software_lang.TSReadLangs("LangChange", "lang_change_notification"), "\n\n", "\n\n"));
            // if (lang_change_message == DialogResult.Yes) { Application.Restart(); }
        }
        private void Lang_engine(string lang_type, string lang_code){
            try{
                lang_path = lang_type;
                lang = lang_code;
                // GLOBAL ENGINE
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                // PROTECTION ERRORS
                TSProtectionErrorMessages.Messages["AES_MasterKeyNotSet"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_master_key_not_set");
                TSProtectionErrorMessages.Messages["AES_KeyNull"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_key_null");
                TSProtectionErrorMessages.Messages["AES_KeyLengthInvalid"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_key_length_invalid");
                TSProtectionErrorMessages.Messages["AES_PlainTextNull"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_plain_text_null");
                TSProtectionErrorMessages.Messages["AES_Base64InputNull"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_base64_input_null");
                TSProtectionErrorMessages.Messages["AES_KeyDerivationFailed"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_key_derivation_failed");
                TSProtectionErrorMessages.Messages["AES_KeyDerivationFailedDecrypt"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_key_derivation_failed_decrypt");
                TSProtectionErrorMessages.Messages["AES_InvalidBase64"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_invalid_base64");
                TSProtectionErrorMessages.Messages["AES_InvalidCipherFormat"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_invalid_cipher_format");
                TSProtectionErrorMessages.Messages["AES_UnsupportedVersion"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_unsupported_version");
                TSProtectionErrorMessages.Messages["AES_InvalidCipherLength"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_invalid_cipher_length");
                TSProtectionErrorMessages.Messages["AES_HMACValidationFailed"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_hmac_validation_failed");
                TSProtectionErrorMessages.Messages["AES_InvalidUTF8"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_invalid_utf8");
                TSProtectionErrorMessages.Messages["AES_EncryptionFailed"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_encryption_failed");
                TSProtectionErrorMessages.Messages["AES_DecryptionFailed"] = software_lang.TSReadLangs("TSProtection", "tsp_aes_decryption_failed");
                TSProtectionErrorMessages.Messages["HKDF_InputKeyNull"] = software_lang.TSReadLangs("TSProtection", "tsp_hkdf_input_key_null");
                TSProtectionErrorMessages.Messages["HKDF_OutputLengthInvalid"] = software_lang.TSReadLangs("TSProtection", "tsp_hkdf_output_length_invalid");
                TSProtectionErrorMessages.Messages["HKDF_OutputLengthTooLarge"] = software_lang.TSReadLangs("TSProtection", "tsp_hkdf_output_length_too_large");
                TSProtectionErrorMessages.Messages["DeriveSubKey_MasterKeyNull"] = software_lang.TSReadLangs("TSProtection", "tsp_derive_subkey_master_key_null");
                TSProtectionErrorMessages.Messages["DeriveSubKey_SaltNull"] = software_lang.TSReadLangs("TSProtection", "tsp_derive_subkey_salt_null");
                TSProtectionErrorMessages.Messages["DeriveSubKey_InfoEmpty"] = software_lang.TSReadLangs("TSProtection", "tsp_derive_subkey_info_empty");
                TSProtectionErrorMessages.Messages["ExtractKey_LoadFailed"] = software_lang.TSReadLangs("TSProtection", "tsp_extract_key_load_failed");
                TSProtectionErrorMessages.Messages["ExtractKey_MissingAttributes"] = software_lang.TSReadLangs("TSProtection", "tsp_extract_key_missing_attributes");
                TSProtectionErrorMessages.Messages["ExtractKey_InvalidBase64"] = software_lang.TSReadLangs("TSProtection", "tsp_extract_key_invalid_base64");
                TSProtectionErrorMessages.Messages["TempKey_Null"] = software_lang.TSReadLangs("TSProtection", "tsp_temp_key_null");
                TSProtectionErrorMessages.Messages["PBKDF2_PasswordNull"] = software_lang.TSReadLangs("TSProtection", "tsp_pbkdf2_password_null");
                TSProtectionErrorMessages.Messages["PBKDF2_SaltNull"] = software_lang.TSReadLangs("TSProtection", "tsp_pbkdf2_salt_null");
                TSProtectionErrorMessages.Messages["PBKDF2_IterationsInvalid"] = software_lang.TSReadLangs("TSProtection", "tsp_pbkdf2_iterations_invalid");
                TSProtectionErrorMessages.Messages["PBKDF2_OutputBytesInvalid"] = software_lang.TSReadLangs("TSProtection", "tsp_pbkdf2_output_bytes_invalid");
                TSProtectionErrorMessages.Messages["Session_PlainDataNull"] = software_lang.TSReadLangs("TSProtection", "tsp_session_plain_data_null");
                TSProtectionErrorMessages.Messages["Session_ProtectedDataNull"] = software_lang.TSReadLangs("TSProtection", "tsp_session_protected_data_null");
                TSProtectionErrorMessages.Messages["Hash_PasswordNull"] = software_lang.TSReadLangs("TSProtection", "tsp_hash_password_null");
                TSProtectionErrorMessages.Messages["Hash_SaltNull"] = software_lang.TSReadLangs("TSProtection", "tsp_hash_salt_null");
                TSProtectionErrorMessages.Messages["Hash_SaltInvalid"] = software_lang.TSReadLangs("TSProtection", "tsp_hash_salt_invalid");
                TSProtectionErrorMessages.Messages["Salt_SizeInvalid"] = software_lang.TSReadLangs("TSProtection", "tsp_salt_size_invalid");
                TSProtectionErrorMessages.Messages["Random_LengthInvalid"] = software_lang.TSReadLangs("TSProtection", "tsp_random_length_invalid");
                TSProtectionErrorMessages.Messages["UnknownError"] = software_lang.TSReadLangs("TSProtection", "tsp_unknown_error");
                // SETTINGS
                settingsToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_settings");
                // THEMES
                themeToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_theme");
                lightThemeToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderThemes", "theme_light");
                darkThemeToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderThemes", "theme_dark");
                systemThemeToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderThemes", "theme_system");
                // LANGS
                languageToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_language");
                arabicToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_ar");
                chineseToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_zh");
                englishToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_en");
                dutchToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_nl");
                frenchToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_fr");
                germanToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_de");
                hindiToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_hi");
                italianToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_it");
                japaneseToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_ja");
                koreanToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_ko");
                polishToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_pl");
                portugueseToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_pt");
                russianToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_ru");
                spanishToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_es");
                turkishToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderLangs", "lang_tr");
                // STARTUP MODE
                startupToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_start");
                windowedToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderViewMode", "header_view_mode_windowed");
                fullScreenToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderViewMode", "header_view_mode_full_screen");
                // DATA TRANSFER
                dataTransferToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_data_transfer");
                exportDataToolStripMenuItem.Text = software_lang.TSReadLangs("DataTransfer", "hdt_export");
                importDataToolStripMenuItem.Text = software_lang.TSReadLangs("DataTransfer", "hdt_import");
                cSVExportFileToolStripMenuItem.Text = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_export_csv"), ts_data_backup_extension_csv_name, string.Format("*.{0}", ts_data_backup_extension_csv_name.ToLower()));
                astelExportFileToolStripMenuItem.Text = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_export_astel"), Application.ProductName, string.Format("*.{0}", Application.ProductName.ToLower()));
                astelImportDataToolStripMenuItem.Text = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_export_astel"), Application.ProductName, string.Format("*.{0}", Application.ProductName.ToLower()));
                cSVImportDataToolStripMenuItem.Text = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_export_csv"), ts_data_backup_extension_csv_name, string.Format("*.{0}", ts_data_backup_extension_csv_name.ToLower()));
                // AUTO BACKUP
                autoDataBackupToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_auto_backup");
                autoDataBackupOnToolStripMenuItem.Text = software_lang.TSReadLangs("AutoBackup", "ab_on");
                autoDataBackupOffToolStripMenuItem.Text = software_lang.TSReadLangs("AutoBackup", "ab_off");
                autoDataBackupFolderToolStripMenuItem.Text = software_lang.TSReadLangs("AutoBackup", "ab_folder");
                // SAFETY WARNINGS
                safetyWarningsToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_safety_warnings");
                safetyWarningsOnToolStripMenuItem.Text = software_lang.TSReadLangs("SafetyWarnings", "sw_on");
                safetyWarningsOffToolStripMenuItem.Text = software_lang.TSReadLangs("SafetyWarnings", "sw_off");
                // MASK PASSOWRD
                PassMaskStatusToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_password_mask");
                PMaskActiveToolStripMenuItem.Text = software_lang.TSReadLangs("PasswordMask", "pm_on");
                PMaskDisabledToolStripMenuItem.Text = software_lang.TSReadLangs("PasswordMask", "pm_off");
                // CHANGE PASSWORD
                changePasswordToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_change_password");
                // UPDATE CHECK
                checkforUpdatesToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_update");
                // PASS GEN
                passwordGeneratorToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_pass_gen");
                // DONATE
                donateToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_donate");
                // ABOUT
                aboutToolStripMenuItem.Text = software_lang.TSReadLangs("HeaderMenu", "header_menu_about");
                // HOME
                DGVColumnFormatter();
                //
                LabelService.Text = software_lang.TSReadLangs("AstelHome", "ah_label_service");
                LabelMail.Text = software_lang.TSReadLangs("AstelHome", "ah_label_mail");
                LabelPassword.Text = software_lang.TSReadLangs("AstelHome", "ah_label_password");
                LabelUrl.Text = software_lang.TSReadLangs("AstelHome", "ah_label_url");
                LabelNote.Text = software_lang.TSReadLangs("AstelHome", "ah_label_note");
                //
                MainToolTip.RemoveAll();
                MainToolTip.SetToolTip(BtnCopyEmail, software_lang.TSReadLangs("AstelHome", "ah_copy_hover"));
                MainToolTip.SetToolTip(BtnCopyPassword, software_lang.TSReadLangs("AstelHome", "ah_copy_hover"));
                MainToolTip.SetToolTip(BtnCopyUrl, software_lang.TSReadLangs("AstelHome", "ah_copy_hover"));
                MainToolTip.SetToolTip(BtnRndPssGen, software_lang.TSReadLangs("AstelHome", "ah_secure_pass_hover"));
                MainToolTip.SetToolTip(BtnOpenUrl, software_lang.TSReadLangs("AstelHome", "ah_open_url_hover"));
                //
                AddBtn.Text = " " + software_lang.TSReadLangs("AstelHome", "ah_button_add");
                UpdateBtn.Text = " " + software_lang.TSReadLangs("AstelHome", "ah_button_update");
                DeleteBtn.Text = " " + software_lang.TSReadLangs("AstelHome", "ah_button_delete");
                //
                Software_other_page_preloader();
            }catch (Exception){ }
        }
        // DGV COLUMN FORMATTER
        // ============================
        private void DGVColumnFormatter(){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            DataMainTable.Columns[1].HeaderText = software_lang.TSReadLangs("AstelHome", "ah_table_service");
            DataMainTable.Columns[2].HeaderText = software_lang.TSReadLangs("AstelHome", "ah_table_mail");
            DataMainTable.Columns[3].HeaderText = software_lang.TSReadLangs("AstelHome", "ah_table_password");
            DataMainTable.Columns[4].HeaderText = software_lang.TSReadLangs("AstelHome", "ah_table_url");
            DataMainTable.Columns[5].HeaderText = software_lang.TSReadLangs("AstelHome", "ah_table_note");
            DataMainTable.Columns[6].HeaderText = software_lang.TSReadLangs("AstelHome", "ah_table_update_date");
        }
        private void Software_other_page_preloader(){
            // CHANGE PASSWORD PAGE
            try{
                AstelChangePassword software_change_password = new AstelChangePassword();
                string software_change_password_name = "astel_change_password";
                software_change_password.Name = software_change_password_name;
                if (Application.OpenForms[software_change_password_name] != null){
                    software_change_password = (AstelChangePassword)Application.OpenForms[software_change_password_name];
                    software_change_password.Change_password_system_preloader();
                }
            }catch (Exception){ }
            // PASSWORD GENERATOR
            try{
                AstelPasswordGenerator software_pass_generator = new AstelPasswordGenerator();
                string software_pass_generator_name = "astel_password_generator";
                software_pass_generator.Name = software_pass_generator_name;
                if (Application.OpenForms[software_pass_generator_name] != null){
                    software_pass_generator = (AstelPasswordGenerator)Application.OpenForms[software_pass_generator_name];
                    software_pass_generator.Password_generator_preloader();
                }
            }catch (Exception) { }
            // SOFTWARE ABOUT
            try{
                AstelAbout software_about = new AstelAbout();
                string software_about_name = "astel_about";
                software_about.Name = software_about_name;
                if (Application.OpenForms[software_about_name] != null){
                    software_about = (AstelAbout)Application.OpenForms[software_about_name];
                    software_about.About_Preloader();
                }
            }catch (Exception){ }
        }
        // STARTUP SETINGS
        // ======================================================================================================
        private ToolStripMenuItem selected_startup_mode = null;
        private void Select_startup_mode_active(object target_startup_mode){
            if (target_startup_mode == null)
                return;
            ToolStripMenuItem clicked_startup_mode = (ToolStripMenuItem)target_startup_mode;
            if (selected_startup_mode == clicked_startup_mode)
                return;
            Select_startup_mode_deactive();
            selected_startup_mode = clicked_startup_mode;
            selected_startup_mode.Checked = true;
        }
        private void Select_startup_mode_deactive(){
            foreach (ToolStripMenuItem disabled_startup in startupToolStripMenuItem.DropDownItems){
                disabled_startup.Checked = false;
            }
        }
        private void WindowedToolStripMenuItem_Click(object sender, EventArgs e){
            if (startup_status != 0){ startup_status = 0; Startup_mode_settings("0"); Select_startup_mode_active(sender); }
        }
        private void FullScreenToolStripMenuItem_Click(object sender, EventArgs e){
            if (startup_status != 1){ startup_status = 1; Startup_mode_settings("1"); Select_startup_mode_active(sender); }
        }
        private void Startup_mode_settings(string get_startup_value){
            try{
                TSSettingsModule software_setting_save = new TSSettingsModule(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "StartupStatus", get_startup_value);
            }catch (Exception){ }
        }
        // SAFETY WARNINGS SETINGS
        // ======================================================================================================
        private ToolStripMenuItem selected_safety_mode = null;
        private void Safety_warnings_mode_active(object target_safety_mode){
            if (target_safety_mode == null)
                return;
            ToolStripMenuItem clicked_safety_mode = (ToolStripMenuItem)target_safety_mode;
            if (selected_safety_mode == clicked_safety_mode)
                return;
            Safety_warnings_mode_deactive();
            selected_safety_mode = clicked_safety_mode;
            selected_safety_mode.Checked = true;
        }
        private void Safety_warnings_mode_deactive(){
            foreach (ToolStripMenuItem disabled_safety in safetyWarningsToolStripMenuItem.DropDownItems){
                disabled_safety.Checked = false;
            }
        }
        private void SafetyWarningsOnToolStripMenuItem_Click(object sender, EventArgs e){
            if (safety_warnings_status != 1){ safety_warnings_status = 1; Safety_warnings_mode_settings("1"); Safety_warnings_mode_active(sender); }
        }
        private void SafetyWarningsOffToolStripMenuItem_Click(object sender, EventArgs e){
            if (safety_warnings_status != 0){ safety_warnings_status = 0; Safety_warnings_mode_settings("0"); Safety_warnings_mode_active(sender); }
        }
        private void Safety_warnings_mode_settings(string get_safety_warnings_value){
            try{
                TSSettingsModule software_setting_save = new TSSettingsModule(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "SafetyWarnings", get_safety_warnings_value);
            }catch (Exception){ }
        }
        // PASSWORD MASK SETTINGS
        // ======================================================================================================
        private ToolStripMenuItem selected_mask_mode = null;
        private void Password_mask_mode_active(object target_mask_mode){
            if (target_mask_mode == null)
                return;
            ToolStripMenuItem clicked_mask_mode = (ToolStripMenuItem)target_mask_mode;
            if (selected_mask_mode == clicked_mask_mode)
                return;
            Password_mask_mode_deactive();
            selected_mask_mode = clicked_mask_mode;
            selected_mask_mode.Checked = true;
        }
        private void Password_mask_mode_deactive(){
            foreach (ToolStripMenuItem disabled_mask in PassMaskStatusToolStripMenuItem.DropDownItems){
                disabled_mask.Checked = false;
            }
        }
        private void PCVMActiveToolStripMenuItem_Click(object sender, EventArgs e){
            if (password_mask_status != 1){ password_mask_status = 1; DataMainTable.Refresh(); Password_mask_mode_settings("1"); Password_mask_mode_active(sender); }
        }
        private void PCVMDisabledToolStripMenuItem_Click(object sender, EventArgs e){
            if (password_mask_status != 0){ password_mask_status = 0; DataMainTable.Refresh(); Password_mask_mode_settings("0"); Password_mask_mode_active(sender); }
        }
        private void Password_mask_mode_settings(string get_password_mask_value){
            try{
                TSSettingsModule software_setting_save = new TSSettingsModule(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "PasswordMask", get_password_mask_value);
            }catch (Exception){ }
        }
        // AUTO BACKUP SETTINGS
        // ======================================================================================================
        private ToolStripMenuItem selected_abackup_mode = null;
        private void Select_abackup_mode_active(object target_abackup_mode){
            if (target_abackup_mode == null)
                return;
            ToolStripMenuItem clicked_abackup_mode = (ToolStripMenuItem)target_abackup_mode;
            if (selected_abackup_mode == clicked_abackup_mode)
                return;
            Select_abackup_mode_deactive();
            selected_abackup_mode = clicked_abackup_mode;
            selected_abackup_mode.Checked = true;
        }
        private void Select_abackup_mode_deactive(){
            foreach (ToolStripMenuItem disabled_abackup in autoDataBackupToolStripMenuItem.DropDownItems){
                disabled_abackup.Checked = false;
            }
        }
        private void AutoDataBackupOnToolStripMenuItem_Click(object sender, EventArgs e){
            if (auto_backup_status != 1){
                auto_backup_status = 1;
                Auto_backup_mode_settings("1");
                Select_abackup_mode_active(sender);
                try{
                    if (auto_backup == null || auto_backup.IsCompleted){
                        cts = new CancellationTokenSource();
                        auto_backup = StartAutoBackup(cts.Token);
                    }
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    DialogResult open_backup_folder_query = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(software_lang.TSReadLangs("AutoBackup", "ab_info"), "\n\n", ts_data_backup_folder, "\n\n"));
                    if (open_backup_folder_query == DialogResult.Yes){
                        Backup_folder_open();
                    }
                }catch (Exception){ }
            }
        }
        private void AutoDataBackupOffToolStripMenuItem_Click(object sender, EventArgs e){
            if (auto_backup_status != 0){ auto_backup_status = 0; Auto_backup_mode_settings("0"); Select_abackup_mode_active(sender); StopAutoBackup(); } }
        private void Auto_backup_mode_settings(string get_abackup_value){
            try{
                TSSettingsModule software_setting_save = new TSSettingsModule(ts_sf);
                software_setting_save.TSWriteSettings(ts_settings_container, "AutoBackupStatus", get_abackup_value);
            }catch (Exception){ }
        }
        private void AutoDataBackupFolderToolStripMenuItem_Click(object sender, EventArgs e){
            Backup_folder_open();
        }
        private void Backup_folder_open(){
            try{
                if (Directory.Exists(ts_data_backup_folder)){
                    string folderPath = Path.GetFullPath(ts_data_backup_folder);
                    Process.Start(new ProcessStartInfo("explorer.exe", folderPath){ UseShellExecute = true });
                }else{
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("AutoBackup", "ab_not_available"));
                }
            }catch (Exception){ }
        }
        // UPDATE CHECK ENGINE
        // ======================================================================================================
        private void CheckforUpdatesToolStripMenuItem_Click(object sender, EventArgs e){
            Task.Run(() => Software_update_check(1));
        }
        public async void Software_update_check(int _check_update_ui){
            try{
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                SetUpdateMenuEnabled(false);
                if (!await IsNetworkAvailable()){
                    if (_check_update_ui == 1){
                        TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_not_connection"), "\n\n"), string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_title"), Application.ProductName));
                    }
                    return;
                }
                using (HttpClientHandler handler = new HttpClientHandler()){
                    handler.UseProxy = false;
                    using (HttpClient httpClient = new HttpClient(handler)){
                        httpClient.Timeout = TimeSpan.FromSeconds(15);
                        httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue{ NoCache = true, NoStore = true, MustRevalidate = true };
                        httpClient.DefaultRequestHeaders.Pragma.ParseAdd("no-cache");
                        string versionUrl = TS_LinkSystem.github_link_lv;
                        versionUrl += (versionUrl.Contains("?") ? "&" : "?") + "_ts=" + DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                        string response = await httpClient.GetStringAsync(versionUrl);
                        string firstLine = response.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)[0];
                        string client_version_raw = TS_VersionParser.ParseUINormalize(Application.ProductVersion);
                        string last_version_raw = TS_VersionParser.ParseUINormalize(firstLine.Split(new[] { '=' }, 2)[1].Trim());
                        Version client_ver = Version.Parse(client_version_raw);
                        Version last_ver = Version.Parse(last_version_raw);
                        if (client_ver < last_ver){
                            DialogResult info_update = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_available"), Application.ProductName, "\n\n", client_version_raw, "\n", last_version_raw, "\n\n"), string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_title"), Application.ProductName));
                            if (info_update == DialogResult.Yes){
                                try{
                                    string updaterPath = Path.Combine(Application.StartupPath, Program.updater_exe_name);
                                    if (File.Exists(updaterPath)){
                                        string procName = Path.GetFileNameWithoutExtension(updaterPath);
                                        bool isRunning = Process.GetProcessesByName(procName).Length > 0;
                                        if (!isRunning){
                                            Process.Start(new ProcessStartInfo(updaterPath) { UseShellExecute = true, Arguments = $"-app={Application.ProductName}" });
                                        }else{
                                            TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("SoftwareUpdate", "su_ts_updater_c_running"), string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_title"), Application.ProductName));
                                        }
                                        Application.Exit();
                                        return;
                                    }else{
                                        TS_MessageBoxEngine.TS_MessageBox(this, 2, string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_ts_updater_not_available"), Program.updater_exe_name), string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_title"), Application.ProductName));
                                        Process.Start(new ProcessStartInfo(TS_LinkSystem.github_link_lr) { UseShellExecute = true });
                                        Application.Exit();
                                        return;
                                    }
                                }catch (Exception ex){
                                    Debug.WriteLine(ex, $"{Program.updater_exe_name} launch block.");
                                }
                            }
                        }else if (_check_update_ui == 1){
                            string update_msg = client_ver == last_ver ? string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_not_available"), Application.ProductName, "\n", client_version_raw) : string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_newer"), "\n\n", $"v{client_version_raw}");
                            TS_MessageBoxEngine.TS_MessageBox(this, 1, update_msg, string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_title"), Application.ProductName));
                        }
                    }
                }
            }catch (Exception ex){
                Debug.WriteLine(ex, "Software_update_check()");
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_error"), "\n\n", ex.Message), string.Format(software_lang.TSReadLangs("SoftwareUpdate", "su_title"), Application.ProductName));
            }finally{
                SetUpdateMenuEnabled(true);
            }
        }
        private void SetUpdateMenuEnabled(bool enabled){
            if (InvokeRequired){
                BeginInvoke(new Action(() => checkforUpdatesToolStripMenuItem.Enabled = enabled));
            }else{
                checkforUpdatesToolStripMenuItem.Enabled = enabled;
            }
        }
        // DATA TRANSFER
        // ======================================================================================================
        // EXPORT
        // ==========================
        private void AstelExportFileToolStripMenuItem_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (!File.Exists(ts_data_xml_path)) return;
                if (BackupDataCount()){
                    using (var sfd = new SaveFileDialog()){
                        sfd.Title = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_save_location"), Application.ProductName);
                        sfd.Filter = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_save_file_name"), Application.ProductName, string.Format("(*{0})|*{1}", ts_data_backup_extension_astel, ts_data_backup_extension_astel));
                        sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        sfd.FileName = $"{Path.GetFileNameWithoutExtension(ts_data_xml_path)}_{DateTime.Now:dd.MM.yyyy_HH_mm}{ts_data_backup_extension_astel}";
                        if (sfd.ShowDialog() == DialogResult.OK){
                            File.Copy(ts_data_xml_path, sfd.FileName, true);
                            DialogResult open_export_file = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_export_success"), "\n\n", sfd.FileName.Trim(), "\n\n"));
                            if (open_export_file == DialogResult.Yes){
                                string export_file_explorer = $"/select, \"{Path.GetFullPath(sfd.FileName.Trim())}\"";
                                Process.Start(new ProcessStartInfo("explorer.exe", export_file_explorer) { UseShellExecute = true });
                            }
                        }
                    }
                }else{
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("DataTransfer", "hdt_export_not_data"));
                }
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_export_failed"), "\n", "\n\n", ex.Message));
            }
        }
        private void CSVExportFileToolStripMenuItem_Click(object sender, EventArgs e){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            try{
                if (BackupDataCount()){
                    using (var sfd = new SaveFileDialog()){
                        sfd.Title = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_save_location"), Application.ProductName);
                        sfd.Filter = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_save_file_name"), ts_data_backup_extension_csv_name, string.Format("(*{0})|*{1}", ts_data_backup_extension_csv, ts_data_backup_extension_csv));
                        sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        sfd.FileName = $"{Path.GetFileNameWithoutExtension(ts_data_xml_path)}_{DateTime.Now:dd.MM.yyyy_HH_mm}{ts_data_backup_extension_csv}";
                        if (sfd.ShowDialog() == DialogResult.OK){
                            ExportToCSV(DataMainTable, sfd.FileName);
                            DialogResult open_export_file = TS_MessageBoxEngine.TS_MessageBox(this, 5, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_export_success"), "\n\n", sfd.FileName.Trim(), "\n\n"));
                            if (open_export_file == DialogResult.Yes){
                                string export_file_explorer = $"/select, \"{Path.GetFullPath(sfd.FileName.Trim())}\"";
                                Process.Start(new ProcessStartInfo("explorer.exe", export_file_explorer) { UseShellExecute = true });
                            }
                        }
                    }
                }else{
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("DataTransfer", "hdt_export_not_data"));
                }
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_export_failed"), "\n", "\n\n", ex.Message));
            }
        }
        private bool BackupDataCount(){
            return DataMainTable.Rows.Count > 0;
        }
        private string EscapeCsv(string s){
            if (string.IsNullOrEmpty(s))
                return "";
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r")){
                s = s.Replace("\"", "\"\"");
                return $"\"{s}\"";
            }
            return s;
        }
        private string[] ParseCsvLine(string line){
            var values = new List<string>();
            var sb = new StringBuilder();
            int i = 0;
            bool inQuotes = false;
            while (i < line.Length){
                char c = line[i];
                if (c == '"'){
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"'){
                        sb.Append('"');
                        i++;
                    }else{
                        inQuotes = !inQuotes;
                    }
                }else if (c == ',' && !inQuotes){
                    values.Add(sb.ToString());
                    sb.Clear();
                }else{
                    sb.Append(c);
                }
                i++;
            }
            values.Add(sb.ToString());
            return values.ToArray();
        }
        private void ExportToCSV(DataGridView dgv, string filePath){
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("name,url,username,password,note");
            for (int i = 0; i < dgv.Rows.Count; i++){
                DataGridViewRow row = dgv.Rows[i];
                if (!row.IsNewRow){
                    string __service = row.Cells[1].Value?.ToString() ?? "";
                    string __email = row.Cells[2].Value?.ToString() ?? "";
                    string __password = row.Cells[3].Value?.ToString() ?? "";
                    string __url = row.Cells[4].Value?.ToString() ?? "";
                    string __note = row.Cells[5].Value?.ToString() ?? "";
                    string paste_line = string.Join(",",
                        EscapeCsv(__service),
                        EscapeCsv(__url),
                        EscapeCsv(__email),
                        EscapeCsv(__password),
                        EscapeCsv(__note)
                    );
                    sb.Append(paste_line);
                    if (i < dgv.Rows.Count - 1) sb.AppendLine();
                }
            }
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
        // IMPORT
        // ==========================================================================================
        private void AstelImportDataToolStripMenuItem_Click(object sender, EventArgs e){
            using (var ofd = new OpenFileDialog()){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                ofd.Title = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_location"), Application.ProductName, ts_data_backup_extension_astel);
                ofd.Filter = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_file_name"), Application.ProductName, string.Format("(*{0})|*{1}", ts_data_backup_extension_astel, ts_data_backup_extension_astel));
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (ofd.ShowDialog() == DialogResult.OK){
                    bool mergeData = false;
                    if (DataMainTable.Rows.Count > 0){
                        DialogResult result = TS_MessageBoxEngine.TS_MessageBox(this, 11, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_merge_question"), "\n\n", "\n\n"));
                        if (result != DialogResult.Yes && result != DialogResult.No){
                            return;
                        }
                        mergeData = (result == DialogResult.Yes);
                    }
                    ImportAstelFromFile(ofd.FileName, mergeData);
                }
            }
        }
        // CSV IMPORT
        // ==========================================================================================
        private async void CSVImportDataToolStripMenuItem_Click(object sender, EventArgs e){
            using (var ofd = new OpenFileDialog()){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                ofd.Title = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_location"), Application.ProductName, ts_data_backup_extension_csv);
                ofd.Filter = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_file_name"), ts_data_backup_extension_csv_name, string.Format("(*{0})|*{1}", ts_data_backup_extension_csv, ts_data_backup_extension_csv));
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (ofd.ShowDialog() == DialogResult.OK){
                    bool mergeData = false;
                    if (DataMainTable.Rows.Count > 0){
                        DialogResult result = TS_MessageBoxEngine.TS_MessageBox(this, 11, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_merge_question"), "\n\n", "\n\n"));
                        if (result != DialogResult.Yes && result != DialogResult.No){
                            return;
                        }
                        mergeData = (result == DialogResult.Yes);
                    }
                    await ImportCSVFromFile(DataMainTable, ofd.FileName, mergeData);
                }
            }
        }
        // IMPORT ASTEL
        // ==========================================================================================
        private async void ImportAstelFromFile(string filePath, bool mergeData){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            int addedCount = 0;
            int skippedCount = 0;
            int keyMismatchCount = 0;
            try{
                Text = TS_VersionEngine.TS_SoftwareVersion(0) + " - " + software_lang.TSReadLangs("AstelHome", "ah_load");
                string target_data = Path.Combine(ts_session_root_path, ts_data_file_name);
                if (!mergeData){
                    // Overwrite Mode - Validate compatibility before overwriting
                    var validateDoc = XDocument.Load(filePath);
                    var validateRoot = validateDoc.Element("Datas");
                    var firstElement = validateRoot?.Elements("Data").FirstOrDefault();
                    if (firstElement != null){
                        byte[] testKey = TS_AES_Encryption.ExtractKeyFromAstelFile(filePath);
                        if (testKey == null){
                            TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_key_extract_failed"), "\n\n"));
                            return;
                        }
                        try{
                            TS_AES_Encryption.WithTempKey(testKey, () => {
                                TS_AES_Encryption.TS_AES_Decrypt(firstElement.Element("Service")?.Value ?? "");
                                return 0;
                            });
                        }catch{
                            Array.Clear(testKey, 0, testKey.Length);
                            TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_key_mismatch"), "\n\n", 0, "\n\n", 0, "\n", 0));
                            return;
                        }
                        Array.Clear(testKey, 0, testKey.Length);
                    }
                    File.Copy(filePath, target_data, true);
                    var importDoc = XDocument.Load(filePath);
                    var importRoot = importDoc.Element("Datas");
                    if (importRoot != null){
                        addedCount = importRoot.Elements("Data").Count();
                        skippedCount = 0;
                    }
                }else{
                    // Merge Mode
                    if (!File.Exists(ts_data_xml_path)){
                        await InitializeLoaderSecurityAsync();
                    }
                    byte[] importKey = TS_AES_Encryption.ExtractKeyFromAstelFile(filePath);
                    if (importKey == null){
                        TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_key_extract_failed"), "\n\n"));
                        return;
                    }
                    var currentDoc = XDocument.Load(ts_data_xml_path);
                    var currentRoot = currentDoc.Element("Datas");
                    HashSet<string> existingKeys = new HashSet<string>();
                    foreach (var d in currentRoot.Elements("Data")){
                        try{
                            string service = TS_AES_Encryption.TS_AES_Decrypt(d.Element("Service")?.Value ?? "");
                            string email = TS_AES_Encryption.TS_AES_Decrypt(d.Element("Email")?.Value ?? "");
                            string url = TS_AES_Encryption.TS_AES_Decrypt(d.Element("Url")?.Value ?? "");
                            string key = $"{service}|{email}|{url}";
                            existingKeys.Add(key);
                        }catch (Exception){
                            // Debug.WriteLine($"Decrypt error in existing data: {ex.Message}");
                        }
                    }
                    var importDoc = XDocument.Load(filePath);
                    var importRoot = importDoc.Element("Datas");
                    int nextId = GetMaxIdFromXml() + 1;
                    foreach (var data in importRoot.Elements("Data")){
                        try{
                            string service = "", email = "", url = "", password = "", note = "", passChangeDate = "";
                            TS_AES_Encryption.WithTempKey(importKey, () =>{
                                service = TS_AES_Encryption.TS_AES_Decrypt(data.Element("Service")?.Value ?? "");
                                email = TS_AES_Encryption.TS_AES_Decrypt(data.Element("Email")?.Value ?? "");
                                url = TS_AES_Encryption.TS_AES_Decrypt(data.Element("Url")?.Value ?? "");
                                password = TS_AES_Encryption.TS_AES_Decrypt(data.Element("Password")?.Value ?? "");
                                note = TS_AES_Encryption.TS_AES_Decrypt(data.Element("Note")?.Value ?? "");
                                passChangeDate = TS_AES_Encryption.TS_AES_Decrypt(data.Element("PassChangeDate")?.Value ?? "");
                                return 0;
                            });
                            if (string.IsNullOrEmpty(passChangeDate)){
                                passChangeDate = DateTime.Now.ToString("dd.MM.yyyy - HH:mm");
                            }
                            string key = $"{service}|{email}|{url}";
                            if (existingKeys.Contains(key)){
                                skippedCount++;
                                continue;
                            }
                            existingKeys.Add(key);
                            currentRoot.Add(new XElement("Data",
                                new XElement("ID", nextId++),
                                new XElement("Service", TS_AES_Encryption.TS_AES_Encrypt(service)),
                                new XElement("Email", TS_AES_Encryption.TS_AES_Encrypt(email)),
                                new XElement("Password", TS_AES_Encryption.TS_AES_Encrypt(password)),
                                new XElement("Url", TS_AES_Encryption.TS_AES_Encrypt(url)),
                                new XElement("Note", TS_AES_Encryption.TS_AES_Encrypt(note)),
                                new XElement("PassChangeDate", TS_AES_Encryption.TS_AES_Encrypt(passChangeDate))
                            ));
                            addedCount++;
                        }catch (Exception){
                            // Debug.WriteLine($"Import error: {ex.Message}");
                            keyMismatchCount++;
                        }
                    }
                    Array.Clear(importKey, 0, importKey.Length);
                    currentDoc.Save(ts_data_xml_path);
                }
                bool fileReady = false;
                int attempts = 0;
                while (!fileReady && attempts < 10){
                    try{
                        using (var stream = File.Open(ts_data_xml_path, FileMode.Open, FileAccess.Read, FileShare.None)){
                            fileReady = true;
                        }
                    }catch (IOException){
                        attempts++;
                        Thread.Sleep(50);
                    }
                }
                TSSettingsModule software_read_settings = new TSSettingsModule(ts_session_file);
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var root = ts_xDoc.Element("Datas");
                string saved_crossLinker64 = software_read_settings.TSReadSettings(ts_session_container, "CrossLinker");
                string saved_crossLinker = string.IsNullOrEmpty(saved_crossLinker64) ? "" : TS_SessionProtection.UnprotectSessionData(saved_crossLinker64);
                root.SetAttributeValue("CL", saved_crossLinker);
                ts_xDoc.Save(ts_data_xml_path);
                //
                await InitializeLoaderSecurityAsync();
                AstelLoadXMLData();
                DataMainTable.ClearSelection();
                NodeClearInput();
                //
                string inf_message;
                if (addedCount == 0 && skippedCount == 0 && keyMismatchCount == 0){
                    inf_message = software_lang.TSReadLangs("DataTransfer", "hdt_import_no_data");
                }else if (addedCount == 0 && skippedCount > 0 && keyMismatchCount == 0){
                    inf_message = software_lang.TSReadLangs("DataTransfer", "hdt_import_duplicate_data");
                }else if (keyMismatchCount > 0){
                    inf_message = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_key_mismatch"), "\n\n", keyMismatchCount, "\n\n", addedCount, "\n", skippedCount);
                }else{
                    inf_message = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_success"), "\n\n", addedCount, "\n", skippedCount);
                }
                TS_MessageBoxEngine.TS_MessageBox(this, addedCount == 0 ? 2 : 1, inf_message);
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_failed"), "\n", "\n\n", ex.Message));
            }finally{
                Text = TS_VersionEngine.TS_SoftwareVersion(0);
            }
        }
        // IMPORT CSV
        // ==========================================================================================
        private async Task ImportCSVFromFile(DataGridView dgv, string filePath, bool mergeData){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            int addedCount = 0;
            int skippedCount = 0;
            int invalidRowCount = 0;
            try{
                if (!(dgv.DataSource is DataTable dt)){
                    return;
                }
                Text = TS_VersionEngine.TS_SoftwareVersion(0) + " - " + software_lang.TSReadLangs("AstelHome", "ah_load");
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                if (lines.Length <= 1){
                    TS_MessageBoxEngine.TS_MessageBox(this, 2, software_lang.TSReadLangs("DataTransfer", "hdt_import_empty_file"));
                    return;
                }
                if (!File.Exists(ts_data_xml_path)){
                    await InitializeLoaderSecurityAsync();
                }
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var ts_xml_root = ts_xDoc.Element("Datas");
                string existingEK = ts_xml_root.Attribute("EK")?.Value;
                string existingST = ts_xml_root.Attribute("ST")?.Value;
                string existingSV = ts_xml_root.Attribute("SV")?.Value;
                string existingCL = ts_xml_root.Attribute("CL")?.Value;
                HashSet<string> existingKeys = new HashSet<string>();
                if (!mergeData){
                    // Overwrite Mode
                    ts_xml_root.RemoveAll();
                    dt.Rows.Clear();
                }else{
                    // Merge Mode
                    foreach (DataRow r in dt.Rows){
                        string key = $"{r["Service"]}|{r["Email"]}|{r["Url"]}";
                        existingKeys.Add(key);
                    }
                }
                int nextId = mergeData ? GetMaxIdFromXml() + 1 : 1;
                int totalDataRows = lines.Length - 1;
                foreach (string line in lines.Skip(1)){
                    if (string.IsNullOrWhiteSpace(line)){
                        invalidRowCount++;
                        continue;
                    }
                    string[] values = ParseCsvLine(line);
                    if (values.Length < 4){
                        invalidRowCount++;
                        continue;
                    }
                    string __service = values[0].Trim();
                    string __url = values[1].Trim();
                    string __email = values[2].Trim();
                    string __password = values[3].Trim();
                    string __note = values.Length > 4 ? values[4].Trim() : "";
                    string __passChangeDate = DateTime.Now.ToString("dd.MM.yyyy - HH:mm");
                    if (string.IsNullOrEmpty(__service) || string.IsNullOrEmpty(__email) || string.IsNullOrEmpty(__password)){
                        invalidRowCount++;
                        continue;
                    }
                    string key = $"{__service}|{__email}|{__url}";
                    if (mergeData && existingKeys.Contains(key)){
                        skippedCount++;
                        continue;
                    }
                    existingKeys.Add(key);
                    DataRow row = dt.NewRow();
                    row["ID"] = nextId;
                    row["Service"] = __service;
                    row["Email"] = __email;
                    row["Password"] = __password;
                    row["Url"] = __url;
                    row["Note"] = __note;
                    row["PassChangeDate"] = __passChangeDate;
                    dt.Rows.Add(row);
                    ts_xml_root.Add(
                        new XElement("Data",
                            new XElement("ID", nextId),
                            new XElement("Service", TS_AES_Encryption.TS_AES_Encrypt(__service)),
                            new XElement("Email", TS_AES_Encryption.TS_AES_Encrypt(__email)),
                            new XElement("Password", TS_AES_Encryption.TS_AES_Encrypt(__password)),
                            new XElement("Url", TS_AES_Encryption.TS_AES_Encrypt(__url)),
                            new XElement("Note", TS_AES_Encryption.TS_AES_Encrypt(__note)),
                            new XElement("PassChangeDate", TS_AES_Encryption.TS_AES_Encrypt(__passChangeDate))
                        )
                    );
                    nextId++;
                    addedCount++;
                }
                if (!string.IsNullOrEmpty(existingEK))
                    ts_xml_root.SetAttributeValue("EK", existingEK);
                if (!string.IsNullOrEmpty(existingST))
                    ts_xml_root.SetAttributeValue("ST", existingST);
                if (!string.IsNullOrEmpty(existingSV))
                    ts_xml_root.SetAttributeValue("SV", existingSV);
                if (!string.IsNullOrEmpty(existingCL))
                    ts_xml_root.SetAttributeValue("CL", existingCL);
                ts_xDoc.Save(ts_data_xml_path);
                await InitializeLoaderSecurityAsync();
                AstelLoadXMLData();
                DataMainTable.ClearSelection();
                dgv.ClearSelection();
                string inf_message;
                int inf_messageType = 1;
                if (addedCount == 0 && totalDataRows > 0){
                    if (invalidRowCount == totalDataRows){
                        inf_message = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_all_rows_invalid"), totalDataRows, "\n\n", "\n\n");
                        inf_messageType = 2;
                    }else if (skippedCount > 0){
                        inf_message = software_lang.TSReadLangs("DataTransfer", "hdt_import_duplicate_data");
                        inf_messageType = 2;
                    }else{
                        inf_message = software_lang.TSReadLangs("DataTransfer", "hdt_import_no_data_added");
                        inf_messageType = 2;
                    }
                }else if (addedCount > 0){
                    if (!mergeData){
                        if (invalidRowCount > 0){
                            inf_message = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_overwrite_with_invalid"), addedCount, "\n\n", "\n\n", invalidRowCount);
                        }else{
                            inf_message = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_overwrite_success"), addedCount, "\n\n");
                        }
                    }else{
                        if (invalidRowCount > 0){
                            inf_message = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_success_with_invalid"), addedCount, "\n\n", skippedCount, "\n\n", invalidRowCount);
                        }else{
                            inf_message = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_success"), "\n\n", addedCount, "\n", skippedCount);
                        }
                    }
                }else{
                    inf_message = software_lang.TSReadLangs("DataTransfer", "hdt_import_no_changes");
                    inf_messageType = 1;
                }
                TS_MessageBoxEngine.TS_MessageBox(this, inf_messageType, inf_message);
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_failed"), "\n", "\n\n", ex.Message));
            }
            finally{
                Text = TS_VersionEngine.TS_SoftwareVersion(0);
            }
        }
        // DRAG & DROP IMPORT DATA FEATURE
        // ======================================================================================================
        private void Astel_DragEnter(object sender, DragEventArgs e){
            if (e.Data.GetDataPresent(DataFormats.FileDrop)){
                string[] astel_file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (astel_file.Length == 1 && !Directory.Exists(astel_file[0])){
                    string ext = Path.GetExtension(astel_file[0]).ToLower();
                    if (ext == ts_data_backup_extension_astel || ext == ts_data_backup_extension_csv){
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }
        private async void Astel_DragDrop(object sender, DragEventArgs e){
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var astel_file = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (astel_file.Length != 1) return;
            string ext = Path.GetExtension(astel_file[0]).ToLower();
            bool mergeData = false;
            if (DataMainTable.Rows.Count > 0){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                DialogResult result = TS_MessageBoxEngine.TS_MessageBox(this, 11, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_merge_question"), "\n\n", "\n\n"));
                if (result != DialogResult.Yes && result != DialogResult.No){
                    return;
                }
                mergeData = (result == DialogResult.Yes);
            }
            if (ext == ts_data_backup_extension_astel){
                ImportAstelFromFile(astel_file[0], mergeData);
            }else if (ext == ts_data_backup_extension_csv){
                await ImportCSVFromFile(DataMainTable, astel_file[0], mergeData);
            }
        }
        // TS TOOL LAUNCHER MODULE
        // ======================================================================================================
        private void TSToolLauncher<T>(string formName, string langKey) where T : Form, new(){
            try{
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                T tool = new T { Name = formName };
                if (Application.OpenForms[formName] == null){
                    tool.Show();
                }else{
                    if (Application.OpenForms[formName].WindowState == FormWindowState.Minimized){
                        Application.OpenForms[formName].WindowState = FormWindowState.Normal;
                    }
                    string public_message = string.Format(software_lang.TSReadLangs("HeaderHelp", "header_help_info_notification"), software_lang.TSReadLangs("HeaderMenu", langKey));
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, public_message);
                    Application.OpenForms[formName].Activate();
                }
            }catch (Exception){ }
        }
        // CHANGE PASSWORD
        // ======================================================================================================
        private void ChangePasswordToolStripMenuItem_Click(object sender, EventArgs e){
            TSToolLauncher<AstelChangePassword>("astel_change_password", "header_menu_change_password");
        }
        // PASSWORD GENERATOR
        // ======================================================================================================
        private void PasswordGeneratorToolStripMenuItem_Click(object sender, EventArgs e){
            TSToolLauncher<AstelPasswordGenerator>("astel_password_generator", "header_menu_pass_gen");
        }
        // DONATE LINK
        // ======================================================================================================
        private void DonateToolStripMenuItem_Click(object sender, EventArgs e){
            try{
                Process.Start(new ProcessStartInfo(TS_LinkSystem.ts_donate){ UseShellExecute = true });
            }catch (Exception){ }
        }
        // ABOUT PAGE
        // ======================================================================================================
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e){
            TSToolLauncher<AstelAbout>("astel_about", "header_menu_about");
        }
        // EXIT
        // ======================================================================================================
        private void StopAutoBackup(){
            try{
                if (cts != null){
                    cts.Cancel();
                    try{
                        auto_backup?.Wait(5000);
                    }
                    catch (AggregateException) { }
                    catch (TaskCanceledException) { }
                    auto_backup = null;
                    cts.Dispose();
                    cts = null;
                }
            }
            catch (ObjectDisposedException) { }
            catch (Exception){ }
        }
        private void Astel_FormClosing(object sender, FormClosingEventArgs e){
            StopAutoBackup();
            Application.Exit();
        }
    }
}