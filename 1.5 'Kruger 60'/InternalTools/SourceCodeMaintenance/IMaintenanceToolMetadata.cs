// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.ComponentModel;

namespace AlphaCentaury.Tools.SourceCodeMaintenance
{
    public interface IMaintenanceToolMetadata
    {
        string Guid { get; }

        string Name { get; }

        [DefaultValue(null)]
        string CliName { get; }

        [DefaultValue(false)]
        bool HasParameters { get; }

        [DefaultValue(false)]
        bool HasFileParameters { get; }

        [DefaultValue(false)]
        bool HasUi { get; }

        [DefaultValue(false)]
        bool HasUsage { get; }
    } // interface IMaintenanceToolMetadata
} // namespace
