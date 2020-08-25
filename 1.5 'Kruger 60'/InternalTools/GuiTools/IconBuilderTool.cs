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

namespace IpTviewr.Internal.Tools.GuiTools
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", ToolGuid)]
    public class IconBuilderTool : IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{81261C2E-BAB5-42BE-A389-E0D1A741F28B}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new IconBuilder();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "Logos";
        public string Name => "Icon builder";
        public Image GetLogo(int size) => null;

        #endregion
    } // class IconBuilderTool
} // namespace
