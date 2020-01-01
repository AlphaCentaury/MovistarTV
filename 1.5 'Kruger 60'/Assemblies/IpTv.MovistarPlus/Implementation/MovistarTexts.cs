// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.IpTvServices.Implementation;

namespace IpTviewr.IpTvServices.MovistarPlus.Implementation
{
    public sealed class MovistarTexts: ServiceTexts
    {
        protected override ITvServiceProviderTexts GetProviderTexts() => new MovistarProviderTexts();
    } // class MovistarTexts
} // namespace
