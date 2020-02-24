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

namespace IpTviewr.IpTvServices.Implementation
{
    public class ServiceTexts: ITvServiceTexts
    {
        public ITvServiceProviderTexts Provider { get; private set; }

        public void Initialize()
        {
            Provider = GetProviderTexts();
        } // Initialize

        protected virtual ITvServiceProviderTexts GetProviderTexts() => new ServiceProviderTexts();
    } // class ServiceTexts
} // namespace
