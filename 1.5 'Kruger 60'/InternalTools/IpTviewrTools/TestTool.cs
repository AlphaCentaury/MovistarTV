using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.Properties;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", "{CB5FCBE7-3FB9-4280-B29E-9E995B2E0283}")]

    internal class TestTool: IGuiTool, IGuiToolDataProvider
    {
        #region Implementation of IGuiTool

        public Form CreateForm() => new Form1();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => "{CB5FCBE7-3FB9-4280-B29E-9E995B2E0283}";
        public string Category => "Dummy";
        public string Name => "Dummy tool";

        public Image GetLogo(int size)
        {
            if (size <= 16) return Resources.IpTViewrGuiTools_16x;
            if (size <= 24) return Resources.IpTViewrGuiTools_24x;
            if (size <= 32) return Resources.IpTViewrGuiTools_32x;

            return Resources.IpTViewrGuiTools_48x;
        }

        #endregion
    }
}