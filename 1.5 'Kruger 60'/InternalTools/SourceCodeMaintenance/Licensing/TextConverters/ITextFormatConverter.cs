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

using System;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.TextConverters
{
    public interface ITextFormatConverter: IDisposable
    {
        string ConvertFrom(string fromFormat, string text);
    } // interface ITextFormatConverter
} // namespace
