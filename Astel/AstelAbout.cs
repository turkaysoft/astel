using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using static Astel.TSModules;

namespace Astel{
    public partial class AstelAbout : Form{
        public AstelAbout(){
            InitializeComponent();
            //
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, AboutTable, new object[] { true });
            //
            AboutTable.Columns.Add("LangName", "Language");
            AboutTable.Columns.Add("LangTranslator", "Translator");
            AboutTable.AllowUserToResizeColumns = false;
            foreach (DataGridViewColumn A_Column in AboutTable.Columns) { A_Column.SortMode = DataGridViewColumnSortMode.NotSortable; }
            ApplyDpiScaling(this.DeviceDpi);
        }
        // UI DPI (shared standard: TSDpiHelper)
        // ======================================================================================================
        private void ApplyDpiScaling(int dpi){
            if (IsDisposed || Disposing) return;
            if (dpi <= 0) dpi = TSDpiHelper.BaseDpi;
            TSDpiHelper.ScaleDataGridView(AboutTable, new[] { 110 }, 28, dpiOverride: dpi);
        }
        protected override void OnDpiChanged(DpiChangedEventArgs e){
            base.OnDpiChanged(e);
            try{
                ApplyDpiScaling(e.DeviceDpiNew);
                About_Preloader();
                this.PerformLayout();
                this.Invalidate(true);
            }catch (Exception){ }
        }
        // ABOUT LOAD
        // ======================================================================================================
        private async void AstelAbout_Load(object sender, EventArgs e){
            try{
                ApplyDpiScaling(this.DeviceDpi);
                LabelDeveloper.Text = Application.CompanyName;
                LabelSoftware.Text = Application.ProductName;
                LabelVersion.Text = TS_VersionEngine.TS_SoftwareVersion(1);
                LabelCopyright.Text = TS_SoftwareCopyrightDate.ts_scd_preloader;
                // GET PRELOAD SETTINGS
                About_Preloader();
                //
                await Task.Run(() => LoadLanguageConverterName());
            }catch (Exception){ }
        }
        private void LoadLanguageConverterName(){
            foreach (var available_lang_file in AvailableLanguages){
                TSSettingsModule software_read_settings = new TSSettingsModule(available_lang_file);
                string[] get_name = software_read_settings.TSReadSettings("Main", "lang_name").Split('/');
                string get_lang_translator = software_read_settings.TSReadSettings("Main", "translator");
                AboutTable.BeginInvoke((Action)(() => {
                    AboutTable.Rows.Add(get_name[0].Trim(), get_lang_translator.Trim());
                }));
            }
            AboutTable.BeginInvoke((Action)(() => AboutTable.ClearSelection()));
        }
        // DYNAMIC THEME VOID
        // ======================================================================================================
        public void About_Preloader(){
            try{
                TSThemeModeHelper.InitializeThemeForForm(this);
                //
                BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                PanelTxt.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                //
                LabelDeveloper.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1");
                LabelSoftware.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                LabelVersion.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor2");
                LabelCopyright.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor2");
                //
                foreach (Control ui_buttons in PanelTxt.Controls){
                    if (ui_buttons is Button about_button){
                        about_button.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "DynamicThemeActiveBtnBGColor");
                        about_button.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                        about_button.FlatAppearance.BorderColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                        about_button.FlatAppearance.MouseDownBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                        about_button.FlatAppearance.MouseOverBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "AccentColorHover");
                    }
                }
                //
                TSImageRenderer(About_WebsiteBtn, AstelMain.theme == 1 ? Properties.Resources.ct_website_light : Properties.Resources.ct_website_dark, 18, ContentAlignment.MiddleRight);
                TSImageRenderer(About_GitHubBtn, AstelMain.theme == 1 ? Properties.Resources.ct_github_light : Properties.Resources.ct_github_dark, 18, ContentAlignment.MiddleRight);
                TSImageRenderer(About_DonateBtn, AstelMain.theme == 1 ? Properties.Resources.tm_donate_mc_light : Properties.Resources.tm_donate_mc_dark, 18, ContentAlignment.MiddleRight);
                //
                AboutTable.BackgroundColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                AboutTable.GridColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "SelectBoxBorderColor");
                AboutTable.DefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor2");
                AboutTable.DefaultCellStyle.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_LabelColor1");
                AboutTable.AlternatingRowsDefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_BGColor");
                AboutTable.ColumnHeadersDefaultCellStyle.BackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                AboutTable.ColumnHeadersDefaultCellStyle.SelectionBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                AboutTable.ColumnHeadersDefaultCellStyle.ForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "DynamicThemeActiveBtnBGColor");
                AboutTable.DefaultCellStyle.SelectionBackColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "TSBT_AccentColor");
                AboutTable.DefaultCellStyle.SelectionForeColor = TS_ThemeEngine.ColorMode(AstelMain.theme, "DynamicThemeActiveBtnBGColor");
                // ======================================================================================================
                // TEXTS
                TSGetLangs software_lang = new TSGetLangs(AstelMain.lang_path);
                Text = string.Format(software_lang.TSReadLangs("SoftwareAbout", "sa_title"), Application.ProductName);
                About_WebsiteBtn.Text = " " + software_lang.TSReadLangs("SoftwareAbout", "sa_website_link");
                About_GitHubBtn.Text = " " + software_lang.TSReadLangs("SoftwareAbout", "sa_github_link");
                About_DonateBtn.Text = " " + software_lang.TSReadLangs("SoftwareAbout", "sa_donate_link");
                //
                AboutTable.Columns[0].HeaderText = software_lang.TSReadLangs("SoftwareAbout", "sa_lang_name");
                AboutTable.Columns[1].HeaderText = software_lang.TSReadLangs("SoftwareAbout", "sa_lang_translator");
            }catch (Exception){ }
        }
        // DGV CLEAR SELECTION
        // ======================================================================================================
        private void AboutTable_SelectionChanged(object sender, EventArgs e){ AboutTable.ClearSelection(); }
        // WEBSITE LINK
        // ======================================================================================================
        private void About_WebsiteBtn_Click(object sender, EventArgs e){
            try{
                Process.Start(new ProcessStartInfo(TS_LinkSystem.website_link){ UseShellExecute = true });
            }catch (Exception){ }
        }
        // GITHUB LINK
        // ======================================================================================================
        private void About_GitHubBtn_Click(object sender, EventArgs e){
            try{
                Process.Start(new ProcessStartInfo(TS_LinkSystem.github_link){ UseShellExecute = true });
            }catch (Exception){ }
        }
        // DONATE LINK
        // ======================================================================================================
        private void About_DonateBtn_Click(object sender, EventArgs e){
            try{
                Process.Start(new ProcessStartInfo(TS_LinkSystem.ts_donate){ UseShellExecute = true });
            }catch (Exception){ }
        }
    }
}