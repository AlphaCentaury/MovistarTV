// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.Record.Serialization
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
            RecordAction retry;

            retry = new RecordAction()
            {
            };

            return retry;
        } // CreateWithDefaultValues
    } // class RecordAction
} // namespace
