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

using System.ComponentModel;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces
{
    [PublicAPI]
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
