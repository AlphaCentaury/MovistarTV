// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using IpTviewr.Common;
using JetBrains.Annotations;
using SourceCodeMaintenance;

namespace AlphaCentaury.Tools.SourceCodeMaintenance
{
    public static class Program
    {
        private class ToolsContainer
        {
            public ToolsContainer(ComposablePartCatalog catalog)
            {
                CompositionContainer = new CompositionContainer(catalog);
                CompositionContainer.ComposeParts(this);
                if (ToolsComponents == null) return;

                Tools = ToolsComponents.OrderBy(tool => tool.Metadata.CliName).ToArray();
                ToolsNames = Tools.Select(lazy => lazy.Metadata.Name).ToList();
                ToolsUi = ToolsComponents.Where(tool => tool.Metadata.HasUi).OrderBy(tool => tool.Metadata.CliName).ToArray();
                ToolsUiNames = ToolsUi.Select(lazy => lazy.Metadata.Name).ToList();
                GuidTools = Tools.ToDictionary(lazy => Guid.Parse(lazy.Metadata.Guid));
                CliTools = Tools.ToDictionary(lazy => lazy.Metadata.CliName);
            } // constructor

            public CompositionContainer CompositionContainer { get; }

#pragma warning disable 00649 // Field is never assigned to
            [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Can't be readonly: value will be set by MEF when composing parts")]
            [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Value will be set by MEF when composing parts")]
            [SuppressMessage("Warning", "CS0069: Field is never assigned to")]
            [ImportMany(typeof(IMaintenanceTool))]
            [ItemNotNull]
            [CanBeNull]
            internal Lazy<IMaintenanceTool, IMaintenanceToolMetadata>[] ToolsComponents;
#pragma warning restore 00649

            public IReadOnlyDictionary<Guid, Lazy<IMaintenanceTool, IMaintenanceToolMetadata>> GuidTools { get; internal set; }
            public IReadOnlyDictionary<string, Lazy<IMaintenanceTool, IMaintenanceToolMetadata>> CliTools { get; internal set; }
        } // class

        private static ToolsContainer Container;

        public static Lazy<IMaintenanceTool, IMaintenanceToolMetadata>[] Tools { get; private set; }
        public static List<string> ToolsNames { get; private set; }

        public static Lazy<IMaintenanceTool, IMaintenanceToolMetadata>[] ToolsUi { get; private set; }
        public static List<string> ToolsUiNames { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main(string[] arguments)
        {
            LoadTools();
            if (arguments.Length > 0)
            {
                ExecuteCli(arguments);
                return;
            } // if

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        } // Main

        public static Lazy<IMaintenanceTool, IMaintenanceToolMetadata> GetTool(Guid toolGuid)
        {
            return Container.GuidTools.TryGetValue(toolGuid, out var tool) ? tool : null;
        } // GetTool

        public static Lazy<IMaintenanceTool, IMaintenanceToolMetadata> GetTool(string toolCli)
        {
            return Container.CliTools.TryGetValue(toolCli, out var tool) ? tool : null;
        } // GetTool

        private static void ExecuteCli(string[] arguments)
        {
            throw new NotImplementedException();
        } // ExecuteCli

        private static void LoadTools()
        {
            try
            {
                var catalog = new ApplicationCatalog();
                Container = new ToolsContainer (catalog);
            }
            catch (Exception e)
            {
                BaseProgram.HandleException(null, "Unable to load tools: MEF composition failed.", e);
                throw;
            } // try-catch
        } // LoadTools
    } // class Program
} // namespace
