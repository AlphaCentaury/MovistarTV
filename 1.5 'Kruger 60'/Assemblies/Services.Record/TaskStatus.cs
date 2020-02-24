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

namespace IpTviewr.Services.Record
{
    public enum TaskStatus
    {
        Ok = 0,
        MissingXml,
        XmlError,
        WindowsTaskMissing,
        WindowsTaskXmlError,
        WindowsTaskMoved,
    } // enum TaskStatus
} // namespace
