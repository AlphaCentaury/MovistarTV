// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.UiServices.Discovery.Logos
{
    /*
    public class PackedLogos: IEnumerable<PackedLogo>
    {
        private Dictionary<string, PackedLogo> Logos;
        private bool IsReadOnly;

        #region Static methods

        public static PackedLogos FromXml(XmlPackedLogosRoot logos, string kind)
        {
            var result = new PackedLogos()
            {
                Logos = new Dictionary<string, PackedLogo>(logos.Logos.Length),
                IsReadOnly = true,
                Kind = kind,
            };

            foreach (var logo in logos.Logos)
            {
                result.Logos[logo.Key] = logo;
            } // foreach

            return result;
        } // FromXml

        public static XmlPackedLogosRoot ToXml(PackedLogos packedLogos)
        {
            var result = new XmlPackedLogosRoot();
            var logos = new PackedLogo[packedLogos.Logos.Count];

            var index = 0;
            foreach(var logo in packedLogos.Logos.Values)
            {
                logos[index++] = logo;
            } // foreach logo

            result.Logos = logos;

            return result;
        } // ToXml

        public static PackedLogos Create(PackedLogoName[] logoNames, short[] sizes)
        {
            var logos = new PackedLogos()
            {
                Logos = new Dictionary<string, PackedLogo>(logoNames.Length),
            };

            foreach (var logoName in logoNames)
            {
                var logo = new PackedLogo(logoName, sizes);
                logos.Logos[logo.Key] = logo;
            } // foreach

            return logos;
        } // Create

        #endregion

        protected PackedLogos()
        {
            // no-op
        } // constructor

        public string Kind
        {
            get;
            private set;
        } // Kind

        public int Count
        {
            get { return Logos.Count; }
        } // Count

        public PackedLogo this[string key]
        {
            get
            {
                PackedLogo result;

                return (Logos.TryGetValue(key, out result)) ? result : null;
            } // get
        } // this[]

        public PackedLogoPos this[string key, short size]
        {
            get
            {
                var logo = this[key];
                return (logo != null) ? logo[size] : default(PackedLogoPos);
            } // get
        } // this[]

        public void SetPosition(PackedLogoName logoName, short size, short posX, short posY)
        {
            if (IsReadOnly) throw new InvalidOperationException();

            // will throw KeyNotFound exception if not found; this is a desired behavior
            var packedLogo = Logos[logoName.Key];
            var index = packedLogo.GetSizeIndex(size);
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(size));

            packedLogo.Positions[index].X = posX;
            packedLogo.Positions[index].Y = posY;
        } // SetPosition

        public IEnumerator<PackedLogo> GetEnumerator()
        {
            return Logos.Values.GetEnumerator();
        } // GetEnumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Logos.Values.GetEnumerator();
        } // GetEnumerator
    } // class PackedLogos
    */
} // namespace
