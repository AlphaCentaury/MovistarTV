// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class TermsAndConditions: FormattedMultilineText, ICloneable<TermsAndConditions>
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

        object ICloneable.Clone() => Clone();

        public void CopyTo(TermsAndConditions other)
        {
            other.Language = Language;
            base.CopyTo(other);
        } // CopyTo

        public static List<TermsAndConditions> Inherit([CanBeNull] List<TermsAndConditions> list, [CanBeNull]IList<TermsAndConditions> from)
        {
            if (from == null) return null;

            if ((list is null) || (list.Count == 0))
            {
                return from.Clone();
            } // if

            var d = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var terms in list)
            {
                d.Add(terms.Language ?? "<null>");
            } // foreach

            foreach (var fromTerms in from)
            {
                if (d.Contains(fromTerms.Language ?? "<null>")) continue;

                d.Add(fromTerms.Language ?? "<null>");
                list.Add(fromTerms.Clone());
            } // foreach

            return list;
        } // if

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
