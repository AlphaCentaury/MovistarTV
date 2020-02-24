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
using System.IO;
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
        public string RecorderLauncherPath=>Path.Combine(AppConfig.Current.Folders.Install, Properties.InvariantTexts.RecorderLauncher);

        public string Validate(string ownerTag)
        {
            var validationError = RecordSaveLocation.ValidateArray(SaveLocations, "Location", "SaveLocations", ownerTag);
            if (validationError != null) return validationError;

            validationError = RecordTaskSchedulerFolder.ValidateArray(TaskSchedulerFolders, "Folder", "TaskSchedulerFolders", ownerTag);
            if (validationError != null) return validationError;

            validationError = RecorderConfig.ValidateArray(Recorders, "Recorder", "Recorders", ownerTag);
            return validationError;
        } // Validate
    } // class RecordConfig
} // namespace
