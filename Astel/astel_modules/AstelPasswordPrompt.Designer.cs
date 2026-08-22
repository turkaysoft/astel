namespace Astel.astel_modules
{
    partial class AstelPasswordPrompt
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
            this.LabelPassword = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.BtnUnlock = new Astel.TSCustomButton();
            this.LabelHeader = new Astel.TSCustomLabel();
            this.Panel_BG.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_BG
            // 
            this.Panel_BG.BackColor = System.Drawing.Color.Transparent;
            this.Panel_BG.Controls.Add(this.CheckPassword);
            this.Panel_BG.Controls.Add(this.LabelPassword);
            this.Panel_BG.Controls.Add(this.TxtPassword);
            this.Panel_BG.Controls.Add(this.BtnUnlock);
            this.Panel_BG.Controls.Add(this.LabelHeader);
            this.Panel_BG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_BG.Location = new System.Drawing.Point(0, 0);
            this.Panel_BG.Name = "Panel_BG";
            this.Panel_BG.Padding = new System.Windows.Forms.Padding(10);
            this.Panel_BG.Size = new System.Drawing.Size(434, 241);
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
            this.CheckPassword.Location = new System.Drawing.Point(10, 132);
            this.CheckPassword.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.CheckPassword.MaxBorderRadius = 8F;
            this.CheckPassword.MaxBorderThickness = 4F;
            this.CheckPassword.Name = "CheckPassword";
            this.CheckPassword.Size = new System.Drawing.Size(114, 21);
            this.CheckPassword.TabIndex = 3;
            this.CheckPassword.Text = "Show Password";
            this.CheckPassword.UncheckedBackColor = System.Drawing.Color.Transparent;
            this.CheckPassword.UncheckedBorderColor = System.Drawing.Color.Gray;
            this.CheckPassword.UseVisualStyleBackColor = true;
            this.CheckPassword.CheckedChanged += new System.EventHandler(this.CheckPassword_CheckedChanged);
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.BackColor = System.Drawing.Color.Transparent;
            this.LabelPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.LabelPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LabelPassword.Location = new System.Drawing.Point(7, 75);
            this.LabelPassword.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(70, 19);
            this.LabelPassword.TabIndex = 1;
            this.LabelPassword.Text = "Password:";
            // 
            // TxtPassword
            // 
            this.TxtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TxtPassword.Location = new System.Drawing.Point(10, 97);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(3, 3, 3, 7);
            this.TxtPassword.MaxLength = 128;
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(414, 25);
            this.TxtPassword.TabIndex = 2;
            // 
            // BtnUnlock
            // 
            this.BtnUnlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnUnlock.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnUnlock.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(122)))), ((int)(((byte)(25)))));
            this.BtnUnlock.BorderRadius = 10;
            this.BtnUnlock.BorderSize = 0;
            this.BtnUnlock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnUnlock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnUnlock.FlatAppearance.BorderSize = 0;
            this.BtnUnlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUnlock.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnUnlock.ForeColor = System.Drawing.Color.White;
            this.BtnUnlock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnUnlock.Location = new System.Drawing.Point(10, 196);
            this.BtnUnlock.Name = "BtnUnlock";
            this.BtnUnlock.Size = new System.Drawing.Size(414, 35);
            this.BtnUnlock.TabIndex = 4;
            this.BtnUnlock.Text = "UNLOCK";
            this.BtnUnlock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnUnlock.TextColor = System.Drawing.Color.White;
            this.BtnUnlock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnUnlock.UseVisualStyleBackColor = false;
            this.BtnUnlock.Click += new System.EventHandler(this.BtnUnlock_Click);
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
            this.LabelHeader.Text = "UNLOCK VAULT";
            this.LabelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AstelPasswordPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(434, 241);
            this.Controls.Add(this.Panel_BG);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::Astel.Properties.Resources.AstelLogo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AstelPasswordPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AstelPasswordPrompt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AstelPasswordPrompt_FormClosing);
            this.Load += new System.EventHandler(this.AstelPasswordPrompt_Load);
            this.Panel_BG.ResumeLayout(false);
            this.Panel_BG.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_BG;
        private TSCustomCheckBox CheckPassword;
        internal System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.TextBox TxtPassword;
        private TSCustomButton BtnUnlock;
        internal TSCustomLabel LabelHeader;
    }
}