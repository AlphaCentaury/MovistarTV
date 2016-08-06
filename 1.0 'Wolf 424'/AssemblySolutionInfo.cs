// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyFileVersion(SolutionVersion.AssemblyFileVersion)]
[assembly: AssemblyInformationalVersion(SolutionVersion.AssemblyInformationalVersion)]
[assembly: AssemblyProduct(SolutionVersion.AssemblyProduct)]
[assembly: AssemblyCompany("movistartv.codeplex.com")]

internal static class SolutionVersion
{
    public const string DefaultAssemblyVersion = "1.0.40100.0";
    public const string AssemblyFileVersion = "1.0.40100.0";
    public const string AssemblyInformationalVersion = "1.0.40100.0";
    public const string AssemblyProduct = "movistar+ DVB-IPTV software decoder" + " (v" + ProductVersion + ")";
    public const string ProductVersion = "1.0 \"Wolf 424\" Update 1";
} // class SolutionVersion