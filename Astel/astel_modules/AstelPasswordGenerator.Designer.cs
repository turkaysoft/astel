namespace Astel.astel_modules
{
    partial class AstelPasswordGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Panel_BG = new System.Windows.Forms.Panel();
            this.BtnCopyPassword = new Astel.TSCustomButton();
            this.PassGenProLabel = new Astel.TSCustomLabel();
            this.PassGenProBG = new Astel.TSCustomPanel();
            this.PassGenProFE = new Astel.TSCustomPanel();
            this.Panel_Mode = new Astel.TSCustomPanel();
            this.LabelMode = new Astel.TSCustomLabel();
            this.RadioMixed = new Astel.TSCustomRadioButton();
            this.RadioWrite = new Astel.TSCustomRadioButton();
            this.RadioRead = new Astel.TSCustomRadioButton();
            this.Panel_Feature = new Astel.TSCustomPanel();
            this.LabelFeature = new Astel.TSCustomLabel();
            this.CheckSpecialChars = new Astel.TSCustomCheckBox();
            this.CheckNumeric = new Astel.TSCustomCheckBox();
            this.CheckLowercase = new Astel.TSCustomCheckBox();
            this.CheckUppercase = new Astel.TSCustomCheckBox();
            this.PassGenLenght = new Astel.TSCustomTrackBar();
            this.PassResultLabel = new Astel.TSCustomLabel();
            this.BtnGenPass = new Astel.TSCustomButton();
            this.PassLenghtLabel = new Astel.TSCustomLabel();
            this.LabelHeader = new Astel.TSCustomLabel();
            this.Panel_BG.SuspendLayout();
            this.PassGenProBG.SuspendLayout();
            this.Panel_Mode.SuspendLayout();
            this.Panel_Feature.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_BG
            // 
            this.Panel_BG.Controls.Add(this.BtnCopyPassword);
            this.Panel_BG.Controls.Add(this.PassGenProLabel);
            this.Panel_BG.Controls.Add(this.PassGenProBG);
            this.Panel_BG.Controls.Add(this.Panel_Mode);
            this.Panel_BG.Controls.Add(this.Panel_Feature);
            this.Panel_BG.Controls.Add(this.PassGenLenght);
            this.Panel_BG.Controls.Add(this.PassResultLabel);
            this.Panel_BG.Controls.Add(this.BtnGenPass);
            this.Panel_BG.Controls.Add(this.PassLenghtLabel);
            this.Panel_BG.Controls.Add(this.LabelHeader);
            this.Panel_BG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_BG.Location = new System.Drawing.Point(0, 0);
            this.Panel_BG.Name = "Panel_BG";
            this.Panel_BG.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_BG.Size = new System.Drawing.Size(584, 525);
            this.Panel_BG.TabIndex = 0;
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
            this.BtnCopyPassword.Location = new System.Drawing.Point(539, 374);
            this.BtnCopyPassword.Name = "BtnCopyPassword";
            this.BtnCopyPassword.Size = new System.Drawing.Size(35, 40);
            this.BtnCopyPassword.TabIndex = 10;
            this.BtnCopyPassword.TextColor = System.Drawing.Color.White;
            this.BtnCopyPassword.UseVisualStyleBackColor = false;
            this.BtnCopyPassword.Click += new System.EventHandler(this.BtnCopyPassword_Click);
            // 
            // PassGenProLabel
            // 
            this.PassGenProLabel.AutoSize = true;
            this.PassGenProLabel.BorderRadius = 0;
            this.PassGenProLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.PassGenProLabel.Location = new System.Drawing.Point(10, 436);
            this.PassGenProLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.PassGenProLabel.Name = "PassGenProLabel";
            this.PassGenProLabel.Size = new System.Drawing.Size(108, 19);
            this.PassGenProLabel.TabIndex = 7;
            this.PassGenProLabel.Text = "Progress Label...";
            // 
            // PassGenProBG
            // 
            this.PassGenProBG.BackColor = System.Drawing.Color.White;
            this.PassGenProBG.BorderColor = System.Drawing.Color.DodgerBlue;
            this.PassGenProBG.BorderRadius = 4;
            this.PassGenProBG.BorderSize = 0;
            this.PassGenProBG.Controls.Add(this.PassGenProFE);
            this.PassGenProBG.Location = new System.Drawing.Point(10, 422);
            this.PassGenProBG.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.PassGenProBG.Name = "PassGenProBG";
            this.PassGenProBG.Size = new System.Drawing.Size(564, 8);
            this.PassGenProBG.TabIndex = 6;
            // 
            // PassGenProFE
            // 
            this.PassGenProFE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.PassGenProFE.BorderColor = System.Drawing.Color.DodgerBlue;
            this.PassGenProFE.BorderRadius = 4;
            this.PassGenProFE.BorderSize = 0;
            this.PassGenProFE.Dock = System.Windows.Forms.DockStyle.Left;
            this.PassGenProFE.Location = new System.Drawing.Point(0, 0);
            this.PassGenProFE.Name = "PassGenProFE";
            this.PassGenProFE.Size = new System.Drawing.Size(50, 8);
            this.PassGenProFE.TabIndex = 0;
            // 
            // Panel_Mode
            // 
            this.Panel_Mode.BackColor = System.Drawing.Color.White;
            this.Panel_Mode.BorderColor = System.Drawing.Color.DodgerBlue;
            this.Panel_Mode.BorderRadius = 5;
            this.Panel_Mode.BorderSize = 0;
            this.Panel_Mode.Controls.Add(this.LabelMode);
            this.Panel_Mode.Controls.Add(this.RadioMixed);
            this.Panel_Mode.Controls.Add(this.RadioWrite);
            this.Panel_Mode.Controls.Add(this.RadioRead);
            this.Panel_Mode.Location = new System.Drawing.Point(295, 68);
            this.Panel_Mode.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Panel_Mode.Name = "Panel_Mode";
            this.Panel_Mode.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_Mode.Size = new System.Drawing.Size(279, 195);
            this.Panel_Mode.TabIndex = 2;
            // 
            // LabelMode
            // 
            this.LabelMode.BackColor = System.Drawing.SystemColors.Control;
            this.LabelMode.BorderRadius = 5;
            this.LabelMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelMode.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelMode.Location = new System.Drawing.Point(10, 10);
            this.LabelMode.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.LabelMode.Name = "LabelMode";
            this.LabelMode.Size = new System.Drawing.Size(259, 35);
            this.LabelMode.TabIndex = 0;
            this.LabelMode.Text = "Mod Seç";
            this.LabelMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadioMixed
            // 
            this.RadioMixed.AutoSize = true;
            this.RadioMixed.Checked = true;
            this.RadioMixed.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.RadioMixed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RadioMixed.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.RadioMixed.Location = new System.Drawing.Point(10, 122);
            this.RadioMixed.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.RadioMixed.MinimumSize = new System.Drawing.Size(0, 21);
            this.RadioMixed.Name = "RadioMixed";
            this.RadioMixed.Size = new System.Drawing.Size(77, 24);
            this.RadioMixed.TabIndex = 3;
            this.RadioMixed.TabStop = true;
            this.RadioMixed.Text = "Karışık";
            this.RadioMixed.UnCheckedColor = System.Drawing.Color.Gray;
            this.RadioMixed.UseVisualStyleBackColor = true;
            // 
            // RadioWrite
            // 
            this.RadioWrite.AutoSize = true;
            this.RadioWrite.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.RadioWrite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RadioWrite.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.RadioWrite.Location = new System.Drawing.Point(10, 88);
            this.RadioWrite.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.RadioWrite.MinimumSize = new System.Drawing.Size(0, 21);
            this.RadioWrite.Name = "RadioWrite";
            this.RadioWrite.Size = new System.Drawing.Size(123, 24);
            this.RadioWrite.TabIndex = 2;
            this.RadioWrite.Text = "Yazması Kolay";
            this.RadioWrite.UnCheckedColor = System.Drawing.Color.Gray;
            this.RadioWrite.UseVisualStyleBackColor = true;
            // 
            // RadioRead
            // 
            this.RadioRead.AutoSize = true;
            this.RadioRead.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.RadioRead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RadioRead.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.RadioRead.Location = new System.Drawing.Point(10, 54);
            this.RadioRead.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.RadioRead.MinimumSize = new System.Drawing.Size(0, 21);
            this.RadioRead.Name = "RadioRead";
            this.RadioRead.Size = new System.Drawing.Size(128, 24);
            this.RadioRead.TabIndex = 1;
            this.RadioRead.Text = "Okuması Kolay";
            this.RadioRead.UnCheckedColor = System.Drawing.Color.Gray;
            this.RadioRead.UseVisualStyleBackColor = true;
            // 
            // Panel_Feature
            // 
            this.Panel_Feature.BackColor = System.Drawing.Color.White;
            this.Panel_Feature.BorderColor = System.Drawing.Color.DodgerBlue;
            this.Panel_Feature.BorderRadius = 5;
            this.Panel_Feature.BorderSize = 0;
            this.Panel_Feature.Controls.Add(this.LabelFeature);
            this.Panel_Feature.Controls.Add(this.CheckSpecialChars);
            this.Panel_Feature.Controls.Add(this.CheckNumeric);
            this.Panel_Feature.Controls.Add(this.CheckLowercase);
            this.Panel_Feature.Controls.Add(this.CheckUppercase);
            this.Panel_Feature.Location = new System.Drawing.Point(10, 68);
            this.Panel_Feature.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Panel_Feature.Name = "Panel_Feature";
            this.Panel_Feature.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_Feature.Size = new System.Drawing.Size(279, 195);
            this.Panel_Feature.TabIndex = 1;
            // 
            // LabelFeature
            // 
            this.LabelFeature.BackColor = System.Drawing.SystemColors.Control;
            this.LabelFeature.BorderRadius = 5;
            this.LabelFeature.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelFeature.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelFeature.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelFeature.Location = new System.Drawing.Point(10, 10);
            this.LabelFeature.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.LabelFeature.Name = "LabelFeature";
            this.LabelFeature.Size = new System.Drawing.Size(259, 35);
            this.LabelFeature.TabIndex = 0;
            this.LabelFeature.Text = "Özellik Seç";
            this.LabelFeature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckSpecialChars
            // 
            this.CheckSpecialChars.AutoSize = true;
            this.CheckSpecialChars.BorderRadius = 2F;
            this.CheckSpecialChars.BorderThickness = 1F;
            this.CheckSpecialChars.Checked = true;
            this.CheckSpecialChars.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.CheckSpecialChars.CheckMarkColor = System.Drawing.Color.White;
            this.CheckSpecialChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckSpecialChars.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckSpecialChars.DrawUncheckedFill = false;
            this.CheckSpecialChars.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckSpecialChars.Location = new System.Drawing.Point(10, 160);
            this.CheckSpecialChars.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.CheckSpecialChars.MaxBorderRadius = 8F;
            this.CheckSpecialChars.MaxBorderThickness = 4F;
            this.CheckSpecialChars.Name = "CheckSpecialChars";
            this.CheckSpecialChars.Size = new System.Drawing.Size(115, 21);
            this.CheckSpecialChars.TabIndex = 4;
            this.CheckSpecialChars.Text = "Özel Karakter";
            this.CheckSpecialChars.UncheckedBackColor = System.Drawing.Color.Transparent;
            this.CheckSpecialChars.UncheckedBorderColor = System.Drawing.Color.Gray;
            this.CheckSpecialChars.UseVisualStyleBackColor = true;
            // 
            // CheckNumeric
            // 
            this.CheckNumeric.AutoSize = true;
            this.CheckNumeric.BorderRadius = 2F;
            this.CheckNumeric.BorderThickness = 1F;
            this.CheckNumeric.Checked = true;
            this.CheckNumeric.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.CheckNumeric.CheckMarkColor = System.Drawing.Color.White;
            this.CheckNumeric.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckNumeric.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckNumeric.DrawUncheckedFill = false;
            this.CheckNumeric.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckNumeric.Location = new System.Drawing.Point(10, 126);
            this.CheckNumeric.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.CheckNumeric.MaxBorderRadius = 8F;
            this.CheckNumeric.MaxBorderThickness = 4F;
            this.CheckNumeric.Name = "CheckNumeric";
            this.CheckNumeric.Size = new System.Drawing.Size(75, 21);
            this.CheckNumeric.TabIndex = 3;
            this.CheckNumeric.Text = "Rakam";
            this.CheckNumeric.UncheckedBackColor = System.Drawing.Color.Transparent;
            this.CheckNumeric.UncheckedBorderColor = System.Drawing.Color.Gray;
            this.CheckNumeric.UseVisualStyleBackColor = true;
            // 
            // CheckLowercase
            // 
            this.CheckLowercase.AutoSize = true;
            this.CheckLowercase.BorderRadius = 2F;
            this.CheckLowercase.BorderThickness = 1F;
            this.CheckLowercase.Checked = true;
            this.CheckLowercase.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.CheckLowercase.CheckMarkColor = System.Drawing.Color.White;
            this.CheckLowercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckLowercase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckLowercase.DrawUncheckedFill = false;
            this.CheckLowercase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckLowercase.Location = new System.Drawing.Point(10, 92);
            this.CheckLowercase.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.CheckLowercase.MaxBorderRadius = 8F;
            this.CheckLowercase.MaxBorderThickness = 4F;
            this.CheckLowercase.Name = "CheckLowercase";
            this.CheckLowercase.Size = new System.Drawing.Size(101, 21);
            this.CheckLowercase.TabIndex = 2;
            this.CheckLowercase.Text = "Küçük Harf";
            this.CheckLowercase.UncheckedBackColor = System.Drawing.Color.Transparent;
            this.CheckLowercase.UncheckedBorderColor = System.Drawing.Color.Gray;
            this.CheckLowercase.UseVisualStyleBackColor = true;
            // 
            // CheckUppercase
            // 
            this.CheckUppercase.AutoSize = true;
            this.CheckUppercase.BorderRadius = 2F;
            this.CheckUppercase.BorderThickness = 1F;
            this.CheckUppercase.Checked = true;
            this.CheckUppercase.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.CheckUppercase.CheckMarkColor = System.Drawing.Color.White;
            this.CheckUppercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckUppercase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckUppercase.DrawUncheckedFill = false;
            this.CheckUppercase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckUppercase.Location = new System.Drawing.Point(10, 58);
            this.CheckUppercase.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.CheckUppercase.MaxBorderRadius = 8F;
            this.CheckUppercase.MaxBorderThickness = 4F;
            this.CheckUppercase.Name = "CheckUppercase";
            this.CheckUppercase.Size = new System.Drawing.Size(102, 21);
            this.CheckUppercase.TabIndex = 1;
            this.CheckUppercase.Text = "Büyük Harf";
            this.CheckUppercase.UncheckedBackColor = System.Drawing.Color.Transparent;
            this.CheckUppercase.UncheckedBorderColor = System.Drawing.Color.Gray;
            this.CheckUppercase.UseVisualStyleBackColor = true;
            // 
            // PassGenLenght
            // 
            this.PassGenLenght.BackColor = System.Drawing.Color.White;
            this.PassGenLenght.BorderRadius = 5;
            this.PassGenLenght.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PassGenLenght.Location = new System.Drawing.Point(10, 321);
            this.PassGenLenght.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.PassGenLenght.Maximum = 48;
            this.PassGenLenght.Minimum = 8;
            this.PassGenLenght.Name = "PassGenLenght";
            this.PassGenLenght.Size = new System.Drawing.Size(564, 45);
            this.PassGenLenght.TabIndex = 4;
            this.PassGenLenght.Text = "tsCustomTrackBar1";
            this.PassGenLenght.ThumbBorderColor = System.Drawing.Color.DimGray;
            this.PassGenLenght.ThumbBorderThickness = 0F;
            this.PassGenLenght.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.PassGenLenght.ThumbHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.PassGenLenght.ThumbPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.PassGenLenght.ThumbRadius = 10F;
            this.PassGenLenght.TrackColor = System.Drawing.Color.LightGray;
            this.PassGenLenght.TrackFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.PassGenLenght.TrackHeight = 8F;
            this.PassGenLenght.TrackRadius = 5F;
            this.PassGenLenght.Value = 15;
            this.PassGenLenght.Vertical = false;
            this.PassGenLenght.ValueChanged += new System.EventHandler(this.PassGenLenght_ValueChanged);
            // 
            // PassResultLabel
            // 
            this.PassResultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PassResultLabel.BackColor = System.Drawing.Color.White;
            this.PassResultLabel.BorderRadius = 5;
            this.PassResultLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.PassResultLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.PassResultLabel.Location = new System.Drawing.Point(10, 374);
            this.PassResultLabel.Margin = new System.Windows.Forms.Padding(3, 3, 5, 5);
            this.PassResultLabel.Name = "PassResultLabel";
            this.PassResultLabel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.PassResultLabel.Size = new System.Drawing.Size(521, 40);
            this.PassResultLabel.TabIndex = 5;
            this.PassResultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnGenPass
            // 
            this.BtnGenPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnGenPass.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnGenPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(111)))), ((int)(((byte)(141)))));
            this.BtnGenPass.BorderRadius = 10;
            this.BtnGenPass.BorderSize = 0;
            this.BtnGenPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGenPass.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnGenPass.FlatAppearance.BorderSize = 0;
            this.BtnGenPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGenPass.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnGenPass.ForeColor = System.Drawing.Color.White;
            this.BtnGenPass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGenPass.Location = new System.Drawing.Point(10, 480);
            this.BtnGenPass.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.BtnGenPass.Name = "BtnGenPass";
            this.BtnGenPass.Size = new System.Drawing.Size(564, 35);
            this.BtnGenPass.TabIndex = 8;
            this.BtnGenPass.Text = "KAYDET";
            this.BtnGenPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGenPass.TextColor = System.Drawing.Color.White;
            this.BtnGenPass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnGenPass.UseVisualStyleBackColor = false;
            this.BtnGenPass.Click += new System.EventHandler(this.BtnGenPass_Click);
            // 
            // PassLenghtLabel
            // 
            this.PassLenghtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PassLenghtLabel.BackColor = System.Drawing.Color.White;
            this.PassLenghtLabel.BorderRadius = 5;
            this.PassLenghtLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.PassLenghtLabel.Location = new System.Drawing.Point(10, 271);
            this.PassLenghtLabel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 5);
            this.PassLenghtLabel.Name = "PassLenghtLabel";
            this.PassLenghtLabel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.PassLenghtLabel.Size = new System.Drawing.Size(564, 42);
            this.PassLenghtLabel.TabIndex = 3;
            this.PassLenghtLabel.Text = "Parola Uzunluğu:";
            this.PassLenghtLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelHeader
            // 
            this.LabelHeader.BackColor = System.Drawing.Color.White;
            this.LabelHeader.BorderRadius = 5;
            this.LabelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelHeader.Location = new System.Drawing.Point(10, 10);
            this.LabelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.LabelHeader.Name = "LabelHeader";
            this.LabelHeader.Size = new System.Drawing.Size(564, 38);
            this.LabelHeader.TabIndex = 0;
            this.LabelHeader.Text = "Parola Oluştur";
            this.LabelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AstelPasswordGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(584, 525);
            this.Controls.Add(this.Panel_BG);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::Astel.Properties.Resources.AstelLogo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AstelPasswordGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AstelPasswordGenerator";
            this.Load += new System.EventHandler(this.AstelPasswordGenerator_Load);
            this.Panel_BG.ResumeLayout(false);
            this.Panel_BG.PerformLayout();
            this.PassGenProBG.ResumeLayout(false);
            this.Panel_Mode.ResumeLayout(false);
            this.Panel_Mode.PerformLayout();
            this.Panel_Feature.ResumeLayout(false);
            this.Panel_Feature.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_BG;
        private TSCustomLabel PassResultLabel;
        private TSCustomPanel Panel_Feature;
        internal TSCustomLabel LabelFeature;
        private TSCustomCheckBox CheckSpecialChars;
        private TSCustomCheckBox CheckNumeric;
        private TSCustomCheckBox CheckLowercase;
        private TSCustomCheckBox CheckUppercase;
        private TSCustomPanel Panel_Mode;
        internal TSCustomLabel LabelMode;
        private TSCustomRadioButton RadioMixed;
        private TSCustomRadioButton RadioWrite;
        private TSCustomRadioButton RadioRead;
        private TSCustomButton BtnGenPass;
        private TSCustomLabel PassLenghtLabel;
        internal TSCustomLabel LabelHeader;
        private TSCustomTrackBar PassGenLenght;
        private TSCustomPanel PassGenProBG;
        private TSCustomLabel PassGenProLabel;
        private TSCustomPanel PassGenProFE;
        private TSCustomButton BtnCopyPassword;
    }
}