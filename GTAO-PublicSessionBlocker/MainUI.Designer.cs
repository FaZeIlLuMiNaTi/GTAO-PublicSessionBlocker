﻿namespace GTAO_PublicSessionBlocker
{
    partial class GTAOPSBMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTAOPSBMain));
            this.BtnBind = new System.Windows.Forms.Button();
            this.CmbKey = new System.Windows.Forms.ComboBox();
            this.BtnSuspend = new System.Windows.Forms.Button();
            this.BtnResume = new System.Windows.Forms.Button();
            this.ChkBlockPort = new System.Windows.Forms.CheckBox();
            this.ChkTimerMode = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LblKeyToBind = new System.Windows.Forms.Label();
            this.LblSettings = new System.Windows.Forms.Label();
            this.LblControls = new System.Windows.Forms.Label();
            this.LblGameToTarget = new System.Windows.Forms.Label();
            this.CmbTarget = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnBind
            // 
            this.BtnBind.Location = new System.Drawing.Point(142, 150);
            this.BtnBind.Name = "BtnBind";
            this.BtnBind.Size = new System.Drawing.Size(94, 23);
            this.BtnBind.TabIndex = 0;
            this.BtnBind.Text = "Bind";
            this.BtnBind.UseVisualStyleBackColor = true;
            this.BtnBind.Click += new System.EventHandler(this.BtnBind_Click);
            // 
            // CmbKey
            // 
            this.CmbKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbKey.FormattingEnabled = true;
            this.CmbKey.Location = new System.Drawing.Point(42, 152);
            this.CmbKey.Name = "CmbKey";
            this.CmbKey.Size = new System.Drawing.Size(94, 21);
            this.CmbKey.TabIndex = 1;
            this.CmbKey.SelectedIndexChanged += new System.EventHandler(this.CmbKey_SelectedIndexChanged);
            // 
            // BtnSuspend
            // 
            this.BtnSuspend.Location = new System.Drawing.Point(42, 301);
            this.BtnSuspend.Name = "BtnSuspend";
            this.BtnSuspend.Size = new System.Drawing.Size(94, 23);
            this.BtnSuspend.TabIndex = 2;
            this.BtnSuspend.Text = "Suspend";
            this.BtnSuspend.UseVisualStyleBackColor = true;
            this.BtnSuspend.Click += new System.EventHandler(this.BtnSuspend_Click);
            // 
            // BtnResume
            // 
            this.BtnResume.Location = new System.Drawing.Point(142, 301);
            this.BtnResume.Name = "BtnResume";
            this.BtnResume.Size = new System.Drawing.Size(94, 23);
            this.BtnResume.TabIndex = 3;
            this.BtnResume.Text = "Resume";
            this.BtnResume.UseVisualStyleBackColor = true;
            this.BtnResume.Click += new System.EventHandler(this.BtnResume_Click);
            // 
            // ChkBlockPort
            // 
            this.ChkBlockPort.AutoSize = true;
            this.ChkBlockPort.Location = new System.Drawing.Point(76, 244);
            this.ChkBlockPort.Name = "ChkBlockPort";
            this.ChkBlockPort.Size = new System.Drawing.Size(133, 17);
            this.ChkBlockPort.TabIndex = 4;
            this.ChkBlockPort.Text = "Block GTA Online Port";
            this.ChkBlockPort.UseVisualStyleBackColor = true;
            this.ChkBlockPort.CheckedChanged += new System.EventHandler(this.ChkBlockPort_CheckedChanged);
            // 
            // ChkTimerMode
            // 
            this.ChkTimerMode.AutoSize = true;
            this.ChkTimerMode.Location = new System.Drawing.Point(76, 221);
            this.ChkTimerMode.Name = "ChkTimerMode";
            this.ChkTimerMode.Size = new System.Drawing.Size(104, 17);
            this.ChkTimerMode.TabIndex = 5;
            this.ChkTimerMode.Text = "Use Timer Mode";
            this.ChkTimerMode.UseVisualStyleBackColor = true;
            this.ChkTimerMode.CheckedChanged += new System.EventHandler(this.ChkTimerMode_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check for updates";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // LblKeyToBind
            // 
            this.LblKeyToBind.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblKeyToBind.Location = new System.Drawing.Point(82, 111);
            this.LblKeyToBind.Name = "LblKeyToBind";
            this.LblKeyToBind.Size = new System.Drawing.Size(121, 26);
            this.LblKeyToBind.TabIndex = 0;
            this.LblKeyToBind.Text = "Key to bind";
            // 
            // LblSettings
            // 
            this.LblSettings.AutoSize = true;
            this.LblSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.LblSettings.Location = new System.Drawing.Point(97, 187);
            this.LblSettings.Name = "LblSettings";
            this.LblSettings.Size = new System.Drawing.Size(90, 25);
            this.LblSettings.TabIndex = 7;
            this.LblSettings.Text = "Settings";
            // 
            // LblControls
            // 
            this.LblControls.AutoSize = true;
            this.LblControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.LblControls.Location = new System.Drawing.Point(96, 269);
            this.LblControls.Name = "LblControls";
            this.LblControls.Size = new System.Drawing.Size(92, 25);
            this.LblControls.TabIndex = 8;
            this.LblControls.Text = "Controls";
            // 
            // LblGameToTarget
            // 
            this.LblGameToTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblGameToTarget.Location = new System.Drawing.Point(65, 37);
            this.LblGameToTarget.Name = "LblGameToTarget";
            this.LblGameToTarget.Size = new System.Drawing.Size(154, 26);
            this.LblGameToTarget.TabIndex = 9;
            this.LblGameToTarget.Text = "Game to target";
            // 
            // CmbTarget
            // 
            this.CmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTarget.FormattingEnabled = true;
            this.CmbTarget.Items.AddRange(new object[] {
            "Grand Theft Auto V",
            "Red Dead Redemption 2"});
            this.CmbTarget.Location = new System.Drawing.Point(68, 75);
            this.CmbTarget.Name = "CmbTarget";
            this.CmbTarget.Size = new System.Drawing.Size(149, 21);
            this.CmbTarget.TabIndex = 10;
            this.CmbTarget.SelectedIndexChanged += new System.EventHandler(this.CmbTarget_SelectedIndexChanged);
            // 
            // GTAOPSBMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 336);
            this.Controls.Add(this.CmbTarget);
            this.Controls.Add(this.LblGameToTarget);
            this.Controls.Add(this.LblControls);
            this.Controls.Add(this.ChkTimerMode);
            this.Controls.Add(this.ChkBlockPort);
            this.Controls.Add(this.LblSettings);
            this.Controls.Add(this.LblKeyToBind);
            this.Controls.Add(this.BtnResume);
            this.Controls.Add(this.BtnSuspend);
            this.Controls.Add(this.CmbKey);
            this.Controls.Add(this.BtnBind);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "GTAOPSBMain";
            this.Text = "GTAO-PublicSessionBlocker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GTAOPSBMain_FormClosing);
            this.Load += new System.EventHandler(this.GTAOPSBMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnBind;
        private System.Windows.Forms.ComboBox CmbKey;
        private System.Windows.Forms.Button BtnSuspend;
        private System.Windows.Forms.Button BtnResume;
        private System.Windows.Forms.CheckBox ChkBlockPort;
        private System.Windows.Forms.CheckBox ChkTimerMode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label LblKeyToBind;
        private System.Windows.Forms.Label LblSettings;
        private System.Windows.Forms.Label LblControls;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.Label LblGameToTarget;
        private System.Windows.Forms.ComboBox CmbTarget;
    }
}

