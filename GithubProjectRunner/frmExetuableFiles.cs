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
    public partial class frmExetuableFiles : Form
    {
        private int initColumnWidthExludePath = 0;
        public List<FileInfo> Files { get; set; } = new List<FileInfo>();

        public frmExetuableFiles()
        {
            InitializeComponent();
        }

        public frmExetuableFiles(List<FileInfo> files)
        {
            InitializeComponent();
            this.Files = files;
        }

        private void frmExetuableFiles_Load(object sender, EventArgs e)
        {
            this.InitControls();
        }

        private void InitControls()
        {
            foreach (var file in this.Files)
            {
                ListViewItem li = new ListViewItem();
                li.Tag = file;
                li.Name = file.Name;
                li.Text = file.Name;
                li.SubItems.Add(file.FullName);

                this.lvFiles.Items.Add(li);
            }

            foreach (ColumnHeader col in this.lvFiles.Columns)
            {
                if (col.DisplayIndex != 1)
                {
                    this.initColumnWidthExludePath += col.Width;
                }
            }
        }

        private void tsmiOpenExplorer_Click(object sender, EventArgs e)
        {
            if (this.lvFiles.FocusedItem != null)
            {
                this.OpenInExplorer((this.lvFiles.FocusedItem.Tag as FileInfo)?.FullName);
            }
        }

        private void OpenInExplorer(string filePath)
        {
            if(!string.IsNullOrEmpty(filePath))
            {
                Utility.OpenInExplorer(filePath);
            }           
        }

        private void tsmiRun_Click(object sender, EventArgs e)
        {
            if (this.lvFiles.FocusedItem != null)
            {
                this.Run(this.lvFiles.FocusedItem.Tag as FileInfo);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.lvFiles.FocusedItem != null)
            {
                this.Run(this.lvFiles.FocusedItem.Tag as FileInfo);
            }
            else
            {
                MessageBox.Show("Please select one row.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvFiles_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvFiles.FocusedItem != null)
            {
                this.Run(this.lvFiles.FocusedItem.Tag as FileInfo);
            }
        }

        private void Run(FileInfo file)
        {
            if (file != null)
            {
                ProcessHelper.StartFileByCmd(file.FullName);
            }
        }

        private void lvFiles_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lvFiles.FocusedItem.Bounds.Contains(e.Location))
                {
                    this.contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void frmExetuableFiles_SizeChanged(object sender, EventArgs e)
        {
            this.lvFiles.Columns[1].Width = this.lvFiles.Width - this.initColumnWidthExludePath - 10;
        }
    }
}
