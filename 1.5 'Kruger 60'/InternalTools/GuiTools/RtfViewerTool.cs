using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools.GuiTools
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", ToolGuid)]
    public class RtfViewerTool : IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{4EDB979F-9A6E-46FA-A702-7D78C4475627}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new RtfViewer();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "Xtra: Viewers";
        public string Name => "RTF viewer";
        public Image GetLogo(int size) => null;

        #endregion
    } // class RtfViewerTool
} // namespace