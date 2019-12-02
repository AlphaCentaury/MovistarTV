// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using IpTviewr.Common.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Batch
{
    [Export(typeof(IMaintenanceTool))]
    [ExportMetadata("Name", "Batch execution")]
    [ExportMetadata("Guid", "{6F08CBEF-417F-49EA-8704-1686B1BBF5A9}")]
    [ExportMetadata("HasParameters", true)]
    [ExportMetadata("HasFileParameters", true)]
    [ExportMetadata("HasUi", true)]
    [ExportMetadata("CliName", "Batch")]
    public class BatchExecution: IMaintenanceTool
    {
        #region Implementation of IMaintenanceTool

        public void Execute([NotNull] string[] arguments, [NotNull] Action<string> writeLine)
        {
            if (arguments.Length == 0)
            {
                ShowUsage(writeLine);
                return;
            } // if

            var batch = XmlSerialization.Deserialize<Serialization.Batch>(arguments[0]);
            if (batch == null) throw new System.IO.InvalidDataException();

            DoExecute(batch, writeLine);
        } // Execute

        public void Execute(Serialization.Batch batch, Action<string> writeLine)
        {
            if (batch == null) throw new ArgumentNullException(nameof(batch));
            if (writeLine == null) throw new ArgumentNullException(nameof(writeLine));
            
            DoExecute(batch, writeLine);
        } // Execute

        public void ShowUsage([NotNull] Action<string> writeLine)
        {
            throw new NotImplementedException();
        } // ShowUsage

        public Form GetUi() => new BatchDialog();

        string IMaintenanceTool.SelectFileFilter => Batch_Texts.SelectFileFilter;

        #endregion

        private static void DoExecute([NotNull] Serialization.Batch batch, [NotNull] Action<string> writeLine)
        {
            throw new NotImplementedException();
        } // DoExecute
    } // class BatchExecution
} // namespace
