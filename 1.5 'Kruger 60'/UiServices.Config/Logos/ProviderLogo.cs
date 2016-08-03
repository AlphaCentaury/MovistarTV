// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.UiServices.Configuration.Logos
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
