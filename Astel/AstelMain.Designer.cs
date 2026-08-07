namespace Astel
{
    partial class AstelMain
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing)
            {
                _secureRng?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel_Main = new System.Windows.Forms.Panel();
            this.DataMainTable = new System.Windows.Forms.DataGridView();
            this.Panel_Footer = new System.Windows.Forms.Panel();
            this.BtnOpenUrl = new Astel.TSCustomButton();
            this.BtnRndPssGen = new Astel.TSCustomButton();
            this.BtnCopyUrl = new Astel.TSCustomButton();
            this.LabelUrl = new System.Windows.Forms.Label();
            this.TxtUrl = new System.Windows.Forms.TextBox();
            this.CmbService = new Astel.TSCustomComboBox();
            this.FLP_Btns = new System.Windows.Forms.FlowLayoutPanel();
            this.DeleteBtn = new Astel.TSCustomButton();
            this.UpdateBtn = new Astel.TSCustomButton();
            this.AddBtn = new Astel.TSCustomButton();
            this.LabelService = new System.Windows.Forms.Label();
            this.TxtService = new System.Windows.Forms.TextBox();
            this.BtnCopyPassword = new Astel.TSCustomButton();
            this.BtnCopyEmail = new Astel.TSCustomButton();
            this.LabelNote = new System.Windows.Forms.Label();
            this.TxtNote = new System.Windows.Forms.TextBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.LabelMail = new System.Windows.Forms.Label();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.HeaderMenu = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arabicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dutchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.germanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hindiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.japaneseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.koreanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portugueseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spanishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turkishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataTransferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.astelExportFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVExportFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.astelImportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVImportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDataBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDataBackupOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDataBackupOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDataBackupFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.safetyWarningsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.safetyWarningsOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.safetyWarningsOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PassMaskStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PMaskActiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PMaskDisabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkforUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataMainTable)).BeginInit();
            this.Panel_Footer.SuspendLayout();
            this.FLP_Btns.SuspendLayout();
            this.HeaderMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Main
            // 
            this.Panel_Main.Controls.Add(this.DataMainTable);
            this.Panel_Main.Controls.Add(this.Panel_Footer);
            this.Panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Main.Location = new System.Drawing.Point(0, 24);
            this.Panel_Main.Name = "Panel_Main";
            this.Panel_Main.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_Main.Size = new System.Drawing.Size(1008, 577);
            this.Panel_Main.TabIndex = 1;
            // 
            // DataMainTable
            // 
            this.DataMainTable.AllowUserToAddRows = false;
            this.DataMainTable.AllowUserToDeleteRows = false;
            this.DataMainTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.DataMainTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataMainTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataMainTable.BackgroundColor = System.Drawing.Color.White;
            this.DataMainTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataMainTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataMainTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataMainTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataMainTable.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataMainTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataMainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataMainTable.EnableHeadersVisualStyles = false;
            this.DataMainTable.GridColor = System.Drawing.Color.Gray;
            this.DataMainTable.Location = new System.Drawing.Point(10, 10);
            this.DataMainTable.MultiSelect = false;
            this.DataMainTable.Name = "DataMainTable";
            this.DataMainTable.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataMainTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataMainTable.RowHeadersVisible = false;
            this.DataMainTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataMainTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataMainTable.Size = new System.Drawing.Size(988, 377);
            this.DataMainTable.TabIndex = 0;
            this.DataMainTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataMainTable_CellClick);
            // 
            // Panel_Footer
            // 
            this.Panel_Footer.Controls.Add(this.BtnOpenUrl);
            this.Panel_Footer.Controls.Add(this.BtnRndPssGen);
            this.Panel_Footer.Controls.Add(this.BtnCopyUrl);
            this.Panel_Footer.Controls.Add(this.LabelUrl);
            this.Panel_Footer.Controls.Add(this.TxtUrl);
            this.Panel_Footer.Controls.Add(this.CmbService);
            this.Panel_Footer.Controls.Add(this.FLP_Btns);
            this.Panel_Footer.Controls.Add(this.LabelService);
            this.Panel_Footer.Controls.Add(this.TxtService);
            this.Panel_Footer.Controls.Add(this.BtnCopyPassword);
            this.Panel_Footer.Controls.Add(this.BtnCopyEmail);
            this.Panel_Footer.Controls.Add(this.LabelNote);
            this.Panel_Footer.Controls.Add(this.TxtNote);
            this.Panel_Footer.Controls.Add(this.LabelPassword);
            this.Panel_Footer.Controls.Add(this.TxtPassword);
            this.Panel_Footer.Controls.Add(this.LabelMail);
            this.Panel_Footer.Controls.Add(this.TxtEmail);
            this.Panel_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Footer.Location = new System.Drawing.Point(10, 387);
            this.Panel_Footer.Name = "Panel_Footer";
            this.Panel_Footer.Padding = new System.Windows.Forms.Padding(10, 10, 0, 5);
            this.Panel_Footer.Size = new System.Drawing.Size(988, 180);
            this.Panel_Footer.TabIndex = 1;
            // 
            // BtnOpenUrl
            // 
            this.BtnOpenUrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnOpenUrl.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnOpenUrl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnOpenUrl.BorderRadius = 3;
            this.BtnOpenUrl.BorderSize = 0;
            this.BtnOpenUrl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnOpenUrl.FlatAppearance.BorderSize = 0;
            this.BtnOpenUrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOpenUrl.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnOpenUrl.ForeColor = System.Drawing.Color.White;
            this.BtnOpenUrl.Location = new System.Drawing.Point(678, 31);
            this.BtnOpenUrl.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.BtnOpenUrl.Name = "BtnOpenUrl";
            this.BtnOpenUrl.Size = new System.Drawing.Size(27, 27);
            this.BtnOpenUrl.TabIndex = 13;
            this.BtnOpenUrl.TextColor = System.Drawing.Color.White;
            this.BtnOpenUrl.UseVisualStyleBackColor = false;
            this.BtnOpenUrl.Click += new System.EventHandler(this.BtnOpenUrl_Click);
            // 
            // BtnRndPssGen
            // 
            this.BtnRndPssGen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnRndPssGen.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnRndPssGen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnRndPssGen.BorderRadius = 3;
            this.BtnRndPssGen.BorderSize = 0;
            this.BtnRndPssGen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRndPssGen.FlatAppearance.BorderSize = 0;
            this.BtnRndPssGen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRndPssGen.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnRndPssGen.ForeColor = System.Drawing.Color.White;
            this.BtnRndPssGen.Location = new System.Drawing.Point(285, 149);
            this.BtnRndPssGen.Name = "BtnRndPssGen";
            this.BtnRndPssGen.Size = new System.Drawing.Size(27, 27);
            this.BtnRndPssGen.TabIndex = 8;
            this.BtnRndPssGen.TextColor = System.Drawing.Color.White;
            this.BtnRndPssGen.UseVisualStyleBackColor = false;
            this.BtnRndPssGen.Click += new System.EventHandler(this.BtnRndPssGen_Click);
            // 
            // BtnCopyUrl
            // 
            this.BtnCopyUrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnCopyUrl.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnCopyUrl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnCopyUrl.BorderRadius = 3;
            this.BtnCopyUrl.BorderSize = 0;
            this.BtnCopyUrl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCopyUrl.FlatAppearance.BorderSize = 0;
            this.BtnCopyUrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCopyUrl.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnCopyUrl.ForeColor = System.Drawing.Color.White;
            this.BtnCopyUrl.Location = new System.Drawing.Point(645, 31);
            this.BtnCopyUrl.Name = "BtnCopyUrl";
            this.BtnCopyUrl.Size = new System.Drawing.Size(27, 27);
            this.BtnCopyUrl.TabIndex = 12;
            this.BtnCopyUrl.TextColor = System.Drawing.Color.White;
            this.BtnCopyUrl.UseVisualStyleBackColor = false;
            this.BtnCopyUrl.Click += new System.EventHandler(this.BtnCopyUrl_Click);
            // 
            // LabelUrl
            // 
            this.LabelUrl.AutoSize = true;
            this.LabelUrl.BackColor = System.Drawing.Color.Transparent;
            this.LabelUrl.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelUrl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelUrl.Location = new System.Drawing.Point(356, 10);
            this.LabelUrl.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelUrl.Name = "LabelUrl";
            this.LabelUrl.Size = new System.Drawing.Size(56, 19);
            this.LabelUrl.TabIndex = 10;
            this.LabelUrl.Text = "E-Posta";
            // 
            // TxtUrl
            // 
            this.TxtUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUrl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtUrl.Location = new System.Drawing.Point(360, 32);
            this.TxtUrl.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtUrl.MaxLength = 128;
            this.TxtUrl.Name = "TxtUrl";
            this.TxtUrl.Size = new System.Drawing.Size(279, 25);
            this.TxtUrl.TabIndex = 11;
            // 
            // CmbService
            // 
            this.CmbService.ArrowColor = System.Drawing.SystemColors.WindowText;
            this.CmbService.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.CmbService.ButtonColor = System.Drawing.SystemColors.ControlDark;
            this.CmbService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CmbService.DisabledArrowColor = System.Drawing.SystemColors.GrayText;
            this.CmbService.DisabledBackColor = System.Drawing.SystemColors.Control;
            this.CmbService.DisabledButtonColor = System.Drawing.SystemColors.ControlDark;
            this.CmbService.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            this.CmbService.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CmbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbService.FocusedBorderColor = System.Drawing.Color.DodgerBlue;
            this.CmbService.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CmbService.FormattingEnabled = true;
            this.CmbService.HoverBackColor = System.Drawing.SystemColors.Window;
            this.CmbService.HoverButtonColor = System.Drawing.SystemColors.ControlDark;
            this.CmbService.HoverForeColor = System.Drawing.SystemColors.WindowText;
            this.CmbService.Location = new System.Drawing.Point(206, 32);
            this.CmbService.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.CmbService.Name = "CmbService";
            this.CmbService.SelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.CmbService.SelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.CmbService.Size = new System.Drawing.Size(139, 25);
            this.CmbService.TabIndex = 2;
            this.CmbService.SelectedIndexChanged += new System.EventHandler(this.CmbService_SelectedIndexChanged);
            // 
            // FLP_Btns
            // 
            this.FLP_Btns.AutoSize = true;
            this.FLP_Btns.Controls.Add(this.DeleteBtn);
            this.FLP_Btns.Controls.Add(this.UpdateBtn);
            this.FLP_Btns.Controls.Add(this.AddBtn);
            this.FLP_Btns.Dock = System.Windows.Forms.DockStyle.Right;
            this.FLP_Btns.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.FLP_Btns.Location = new System.Drawing.Point(763, 10);
            this.FLP_Btns.Name = "FLP_Btns";
            this.FLP_Btns.Size = new System.Drawing.Size(225, 165);
            this.FLP_Btns.TabIndex = 16;
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.DeleteBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.DeleteBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.DeleteBtn.BorderRadius = 10;
            this.DeleteBtn.BorderSize = 0;
            this.DeleteBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteBtn.FlatAppearance.BorderSize = 0;
            this.DeleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.DeleteBtn.ForeColor = System.Drawing.Color.White;
            this.DeleteBtn.Location = new System.Drawing.Point(0, 125);
            this.DeleteBtn.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.DeleteBtn.Size = new System.Drawing.Size(225, 40);
            this.DeleteBtn.TabIndex = 2;
            this.DeleteBtn.Text = "SİL";
            this.DeleteBtn.TextColor = System.Drawing.Color.White;
            this.DeleteBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DeleteBtn.UseVisualStyleBackColor = false;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.UpdateBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.UpdateBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.UpdateBtn.BorderRadius = 10;
            this.UpdateBtn.BorderSize = 0;
            this.UpdateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateBtn.FlatAppearance.BorderSize = 0;
            this.UpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.UpdateBtn.ForeColor = System.Drawing.Color.White;
            this.UpdateBtn.Location = new System.Drawing.Point(0, 73);
            this.UpdateBtn.Margin = new System.Windows.Forms.Padding(0, 7, 0, 7);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.UpdateBtn.Size = new System.Drawing.Size(225, 40);
            this.UpdateBtn.TabIndex = 1;
            this.UpdateBtn.Text = "GÜNCELLE";
            this.UpdateBtn.TextColor = System.Drawing.Color.White;
            this.UpdateBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.UpdateBtn.UseVisualStyleBackColor = false;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.AddBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.AddBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.AddBtn.BorderRadius = 10;
            this.AddBtn.BorderSize = 0;
            this.AddBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddBtn.FlatAppearance.BorderSize = 0;
            this.AddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.AddBtn.ForeColor = System.Drawing.Color.White;
            this.AddBtn.Location = new System.Drawing.Point(0, 21);
            this.AddBtn.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.AddBtn.Size = new System.Drawing.Size(225, 40);
            this.AddBtn.TabIndex = 0;
            this.AddBtn.Text = "EKLE";
            this.AddBtn.TextColor = System.Drawing.Color.White;
            this.AddBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.AddBtn.UseVisualStyleBackColor = false;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // LabelService
            // 
            this.LabelService.AutoSize = true;
            this.LabelService.BackColor = System.Drawing.Color.Transparent;
            this.LabelService.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelService.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelService.Location = new System.Drawing.Point(-4, 10);
            this.LabelService.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelService.Name = "LabelService";
            this.LabelService.Size = new System.Drawing.Size(54, 19);
            this.LabelService.TabIndex = 0;
            this.LabelService.Text = "Hizmet";
            // 
            // TxtService
            // 
            this.TxtService.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtService.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtService.Location = new System.Drawing.Point(0, 32);
            this.TxtService.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtService.MaxLength = 64;
            this.TxtService.Name = "TxtService";
            this.TxtService.Size = new System.Drawing.Size(200, 25);
            this.TxtService.TabIndex = 1;
            this.TxtService.TextChanged += new System.EventHandler(this.TxtService_TextChanged);
            // 
            // BtnCopyPassword
            // 
            this.BtnCopyPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnCopyPassword.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnCopyPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnCopyPassword.BorderRadius = 3;
            this.BtnCopyPassword.BorderSize = 0;
            this.BtnCopyPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCopyPassword.FlatAppearance.BorderSize = 0;
            this.BtnCopyPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCopyPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnCopyPassword.ForeColor = System.Drawing.Color.White;
            this.BtnCopyPassword.Location = new System.Drawing.Point(318, 149);
            this.BtnCopyPassword.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.BtnCopyPassword.Name = "BtnCopyPassword";
            this.BtnCopyPassword.Size = new System.Drawing.Size(27, 27);
            this.BtnCopyPassword.TabIndex = 9;
            this.BtnCopyPassword.TextColor = System.Drawing.Color.White;
            this.BtnCopyPassword.UseVisualStyleBackColor = false;
            this.BtnCopyPassword.Click += new System.EventHandler(this.BtnCopyPassword_Click);
            // 
            // BtnCopyEmail
            // 
            this.BtnCopyEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnCopyEmail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnCopyEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnCopyEmail.BorderRadius = 3;
            this.BtnCopyEmail.BorderSize = 0;
            this.BtnCopyEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCopyEmail.FlatAppearance.BorderSize = 0;
            this.BtnCopyEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCopyEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnCopyEmail.ForeColor = System.Drawing.Color.White;
            this.BtnCopyEmail.Location = new System.Drawing.Point(318, 90);
            this.BtnCopyEmail.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.BtnCopyEmail.Name = "BtnCopyEmail";
            this.BtnCopyEmail.Size = new System.Drawing.Size(27, 27);
            this.BtnCopyEmail.TabIndex = 5;
            this.BtnCopyEmail.TextColor = System.Drawing.Color.White;
            this.BtnCopyEmail.UseVisualStyleBackColor = false;
            this.BtnCopyEmail.Click += new System.EventHandler(this.BtnCopyEmail_Click);
            // 
            // LabelNote
            // 
            this.LabelNote.AutoSize = true;
            this.LabelNote.BackColor = System.Drawing.Color.Transparent;
            this.LabelNote.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelNote.Location = new System.Drawing.Point(356, 69);
            this.LabelNote.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelNote.Name = "LabelNote";
            this.LabelNote.Size = new System.Drawing.Size(33, 19);
            this.LabelNote.TabIndex = 14;
            this.LabelNote.Text = "Not";
            // 
            // TxtNote
            // 
            this.TxtNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNote.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtNote.Location = new System.Drawing.Point(360, 91);
            this.TxtNote.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtNote.MaxLength = 128;
            this.TxtNote.Multiline = true;
            this.TxtNote.Name = "TxtNote";
            this.TxtNote.Size = new System.Drawing.Size(345, 84);
            this.TxtNote.TabIndex = 15;
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.BackColor = System.Drawing.Color.Transparent;
            this.LabelPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelPassword.Location = new System.Drawing.Point(-4, 128);
            this.LabelPassword.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(38, 19);
            this.LabelPassword.TabIndex = 6;
            this.LabelPassword.Text = "Şifre";
            // 
            // TxtPassword
            // 
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtPassword.Location = new System.Drawing.Point(0, 150);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtPassword.MaxLength = 64;
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(279, 25);
            this.TxtPassword.TabIndex = 7;
            // 
            // LabelMail
            // 
            this.LabelMail.AutoSize = true;
            this.LabelMail.BackColor = System.Drawing.Color.Transparent;
            this.LabelMail.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelMail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelMail.Location = new System.Drawing.Point(-4, 69);
            this.LabelMail.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelMail.Name = "LabelMail";
            this.LabelMail.Size = new System.Drawing.Size(56, 19);
            this.LabelMail.TabIndex = 3;
            this.LabelMail.Text = "E-Posta";
            // 
            // TxtEmail
            // 
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtEmail.Location = new System.Drawing.Point(0, 91);
            this.TxtEmail.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtEmail.MaxLength = 64;
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(312, 25);
            this.TxtEmail.TabIndex = 4;
            // 
            // MainToolTip
            // 
            this.MainToolTip.OwnerDraw = true;
            this.MainToolTip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.MainToolTip_Draw);
            // 
            // HeaderMenu
            // 
            this.HeaderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.passwordGeneratorToolStripMenuItem,
            this.donateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.HeaderMenu.Location = new System.Drawing.Point(0, 0);
            this.HeaderMenu.Name = "HeaderMenu";
            this.HeaderMenu.Size = new System.Drawing.Size(1008, 24);
            this.HeaderMenu.TabIndex = 0;
            this.HeaderMenu.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.startupToolStripMenuItem,
            this.dataTransferToolStripMenuItem,
            this.autoDataBackupToolStripMenuItem,
            this.safetyWarningsToolStripMenuItem,
            this.PassMaskStatusToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.checkforUpdatesToolStripMenuItem});
            this.settingsToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightThemeToolStripMenuItem,
            this.darkThemeToolStripMenuItem,
            this.systemThemeToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // lightThemeToolStripMenuItem
            // 
            this.lightThemeToolStripMenuItem.Name = "lightThemeToolStripMenuItem";
            this.lightThemeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.lightThemeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.lightThemeToolStripMenuItem.Text = "Light Theme";
            this.lightThemeToolStripMenuItem.Click += new System.EventHandler(this.LightThemeToolStripMenuItem_Click);
            // 
            // darkThemeToolStripMenuItem
            // 
            this.darkThemeToolStripMenuItem.Name = "darkThemeToolStripMenuItem";
            this.darkThemeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.darkThemeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.darkThemeToolStripMenuItem.Text = "Dark Theme";
            this.darkThemeToolStripMenuItem.Click += new System.EventHandler(this.DarkThemeToolStripMenuItem_Click);
            // 
            // systemThemeToolStripMenuItem
            // 
            this.systemThemeToolStripMenuItem.Name = "systemThemeToolStripMenuItem";
            this.systemThemeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.systemThemeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.systemThemeToolStripMenuItem.Text = "System Theme";
            this.systemThemeToolStripMenuItem.Click += new System.EventHandler(this.SystemThemeToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arabicToolStripMenuItem,
            this.chineseToolStripMenuItem,
            this.englishToolStripMenuItem,
            this.dutchToolStripMenuItem,
            this.frenchToolStripMenuItem,
            this.germanToolStripMenuItem,
            this.hindiToolStripMenuItem,
            this.italianToolStripMenuItem,
            this.japaneseToolStripMenuItem,
            this.koreanToolStripMenuItem,
            this.polishToolStripMenuItem,
            this.portugueseToolStripMenuItem,
            this.russianToolStripMenuItem,
            this.spanishToolStripMenuItem,
            this.turkishToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // arabicToolStripMenuItem
            // 
            this.arabicToolStripMenuItem.Name = "arabicToolStripMenuItem";
            this.arabicToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.arabicToolStripMenuItem.Text = "Arabic";
            // 
            // chineseToolStripMenuItem
            // 
            this.chineseToolStripMenuItem.Name = "chineseToolStripMenuItem";
            this.chineseToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.chineseToolStripMenuItem.Text = "Chinese";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.englishToolStripMenuItem.Text = "English";
            // 
            // dutchToolStripMenuItem
            // 
            this.dutchToolStripMenuItem.Name = "dutchToolStripMenuItem";
            this.dutchToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.dutchToolStripMenuItem.Text = "Dutch";
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            this.frenchToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.frenchToolStripMenuItem.Text = "French";
            // 
            // germanToolStripMenuItem
            // 
            this.germanToolStripMenuItem.Name = "germanToolStripMenuItem";
            this.germanToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.germanToolStripMenuItem.Text = "German";
            // 
            // hindiToolStripMenuItem
            // 
            this.hindiToolStripMenuItem.Name = "hindiToolStripMenuItem";
            this.hindiToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.hindiToolStripMenuItem.Text = "Hindi";
            // 
            // italianToolStripMenuItem
            // 
            this.italianToolStripMenuItem.Name = "italianToolStripMenuItem";
            this.italianToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.italianToolStripMenuItem.Text = "Italian";
            // 
            // japaneseToolStripMenuItem
            // 
            this.japaneseToolStripMenuItem.Name = "japaneseToolStripMenuItem";
            this.japaneseToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.japaneseToolStripMenuItem.Text = "Japanese";
            // 
            // koreanToolStripMenuItem
            // 
            this.koreanToolStripMenuItem.Name = "koreanToolStripMenuItem";
            this.koreanToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.koreanToolStripMenuItem.Text = "Korean";
            // 
            // polishToolStripMenuItem
            // 
            this.polishToolStripMenuItem.Name = "polishToolStripMenuItem";
            this.polishToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.polishToolStripMenuItem.Text = "Polish";
            // 
            // portugueseToolStripMenuItem
            // 
            this.portugueseToolStripMenuItem.Name = "portugueseToolStripMenuItem";
            this.portugueseToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.portugueseToolStripMenuItem.Text = "Portuguese";
            // 
            // russianToolStripMenuItem
            // 
            this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
            this.russianToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.russianToolStripMenuItem.Text = "Russian";
            // 
            // spanishToolStripMenuItem
            // 
            this.spanishToolStripMenuItem.Name = "spanishToolStripMenuItem";
            this.spanishToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.spanishToolStripMenuItem.Text = "Spanish";
            // 
            // turkishToolStripMenuItem
            // 
            this.turkishToolStripMenuItem.Name = "turkishToolStripMenuItem";
            this.turkishToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.turkishToolStripMenuItem.Text = "Turkish";
            // 
            // startupToolStripMenuItem
            // 
            this.startupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowedToolStripMenuItem,
            this.fullScreenToolStripMenuItem});
            this.startupToolStripMenuItem.Name = "startupToolStripMenuItem";
            this.startupToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.startupToolStripMenuItem.Text = "Startup";
            // 
            // windowedToolStripMenuItem
            // 
            this.windowedToolStripMenuItem.Name = "windowedToolStripMenuItem";
            this.windowedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.windowedToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.windowedToolStripMenuItem.Text = "Windowed";
            this.windowedToolStripMenuItem.Click += new System.EventHandler(this.WindowedToolStripMenuItem_Click);
            // 
            // fullScreenToolStripMenuItem
            // 
            this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
            this.fullScreenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.fullScreenToolStripMenuItem.Text = "Full Screen";
            this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.FullScreenToolStripMenuItem_Click);
            // 
            // dataTransferToolStripMenuItem
            // 
            this.dataTransferToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportDataToolStripMenuItem,
            this.importDataToolStripMenuItem});
            this.dataTransferToolStripMenuItem.Name = "dataTransferToolStripMenuItem";
            this.dataTransferToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.dataTransferToolStripMenuItem.Text = "Data Transfer";
            // 
            // exportDataToolStripMenuItem
            // 
            this.exportDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.astelExportFileToolStripMenuItem,
            this.cSVExportFileToolStripMenuItem});
            this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
            this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.exportDataToolStripMenuItem.Text = "Export Data";
            // 
            // astelExportFileToolStripMenuItem
            // 
            this.astelExportFileToolStripMenuItem.Name = "astelExportFileToolStripMenuItem";
            this.astelExportFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D1)));
            this.astelExportFileToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.astelExportFileToolStripMenuItem.Text = "Astel Export File";
            this.astelExportFileToolStripMenuItem.Click += new System.EventHandler(this.AstelExportFileToolStripMenuItem_Click);
            // 
            // cSVExportFileToolStripMenuItem
            // 
            this.cSVExportFileToolStripMenuItem.Name = "cSVExportFileToolStripMenuItem";
            this.cSVExportFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D2)));
            this.cSVExportFileToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.cSVExportFileToolStripMenuItem.Text = "CSV Export File";
            this.cSVExportFileToolStripMenuItem.Click += new System.EventHandler(this.CSVExportFileToolStripMenuItem_Click);
            // 
            // importDataToolStripMenuItem
            // 
            this.importDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.astelImportDataToolStripMenuItem,
            this.cSVImportDataToolStripMenuItem});
            this.importDataToolStripMenuItem.Name = "importDataToolStripMenuItem";
            this.importDataToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.importDataToolStripMenuItem.Text = "Import Data";
            // 
            // astelImportDataToolStripMenuItem
            // 
            this.astelImportDataToolStripMenuItem.Name = "astelImportDataToolStripMenuItem";
            this.astelImportDataToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D3)));
            this.astelImportDataToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.astelImportDataToolStripMenuItem.Text = "Astel Import Data";
            this.astelImportDataToolStripMenuItem.Click += new System.EventHandler(this.AstelImportDataToolStripMenuItem_Click);
            // 
            // cSVImportDataToolStripMenuItem
            // 
            this.cSVImportDataToolStripMenuItem.Name = "cSVImportDataToolStripMenuItem";
            this.cSVImportDataToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D4)));
            this.cSVImportDataToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.cSVImportDataToolStripMenuItem.Text = "CSV Import Data";
            this.cSVImportDataToolStripMenuItem.Click += new System.EventHandler(this.CSVImportDataToolStripMenuItem_Click);
            // 
            // autoDataBackupToolStripMenuItem
            // 
            this.autoDataBackupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoDataBackupOnToolStripMenuItem,
            this.autoDataBackupOffToolStripMenuItem,
            this.autoDataBackupFolderToolStripMenuItem});
            this.autoDataBackupToolStripMenuItem.Name = "autoDataBackupToolStripMenuItem";
            this.autoDataBackupToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.autoDataBackupToolStripMenuItem.Text = "Data Backup";
            // 
            // autoDataBackupOnToolStripMenuItem
            // 
            this.autoDataBackupOnToolStripMenuItem.Name = "autoDataBackupOnToolStripMenuItem";
            this.autoDataBackupOnToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D5)));
            this.autoDataBackupOnToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.autoDataBackupOnToolStripMenuItem.Text = "Data Backup On";
            this.autoDataBackupOnToolStripMenuItem.Click += new System.EventHandler(this.AutoDataBackupOnToolStripMenuItem_Click);
            // 
            // autoDataBackupOffToolStripMenuItem
            // 
            this.autoDataBackupOffToolStripMenuItem.Name = "autoDataBackupOffToolStripMenuItem";
            this.autoDataBackupOffToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D6)));
            this.autoDataBackupOffToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.autoDataBackupOffToolStripMenuItem.Text = "Data Backup Off";
            this.autoDataBackupOffToolStripMenuItem.Click += new System.EventHandler(this.AutoDataBackupOffToolStripMenuItem_Click);
            // 
            // autoDataBackupFolderToolStripMenuItem
            // 
            this.autoDataBackupFolderToolStripMenuItem.Name = "autoDataBackupFolderToolStripMenuItem";
            this.autoDataBackupFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D7)));
            this.autoDataBackupFolderToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.autoDataBackupFolderToolStripMenuItem.Text = "Data Backup Folder";
            this.autoDataBackupFolderToolStripMenuItem.Click += new System.EventHandler(this.AutoDataBackupFolderToolStripMenuItem_Click);
            // 
            // safetyWarningsToolStripMenuItem
            // 
            this.safetyWarningsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.safetyWarningsOnToolStripMenuItem,
            this.safetyWarningsOffToolStripMenuItem});
            this.safetyWarningsToolStripMenuItem.Name = "safetyWarningsToolStripMenuItem";
            this.safetyWarningsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.safetyWarningsToolStripMenuItem.Text = "Safety Warnings";
            // 
            // safetyWarningsOnToolStripMenuItem
            // 
            this.safetyWarningsOnToolStripMenuItem.Name = "safetyWarningsOnToolStripMenuItem";
            this.safetyWarningsOnToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.safetyWarningsOnToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.safetyWarningsOnToolStripMenuItem.Text = "Safety Warnings On";
            this.safetyWarningsOnToolStripMenuItem.Click += new System.EventHandler(this.SafetyWarningsOnToolStripMenuItem_Click);
            // 
            // safetyWarningsOffToolStripMenuItem
            // 
            this.safetyWarningsOffToolStripMenuItem.Name = "safetyWarningsOffToolStripMenuItem";
            this.safetyWarningsOffToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.safetyWarningsOffToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.safetyWarningsOffToolStripMenuItem.Text = "Safety Warnings Off";
            this.safetyWarningsOffToolStripMenuItem.Click += new System.EventHandler(this.SafetyWarningsOffToolStripMenuItem_Click);
            // 
            // PassMaskStatusToolStripMenuItem
            // 
            this.PassMaskStatusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PMaskActiveToolStripMenuItem,
            this.PMaskDisabledToolStripMenuItem});
            this.PassMaskStatusToolStripMenuItem.Name = "PassMaskStatusToolStripMenuItem";
            this.PassMaskStatusToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.PassMaskStatusToolStripMenuItem.Text = "Pass Mask Status";
            // 
            // PMaskActiveToolStripMenuItem
            // 
            this.PMaskActiveToolStripMenuItem.Name = "PMaskActiveToolStripMenuItem";
            this.PMaskActiveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.PMaskActiveToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.PMaskActiveToolStripMenuItem.Text = "P Mask Active";
            this.PMaskActiveToolStripMenuItem.Click += new System.EventHandler(this.PCVMActiveToolStripMenuItem_Click);
            // 
            // PMaskDisabledToolStripMenuItem
            // 
            this.PMaskDisabledToolStripMenuItem.Name = "PMaskDisabledToolStripMenuItem";
            this.PMaskDisabledToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.PMaskDisabledToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.PMaskDisabledToolStripMenuItem.Text = "P Mask Disabled";
            this.PMaskDisabledToolStripMenuItem.Click += new System.EventHandler(this.PCVMDisabledToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.ChangePasswordToolStripMenuItem_Click);
            // 
            // checkforUpdatesToolStripMenuItem
            // 
            this.checkforUpdatesToolStripMenuItem.Name = "checkforUpdatesToolStripMenuItem";
            this.checkforUpdatesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.checkforUpdatesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.checkforUpdatesToolStripMenuItem.Text = "Check Update";
            this.checkforUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckforUpdatesToolStripMenuItem_Click);
            // 
            // passwordGeneratorToolStripMenuItem
            // 
            this.passwordGeneratorToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.passwordGeneratorToolStripMenuItem.Name = "passwordGeneratorToolStripMenuItem";
            this.passwordGeneratorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.passwordGeneratorToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.passwordGeneratorToolStripMenuItem.Text = "Password Generator";
            this.passwordGeneratorToolStripMenuItem.Click += new System.EventHandler(this.PasswordGeneratorToolStripMenuItem_Click);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D)));
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.donateToolStripMenuItem.Text = "Donate";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.DonateToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // AstelMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1008, 601);
            this.Controls.Add(this.Panel_Main);
            this.Controls.Add(this.HeaderMenu);
            this.DoubleBuffered = true;
            this.Icon = global::Astel.Properties.Resources.AstelLogo;
            this.MainMenuStrip = this.HeaderMenu;
            this.MinimumSize = new System.Drawing.Size(1024, 640);
            this.Name = "AstelMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Astel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Astel_FormClosing);
            this.Load += new System.EventHandler(this.Astel_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Astel_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Astel_DragEnter);
            this.Panel_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataMainTable)).EndInit();
            this.Panel_Footer.ResumeLayout(false);
            this.Panel_Footer.PerformLayout();
            this.FLP_Btns.ResumeLayout(false);
            this.HeaderMenu.ResumeLayout(false);
            this.HeaderMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel Panel_Main;
        private System.Windows.Forms.Panel Panel_Footer;
        private TSCustomButton BtnCopyPassword;
        private TSCustomButton BtnCopyEmail;
        internal System.Windows.Forms.Label LabelNote;
        private System.Windows.Forms.TextBox TxtNote;
        internal System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.TextBox TxtPassword;
        internal System.Windows.Forms.Label LabelMail;
        private System.Windows.Forms.TextBox TxtEmail;
        private TSCustomButton AddBtn;
        private TSCustomButton UpdateBtn;
        private TSCustomButton DeleteBtn;
        private System.Windows.Forms.DataGridView DataMainTable;
        private System.Windows.Forms.ToolTip MainToolTip;
        private System.Windows.Forms.MenuStrip HeaderMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turkishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkforUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passwordGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
        internal System.Windows.Forms.Label LabelService;
        private System.Windows.Forms.TextBox TxtService;
        private System.Windows.Forms.FlowLayoutPanel FLP_Btns;
        private System.Windows.Forms.ToolStripMenuItem dataTransferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDataBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDataBackupOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDataBackupOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDataBackupFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private TSCustomComboBox CmbService;
        private System.Windows.Forms.ToolStripMenuItem cSVExportFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem astelExportFileToolStripMenuItem;
        private TSCustomButton BtnCopyUrl;
        internal System.Windows.Forms.Label LabelUrl;
        private System.Windows.Forms.TextBox TxtUrl;
        private System.Windows.Forms.ToolStripMenuItem astelImportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVImportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem safetyWarningsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem safetyWarningsOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem safetyWarningsOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem italianToolStripMenuItem;
        private TSCustomButton BtnRndPssGen;
        private System.Windows.Forms.ToolStripMenuItem systemThemeToolStripMenuItem;
        private TSCustomButton BtnOpenUrl;
        private System.Windows.Forms.ToolStripMenuItem polishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arabicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem germanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hindiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem japaneseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem koreanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portugueseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem russianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spanishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dutchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PassMaskStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PMaskActiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PMaskDisabledToolStripMenuItem;
    }
}


