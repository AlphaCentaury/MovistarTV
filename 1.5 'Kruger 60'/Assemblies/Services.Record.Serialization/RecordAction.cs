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

using System;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public class RecordAction
    {
        public string Filename
        {
            get;
            set;
        } // Filename

        public string FileExtension
        {
            get;
            set;
        } // FileExtension

        public string SaveLocationName
        {
            get;
            set;
        } // SaveLocationName

        public string SaveLocationPath
        {
            get;
            set;
        } // SaveLocationPath

        public RecordRecorder Recorder
        {
            get;
            set;
        } // Recorder

        public static RecordAction CreateWithDefaultValues()
        {
            RecordAction action;

            action = new RecordAction()
            {
            };

            return action;
        } // CreateWithDefaultValues
    } // class RecordAction
} // namespace
