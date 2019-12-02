// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using AlphaCentaury.Tools.SourceCodeMaintenance.Batch.Serialization;
using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Common.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Batch
{
    public sealed partial class BatchDialog : CommonBaseForm
    {
        private bool _isDirty;
        private bool _hasResults;
        private bool _hasLines;
        private Lazy<IMaintenanceTool, IMaintenanceToolMetadata> _selectedTool;

        public BatchDialog()
        {
            InitializeComponent();
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
                if (!value)
                {
                    buttonRemove.Enabled = false;
                    buttonMoveUp.Enabled = false;
                    buttonMoveDown.Enabled = false;
                } // if
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
                textBoxArguments.Enabled = hasArguments;
                buttonUsage.Enabled = enable && value.Metadata.HasUsage;
                buttonSelectFile.Enabled = hasArguments && value.Metadata.HasFileParameters;
                buttonArgumentsEditor.Enabled = hasArguments;
                buttonAdd.Enabled = enable;
            } // set;
        } // SelectedTools

        private void BatchDialog_Load(object sender, EventArgs e)
        {
            IsDirty = false;
            HasResults = false;
            HasLines = false;
            if (Program.ToolsNames.Count > 0)
            {
                comboBoxTools.DataSource = Program.ToolsNames;
                comboBoxTools.SelectedIndex = 0;
            }
            else
            {
                SelectedTool = null;
            } // if-else

            buttonSelectFile.Image = openToolStripButton.Image;
        } // BatchDialog_Load

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
                    SafeCall(() => SaveBatch());
                    return true;
                case Keys.F5:
                    SafeCall(ExecuteBatch);
                    return true; // if
                case Keys.Control | Keys.C:
                    SafeCall(CopyResults);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            } // switch
        } // ProcessCmdKey

        #endregion

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(NewBatch);
        } // // newToolStripButton_Click

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(OpenBatch);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(() => SaveBatch());
        }

        private void executeStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(ExecuteBatch);
        }

        private void copyStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(CopyResults);
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            SafeCall(ClearResults);
        }

        private void closeStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTool = (comboBoxTools.SelectedIndex >= 0) ? Program.Tools[comboBoxTools.SelectedIndex] : null;
        } // comboBoxTools_SelectedIndexChanged

        private void buttonUsage_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        } //buttonUsage_Click

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            SafeCall(SelectFile, textBoxArguments);
        } // buttonSelectFile_Click

        private void buttonArgumentsEditor_Click(object sender, EventArgs e)
        {
            using var dialog = new ToolArgumentsDialog
            {
                SelectFileButtonImage = openToolStripButton.Image,
                Arguments = SplitArguments(textBoxArguments.Text).ToArray(),
                SelectFileAction = SelectedTool.Metadata.HasFileParameters ? SelectFile : (Action<TextBox>)null
            };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            textBoxArguments.Text = string.Join(" ", dialog.Arguments);
        } // buttonArgumentsEditor_Click

        private void listBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = (listBatch.SelectedIndices.Count > 0) ? listBatch.SelectedIndices[0] : -1;
            buttonRemove.Enabled = (index >= 0);
            buttonMoveUp.Enabled = (index > 0);
            buttonMoveDown.Enabled = (index < (listBatch.Items.Count - 1));
        } // listBatch_SelectedIndexChanged

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if ((textBoxArguments.Enabled) && (textBoxArguments.Text.Trim().Length == 0))
            {
                MessageBox.Show(this, Batch_Texts.ArgumentsNotOptional, Batch_Texts.ArgumentsNotOptionalCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } // if

            AddBatch(SelectedTool, textBoxArguments.Text);
        } // buttonAdd_Click

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
            if (!SaveIfDirty(Batch_Texts.NewBatchSaveExplanation)) return;
            IsDirty = false;
            listBatch.Items.Clear();
            HasLines = false;
        } // NewBatch

        private void OpenBatch()
        {
            if (!SaveIfDirty(Batch_Texts.OpenBatchSaveExplanation)) return;
            IsDirty = false;
            NewBatch();

            openBatchDialog.Filter = Batch_Texts.SelectFileFilter;
            if (openBatchDialog.ShowDialog(this) != DialogResult.OK) return;
            SafeCall(() =>
            {
                var batch = XmlSerialization.Deserialize<Batch.Serialization.Batch>(openBatchDialog.FileName);
                listBatch.BeginUpdate();
                foreach (var item in batch.Lines)
                {
                    AddBatch(Program.GetTool(item.Guid), string.Join(" ", item.Arguments));
                } // foreach
                listBatch.EndUpdate();

                HasLines = listBatch.Items.Count > 0;
            });

            IsDirty = false;
        } // OpenBatch

        private bool SaveIfDirty(string explanation)
        {
            if (!IsDirty) return true;
            var result = MessageBox.Show(string.Format(Batch_Texts.SaveIfDirty, Batch_Texts.SaveBatchChanges, explanation), Batch_Texts.SaveBatchChangesCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes && !SaveBatch()) return false;

            return (result == DialogResult.No);
        } // SaveIfDirty

        private bool SaveBatch()
        {
            if (!IsDirty) return true;
            saveBatchDialog.Filter = Batch_Texts.SelectFileFilter;
            if (saveBatchDialog.ShowDialog(this) != DialogResult.OK) return false;

            var batch = new Batch.Serialization.Batch
            {
                Lines = (from item in listBatch.Items.Cast<ListViewItem>()
                         let line = item.Tag as BatchExecute
                         where line != null
                         select line).ToList()
            };
            XmlSerialization.Serialize(saveBatchDialog.FileName, batch);

            IsDirty = false;
            return true;
        } // SaveBatch

        private void ExecuteBatch()
        {
            throw new NotImplementedException();
        } // ExecuteBatch

        private void AddBatch(Lazy<IMaintenanceTool, IMaintenanceToolMetadata> tool, string arguments)
        {
            AddBatch(tool, new BatchExecute
            {
                Guid = Guid.Parse(tool.Metadata.Guid),
                Name = tool.Metadata.CliName,
                Arguments = SplitArguments(arguments)
            });
        } // AddBatch

        private void AddBatch(Lazy<IMaintenanceTool, IMaintenanceToolMetadata> tool, BatchExecute line)
        {
            var item = new ListViewItem(tool.Metadata.Name);
            item.SubItems.Add(string.Join(" ", line.Arguments));
            item.Tag = line;
            item.Selected = true;
            listBatch.SelectedItems.Clear();
            listBatch.Items.Add(item);
            listBatch.Enabled = true;

            IsDirty = true;
        } // AddBatch

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
            openFileDialog.Filter = SelectedTool.Value.SelectFileFilter;
            if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

            var file = openFileDialog.FileName;
            if (file.IndexOf(' ') >= 0) file = '"' + file + '"';
            var text = textBox.Text;
            var start = textBox.SelectionStart;
            var end = start + textBox.SelectionLength;
            if (end >= text.Length) end = text.Length - 1;
            textBox.Text = text.Substring(0, start) + file + text.Substring(end + 1);
        } // SelectFile

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
    } // class BatchDialog
} // namespace
