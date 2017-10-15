// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Configuration
{
    // interface for marking configuration items
    public interface IConfigurationItem
    {
        bool SupportsInitialization { get; }
        InitializationResult Initializate();

        bool SupportsValidation { get; }
        string Validate(string ownerTag);
    } // interface IConfigurationItem
} // namespace
