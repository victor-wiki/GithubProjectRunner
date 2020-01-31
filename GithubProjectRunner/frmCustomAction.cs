using GithubProjectHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GithubProjectRunner
{
    public partial class frmCustomAction : Form
    {        
        public CustomActionType CustomActionType { get; set; }
        public string CustomActionContent { get; set; }

        public frmCustomAction()
        {
            InitializeComponent();           
        }

        private void frmCustomAction_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(this.CustomActionContent))
            {
                if(this.CustomActionType==CustomActionType.File)
                {
                    this.rbFile.Checked = true;
                    this.txtActionFile.Text = this.CustomActionContent;
                }
                else if(this.CustomActionType==CustomActionType.Text)
                {
                    this.rbText.Checked = true;
                    this.txtActionText.Text = this.CustomActionContent;
                }
            }

            this.SetControlState();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(this.rbFile.Checked)
            {
                this.CustomActionType = CustomActionType.File;
                this.CustomActionContent = this.txtActionFile.Text;
            }
            else if(this.rbText.Checked)
            {
                this.CustomActionType = CustomActionType.Text;
                this.CustomActionContent = this.txtActionText.Text;
            }

            if(string.IsNullOrEmpty(this.CustomActionContent))
            {
                if (this.rbFile.Checked)
                {
                    MessageBox.Show("The action file can't be empty.");
                }
                else if (this.rbText.Checked)
                {
                    MessageBox.Show("The action text can't be empty.");
                }
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActionFile_Click(object sender, EventArgs e)
        {
            if(this.rbFile.Checked && !string.IsNullOrEmpty(this.txtActionFile.Text) && File.Exists(this.CustomActionContent))
            {
                this.openFileDialog1.FileName = this.CustomActionContent;                
            }
            else
            {
                this.openFileDialog1.FileName = "";
            }

            DialogResult result = this.openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.txtActionFile.Text = this.openFileDialog1.FileName;
            }
        }

        private void SetControlState()
        {
            this.txtActionFile.Enabled = this.rbFile.Checked;
            this.btnActionFile.Enabled = this.rbFile.Checked;
            this.txtActionText.Enabled = this.rbText.Checked;
        }

        private void rbFile_CheckedChanged(object sender, EventArgs e)
        {
            this.SetControlState();
        }

        private void rbText_CheckedChanged(object sender, EventArgs e)
        {
            this.SetControlState();
        }
    }
}
