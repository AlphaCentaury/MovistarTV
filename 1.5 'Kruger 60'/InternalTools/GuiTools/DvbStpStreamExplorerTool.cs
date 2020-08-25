using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools.GuiTools
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", ToolGuid)]
    public class DvbStpStreamExplorerTool : IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{2D1B7282-F446-4FA2-94B6-FF477D441046}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new DvbStpStreamExplorerForm();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "Multicast / DVB-STP";
        public string Name => "DVB-STP Stream Explorer";
        public Image GetLogo(int size) => null;

        #endregion
    } // class DvbStpStreamExplorerTool
} // namespace