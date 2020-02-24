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

using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", ToolGuid)]
    public class LogosMaintenanceTool : IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{6489E249-343F-4C7C-B3AC-6738C5769A27}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new FormSvgLogosExporter();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "Logos";
        public string Name => "SVG exporter";
        public Image GetLogo(int size) => null;

        #endregion
    } // class LogosMaintenanceTool
} // namespace