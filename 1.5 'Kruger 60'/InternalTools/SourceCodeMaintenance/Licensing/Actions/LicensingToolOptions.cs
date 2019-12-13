using System;
using System.Runtime.Remoting.Messaging;
using System.Xml.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    [Serializable]
    [XmlRoot("LicensingTool.Options")]
    public class LicensingToolOptions
    {
        public LicensingToolOptions()
        {
            // no-op
        } // constructor

        public LicensingToolOptions(bool notNull)
        {
            if (!notNull) return;

            Checker = new CheckerOptions();
            Writer = new WriterOptions();
        } // constructor

        public CheckerOptions Checker { get; set; }

        public WriterOptions Writer { get; set; }
    } // class LicensingToolOptions
} // namespace