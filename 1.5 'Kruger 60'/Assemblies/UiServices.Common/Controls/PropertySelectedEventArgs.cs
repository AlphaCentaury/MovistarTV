using System;

namespace IpTviewr.UiServices.Common.Controls
{
    public class PropertySelectedEventArgs: EventArgs
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int Index { get; set; }
    } // PropertySelectedEventArgs
} // namespace