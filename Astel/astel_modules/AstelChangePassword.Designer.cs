namespace Astel.astel_modules
{
    partial class AstelChangePassword
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
            this.CheckPassword = new Astel.TSCustomCheckBox();
            this.LabelNewPasswordRepeat = new System.Windows.Forms.Label();
            this.TxtNewPasswordRepeat = new System.Windows.Forms.TextBox();
            this.LabelNewPassword = new System.Windows.Forms.Label();
            this.TxtNewPassword = new System.Windows.Forms.TextBox();
            this.LabelCurrentPassword = new System.Windows.Forms.Label();
            this.TxtCurrentPassword = new System.Windows.Forms.TextBox();
            this.BtnChangePassword = new Astel.TSCustomButton();
            this.LabelHeader = new Astel.TSCustomLabel();
            this.Panel_BG.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_BG
            // 
            this.Panel_BG.BackColor = System.Drawing.Color.Transparent;
            this.Panel_BG.Controls.Add(this.CheckPassword);
            this.Panel_BG.Controls.Add(this.LabelNewPasswordRepeat);
            this.Panel_BG.Controls.Add(this.TxtNewPasswordRepeat);
            this.Panel_BG.Controls.Add(this.LabelNewPassword);
            this.Panel_BG.Controls.Add(this.TxtNewPassword);
            this.Panel_BG.Controls.Add(this.LabelCurrentPassword);
            this.Panel_BG.Controls.Add(this.TxtCurrentPassword);
            this.Panel_BG.Controls.Add(this.BtnChangePassword);
            this.Panel_BG.Controls.Add(this.LabelHeader);
            this.Panel_BG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_BG.Location = new System.Drawing.Point(0, 0);
            this.Panel_BG.Name = "Panel_BG";
            this.Panel_BG.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_BG.Size = new System.Drawing.Size(434, 356);
            this.Panel_BG.TabIndex = 0;
            // 
            // CheckPassword
            // 
            this.CheckPassword.AutoSize = true;
            this.CheckPassword.BorderRadius = 2F;
            this.CheckPassword.BorderThickness = 1F;
            this.CheckPassword.CheckedColor = System.Drawing.Color.DodgerBlue;
            this.CheckPassword.CheckMarkColor = System.Drawing.Color.White;
            this.CheckPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckPassword.DrawUncheckedFill = false;
            this.CheckPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.CheckPassword.Location = new System.Drawing.Point(10, 250);
            this.CheckPassword.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.CheckPassword.MaxBorderRadius = 8F;
            this.CheckPassword.MaxBorderThickness = 4F;
            this.CheckPassword.Name = "CheckPassword";
            this.CheckPassword.Size = new System.Drawing.Size(114, 21);
            this.CheckPassword.TabIndex = 7;
            this.CheckPassword.Text = "Şifreyi Göster";
            this.CheckPassword.UncheckedBackColor = System.Drawing.Color.Transparent;
            this.CheckPassword.UncheckedBorderColor = System.Drawing.Color.Gray;
            this.CheckPassword.UseVisualStyleBackColor = true;
            this.CheckPassword.CheckedChanged += new System.EventHandler(this.CheckPassword_CheckedChanged);
            // 
            // LabelNewPasswordRepeat
            // 
            this.LabelNewPasswordRepeat.AutoSize = true;
            this.LabelNewPasswordRepeat.BackColor = System.Drawing.Color.Transparent;
            this.LabelNewPasswordRepeat.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelNewPasswordRepeat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelNewPasswordRepeat.Location = new System.Drawing.Point(7, 193);
            this.LabelNewPasswordRepeat.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelNewPasswordRepeat.Name = "LabelNewPasswordRepeat";
            this.LabelNewPasswordRepeat.Size = new System.Drawing.Size(114, 19);
            this.LabelNewPasswordRepeat.TabIndex = 5;
            this.LabelNewPasswordRepeat.Text = "Yeni Şifre Tekrar:";
            // 
            // TxtNewPasswordRepeat
            // 
            this.TxtNewPasswordRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtNewPasswordRepeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNewPasswordRepeat.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtNewPasswordRepeat.Location = new System.Drawing.Point(10, 215);
            this.TxtNewPasswordRepeat.Margin = new System.Windows.Forms.Padding(3, 3, 3, 7);
            this.TxtNewPasswordRepeat.MaxLength = 128;
            this.TxtNewPasswordRepeat.Name = "TxtNewPasswordRepeat";
            this.TxtNewPasswordRepeat.Size = new System.Drawing.Size(414, 25);
            this.TxtNewPasswordRepeat.TabIndex = 6;
            // 
            // LabelNewPassword
            // 
            this.LabelNewPassword.AutoSize = true;
            this.LabelNewPassword.BackColor = System.Drawing.Color.Transparent;
            this.LabelNewPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelNewPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelNewPassword.Location = new System.Drawing.Point(7, 134);
            this.LabelNewPassword.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelNewPassword.Name = "LabelNewPassword";
            this.LabelNewPassword.Size = new System.Drawing.Size(71, 19);
            this.LabelNewPassword.TabIndex = 3;
            this.LabelNewPassword.Text = "Yeni Şifre:";
            // 
            // TxtNewPassword
            // 
            this.TxtNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNewPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtNewPassword.Location = new System.Drawing.Point(10, 156);
            this.TxtNewPassword.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtNewPassword.MaxLength = 128;
            this.TxtNewPassword.Name = "TxtNewPassword";
            this.TxtNewPassword.Size = new System.Drawing.Size(414, 25);
            this.TxtNewPassword.TabIndex = 4;
            // 
            // LabelCurrentPassword
            // 
            this.LabelCurrentPassword.AutoSize = true;
            this.LabelCurrentPassword.BackColor = System.Drawing.Color.Transparent;
            this.LabelCurrentPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelCurrentPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelCurrentPassword.Location = new System.Drawing.Point(7, 75);
            this.LabelCurrentPassword.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelCurrentPassword.Name = "LabelCurrentPassword";
            this.LabelCurrentPassword.Size = new System.Drawing.Size(92, 19);
            this.LabelCurrentPassword.TabIndex = 1;
            this.LabelCurrentPassword.Text = "Mevcut Şifre:";
            // 
            // TxtCurrentPassword
            // 
            this.TxtCurrentPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtCurrentPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCurrentPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtCurrentPassword.Location = new System.Drawing.Point(10, 97);
            this.TxtCurrentPassword.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TxtCurrentPassword.MaxLength = 128;
            this.TxtCurrentPassword.Name = "TxtCurrentPassword";
            this.TxtCurrentPassword.Size = new System.Drawing.Size(414, 25);
            this.TxtCurrentPassword.TabIndex = 2;
            // 
            // BtnChangePassword
            // 
            this.BtnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnChangePassword.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnChangePassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnChangePassword.BorderRadius = 10;
            this.BtnChangePassword.BorderSize = 0;
            this.BtnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnChangePassword.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnChangePassword.FlatAppearance.BorderSize = 0;
            this.BtnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnChangePassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnChangePassword.ForeColor = System.Drawing.Color.White;
            this.BtnChangePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnChangePassword.Location = new System.Drawing.Point(10, 311);
            this.BtnChangePassword.Name = "BtnChangePassword";
            this.BtnChangePassword.Size = new System.Drawing.Size(414, 35);
            this.BtnChangePassword.TabIndex = 8;
            this.BtnChangePassword.Text = "KAYDET";
            this.BtnChangePassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnChangePassword.TextColor = System.Drawing.Color.White;
            this.BtnChangePassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnChangePassword.UseVisualStyleBackColor = false;
            this.BtnChangePassword.Click += new System.EventHandler(this.BtnChangePassword_Click);
            // 
            // LabelHeader
            // 
            this.LabelHeader.BackColor = System.Drawing.Color.White;
            this.LabelHeader.BorderRadius = 5;
            this.LabelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelHeader.Location = new System.Drawing.Point(10, 10);
            this.LabelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 30);
            this.LabelHeader.Name = "LabelHeader";
            this.LabelHeader.Size = new System.Drawing.Size(414, 35);
            this.LabelHeader.TabIndex = 0;
            this.LabelHeader.Text = "PAROLA DEĞİŞTİR";
            this.LabelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AstelChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(434, 356);
            this.Controls.Add(this.Panel_BG);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::Astel.Properties.Resources.AstelLogo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AstelChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AstelChangePassword";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AspChangePassword_FormClosing);
            this.Load += new System.EventHandler(this.AstelChangePassword_Load);
            this.Panel_BG.ResumeLayout(false);
            this.Panel_BG.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_BG;
        private TSCustomCheckBox CheckPassword;
        internal System.Windows.Forms.Label LabelNewPasswordRepeat;
        private System.Windows.Forms.TextBox TxtNewPasswordRepeat;
        internal System.Windows.Forms.Label LabelNewPassword;
        private System.Windows.Forms.TextBox TxtNewPassword;
        internal System.Windows.Forms.Label LabelCurrentPassword;
        private System.Windows.Forms.TextBox TxtCurrentPassword;
        private TSCustomButton BtnChangePassword;
        internal TSCustomLabel LabelHeader;
    }
}