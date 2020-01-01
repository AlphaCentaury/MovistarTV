using System;
using System.ComponentModel;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public interface IToolMetadata
    {
        string Guid { get; }

        [DefaultValue(true)]
        bool HasGui { get; }

        [DefaultValue(false)]
        bool HasCli { get; }
    } // IToolMetadata
} // namespace