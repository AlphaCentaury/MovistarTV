// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Net;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    public class WebClientEx : WebClient
    {
        public WebClientEx(CookieContainer container)
        {
            CookieContainer = container;
        } // constructor

        public CookieContainer CookieContainer { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var r = base.GetWebRequest(address);
            if (r is HttpWebRequest request)
            {
                request.CookieContainer = CookieContainer;
            }
            return r;
        } // GetWebRequest

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            var response = base.GetWebResponse(request, result);
            ReadCookies(response);
            return response;
        } // GetWebResponse

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = base.GetWebResponse(request);
            ReadCookies(response);
            return response;
        } // GetWebResponse

        private void ReadCookies(WebResponse r)
        {
            if (!(r is HttpWebResponse response)) return;

            var cookies = response.Cookies;
            CookieContainer.Add(cookies);
        } // ReadCookies
    }
}
