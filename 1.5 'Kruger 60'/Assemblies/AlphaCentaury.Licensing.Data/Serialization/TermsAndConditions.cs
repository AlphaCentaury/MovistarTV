using System;
using System.Xml;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class TermsAndConditions: FormattedMultilineText, IXmlSerializable
    {
        [XmlAttribute("language")]
        public string Language { set; get; }

        public new TermsAndConditions Clone()
        {
            var clone = new TermsAndConditions
            {
                Language = Language
            };
            CopyTo(clone);

            return clone;
        } // Clone

        public void CopyTo(TermsAndConditions other)
        {
            other.Language = Language;
            base.CopyTo(other);
        } // CopyTo

        #region Overrides of FormattedMultilineText

        protected override void ReadXml(XmlReader reader)
        {
            if (reader.MoveToAttribute("language"))
            {
                Language = reader.Value;
                reader.MoveToElement();
            } // if

            base.ReadXml(reader);
        } // ReadXml

        protected override void WriteXml(XmlWriter writer)
        {
            if (Language != null) writer.WriteAttributeString("language", Language);
            base.WriteXml(writer);
        } // WriteXml

        #endregion
    } // class TermsAndConditions
} // namespace
