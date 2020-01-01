using System;
using System.Drawing;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public interface IToolDataProvider
    {
        string Guid { get; }

        string Category { get; }

        string Name { get; }

        Image GetLogo(int size);
    } // interface IToolDataProvider
} // namespace