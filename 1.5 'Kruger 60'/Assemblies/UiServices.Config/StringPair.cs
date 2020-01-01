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
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration
{
    [Serializable]
    public class StringPair
    {
        private int? _hashCode;
        private string _item1, _item2;

        public StringPair()
        {
            // no op
        } // constructor

        public StringPair(string item1, string item2)
        {
            _item1 = item1;
            _item2 = item2;
        } // constructor

        [XmlIgnore]
        public string Item1
        {
            get => _item1;
            set
            {
                _item1 = value;
                CalcHashCode();
            } // set
        } // Item1

        [XmlIgnore]
        public string Item2
        {
            get => _item2;
            set
            {
                _item2 = value;
                CalcHashCode();
            } // set
        } // Item2

        public string this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Item1;
                    case 1: return Item2;
                    default:
                        throw new ArgumentOutOfRangeException();
                } // switch
            } // get
            set
            {
                switch (index)
                {
                    case 0: Item1 = value; break;
                    case 1: Item2 = value; break;
                    default:
                        throw new ArgumentOutOfRangeException();
                } // switch
            } // set
        } // this[int]

        public void SetPair(string item1, string item2)
        {
            _item1 = item1;
            _item2 = item2;
            CalcHashCode();
        } // SetPair

        public override int GetHashCode()
        {
            if (!_hashCode.HasValue)
            {
                CalcHashCode();
            } // if

            return _hashCode.Value;
        } // GetHashCode

        public override string ToString()
        {
            return $"<{Item1}, {Item2}>";
        } // ToString

        public override bool Equals(object obj)
        {
            var pair = obj as StringPair;

            if (ReferenceEquals(this, obj)) return true;
            if (pair == null) return false;

            return ((pair.Item1 == Item1) && (pair.Item2 == Item2));
        } // Equals

        public static bool operator == (StringPair a, StringPair b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((a == null) || (b == null)) return false;
            return ((a.Item1 == b.Item1) && (a.Item2 == b.Item2));
        } // operator ==

        public static bool operator !=(StringPair a, StringPair b)
        {
            return !(a == b);
        } // operator !=

        private void CalcHashCode()
        {
            _hashCode = ToString().GetHashCode();
        } // CalcHashCode
    } // StringPair
} // namespace
