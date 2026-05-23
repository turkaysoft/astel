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
            CheckForIllegalCrossThreadCalls = false;
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
        }
        // GLOBAL VARIABLES
        // ======================================================================================================
        public static string lang, lang_path;
        public static int theme, themeSystem, startup_status, auto_backup_status, safety_warnings_status;
        // LOCAL VARIABLES
        // ======================================================================================================
        Task auto_backup;
        private CancellationTokenSource cts;
        private bool suppressComboBoxEvent = false;
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
            DataMainTable.RowTemplate.Height = (int)(26 * this.DeviceDpi / 96f);
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
            // THEME - LANG - VIEW MODE PRELOADER
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
        }
        // MAIN TOOLTIP SETTINGS
        // ======================================================================================================
        private void MainToolTip_Draw(object sender, DrawToolTipEventArgs e){ e.DrawBackground(); e.DrawBorder(); e.DrawText(); }
        // LOAD
        // ======================================================================================================
        private async void Astel_Load(object sender, EventArgs e){
            // PREFETCH
            ServiceListAdd();
            RunSoftwareEngine();
            //
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            Text = TS_VersionEngine.TS_SofwareVersion(0) + " - " + software_lang.TSReadLangs("AstelHome", "ah_load");
            HeaderMenu.Cursor = Cursors.Hand;
            // TEMPORARY COLUMN CLEAR
            DataMainTable.Columns.Clear();
            // LOGIN SECURITY
            await InitializeLoaderSecurityAsync();
            // LOAD
            AstelLoadXMLData();
            DGVColumnFormatter();
            //
            Text = TS_VersionEngine.TS_SofwareVersion(0);
            //
            DataMainTable.Columns[0].Width = (int)(50 * this.DeviceDpi / 96f);
            DataMainTable.Columns[1].Width = (int)(100 * this.DeviceDpi / 96f);
            DataMainTable.Columns[2].Width = (int)(140 * this.DeviceDpi / 96f);
            DataMainTable.Columns[3].Width = (int)(140 * this.DeviceDpi / 96f);
            DataMainTable.Columns[4].Width = (int)(140 * this.DeviceDpi / 96f);
            DataMainTable.Columns[5].Width = (int)(140 * this.DeviceDpi / 96f);
            DataMainTable.Columns[6].Width = (int)(160 * this.DeviceDpi / 96f);
            foreach (DataGridViewColumn columnPadding in DataMainTable.Columns){
                int scaledPadding = (int)(3 * this.DeviceDpi / 96f);
                columnPadding.DefaultCellStyle.Padding = new Padding(scaledPadding, 0, 0, 0);
            }
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
                "Facebook", "Instagram", "Threads", "Twitter (X)", "LinkedIn", "Reddit",
                "Pinterest", "Quora", "Medium", "TikTok", "Snapchat",
                "WhatsApp", "Telegram", "Signal", "Viber", "WeChat", "LINE",
                "Discord", "Slack",
                // Entertainment / Media
                "Netflix", "Spotify", "Disney+", "HBO Max", "Paramount+", "Peacock",
                "Twitch", "Kick", "SoundCloud", "Deezer", "Crunchyroll",
                // Game
                "Steam", "Epic Games", "Roblox", "PlayStation Network", "Battle.net", "Ubisoft", "EA Play",
                // Shopping / Finance (Global)
                "Amazon", "eBay", "Etsy", "Shopify",
                "PayPal", "Stripe", "Venmo", "Wise", "Revolut",
                // Productivity / Cloud – Primary Identities
                "Google", "Apple", "Microsoft", "Samsung", "Xiaomi",
                // Independent Cloud / Productivity
                "Dropbox", "MEGA", "Notion", "Trello", "Asana", "ClickUp",
                "Monday.com", "Miro", "Zoom",
                // Developer
                "GitHub", "GitLab", "Bitbucket", "Stack Overflow",
                // Travel / Transportation (Global)
                "Uber", "Bolt", "Airbnb"
            };
            string[] turkeyServices = new string[]{
                // Turkey – E-Commerce / Service
                "Trendyol", "Hepsiburada", "n11", "Yemeksepeti", "Getir", "Migros Hemen",
                "BİM Online", "A101 Kapıda",
                "Sahibinden", "Letgo", "PTT", "PTTAVM",
                // Turkey – Transportation / Tickets
                "Biletix", "Passo", "Obilet", "TCDD",
                "THY", "Pegasus", "AJet", "SunExpress",
                "İSPARK",
                // Turkey – Finance / Banks
                "Ziraat Bankası", "Ziraat Katılım", "İş Bankası", "Garanti BBVA", "Yapı Kredi",
                "Akbank", "QNB Finansbank", "DenizBank", "TEB", "ING Bank", "Halkbank",
                "VakıfBank", "Şekerbank", "AnadoluBank", "Fibabanka", "Burgan Bank",
                "Odeabank", "Alternatif Bank", "HSBC Türkiye",
                "Kuveyt Türk", "Albaraka Türk", "Türkiye Finans",
                "Papara", "Enpara", "İninal", "Paycell",
                // Turkey – Government / Operator
                "E-Devlet", "E-Nabız", "MHRS",
                "Turkcell", "Vodafone", "Türk Telekom",
                "İstanbulkart"
            };
            var services_main = new List<string>(globalServices);
            var get_region = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            if (get_region.TwoLetterISORegionName == "TR"){ services_main.AddRange(turkeyServices); }
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
                    byte[] aesKey;
                    using (var kdf = new Rfc2898DeriveBytes(keyMaterial, salt, 210_000, HashAlgorithmName.SHA512)){
                        aesKey = kdf.GetBytes(32);
                    }
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
            root.SetAttributeValue("SV", TS_VersionEngine.TS_SofwareVersion(1));
            string saved_crossLinker64 = software_read_settings.TSReadSettings(ts_session_container, "CrossLinker").Trim();
            string crossLinker64 = root.Attribute("CL")?.Value.Trim();
            if (!string.IsNullOrEmpty(crossLinker64)){
                if (crossLinker64 != saved_crossLinker64){
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
                root.SetAttributeValue("CL", saved_crossLinker64);
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
                string newVersion = TS_VersionEngine.TS_SofwareVersion(1);
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
            }
            ts_dataSet.Tables.Add(ts_dataTable);
            DataMainTable.DataSource = ts_dataSet.Tables[0];
            DataMainTable.ClearSelection();
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
        }
        // UPDATE DATA
        // ======================================================================================================
        private async void UpdateBtn_Click(object sender, EventArgs e){
            await ProgressData(true);
        }
        // VALIDATE AND PROGRESS FUNCTIONS
        // ======================================================================================================
        private bool ValidateInputs(out string in_service, out string in_email, out string in_password, out string in_url, out string in_note, out string errorMsg){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            in_service = string.Join(" ", TxtService.Text.Split(' ').Select(k => string.IsNullOrWhiteSpace(k) ? "" : char.ToUpper(k[0]) + k.Substring(1).ToLower()));
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
                { software_lang.TSReadLangs("SafetyWarningsPassword", "swp_pass_req_5"), Regex.IsMatch(password, "[!@#$%^&*(),.?\":{}|<>]") }
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
                if (isUpdate){
                    var confirm = TS_MessageBoxEngine.TS_MessageBox(this, 4, software_lang.TSReadLangs("AstelHome", "ah_update_question_info"));
                    if (confirm != DialogResult.Yes)
                        return;
                }
                //
                if (!File.Exists(ts_data_xml_path)) await InitializeLoaderSecurityAsync();
                //
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var ts_xml_root = ts_xDoc.Element("Datas");
                //
                if (isUpdate){
                    int selectedId = int.Parse(DataMainTable.SelectedRows[0].Cells["ID"].Value.ToString());
                    var elementToUpdate = ts_xml_root.Elements("Data").FirstOrDefault(x => (int)x.Element("ID") == selectedId);
                    //
                    if (elementToUpdate != null){
                        elementToUpdate.SetElementValue("Service", TS_AES_Encryption.TS_AES_Encrypt(in_service));
                        elementToUpdate.SetElementValue("Email", TS_AES_Encryption.TS_AES_Encrypt(in_email));
                        elementToUpdate.SetElementValue("Password", TS_AES_Encryption.TS_AES_Encrypt(in_password));
                        elementToUpdate.SetElementValue("Url", TS_AES_Encryption.TS_AES_Encrypt(in_url));
                        elementToUpdate.SetElementValue("Note", TS_AES_Encryption.TS_AES_Encrypt(in_note));
                        elementToUpdate.SetElementValue("PassChangeDate", TS_AES_Encryption.TS_AES_Encrypt(DateTime.Now.ToString("dd.MM.yyyy - HH:mm")));
                    }
                }else{
                    ts_xml_root.Add(new XElement("Data",
                        new XElement("ID", TSGenerateNewID()),
                        new XElement("Service", TS_AES_Encryption.TS_AES_Encrypt(in_service)),
                        new XElement("Email", TS_AES_Encryption.TS_AES_Encrypt(in_email)),
                        new XElement("Password", TS_AES_Encryption.TS_AES_Encrypt(in_password)),
                        new XElement("Url", TS_AES_Encryption.TS_AES_Encrypt(in_url)),
                        new XElement("Note", TS_AES_Encryption.TS_AES_Encrypt(in_note)),
                        new XElement("PassChangeDate", TS_AES_Encryption.TS_AES_Encrypt(DateTime.Now.ToString("dd.MM.yyyy - HH:mm")))
                    ));
                }
                //
                ts_xDoc.Save(ts_data_xml_path);
                AstelLoadXMLData();
                NodeClearInput();
                //
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
                DialogResult checkDeleteQuery = TS_MessageBoxEngine.TS_MessageBox(this, 4, software_lang.TSReadLangs("AstelHome", "ah_delete_question_info"));
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
                    //
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", "ah_delete_success"));
                }
            }catch (Exception){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("AstelHome", "ah_delete_failed"), "\n"));
            }
        }
        // COPY DATA
        // ======================================================================================================
        private void BtnCopyEmail_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtEmail.Text.Trim())){
                    Clipboard.SetText(TxtEmail.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", "ah_copy_email"));
                }
            }catch (Exception){ }
        }
        private void BtnCopyPassword_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtPassword.Text.Trim())){
                    Clipboard.SetText(TxtPassword.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", "ah_copy_password"));
                }
            }catch (Exception){ }
        }
        private void BtnCopyUrl_Click(object sender, EventArgs e){
            try{
                if (!string.IsNullOrEmpty(TxtUrl.Text.Trim())){
                    Clipboard.SetText(TxtUrl.Text.Trim());
                    TSGetLangs software_lang = new TSGetLangs(lang_path);
                    TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("AstelHome", "ah_copy_url"));
                }
            }catch (Exception){ }
        }
        // RANDOM PASSWORD GENERATOR
        // ======================================================================================================
        private readonly Random rnd_pass = new Random();
        private void BtnRndPssGen_Click(object sender, EventArgs e){
            GenerateRandomPassword();
        }
        private void GenerateRandomPassword(){
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lower = "abcdefghijklmnopqrstuvwxyz";
            string digits = "0123456789";
            string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";
            var initialChars = new[] {
                upper[rnd_pass.Next(upper.Length)],
                lower[rnd_pass.Next(lower.Length)],
                digits[rnd_pass.Next(digits.Length)],
                symbols[rnd_pass.Next(symbols.Length)]
            }.ToList();
            string allChars = upper + lower + digits + symbols;
            initialChars.AddRange(Enumerable.Range(0, rnd_pass.Next(10, 18) - initialChars.Count).Select(_ => allChars[rnd_pass.Next(allChars.Length)]));
            for (int i = initialChars.Count - 1; i > 0; i--){
                int j = rnd_pass.Next(i + 1);
                (initialChars[j], initialChars[i]) = (initialChars[i], initialChars[j]);
            }
            TxtPassword.Text = new string(initialChars.ToArray());
        }
        // OPEN URL TO BROWSER
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
                    DataGridViewRow xml_select_row = DataMainTable.Rows[e.RowIndex];
                    //
                    TxtService.Text = xml_select_row.Cells[1].Value.ToString();
                    TxtEmail.Text = xml_select_row.Cells[2].Value.ToString();
                    TxtPassword.Text = xml_select_row.Cells[3].Value.ToString();
                    TxtUrl.Text = xml_select_row.Cells[4].Value.ToString();
                    TxtNote.Text = xml_select_row.Cells[5].Value.ToString();
                }
            }catch (Exception){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("AstelHome", "ah_select_failed"), "\n"));
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
        // CLEAR INPUT
        // ======================================================================================================
        private void NodeClearInput(){
            TxtService.Clear();
            TxtPassword.Clear();
            TxtEmail.Clear();
            TxtUrl.Clear();
            TxtNote.Clear();
            DataMainTable.ClearSelection();
        }
        // ======================================================================================================
        // AUTO BACKUP DATA
        private async Task StartAutoBackup(CancellationToken token){
            while (!token.IsCancellationRequested){
                if (DataMainTable.Rows.Count > 0){
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
                        string backupFileName = $"{Path.GetFileNameWithoutExtension(ts_data_xml_path)}_{DateTime.Now:ddMMyyyy_HHmm}_{GenerateSecureRandomString(7).Substring(3)}{ts_data_backup_extension_astel}";
                        string backupFilePath = Path.Combine(ts_data_backup_folder, backupFileName);
                        try{
                            if (!Directory.Exists(ts_data_backup_folder)){
                                Directory.CreateDirectory(ts_data_backup_folder);
                            }
                            File.Copy(ts_data_xml_path, backupFilePath, overwrite: true);
                        }catch (Exception){ }
                    }
                }
                try{
                    await Task.Delay(60000, token);
                }catch (TaskCanceledException){
                    break;
                }
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
                header_colors[0] = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor");
                header_colors[1] = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor");
                header_colors[2] = TS_ThemeEngine.ColorMode(theme, "AccentColor");
                HeaderMenu.Renderer = new HeaderMenuColors();
                // TOOLTIP
                MainToolTip.ForeColor = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                MainToolTip.BackColor = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                // HEADER MENU
                var bg = TS_ThemeEngine.ColorMode(theme, "HeaderBGColor2");
                var fg = TS_ThemeEngine.ColorMode(theme, "HeaderFEColor2");
                HeaderMenu.ForeColor = fg;
                HeaderMenu.BackColor = bg;
                SetMenuStripColors(HeaderMenu, bg, fg);
                // CONTENT BG
                BackColor = TS_ThemeEngine.ColorMode(theme, "PageContainerBGColor");
                // ALL LABEL
                foreach (Control control in Panel_Footer.Controls){
                    if (control is Label label){
                        label.ForeColor = TS_ThemeEngine.ColorMode(theme, "ContentLabelLeftColor");
                    }
                }
                // ALL TEXTBOX
                foreach (Control control in Panel_Footer.Controls){
                    if (control is TextBox textbox){
                        textbox.BackColor = TS_ThemeEngine.ColorMode(theme, "TextboxBGColor");
                        textbox.ForeColor = TS_ThemeEngine.ColorMode(theme, "TextboxFEColor");
                    }
                }
                // ALL BUTTON
                var combinedBtnsControls = FLP_Btns.Controls.Cast<Control>().Concat(Panel_Footer.Controls.Cast<Control>());
                foreach (Control control in combinedBtnsControls){
                    if (control is Button button){
                        button.ForeColor = TS_ThemeEngine.ColorMode(theme, "DynamicThemeActiveBtnBGColor");
                        button.BackColor = TS_ThemeEngine.ColorMode(theme, "AccentColor");
                        button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(theme, "AccentColor");
                        button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(theme, "AccentColor");
                        button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(theme, "AccentColorHover");
                    }
                }
                CmbService.BackColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBGColor");
                CmbService.ForeColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxFEColor");
                CmbService.HoverBackColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBGColor");
                CmbService.ButtonColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBGColor2");
                CmbService.ArrowColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxFEColor");
                CmbService.HoverButtonColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBGColor2");
                CmbService.BorderColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBorderColor");
                CmbService.FocusedBorderColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBorderColor");
                CmbService.HoverForeColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxFEColor");
                CmbService.SelectedBackColor = TS_ThemeEngine.ColorMode(theme, "AccentColor");
                CmbService.SelectedForeColor = TS_ThemeEngine.ColorMode(theme, "SelectBoxBGColor");
                // DATA TABLE
                DataMainTable.BackgroundColor = TS_ThemeEngine.ColorMode(theme, "DataGridBGColor");
                DataMainTable.GridColor = TS_ThemeEngine.ColorMode(theme, "DataGridGridColor");
                DataMainTable.DefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(theme, "DataGridBGColor");
                DataMainTable.DefaultCellStyle.ForeColor = TS_ThemeEngine.ColorMode(theme, "DataGridFEColor");
                DataMainTable.AlternatingRowsDefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(theme, "DataGridAlternatingColor");
                DataMainTable.ColumnHeadersDefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderBGColor");
                DataMainTable.ColumnHeadersDefaultCellStyle.SelectionBackColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderBGColor");
                DataMainTable.ColumnHeadersDefaultCellStyle.ForeColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderFEColor");
                DataMainTable.DefaultCellStyle.SelectionBackColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderBGColor");
                DataMainTable.DefaultCellStyle.SelectionForeColor = TS_ThemeEngine.ColorMode(theme, "DataGridHeaderFEColor");
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
        private void Select_lang_active(object target_lang){
            ToolStripMenuItem selected_lang = null;
            Select_lang_deactive();
            if (target_lang != null){
                if (selected_lang != (ToolStripMenuItem)target_lang){
                    selected_lang = (ToolStripMenuItem)target_lang;
                    selected_lang.Checked = true;
                }
            }
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
            }catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                    software_about.About_preloader();
                }
            }catch (Exception){ }
        }
        // STARTUP SETINGS
        // ======================================================================================================
        private void Select_startup_mode_active(object target_startup_mode){
            ToolStripMenuItem selected_startup_mode = null;
            Select_startup_mode_deactive();
            if (target_startup_mode != null){
                if (selected_startup_mode != (ToolStripMenuItem)target_startup_mode){
                    selected_startup_mode = (ToolStripMenuItem)target_startup_mode;
                    selected_startup_mode.Checked = true;
                }
            }
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
        private void Safety_warnings_mode_active(object target_safety_mode){
            ToolStripMenuItem selected_safety_mode = null;
            Safety_warnings_mode_deactive();
            if (target_safety_mode != null){
                if (selected_safety_mode != (ToolStripMenuItem)target_safety_mode){
                    selected_safety_mode = (ToolStripMenuItem)target_safety_mode;
                    selected_safety_mode.Checked = true;
                }
            }
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
        // AUTO BACKUP SETTINGS
        // ======================================================================================================
        private void Select_abackup_mode_active(object target_abackup_mode){
            ToolStripMenuItem selected_abackup_mode = null;
            Select_abackup_mode_deactive();
            if (target_abackup_mode != null){
                if (selected_abackup_mode != (ToolStripMenuItem)target_abackup_mode){
                    selected_abackup_mode = (ToolStripMenuItem)target_abackup_mode;
                    selected_abackup_mode.Checked = true;
                }
            }
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
            if (auto_backup_status != 0){ auto_backup_status = 0; Auto_backup_mode_settings("0"); Select_abackup_mode_active(sender); StopAutoBackup(); }
            }
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
                        httpClient.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue{ NoCache = true, NoStore = true, MustRevalidate = true };
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
            if (string.IsNullOrEmpty(s)) return "";
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n")){
                s = s.Replace("\"", "\"\"");
                return $"\"{s}\"";
            }
            return s;
        }
        private string[] ParseCsvLine(string line){
            var values = new List<string>();
            int i = 0;
            var sb = new StringBuilder();
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
            foreach (DataGridViewRow row in dgv.Rows){
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
                    sb.AppendLine(paste_line);
                }
            }
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
        // IMPORT 
        // ==========================
        private void AstelImportDataToolStripMenuItem_Click(object sender, EventArgs e){
            using (var ofd = new OpenFileDialog()){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                ofd.Title = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_location"), Application.ProductName, ts_data_backup_extension_astel);
                ofd.Filter = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_file_name"), Application.ProductName, string.Format("(*{0})|*{1}", ts_data_backup_extension_astel, ts_data_backup_extension_astel));
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (ofd.ShowDialog() == DialogResult.OK){
                    ImportAstelFromFile(ofd.FileName);
                }
            }
        }
        private async void CSVImportDataToolStripMenuItem_Click(object sender, EventArgs e){
            using (var ofd = new OpenFileDialog()){
                TSGetLangs software_lang = new TSGetLangs(lang_path);
                ofd.Title = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_location"), Application.ProductName, ts_data_backup_extension_csv);
                ofd.Filter = string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_file_name"), ts_data_backup_extension_csv_name, string.Format("(*{0})|*{1}", ts_data_backup_extension_csv, ts_data_backup_extension_csv));
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (ofd.ShowDialog() == DialogResult.OK){
                   await ImportCSVFromFile(DataMainTable, ofd.FileName);
                }
            }
        }
        private async void ImportAstelFromFile(string filePath){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            if (DataMainTable.Rows.Count > 0){
                DialogResult import_warning = TS_MessageBoxEngine.TS_MessageBox(this, 6, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_warning"), "\n\n", "\n\n"));
                if (import_warning != DialogResult.Yes) return;
            }
            try{
                Text = TS_VersionEngine.TS_SofwareVersion(0) + " - " + software_lang.TSReadLangs("AstelHome", "ah_load");
                //
                string target_data = Path.Combine(ts_session_root_path, "AstelData.xml");
                File.Copy(filePath, target_data, true);
                //
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
                //
                TSSettingsModule software_read_settings = new TSSettingsModule(ts_session_file);
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var root = ts_xDoc.Element("Datas");
                string saved_crossLinker64 = software_read_settings.TSReadSettings(ts_session_container, "CrossLinker");
                root.SetAttributeValue("CL", saved_crossLinker64);
                ts_xDoc.Save(ts_data_xml_path);
                //
                await InitializeLoaderSecurityAsync();
                AstelLoadXMLData();
                NodeClearInput();
                //
                TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("DataTransfer", "hdt_import_success"));
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_failed"), "\n", "\n\n", ex.Message));
            }finally{
                Text = TS_VersionEngine.TS_SofwareVersion(0);
            }
        }
        private async Task ImportCSVFromFile(DataGridView dgv, string filePath){
            TSGetLangs software_lang = new TSGetLangs(lang_path);
            if (DataMainTable.Rows.Count > 0){
                DialogResult import_warning = TS_MessageBoxEngine.TS_MessageBox(this, 6, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_warning"), "\n\n", "\n\n"));
                if (import_warning != DialogResult.Yes) return;
            }
            try{
                if (!(dgv.DataSource is DataTable dt)){
                    return;
                }
                //
                Text = TS_VersionEngine.TS_SofwareVersion(0) + " - " + software_lang.TSReadLangs("AstelHome", "ah_load");
                //
                dt.Rows.Clear();
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                if (lines.Length <= 1) return;
                if (!File.Exists(ts_data_xml_path)) await InitializeLoaderSecurityAsync();
                var ts_xDoc = XDocument.Load(ts_data_xml_path);
                var ts_xml_root = ts_xDoc.Element("Datas");
                int currentMaxId = GetMaxIdFromXml();
                int nextId = currentMaxId + 1;
                foreach (string line in lines.Skip(1)){
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] values = ParseCsvLine(line);
                    if (values.Length >= 4){
                        string __service = values[0].Trim();
                        string __url = values[1].Trim();
                        string __email = values[2].Trim();
                        string __password = values[3].Trim();
                        string __note = values.Length > 4 ? values[4].Trim() : "";
                        string __passChangeDate = DateTime.Now.ToString("dd.MM.yyyy - HH:mm");
                        //
                        if (string.IsNullOrEmpty(__service) || string.IsNullOrEmpty(__email) || string.IsNullOrEmpty(__password)){
                            continue;
                        }
                        //
                        DataRow row = dt.NewRow();
                        row["ID"] = nextId;
                        row["Service"] = __service;
                        row["Email"] = __email;
                        row["Password"] = __password;
                        row["Url"] = __url;
                        row["Note"] = __note;
                        row["PassChangeDate"] = __passChangeDate;
                        dt.Rows.Add(row);
                        //
                        ts_xml_root.Add(new XElement("Data",
                            new XElement("ID", nextId),
                            new XElement("Service", TS_AES_Encryption.TS_AES_Encrypt(__service)),
                            new XElement("Email", TS_AES_Encryption.TS_AES_Encrypt(__email)),
                            new XElement("Password", TS_AES_Encryption.TS_AES_Encrypt(__password)),
                            new XElement("Url", TS_AES_Encryption.TS_AES_Encrypt(__url)),
                            new XElement("Note", TS_AES_Encryption.TS_AES_Encrypt(__note)),
                            new XElement("PassChangeDate", TS_AES_Encryption.TS_AES_Encrypt(__passChangeDate))
                        ));
                        nextId++;
                    }
                }
                ts_xDoc.Save(ts_data_xml_path);
                AstelLoadXMLData();
                dgv.ClearSelection();
                TS_MessageBoxEngine.TS_MessageBox(this, 1, software_lang.TSReadLangs("DataTransfer", "hdt_import_success"));
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(this, 3, string.Format(software_lang.TSReadLangs("DataTransfer", "hdt_import_failed"), "\n", "\n\n", ex.Message));
            }finally{
                Text = TS_VersionEngine.TS_SofwareVersion(0);
            }
        }
        // DRAG & DROP IMPORT DATA FEATURE
        // ==========================
        private void Astel_DragEnter(object sender, DragEventArgs e){
            if (e.Data.GetDataPresent(DataFormats.FileDrop)){
                string[] astel_file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (astel_file.Length == 1 && !Directory.Exists(astel_file[0])){
                    if (Path.GetExtension(astel_file[0]).ToLower() == ts_data_backup_extension_astel || Path.GetExtension(astel_file[0]).ToLower() == ts_data_backup_extension_csv){
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }
        private async void Astel_DragDrop(object sender, DragEventArgs e){
            if (e.Data.GetDataPresent(DataFormats.FileDrop)){
                var astel_file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (astel_file.Length == 1 && Path.GetExtension(astel_file[0]).ToLower() == ts_data_backup_extension_astel){
                    ImportAstelFromFile(astel_file[0]);
                }else if (astel_file.Length == 1 && Path.GetExtension(astel_file[0]).ToLower() == ts_data_backup_extension_csv){
                    await ImportCSVFromFile(DataMainTable, astel_file[0]);
                }
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
                    auto_backup?.Wait();
                    auto_backup = null;
                    cts.Dispose();
                    cts = null;
                }
            }catch (Exception){ }
        }
        private void Astel_FormClosing(object sender, FormClosingEventArgs e){
            StopAutoBackup();
            Application.Exit();
        }
    }
}