using System;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    [Serializable]
    public class WriterOptions
    {
        public bool WriteHtml { get; set; }

        public bool SkipLicensingHtml { get; set; }
    } // class WriterOptions
} // namespace