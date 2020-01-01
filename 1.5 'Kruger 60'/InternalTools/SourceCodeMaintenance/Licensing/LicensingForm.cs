// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using AlphaCentaury.Tools.SourceCodeMaintenance.Helpers;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public sealed partial class LicensingForm : LicensingFormDocumentView
    {
        private readonly TextBoxOutputWriter _outputWriter;
        private LicensingToolOptions _options;

        public LicensingForm()
        {
            InitializeComponent();
            _outputWriter = new TextBoxOutputWriter(textBoxOutput, timerRefreshOutput, 4);
            _options = new LicensingToolOptions(true);
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

        protected override void openSolutionFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeCall(LoadSolutionFile);
        } // SafeCall

        protected override void openLicensingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SafeCall(LoadLicensingData);
        } // openLicensingDataToolStripMenuItem_Click

        protected override void createStripButton_Click(object sender, EventArgs e)
        {
            ExecuteAsync(_outputWriter, (token) => LicensingMaintenance.CreateMissingLicensingFilesAsync(CurrentSolution,
                _outputWriter,
                LicensingMaintenance.Helper.GetImplicitDefaultsPath(CurrentSolution),
                token));
        } // createStripButton_Click

        protected override void checkStripButton_Click(object sender, EventArgs e)
        {
            ExecuteAsync(_outputWriter, (token) => LicensingMaintenance.CheckLicensingFilesAsync(CurrentSolution,
                _outputWriter,
                _options.Checker,
                LicensingMaintenance.Helper.GetImplicitDefaultsPath(CurrentSolution),
                token));
        } // checkStripButton_Click

        protected override void updateStripButton_Click(object sender, EventArgs e)
        {
            ExecuteAsync(_outputWriter, token => LicensingMaintenance.UpdateLicensingFilesAsync(CurrentSolution, _outputWriter, token));
        } // updateStripButton_Click

        protected override void licensingWriteStripButton_Click(object sender, EventArgs e)
        {
            ExecuteAsync(_outputWriter, 
                asyncAction: token => LicensingMaintenance.WriteLicenseFilesAsync(CurrentSolution, _outputWriter,
                    _options.Writer, _options.SolutionWriter, token));
        } // licensingWriteStripButton_Click

        protected override void licensingOptionsStripButton_Click(object sender, EventArgs e)
        {
            using var dlg = new LicensingToolOptionsDialog
            {
                Options = _options
            };

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _options = dlg.Options;
            } // if
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
            var root = LicensingVsUi.GetProjectTree(vsProject, CurrentSolution);
            root.Expand();
            root.Nodes[0].Expand();
            treeViewDetails.Nodes.Add(root);
            DetailsMode = DetailsModeEnum.VsProject;
        } // OnVsProjectSelected

        private protected override void OnLicensingNodeSelected(LicensingDataNode node)
        {
            treeViewDetails.Nodes.Clear();
            if (!node.Exists) return;

            DetailsMode = DetailsModeEnum.LicensingData;

            var name = node.FilePath.Substring(CurrentSolution.SolutionPath.Length + 1);
            treeViewDetails.ImageList = licensingDataViewer.ImageList;
            treeViewDetails.Nodes.Add(licensingDataViewer.Ui.DataToTree(name, node.Value));

            licensingDataViewer.LicensingDataName = name;
            licensingDataViewer.LicensingData = node.Value;
        } // OnLicensingNodeSelected

        private protected override void OnDetailsLicensingNodeSelected(LicensingDataNode node)
        {
            var details = new StringBuilder();
            details.AppendLine($"File exists: {node.Exists}");
            if (node.Exists)
            {
                var licensing = node.Value;
                details.AppendLine($"{licensing}");

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

                licensingDataViewer.LicensingData = node.Value;
                licensingDataViewer.LicensingDataName = node.FilePath.Substring(CurrentSolution.SolutionPath.Length + 1);
            } // if

            textBoxDetails.Text = details.ToString();
            textBoxDetails.Enabled = true;
        } // OnDetailsLicensingNodeSelected

        private void LoadSolutionFolder()
        {
            if (string.IsNullOrEmpty(selectFolderDialog.SelectedPath))
            {
#if DEBUG
                selectFolderDialog.SelectedPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "..\\..\\..\\.."));
#else
                selectFolderDialog.SelectedPath = Application.ExecutablePath;
#endif
            } // if

            if (selectFolderDialog.ShowDialog(this) != DialogResult.OK) return;

            tabControlSolution.SelectedTab = tabPageSolution;
            LoadSolutionFolderAsync(selectFolderDialog.SelectedPath);
        } // LoadSolutionFolder

        private void LoadSolutionFile()
        {
            openFileDialog.Filter = "Solution files|*.sln|All files|*.*";
            openFileDialog.Title = "Open VisualStudio solution file";
            openFileDialog.FileName = null;

            if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

            tabControlSolution.SelectedTab = tabPageSolution;
            LoadSolutionFileAsync(openFileDialog.FileName);
        } // LoadSolutionFile

        private void LoadLicensingData()
        {
            openFileDialog.Filter = "Licensing data files|*.xml|All files|*.*";
            openFileDialog.Title = "Open licensing data file";
            openFileDialog.FileName = null;
            if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

            var data = XmlSerialization.Deserialize<LicensingData>(openFileDialog.FileName);
            var name = openFileDialog.FileName;
            if ((CurrentSolution != null) && name.StartsWith(CurrentSolution.SolutionPath))
            {
                name = name.Substring(CurrentSolution.SolutionPath.Length + 1);
            } // if
            licensingDataViewer.LicensingData = data;
            licensingDataViewer.LicensingDataName = name;
            tabControlSolution.SelectedTab = tabPageLicensing;
        } // LoadLicensingData
    } // class LicensingForm
} // namespace
