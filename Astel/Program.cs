using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
// TS Modules
using Astel.astel_modules;
using static Astel.TSModules;
using static Astel.TSSecureModule;

namespace Astel{
    internal static class Program{
        // ======================================================================================================
        // GLOBAL SYSTEM INFO
        public static int windows_mode = 0;
        // ======================================================================================================
        // TS UPDATER TEXT
        public static readonly string updater_exe_name = "TSUpdater.exe";
        public static readonly string updater_old_exe_name = "TSUpdater.exe.old";
        // ======================================================================================================
        [STAThread]
        static void Main(){
            SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
            // CHECK WINDOWS VERSION
            try{
                using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption FROM Win32_OperatingSystem"))
                using (var results = searcher.Get()){
                    string caption = results.Cast<ManagementObject>().Select(mo => mo["Caption"]?.ToString()).FirstOrDefault();
                    windows_mode = (caption?.IndexOf("Windows 11", StringComparison.OrdinalIgnoreCase) >= 0) ? 1 : 0;
                }
            }catch (Exception){ }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // TS HYPER LOADER
            // -----------------------------------
            if (!TSHyperLoader()){
                return;
            }
            // -----------------------------------
            string bootstrap_mode = new TSSettingsModule(ts_session_file).TSReadSettings(ts_session_container, "SessionMode");
            Form targetForm = (bootstrap_mode == "1") ? (Form)new AstelLogin() : new AstelSignIn();
            Application.Run(targetForm);
        }
        // TS HYPER LOADER WRAPPER
        // ======================================================================================================
        private static bool TSHyperLoader(){
            try{
                // CHECK LANGS FOLDER
                if (!Directory.Exists(ts_lf)){
                    ShowLanguageAlert(0);
                    return false;
                }
                // CHECK ANY LANG FILE EXISTS
                var lang_files = Directory.GetFiles(ts_lf, "*.ini");
                if (lang_files.Length == 0){
                    ShowLanguageAlert(1);
                    return false;
                }
                // CHECK ENGLISH LANG FILE (MANDATORY)
                if (!File.Exists(ts_lang_en)){
                    ShowLanguageAlert(2);
                    return false;
                }
                // ENSURE SETTINGS
                EnsureSettingsFileAndSchema();
                // CHECK SESSION FILE
                if (!File.Exists(ts_session_file)){
                    try{
                        Directory.CreateDirectory(ts_session_root_path);
                        TSSettingsModule session_settings_save = new TSSettingsModule(ts_session_file);
                        session_settings_save.TSWriteSettings(ts_session_container, "SessionMode", "0");
                    }catch (Exception ex){
                        TS_MessageBoxEngine.TS_MessageBox(null, 3, ex.Message);
                        return false;
                    }
                }
                // DELETE OLD TS UPDATER EXE
                try{
                    if (File.Exists(updater_old_exe_name)){
                        File.Delete(updater_old_exe_name);
                    }
                }catch (Exception ex){
                    Debug.WriteLine($"Could not delete old updater: {ex.Message}");
                }
                return true;
            }catch (Exception ex){
                TS_MessageBoxEngine.TS_MessageBox(null, 3, ex.Message);
                return false;
            }
        }
        // SHOW LANGUAGE ALERT TO USER
        // ======================================================================================================
        private static void ShowLanguageAlert(int alertMode){
            string set_message;
            switch (alertMode){
                case 0:
                    set_message = $"{ts_lf} folder could not be found.{Environment.NewLine}The language folder may be missing or corrupted.{Environment.NewLine}{Environment.NewLine}Would you like to open the latest {Application.ProductName} release page to download it again?";
                    break;
                case 1:
                    set_message = $"No language files were found.{Environment.NewLine}The language files may be missing or corrupted.{Environment.NewLine}{Environment.NewLine}Would you like to open the latest {Application.ProductName} release page to download them again?";
                    break;
                case 2:
                    set_message = $"The English language file (English.ini) is required but could not be found.{Environment.NewLine}{Environment.NewLine}Would you like to open the latest {Application.ProductName} release page to download it again?";
                    break;
                default:
                    set_message = "An unexpected error occurred.";
                    break;
            }
            if (!string.IsNullOrEmpty(set_message)){
                DialogResult open_last_release = TS_MessageBoxEngine.TS_MessageBox(null, 7, set_message);
                if (open_last_release == DialogResult.Yes){
                    Process.Start(new ProcessStartInfo(TS_LinkSystem.github_link_lr){
                        UseShellExecute = true
                    });
                }
                Application.Exit();
            }
        }
        // CONFIGURATION FILE & SCHEMA MANAGEMENT
        // ======================================================================================================
        private static void EnsureSettingsFileAndSchema(){
            try{
                if (!File.Exists(ts_sf)){
                    var dir = Path.GetDirectoryName(ts_sf);
                    if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir)){
                        Directory.CreateDirectory(dir);
                    }
                    File.WriteAllText(ts_sf, string.Empty);
                }
                string uiLang = CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.Trim();
                TSSettingsModule settings = new TSSettingsModule(ts_sf);
                var defaults = GetDefaultSettings(uiLang).ToList();
                foreach (var (key, valueFactory) in defaults){
                    EnsureSettingKey(settings, ts_settings_container, key, valueFactory());
                }
                settings.TSOrderSectionKeys(ts_settings_container, defaults.Select(x => x.Key));
            }catch (Exception){ }
        }
        private static void EnsureSettingKey(TSSettingsModule settings, string section, string key, string defaultValue){
            try{
                string current = settings.TSReadSettings(section, key);
                if (string.IsNullOrWhiteSpace(current)){
                    settings.TSWriteSettings(section, key, defaultValue);
                }
            }catch (Exception){ }
        }
        // DEFAULT SETTINGS
        // ======================================================================================================
        /*
            -- ThemeStatus
            ---------------------------
            0 = Dark Theme
            1 = Light Theme
            2 = System Theme

            -- LanguageStatus
            ---------------------------
            Moved to TSModules.cs

            -- StartupStatus
            ---------------------------
            1 = Full Screen
            0 = Windowed

            -- AutoBackupStatus
            ---------------------------
            1 = On
            0 = Off

            -- SafetyWarnings
            ---------------------------
            1 = On
            0 = Off

            -- PasswordMask
            ---------------------------
            1 = On
            0 = Off

            -- LoginPassVisible
            ---------------------------
            1 = On
            0 = Off
        */
        private static IEnumerable<(string Key, Func<string> ValueFactory)> GetDefaultSettings(string uiLang){
            // GLOBAL
            yield return ("ThemeStatus", () => Convert.ToString(TSThemeModeHelper.GetSystemTheme(2)));
            yield return ("LanguageStatus", () => TSPreloaderSetDefaultLanguage(uiLang));
            yield return ("StartupStatus", () => "0");
            // APPLICATION SPECIFIC
            yield return ("AutoBackupStatus", () => "1");
            yield return ("SafetyWarnings", () => "1");
            yield return ("PasswordMask", () => "1");
            // APPLICATION LOGIN
            yield return ("LoginPassVisible", () => "0");
        }
    }
}