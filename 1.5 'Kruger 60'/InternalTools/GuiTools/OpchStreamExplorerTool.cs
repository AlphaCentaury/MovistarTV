using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools.GuiTools
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", ToolGuid)]
    public class OpchStreamExplorerTool : IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{371CE8A8-02CA-40F8-9AEF-3764D50C19E3}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new OpchExplorerForm();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "Multicast / DVB-STP";
        public string Name => "OPCH Stream Explorer";
        public Image GetLogo(int size) => null;

        #endregion
    } // class OpchStreamExplorerTool
} // namespace