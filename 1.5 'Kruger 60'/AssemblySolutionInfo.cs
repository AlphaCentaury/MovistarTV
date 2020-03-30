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

using System.Reflection;

[assembly: AssemblyFileVersion(SolutionVersion.AssemblyFileVersion)]
[assembly: AssemblyInformationalVersion(SolutionVersion.AssemblyInformationalVersion)]
[assembly: AssemblyProduct(SolutionVersion.AssemblyProduct)]
[assembly: AssemblyCompany(SolutionVersion.AssemblyCompany)]

internal static class SolutionVersion
{
    public const string DefaultAssemblyVersion = "1.5.1105.0";
    public const string AssemblyFileVersion = "1.5.1105.0";
    public const string AssemblyInformationalVersion = "1.5.1105.0";
    public const string AssemblyProduct = "IPTViewr: virtual decoder for movistar+" + " (v" + ProductVersion + ")";
    public const string ProductVersion = "1.5 \"Kruger 60\" beta 1 SP1";
    public const string DefaultCopyright = "Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury and contributors";
    public const string AssemblyCompany = "www.alphacentaury.org";

    public static string[] CopyrightHeaderLines =
    {
        "==============================================================================",
        "",
        "  Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury",
        "  All rights reserved.",
        "",
        "    See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root",
        "    for complete license information.",
        "",
        "  http://www.alphacentaury.org/movistartv",
        "  https://github.com/AlphaCentaury",
        "",
        "=============================================================================="
    };
} // class SolutionVersion
