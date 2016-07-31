// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyFileVersion(SolutionVersion.AssemblyFileVersion)]
[assembly: AssemblyInformationalVersion(SolutionVersion.AssemblyInformationalVersion)]
[assembly: AssemblyProduct(SolutionVersion.AssemblyProduct)]
[assembly: AssemblyCompany("")]
[assembly: AssemblyTrademark("")]

internal static class SolutionVersion
{
    public const string DefaultAssemblyVersion = "1.0.1000.0";
    public const string AssemblyFileVersion = "1.0.1000.0";
    public const string AssemblyInformationalVersion = "1.0.1000.0";
    public const string AssemblyProduct = "MovistarTV DVB-IPTV software decoder" + " (" + ProductVersion + ")";
    public const string ProductVersion = "1.0 beta \"Wolf 424\"";
} // class SolutionVersion