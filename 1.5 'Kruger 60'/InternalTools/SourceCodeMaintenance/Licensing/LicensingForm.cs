using AlphaCentaury.Tools.SourceCodeMaintenance.Helpers;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public sealed partial class LicensingForm : LicensingFormDocumentView
    {
        private readonly TextBoxOutputWriter _outputWriter;
        private readonly CheckerOptions _checkerOptions;

        public LicensingForm()
        {
            InitializeComponent();
            _outputWriter = new TextBoxOutputWriter(textBoxOutput, timerRefreshOutput, 4);
            _checkerOptions = new CheckerOptions();
        } // constructor

        #region Overrides of LicensingFormDocumentView

        protected override void openStripSplitButton_ButtonClick(object sender, EventArgs e)
        {
            SafeCall(LoadSolutionFolder);
        } // openStripSplitButton_ButtonClick

        protected override void openSolutionFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeCall(LoadSolutionFolder);
        } // openSolutionFolderToolStripMenuItem_Click

        protected override void createStripButton_Click(object sender, EventArgs e)
        {
            ExecuteAsync((token) =>
            {
                var defaultsPath = Path.GetDirectoryName(Application.ExecutablePath);
                return LicensingMaintenance.CreateMissingLicensingFilesAsync(CurrentSolution, _outputWriter, defaultsPath, token);
            });
        } // createStripButton_Click

        protected override void checkStripButton_Click(object sender, EventArgs e)
        {
            ExecuteAsync((token) =>
            {
                var defaultsPath = Path.GetDirectoryName(Application.ExecutablePath);
                return LicensingMaintenance.CheckLicensingFilesAsync(CurrentSolution, _outputWriter, _checkerOptions, defaultsPath, token);
            });
        } // checkStripButton_Click

        protected override void updateStripButton_Click(object sender, EventArgs e)
        {
            ExecuteAsync((token) => LicensingMaintenance.UpdateLicensingFilesAsync(CurrentSolution, _outputWriter, token));
        } // updateStripButton_Click

        protected override void licensingWriteStripButton_Click(object sender, EventArgs e)
        {
            ExecuteAsync((token) => LicensingMaintenance.WriteLicenseFilesAsync(CurrentSolution, _outputWriter, token));
        } // licensingWriteStripButton_Click

        protected override void licensingOptionsStripButton_Click(object sender, EventArgs e)
        {
            using var dlg = new LicensingToolOptionsDialog();
            dlg.CheckerOptions = _checkerOptions;
            dlg.ShowDialog(this);
        } // licensingOptionsStripButton_Click

        #endregion

        protected override void OnVsSolutionSelected(VsSolution solution)
        {
            DetailsMode = DetailsModeEnum.None;

            var prefix = solution.SolutionPath.Length + 1;
            var root = new TreeNode("Licensing files", SolutionImages.Certificate, SolutionImages.Certificate);
            root.Expand();
            foreach (var (licensing, treeNode) in LicensingNodes.OrderBy(ln => ln.Licensing.FilePath))
            {
                var node = new TreeNode(licensing.FilePath.Substring(prefix), treeNode.ImageIndex, treeNode.SelectedImageIndex) { Tag = licensing };
                root.Nodes.Add(node);
            } // foreach

            treeViewDetails.ImageList = imageListSolutionTreeMedium;
            treeViewDetails.Nodes.Add(root);
            DetailsMode = DetailsModeEnum.LicensingDataCollection;
        } // OnVsProjectSelected

        protected override void OnVsProjectSelected(VsProject vsProject)
        {
            DetailsMode = DetailsModeEnum.None;

            treeViewDetails.ImageList = imageListSolutionTreeSmall;
            treeViewDetails.Nodes.Add(LicensingVsUi.GetProjectTree(vsProject, CurrentSolution));
            DetailsMode = DetailsModeEnum.VsProject;
        } // OnVsProjectSelected

        private protected override void OnLicensingNodeSelected(LicensingDataNode node)
        {
            treeViewDetails.Nodes.Clear();
            if (!node.Exists) return;

            treeViewDetails.ImageList = LicensingImageList;
            treeViewDetails.Nodes.Add(LicensingUi.FileToTreeAlt(Path.GetFileName(node.FilePath), node.Value));

            treeViewLicensingData.Nodes.Clear();
            treeViewLicensingData.ImageList = LicensingImageList;
            treeViewLicensingData.Nodes.Add(LicensingUi.FileToTreeAlt(Path.GetFileName(node.FilePath), node.Value));
        } // OnLicensingNodeSelected

        private protected override void OnDetailsLicensingNodeSelected(LicensingDataNode node)
        {
            var details = new StringBuilder();
            details.AppendLine($"File exists: {node.Exists}");
            if (node.Exists)
            {
                var licensing = node.Value;
                details.AppendLine($"{licensing}");

                var thirdParty = licensing.ThirdParty?.Count ?? -1;
                details.AppendLine($"    Third party components: {(thirdParty >= 0 ? thirdParty.ToString(CultureInfo.DefaultThreadCurrentCulture) : "none")}");
                
                if (licensing.Dependencies == null)
                {
                    details.AppendLine("No dependencies");
                }
                else
                {
                    details.AppendLine("Dependencies:");
                    var libraries = licensing.Dependencies.Libraries?.Count ?? -1;
                    details.AppendLine($"    Library dependencies: {(libraries >= 0 ? libraries.ToString(CultureInfo.DefaultThreadCurrentCulture) : "none")}");
                    var dependencies = licensing.Dependencies.ThirdParty?.Count ?? -1;
                    details.AppendLine($"    Third-party dependencies: {(dependencies >= 0 ? dependencies.ToString(CultureInfo.DefaultThreadCurrentCulture) : "none")}");
                } // if-else

                if (licensing.Licenses == null)
                {
                    details.AppendLine("Licenses: none");
                }
                else
                {
                    details.AppendLine($"Licenses: {licensing.Licenses.Count} ");
                    foreach (var license in licensing.Licenses)
                    {
                        details.AppendLine($"    {license.Id}: {license.Name}");
                    } // foreach
                } // if-else
            } // if

            textBoxDetails.Text = details.ToString();
            textBoxDetails.Enabled = true;
        } // OnDetailsLicensingNodeSelected

        private void LoadSolutionFolder()
        {
#if DEBUG
            selectFolderDialog.SelectedPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "..\\..\\..\\.."));
#else
            selectFolderDialog.SelectedPath = Application.ExecutablePath;
#endif
            if (selectFolderDialog.ShowDialog(this) != DialogResult.OK) return;

            LoadSolutionFolderAsync(selectFolderDialog.SelectedPath);
        } // LoadSolutionFolder

        #region

        private void CreateMissingLicensingFiles()
        {
        } // CreateMissingLicensingFiles

        private async void ExecuteAsync(Func<CancellationToken, Task> asyncAction)
        {
            if (CurrentSolution == null) return;

            try
            {
                tabControlSolution.SelectedTab = tabPageOutput;
                _outputWriter.Clear();
                _outputWriter.Start();
                BeginAsyncOperation();

                await asyncAction(GetCancellationToken());
            }
            catch (OperationCanceledException)
            {
                // ignore
            } // catch
            catch (Exception ex)
            {
                _outputWriter.WriteException(ex);
            } // try-catch

            EndAsyncOperation();
            _outputWriter.Stop();
        } // ExecuteAsync

        #endregion
    } // class LicensingForm
} // namespace
