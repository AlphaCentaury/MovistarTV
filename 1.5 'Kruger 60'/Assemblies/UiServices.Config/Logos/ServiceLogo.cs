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

namespace IpTviewr.UiServices.Configuration.Logos
{
    public class ServiceLogo : BaseLogo
    {
        internal ServiceLogo(ILogoMapping mapping, string mappingKey, string entry, string uniqueKey) : base(mapping, mappingKey, entry, uniqueKey)
        {
            // no-op
        } // constructor

        protected override string ImageNotFoundExceptionText => Properties.Texts.ExceptionLogosServiceImageNotFound;

        protected override string ImageLoadExceptionText => Properties.Texts.ExceptionLogosServiceImageLoadError;
    } // class ServiceLogo
} // namespace
