// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    [XmlType(TypeName="RecordConfig", Namespace=ConfigCommon.ConfigXmlNamespace)]
    public class RecordConfig
    {
        [XmlArray(ElementName="SaveLocations", Namespace = ConfigCommon.ConfigXmlNamespace)]
        [XmlArrayItem(ElementName="Location")]
        public RecordSaveLocation[] SaveLocations
        {
            get;
            set;
        } // SaveLocations

        [XmlArray("TaskSchedulerFolders", Namespace = ConfigCommon.ConfigXmlNamespace)]
        [XmlArrayItem(ElementName="Folder")]
        public RecordTaskSchedulerFolder[] TaskSchedulerFolders
        {
            get;
            set;
        } // TaskSchedulerFolders

        [XmlArray("Recorders", Namespace = ConfigCommon.ConfigXmlNamespace)]
        [XmlArrayItem("Recorder")]
        public RecorderConfig[] Recorders
        {
            get;
            set;
        } // Recorders

        [XmlIgnore]
        public string RecorderLauncherPath
        {
            get { return Path.Combine(AppUiConfiguration.Current.Folders.Install, Properties.InvariantTexts.RecorderLauncher); }
        } // RecorderLauncherPath

        public string Validate(string ownerTag)
        {
            string validationError;

            validationError = RecordSaveLocation.ValidateArray(SaveLocations, "Location", "SaveLocations", ownerTag);
            if (validationError != null) return validationError;

            validationError = RecordTaskSchedulerFolder.ValidateArray(TaskSchedulerFolders, "Folder", "TaskSchedulerFolders", ownerTag);
            if (validationError != null) return validationError;

            validationError = RecorderConfig.ValidateArray(Recorders, "Recorder", "Recorders", ownerTag);
            if (validationError != null) return validationError;

            return null;
        } // Validate
    } // class RecordConfig
} // namespace
