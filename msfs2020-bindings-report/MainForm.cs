using FSControls.Classes;

namespace FSControls
{
    public partial class MainForm : Form
    {
        public enum FocusControl { btnBasePath, btnProcessFolders }

        public MainLogic Logic { get; }

        public MainForm()
        {
            InitializeComponent();
            Logic = new MainLogic(this);
            //Developer option: uncomment the following line to rebuild the list of "Known" bindings
            //It will output the new file in C:\Temp\KnownBindings.xml. You will need to manually move
            //to correct program location
            //btnRebuild.Visible = true;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Logic.GetDefaultPath())
            {
                //if able to determine the path, automatically process it and set focus to list
                btnProcessFolders.PerformClick();
                clbMappings.Focus();
            }
            else
            {
                btnBasePath.Focus();
            }

            txtOutputFile.Text = Logic.GetDefaultOutputFile();
        }

        private void btnBasePath_Click(object sender, EventArgs e)
        {
            Logic.SelectBasePath(txtBasePath, fbdBasePath);
        }

        private void btnProcessFolders_Click(object sender, EventArgs e)
        {
            Logic.ProcessFolders(txtBasePath, clbMappings);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var contentMode = (ContentMode)cmbContent.SelectedIndex;
            Logic.GenerateMappingList(clbMappings, contentMode);
        }

        private void btnRebuild_Click(object sender, EventArgs e)
        {
            Logic.RebuildKnownBindings(clbMappings);
        }

        public void StartProgress(string message, int progressMax)
        {
            tsStatus.Text = message;
            tsProgress.Maximum = progressMax;
            tsProgress.Visible = true;
            Application.DoEvents();
        }

        public void UpdateProgress(string? message = null, int? progress = null)
        {
            if (!string.IsNullOrEmpty(message))
            {
                tsStatus.Text = message;
            }

            if (progress.HasValue)
            {
                tsProgress.Value = progress.Value;
            }
            Application.DoEvents();
        }

        public void StopProgress(string message)
        {
            tsStatus.Text = message;
            tsProgress.Visible = false;
            Application.DoEvents();
        }
    }
}
