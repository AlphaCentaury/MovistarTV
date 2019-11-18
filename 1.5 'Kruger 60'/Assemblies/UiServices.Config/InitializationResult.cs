// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;

namespace IpTviewr.UiServices.Configuration
{
    public sealed class InitializationResult
    {
        private static readonly InitializationResult OkSingleton = new InitializationResult() { IsOk = true };

        /// <summary>
        /// Gets an <see cref="InitializationResult"/> with IsOk set to <see cref="true"/> and all remaining fields set to <see cref="null"/>
        /// </summary>
        public static InitializationResult Ok => OkSingleton;

        public bool IsOk { get; set; }

        public bool IsError => !IsOk;

        public string Caption { get; set; }

        public string Message { get; set; }

        public Exception InnerException { get; set; }

        public InitializationResult()
        {
            // no op
        } // constructor

        public InitializationResult(string message)
        {
            Message = message;
        } // InitializationResult

        public InitializationResult(Exception exception, string message)
        {
            Message = message;
            InnerException = exception;
        } // InitializationResult
    } // InitializationResult
} // namespace
