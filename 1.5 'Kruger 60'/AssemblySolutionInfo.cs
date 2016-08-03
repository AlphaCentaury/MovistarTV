// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyFileVersion(SolutionVersion.AssemblyFileVersion)]
[assembly: AssemblyInformationalVersion(SolutionVersion.AssemblyInformationalVersion)]
[assembly: AssemblyProduct(SolutionVersion.AssemblyProduct)]
[assembly: AssemblyCompany("movistartv.codeplex.com")]

internal static class SolutionVersion
{
    public const string DefaultAssemblyVersion = "1.5.40.0";
    public const string AssemblyFileVersion = "1.5.40.0";
    public const string AssemblyInformationalVersion = "1.5.40.0";
    public const string AssemblyProduct = "IPTViewr: software decoder for movistar+" + " (v" + ProductVersion + ")";
    public const string ProductVersion = "1.5 \"Kruger 60\" Alpha 4";
    public const string DefaultCopyright = "Copyright (C) 2014-2016, AlphaCentaury and contributors";
} // class SolutionVersion