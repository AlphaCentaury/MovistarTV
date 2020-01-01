using ComponentFactory.Krypton.Ribbon;
using System.Drawing;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public class MdiRibbonContext
    {
        public Color Color { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public KryptonRibbonTab[] Tabs { get; set; }
    } // MdiRibbonContext
} // namespace