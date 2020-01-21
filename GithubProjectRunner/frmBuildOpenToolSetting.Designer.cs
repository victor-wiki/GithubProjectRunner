namespace GithubProjectRunner
{
    partial class frmBuildOpenToolSetting
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblTip = new System.Windows.Forms.Label();
            this.Language = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AfterAction = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BuildToolPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuildToolArgs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenToolPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenToolArgs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenAsAdmin = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(1149, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(1068, 361);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Language,
            this.AfterAction,
            this.BuildToolPath,
            this.BuildToolArgs,
            this.OpenToolPath,
            this.OpenToolArgs,
            this.OpenAsAdmin});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1224, 355);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "exe file|*.exe|all files|*.*";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // lblTip
            // 
            this.lblTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTip.Location = new System.Drawing.Point(12, 372);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(455, 14);
            this.lblTip.TabIndex = 8;
            this.lblTip.Text = "Tip: It cell can show file choosing dialog if double click cell.";
            // 
            // Language
            // 
            this.Language.DataPropertyName = "Language";
            this.Language.Frozen = true;
            this.Language.HeaderText = "Language";
            this.Language.Name = "Language";
            this.Language.ReadOnly = true;
            // 
            // AfterAction
            // 
            this.AfterAction.DataPropertyName = "AfterAction";
            this.AfterAction.DropDownWidth = 150;
            this.AfterAction.FillWeight = 140F;
            this.AfterAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AfterAction.HeaderText = "Action";
            this.AfterAction.Name = "AfterAction";
            // 
            // BuildToolPath
            // 
            this.BuildToolPath.DataPropertyName = "BuildToolPath";
            this.BuildToolPath.HeaderText = "Build tool path";
            this.BuildToolPath.Name = "BuildToolPath";
            this.BuildToolPath.Width = 350;
            // 
            // BuildToolArgs
            // 
            this.BuildToolArgs.DataPropertyName = "BuildToolArgs";
            this.BuildToolArgs.HeaderText = "Build tool args";
            this.BuildToolArgs.Name = "BuildToolArgs";
            this.BuildToolArgs.Width = 150;
            // 
            // OpenToolPath
            // 
            this.OpenToolPath.DataPropertyName = "OpenToolPath";
            this.OpenToolPath.HeaderText = "Open tool path";
            this.OpenToolPath.Name = "OpenToolPath";
            this.OpenToolPath.Width = 350;
            // 
            // OpenToolArgs
            // 
            this.OpenToolArgs.DataPropertyName = "OpenToolArgs";
            this.OpenToolArgs.HeaderText = "Open tool args";
            this.OpenToolArgs.Name = "OpenToolArgs";
            this.OpenToolArgs.Width = 150;
            // 
            // OpenAsAdmin
            // 
            this.OpenAsAdmin.DataPropertyName = "OpenAsAdmin";
            this.OpenAsAdmin.HeaderText = "Open as admin";
            this.OpenAsAdmin.Name = "OpenAsAdmin";
            this.OpenAsAdmin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OpenAsAdmin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OpenAsAdmin.Width = 120;
            // 
            // frmBuildOpenToolSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 396);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "frmBuildOpenToolSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Build and open tool setting";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBuildOpenToolSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Language;
        private System.Windows.Forms.DataGridViewComboBoxColumn AfterAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuildToolPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuildToolArgs;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenToolPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenToolArgs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OpenAsAdmin;
    }
}