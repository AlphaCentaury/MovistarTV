// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
