// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
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
    public class BatchExecution : IMaintenanceTool
    {
        #region Implementation of IMaintenanceTool

        public void Execute([NotNull] IReadOnlyList<string> arguments, [NotNull] Action<string> writeLine)
        {
            if (arguments.Count == 0)
            {
                ShowUsage(writeLine);
                return;
            } // if

            foreach (var argument in arguments)
            {
                writeLine("**********************************************************************");
                writeLine($"Executing '{argument}'");
                writeLine("**********************************************************************");
                writeLine("");

                var batch = XmlSerialization.Deserialize<Serialization.Batch>(argument);
                if (batch == null) throw new System.IO.InvalidDataException();

                DoExecute(batch, writeLine);
            } // foreach
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

        string IMaintenanceTool.SelectFileFilter => BatchResources.SelectFileFilter;

        #endregion

        public static Task ExecuteBatchAsync(Serialization.Batch batch, Action<string> writeLine)
        {
            if (batch == null) throw new ArgumentNullException(nameof(batch));
            if (writeLine == null) throw new ArgumentNullException(nameof(writeLine));

            var task = new Task(() => DoExecute(batch, writeLine), TaskCreationOptions.LongRunning);
            task.Start();

            return task;
        } // ExecuteBatchAsync

        private static void DoExecute([NotNull] Serialization.Batch batch, [NotNull] Action<string> writeLine)
        {
            foreach (var line in batch.Lines)
            {
                var tool = Program.GetTool(line.Guid);
                if (tool == null)
                {
                    continue;
                } // if

                writeLine("======================================================================");
                writeLine($"Executing {tool.Metadata.CliName}");
                foreach (var argument in line.Arguments)
                {
                    writeLine($"\t{argument}");
                } // foreach
                writeLine("======================================================================");
                writeLine("");
                try
                {
                    tool.Value.Execute(line.Arguments, writeLine);
                }
                catch (Exception e)
                {
                    while (e != null)
                    {
                        writeLine($">>>>>  {e.GetType().Name}  <<<<<");
                        writeLine(e.Message);
                        var trace = e.StackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        foreach (var traceLine in trace)
                        {
                            writeLine(traceLine);
                        } // foreach
                        e = e.InnerException;
                    } // while
                } // catch
            } // foreach
        } // DoExecute
    } // class BatchExecution
} // namespace
