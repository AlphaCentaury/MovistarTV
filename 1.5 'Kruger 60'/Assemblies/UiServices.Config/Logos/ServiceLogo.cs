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
        public const string QualityDefault = "(default)";
        public const string QualityStandard = "SD";
        public const string QualityHigh = "HD";
        public const string QualityUltraHigh4K = "4K";
        public const string QualityUltraHigh8K = "8K";

        private readonly string _quality;

        internal ServiceLogo(ILogoMapping mapping, string mappingKey, string entry, string uniqueKey, string quality) : base(mapping, mappingKey, entry, uniqueKey)
        {
            _quality = quality;
        } // constructor

        protected override string ImageNotFoundExceptionText => Properties.Texts.ExceptionLogosServiceImageNotFound;

        protected override string ImageLoadExceptionText => Properties.Texts.ExceptionLogosServiceImageLoadError;
    } // class ServiceLogo
} // namespace
