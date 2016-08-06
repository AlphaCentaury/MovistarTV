// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration
{
    public class ServiceLogo : BaseLogo
    {
        protected override string ImageNotFoundExceptionText
        {
            get { return Properties.Texts.ExceptionLogosServiceImageNotFound; }
        } // ImageNotFoundExceptionText

        protected override string ImageLoadExceptionText
        {
            get { return Properties.Texts.ExceptionLogosServiceImageLoadError; }
        } // ImageLoadExceptionText
    } // class ServiceLogo
} // namespace
