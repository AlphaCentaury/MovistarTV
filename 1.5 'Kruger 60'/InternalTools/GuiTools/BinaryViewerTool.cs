using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools.GuiTools
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", ToolGuid)]
    public class BinaryViewerTool : IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{E06C9D98-C75D-41E4-8CF0-8914B2690AFF}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new BinaryViewerForm();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "Xtra: Viewers";
        public string Name => "Binary viewer";
        public Image GetLogo(int size) => null;

        #endregion
    } // class BinaryViewerTool
} // namespace