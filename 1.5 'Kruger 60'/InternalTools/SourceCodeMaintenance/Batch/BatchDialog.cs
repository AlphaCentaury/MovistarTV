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

using AlphaCentaury.Tools.SourceCodeMaintenance.Batch.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Helpers;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Common.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Batch
{
    public sealed partial class BatchDialog : CommonBaseForm
    {
        private readonly Helpers.AsyncHelper _asyncHelper;
        private bool _isDirty;
        private bool _hasResults;
        private bool _hasLines;
        private Lazy<IMaintenanceTool, IMaintenanceToolMetadata> _selectedTool;
        private readonly Helpers.TextBoxOutputWriter _outputWriter;

        public BatchDialog()
        {
            InitializeComponent();
            _outputWriter = new TextBoxOutputWriter(textBoxResults, timerRefreshOutput, 4);
            _asyncHelper = new AsyncHelper(this, toolStripForm, closeStripButton);
        } // constructor

        private bool IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                saveToolStripButton.Enabled = _isDirty;
            } // set
        } // IsDirty

        private bool HasResults
        {
            get => _hasResults;
            set
            {
                _hasResults = value;
                copyStripButton.Enabled = value;
                cutToolStripButton.Enabled = value;
                groupBox1.Enabled = value;
                textBoxResults.Enabled = value;
                if (!value) textBoxResults.Text = null;
            } // set
        } // HasResults

        private bool HasLines
        {
            get => _hasLines;
            set
            {
                _hasLines = value;
                executeStripButton.Enabled = value;
                listBatch.Enabled = value;
                if (value) return;

                buttonEdit.Enabled = false;
                buttonRemove.Enabled = false;
                buttonMoveUp.Enabled = false;
                buttonMoveDown.Enabled = false;
            } // set
        } // HasLines

        private Lazy<IMaintenanceTool, IMaintenanceToolMetadata> SelectedTool
        {
            get => _selectedTool;
            set
            {
                _selectedTool = value;
                var enable = (value != null);
                var hasArguments = enable && value.Metadata.HasParameters;
                buttonUsage.Enabled = enable && value.Metadata.HasUsage;
                buttonAdd.Enabled = hasArguments;
            } // set;
        } // SelectedTools

        #region Overrides of Form

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.N:
                    SafeCall(NewBatch);
                    return true; // if
                case Keys.Control | Keys.O:
                    SafeCall(OpenBatch);
                    return true; // if
                case Keys.Control | Keys.S:
                    if (saveToolStripButton.Enabled) SafeCall(() => SaveBatch());
                    return true;
                case Keys.F5:
                    if (executeStripButton.Enabled) executeStripButton.PerformClick();
                    return true; // if
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            } // switch
        } // ProcessCmdKey

        #endregion

        private void BatchDialog_Load(object sender, EventArgs e)
        {
            IsDirty = false;
            HasResults = false;
            HasLines = false;
            if (Program.ToolsCliNames.Count > 0)
            {
                comboBoxTools.DataSource = Program.ToolsCliNames;
                comboBoxTools.SelectedIndex = 0;
            }
            else
            {
                SelectedTool = null;
            } // if-else
        } // BatchDialog_Load

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(NewBatch);
        } // // newToolStripButton_Click

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(OpenBatch);
        } // newToolStripButton_Click

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(() => SaveBatch());
        } // saveToolStripButton_Click

        private void executeStripButton_Click(object sender, EventArgs e)
        {
            ExecuteBatchAsync();
        } // executeStripButton_Click

        private void copyStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(CopyResults);
        } // copyStripButton_Click

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(ClearResults);
        } // cutToolStripButton_Click

        private void comboBoxTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTool = (comboBoxTools.SelectedIndex >= 0) ? Program.ToolsCli[comboBoxTools.SelectedIndex] : null;
        } // comboBoxTools_SelectedIndexChanged

        private void buttonUsage_Click(object sender, EventArgs e)
        {
            var writer = new Helpers.TextBoxOutputWriter(textBoxResults, null, 4);
            writer.Start();
            SelectedTool.Value.ShowUsage(writer);
            writer.Stop();
            HasResults = true;
        } //buttonUsage_Click

        private void listBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = (listBatch.SelectedIndices.Count > 0) ? listBatch.SelectedIndices[0] : -1;
            buttonEdit.Enabled = (index >= 0);
            buttonRemove.Enabled = (index >= 0);
            buttonMoveUp.Enabled = (index > 0);
            buttonMoveDown.Enabled = (index < (listBatch.Items.Count - 1));
        } // listBatch_SelectedIndexChanged

        private void listBatch_DoubleClick(object sender, EventArgs e)
        {
            if (listBatch.SelectedItems.Count == 0) return;

            buttonEdit.PerformClick();
        } // listBatch_DoubleClick

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            List<string> arguments;

            if (SelectedTool.Metadata.HasParameters)
            {
                using var dialog = new ToolArgumentsDialog
                {
                    SelectFileButtonImage = openToolStripButton.Image,
                    SelectFileAction = SelectedTool.Metadata.HasFileParameters ? SelectFile : (Action<TextBox>) null

                };
                
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                arguments = new List<string>(dialog.Arguments);
            }
            else
            {
                arguments = null;
            } // if-else

            AddToBatch(SelectedTool, arguments);
        } // buttonAdd_Click

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (!(listBatch.SelectedItems[0].Tag is BatchExecute line)) return;

            using var dialog = new ToolArgumentsDialog
            {
                SelectFileButtonImage = openToolStripButton.Image,
                Arguments = line.Arguments.ToArray(),
                SelectFileAction = SelectedTool.Metadata.HasFileParameters ? SelectFile : (Action<TextBox>)null
            };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            
            line.Arguments.Clear();
            line.Arguments.AddRange(dialog.Arguments);
            listBatch.SelectedItems[0].SubItems[1].Text = GetArgumentsForDisplay(line.Arguments);
            IsDirty = true;
        } // buttonEdit_Click

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            listBatch.SelectedItems[0].Remove();
            IsDirty = true;
        } // buttonRemove_Click

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            var item = listBatch.SelectedItems[0];

            var index = item.Index;
            listBatch.BeginUpdate();
            item.Remove();
            listBatch.Items.Insert(index - 1, item);
            listBatch.EndUpdate();
            IsDirty = true;
        } // buttonMoveUp_Click

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            var item = listBatch.SelectedItems[0];

            var index = item.Index;
            listBatch.BeginUpdate();
            item.Remove();
            listBatch.Items.Insert(index + 1, item);
            listBatch.EndUpdate();
            IsDirty = true;
        } // buttonMoveDown_Click

        private void NewBatch()
        {
            if (!SaveIfDirty(BatchResources.NewBatchSaveExplanation)) return;
            IsDirty = false;
            listBatch.Items.Clear();
            HasLines = false;
            HasResults = false;
        } // NewBatch

        private void OpenBatch()
        {
            if (!SaveIfDirty(BatchResources.OpenBatchSaveExplanation)) return;
            IsDirty = false;
            NewBatch();

            openBatchDialog.Filter = BatchResources.SelectFileFilter;
            if (openBatchDialog.ShowDialog(this) != DialogResult.OK) return;
            SafeCall(() =>
            {
                var batch = XmlSerialization.Deserialize<Batch.Serialization.Batch>(openBatchDialog.FileName);
                listBatch.BeginUpdate();
                foreach (var item in batch.Lines)
                {
                    AddToBatch(item);
                } // foreach
                listBatch.EndUpdate();

                HasLines = listBatch.Items.Count > 0;
            });

            IsDirty = false;
        } // OpenBatch

        private bool SaveIfDirty(string explanation)
        {
            if (!IsDirty) return true;
            var result = MessageBox.Show(string.Format(BatchResources.SaveIfDirty, BatchResources.SaveBatchChanges, explanation), BatchResources.SaveBatchChangesCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes && !SaveBatch()) return false;

            return (result == DialogResult.No);
        } // SaveIfDirty

        private bool SaveBatch()
        {
            if (!IsDirty) return true;
            saveBatchDialog.Filter = BatchResources.SelectFileFilter;
            if (saveBatchDialog.ShowDialog(this) != DialogResult.OK) return false;

            var batch = GetBatch();
            XmlSerialization.Serialize(saveBatchDialog.FileName, batch);

            IsDirty = false;
            return true;
        } // SaveBatch

        private void ExecuteBatchAsync()
        {
            HasResults = false;
            var batch = GetBatch();
            _asyncHelper.ExecuteAsync(_outputWriter, (token) => BatchExecution.ExecuteBatchAsync(batch, _outputWriter, token));
            HasResults = true;
        } // ExecuteBatch

        private void AddToBatch(Lazy<IMaintenanceTool, IMaintenanceToolMetadata> tool, List<string> arguments)
        {
            AddToBatch(new BatchExecute
            {
                Guid = Guid.Parse(tool.Metadata.Guid),
                Arguments = arguments
            });
        } // AddToBatch

        private void AddToBatch(BatchExecute line)
        {
            var tool = Program.GetTool(line.Guid);
            line.Name = tool.Metadata.CliName; // update tool CLI name, just in case it has changed

            var item = new ListViewItem(tool.Metadata.Name);
            item.SubItems.Add(GetArgumentsForDisplay(line.Arguments));
            item.Tag = line;
            item.Selected = true;

            listBatch.SelectedItems.Clear();
            listBatch.Items.Add(item);

            HasLines = true;
            IsDirty = true;
        } // AddToBatch

        private void CopyResults()
        {
            if (!textBoxResults.Enabled) return;

            Clipboard.Clear();
            Clipboard.SetText(textBoxResults.Text);
        } // CopyResults

        private void ClearResults()
        {
            if (!textBoxResults.Enabled) return;

            textBoxResults.Text = null;
            HasResults = false;
        } // ClearResults

        private void SelectFile(TextBox textBox)
        {
            var toolFilter = SelectedTool.Value.SelectFileFilter;
            var filter = (toolFilter != null) ? string.Join("|", toolFilter, BatchResources.SelectAllFilesFilter) : BatchResources.SelectAllFilesFilter;
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

            var file = openFileDialog.FileName;
            var text = textBox.Text;
            var start = textBox.SelectionStart;
            var end = start + textBox.SelectionLength;
            if (end >= text.Length) end = text.Length - 1;
            textBox.Text = text.Substring(0, start) + file + text.Substring(end + 1);
        } // SelectFile

        private Serialization.Batch GetBatch()
        {
            return new Serialization.Batch
            {
                Lines = (from item in listBatch.Items.Cast<ListViewItem>()
                    let line = item.Tag as BatchExecute
                    where line != null
                    select line).ToList()
            };
        } // GetBatch

        public static List<string> SplitArguments(string arguments)
        {
            var result = new List<string>();

            var startIndex = 0;
            while (startIndex < arguments.Length)
            {
                var indexSpace = arguments.IndexOf(' ', startIndex);
                var indexQuote = arguments.IndexOf('"', startIndex);

                if ((indexSpace >= indexQuote) && (indexQuote != -1))
                {
                    // ignore space
                    // look for ending quotes
                    var indexEnd = arguments.IndexOf('"', indexQuote + 1) + 1;
                    if (indexEnd >= 0)
                    {
                        AddArgument(startIndex, indexEnd);
                        startIndex = indexEnd + 1;
                    }
                    else
                    {
                        AddArgument(startIndex, arguments.Length);
                        startIndex = arguments.Length;
                    } // if-else
                }
                else
                {
                    if (indexSpace >= 0)
                    {
                        AddArgument(startIndex, indexSpace);
                        startIndex = indexSpace + 1;
                    }
                    else
                    {
                        break;
                    } // if-else
                } // if

                if (indexQuote >= 0)
                {
                }
            } // while

            if (startIndex < arguments.Length)
            {
                AddArgument(startIndex, arguments.Length);
            } // if

            return result;

            void AddArgument(int start, int end)
            {
                var argument = arguments.Substring(start, end - start).Trim();
                if (argument.Length > 0) result.Add(argument);
            } // AddArgument
        } // SplitArguments

        private string GetArgumentsForDisplay(IList<string> arguments)
        {
            if ((arguments == null) || (arguments.Count == 0)) return "(No arguments)";
            return arguments.Count == 1 ? arguments[0] : $"({arguments.Count} arguments)";
        } // GetArgumentsForDisplay
    } // class BatchDialog
} // namespace
