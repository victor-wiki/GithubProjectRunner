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
    public partial class frmLanguageSetting : Form
    {
        public Setting Setting { get; set; } = new Setting();

        public List<LanguageSetting> LanguageSettings { get { return this.languageSettings; } }

        private List<LanguageSetting> languageSettings = new List<LanguageSetting>();

        public frmLanguageSetting()
        {
            InitializeComponent();
        }

        private void frmBuildOpenToolSetting_Load(object sender, EventArgs e)
        {
            Config config = ConfigManager.GetConfig();

            foreach (string language in config.Languages)
            {
                LanguageSetting setting = new LanguageSetting() { Language = language };
                LanguageSetting oldSetting = this.Setting.LanguageSettings.FirstOrDefault(item => item.Language == language);
                if (oldSetting != null)
                {
                    setting.AfterAction = oldSetting.AfterAction;
                    setting.BuildToolPath = oldSetting.BuildToolPath;
                    setting.BuildToolArgs = oldSetting.BuildToolArgs;
                    setting.OpenToolPath = oldSetting.OpenToolPath;
                    setting.OpenToolArgs = oldSetting.OpenToolArgs;
                    setting.OpenAsAdmin = oldSetting.OpenAsAdmin;
                    setting.CustomActionType = oldSetting.CustomActionType;
                    setting.CustomActionContent = oldSetting.CustomActionContent;                   
                }

                LanguageInterpreter interpreter = LanguageInterpreterHelper.GetInterpreter(language);

                setting.SolutionFileExtension = StringHelper.GetNotEmptyValue(oldSetting?.SolutionFileExtension, interpreter?.SolutionFileExtension);
                setting.ProjectFileExtension = StringHelper.GetNotEmptyValue(oldSetting?.ProjectFileExtension, interpreter?.ProjectFileExtension);
                setting.ExecutableFileExtension = StringHelper.GetNotEmptyValue(oldSetting?.ExecutableFileExtension, interpreter?.ExecutableFileExtension);

                languageSettings.Add(setting);
            }

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = languageSettings;

            this.AfterAction.DataSource = config.AfterActions;
            this.AfterAction.DisplayMember = "Description";
            this.AfterAction.ValueMember = "Name";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                string language = row.Cells["Language"].Value.ToString();
                LanguageSetting setting = this.languageSettings.FirstOrDefault(item => item.Language == language);
                if (setting != null)
                {
                    setting.AfterAction = row.Cells["AfterAction"].Value?.ToString();
                    setting.BuildToolPath = row.Cells["BuildToolPath"].Value?.ToString();
                    setting.BuildToolArgs = row.Cells["BuildToolArgs"].Value?.ToString();
                    setting.OpenToolPath = row.Cells["OpenToolPath"].Value?.ToString();
                    setting.OpenToolArgs = row.Cells["OpenToolArgs"].Value?.ToString();
                    setting.OpenAsAdmin = row.Cells["OpenAsAdmin"]?.Value?.ToString() == "True";
                    setting.SolutionFileExtension = row.Cells["SolutionFileExtension"]?.Value?.ToString();
                    setting.ProjectFileExtension = row.Cells["ProjectFileExtension"]?.Value?.ToString();
                    setting.ExecutableFileExtension = row.Cells["ExecutableFileExtension"]?.Value?.ToString();
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName;

            if ((columnName == nameof(LanguageSetting.BuildToolPath) || columnName == nameof(LanguageSetting.OpenToolPath)) && e.RowIndex >= 0)
            {
                string path = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    this.openFileDialog1.FileName = path;
                }
                else
                {
                    this.openFileDialog1.FileName = "";
                }

                DialogResult result = this.openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DataGridViewCell cell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Value = this.openFileDialog1.FileName;

                    this.dataGridView1.EndEdit();
                    this.dataGridView1.CurrentCell = null;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "CustomActionContent")
            {
                DataGridViewCell actionCell = this.dataGridView1.Rows[e.RowIndex].Cells["AfterAction"];
                if (actionCell.Value?.ToString() != "Custom")
                {
                    MessageBox.Show("The function is only for custom action.");
                    return;
                }

                string language = this.dataGridView1.Rows[e.RowIndex].Cells["Language"].Value.ToString();

                DataGridViewCell cell = this.dataGridView1.CurrentCell;

                LanguageSetting setting = this.languageSettings[e.RowIndex];

                if (setting == null)
                {
                    setting = new LanguageSetting() { Language = language };
                }

                frmCustomAction frmCustomAction = new frmCustomAction()
                {
                    CustomActionType = setting == null ? CustomActionType.File : setting.CustomActionType,
                    CustomActionContent = setting?.CustomActionContent
                };

                DialogResult result = frmCustomAction.ShowDialog();
                if (result == DialogResult.OK)
                {
                    setting.CustomActionType = frmCustomAction.CustomActionType;
                    setting.CustomActionContent = frmCustomAction.CustomActionContent;
                    cell.Tag = setting;
                }
            }
        }
    }
}
