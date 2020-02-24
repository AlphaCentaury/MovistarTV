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
    public class LogosTool: IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{355504D7-B9F4-467B-9C4C-E73BF537ED3C}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new FormLogos();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "Logos";
        public string Name => "Logos grid";
        public Image GetLogo(int size) => null;

        #endregion
    }
}
