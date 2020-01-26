namespace GithubProjectRunner
{
    partial class frmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDownloadFolder = new System.Windows.Forms.TextBox();
            this.btnOpenDownloadFolder = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkEnableDebug = new System.Windows.Forms.CheckBox();
            this.chkEnableLog = new System.Windows.Forms.CheckBox();
            this.dlgDownloadFileSaveFlolder = new System.Windows.Forms.FolderBrowserDialog();
            this.chkAlwaysGetLatestVersion = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuildRunTool = new System.Windows.Forms.Button();
            this.btnOpenGitWorkFolder = new System.Windows.Forms.Button();
            this.txtGitWorkFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dlgGitProjectWorkFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHomepage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Download file save folder:";
            // 
            // txtDownloadFolder
            // 
            this.txtDownloadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDownloadFolder.Location = new System.Drawing.Point(180, 37);
            this.txtDownloadFolder.Name = "txtDownloadFolder";
            this.txtDownloadFolder.Size = new System.Drawing.Size(416, 21);
            this.txtDownloadFolder.TabIndex = 3;
            // 
            // btnOpenDownloadFolder
            // 
            this.btnOpenDownloadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDownloadFolder.Location = new System.Drawing.Point(602, 36);
            this.btnOpenDownloadFolder.Name = "btnOpenDownloadFolder";
            this.btnOpenDownloadFolder.Size = new System.Drawing.Size(36, 23);
            this.btnOpenDownloadFolder.TabIndex = 4;
            this.btnOpenDownloadFolder.Text = "...";
            this.btnOpenDownloadFolder.UseVisualStyleBackColor = true;
            this.btnOpenDownloadFolder.Click += new System.EventHandler(this.btnOpenDownloadFolder_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(474, 273);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(555, 273);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkEnableDebug);
            this.groupBox2.Controls.Add(this.chkEnableLog);
            this.groupBox2.Location = new System.Drawing.Point(12, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(621, 67);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // chkEnableDebug
            // 
            this.chkEnableDebug.AutoSize = true;
            this.chkEnableDebug.Checked = true;
            this.chkEnableDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableDebug.Location = new System.Drawing.Point(123, 30);
            this.chkEnableDebug.Name = "chkEnableDebug";
            this.chkEnableDebug.Size = new System.Drawing.Size(96, 16);
            this.chkEnableDebug.TabIndex = 12;
            this.chkEnableDebug.Text = "Enable debug";
            this.chkEnableDebug.UseVisualStyleBackColor = true;
            // 
            // chkEnableLog
            // 
            this.chkEnableLog.AutoSize = true;
            this.chkEnableLog.Checked = true;
            this.chkEnableLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableLog.Location = new System.Drawing.Point(12, 31);
            this.chkEnableLog.Name = "chkEnableLog";
            this.chkEnableLog.Size = new System.Drawing.Size(84, 16);
            this.chkEnableLog.TabIndex = 11;
            this.chkEnableLog.Text = "Enable log";
            this.chkEnableLog.UseVisualStyleBackColor = true;
            // 
            // chkAlwaysGetLatestVersion
            // 
            this.chkAlwaysGetLatestVersion.AutoSize = true;
            this.chkAlwaysGetLatestVersion.Checked = true;
            this.chkAlwaysGetLatestVersion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlwaysGetLatestVersion.Location = new System.Drawing.Point(15, 98);
            this.chkAlwaysGetLatestVersion.Name = "chkAlwaysGetLatestVersion";
            this.chkAlwaysGetLatestVersion.Size = new System.Drawing.Size(174, 16);
            this.chkAlwaysGetLatestVersion.TabIndex = 8;
            this.chkAlwaysGetLatestVersion.Text = "Always get latest version";
            this.chkAlwaysGetLatestVersion.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Build and open tool:";
            // 
            // btnBuildRunTool
            // 
            this.btnBuildRunTool.Location = new System.Drawing.Point(137, 128);
            this.btnBuildRunTool.Name = "btnBuildRunTool";
            this.btnBuildRunTool.Size = new System.Drawing.Size(75, 23);
            this.btnBuildRunTool.TabIndex = 10;
            this.btnBuildRunTool.Text = "Config";
            this.btnBuildRunTool.UseVisualStyleBackColor = true;
            this.btnBuildRunTool.Click += new System.EventHandler(this.btnBuildRunTool_Click);
            // 
            // btnOpenGitWorkFolder
            // 
            this.btnOpenGitWorkFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenGitWorkFolder.Location = new System.Drawing.Point(602, 63);
            this.btnOpenGitWorkFolder.Name = "btnOpenGitWorkFolder";
            this.btnOpenGitWorkFolder.Size = new System.Drawing.Size(36, 23);
            this.btnOpenGitWorkFolder.TabIndex = 7;
            this.btnOpenGitWorkFolder.Text = "...";
            this.btnOpenGitWorkFolder.UseVisualStyleBackColor = true;
            this.btnOpenGitWorkFolder.Click += new System.EventHandler(this.btnOpenGitWorkFolder_Click);
            // 
            // txtGitWorkFolder
            // 
            this.txtGitWorkFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGitWorkFolder.Location = new System.Drawing.Point(180, 64);
            this.txtGitWorkFolder.Name = "txtGitWorkFolder";
            this.txtGitWorkFolder.Size = new System.Drawing.Size(416, 21);
            this.txtGitWorkFolder.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Git project work folder:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Homepage of github:";
            // 
            // txtHomepage
            // 
            this.txtHomepage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHomepage.Location = new System.Drawing.Point(180, 10);
            this.txtHomepage.Name = "txtHomepage";
            this.txtHomepage.Size = new System.Drawing.Size(416, 21);
            this.txtHomepage.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "Preferred language:";
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(137, 164);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(75, 20);
            this.cboLanguage.TabIndex = 16;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 308);
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHomepage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOpenGitWorkFolder);
            this.Controls.Add(this.txtGitWorkFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBuildRunTool);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkAlwaysGetLatestVersion);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOpenDownloadFolder);
            this.Controls.Add(this.txtDownloadFolder);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDownloadFolder;
        private System.Windows.Forms.Button btnOpenDownloadFolder;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkEnableLog;
        private System.Windows.Forms.CheckBox chkEnableDebug;
        private System.Windows.Forms.FolderBrowserDialog dlgDownloadFileSaveFlolder;
        private System.Windows.Forms.CheckBox chkAlwaysGetLatestVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuildRunTool;
        private System.Windows.Forms.Button btnOpenGitWorkFolder;
        private System.Windows.Forms.TextBox txtGitWorkFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog dlgGitProjectWorkFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHomepage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboLanguage;
    }
}