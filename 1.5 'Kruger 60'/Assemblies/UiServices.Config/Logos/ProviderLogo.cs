// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Configuration.Logos
{
    public class ProviderLogo : BaseLogo
    {
        public ProviderLogo(string basePath, string partialPath, string filePrefix, string key)
        {
            BasePath = basePath;
            PartialPath = partialPath;
            FilePrefix = filePrefix;
            Key = key;
        } // constructor

        protected override string ImageNotFoundExceptionText
        {
            get { return Properties.Texts.ExceptionLogosProviderImageNotFound; }
        } // ImageNotFoundExceptionText

        protected override string ImageLoadExceptionText
        {
            get { return Properties.Texts.ExceptionLogosProviderImageLoadError; }
        } // ImageLoadExceptionText
    } // class ProviderLogo
} // namespace
