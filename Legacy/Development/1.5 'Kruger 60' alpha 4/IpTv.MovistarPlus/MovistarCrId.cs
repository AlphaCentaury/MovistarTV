// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.MovistarPlus
{
    public class MovistarCrId
    {
        public static MovistarCrId Get(string CRID)
        {
            var crid = new Uri(CRID);
            var components = crid.AbsolutePath.Split('/');
            if (components.Length != 4) return null;
            if (components[2] != components[3]) return null;
            if (components[3].Length < 5) return null;

            var result = new MovistarCrId()
            {
                SeriesId = components[1],
                ContentIdRoot = components[3].Substring(0, 4),
                ContentId = components[3]
            };

            return result;
        } // Get

        public string SeriesId
        {
            get;
            private set;
        } // SeriedId

        public string ContentIdRoot
        {
            get;
            private set;
        } // ContentIdRoot

        public string ContentId
        {
            get;
            private set;
        } // ContentId
    } // class MovistarCrId
} // namespace
