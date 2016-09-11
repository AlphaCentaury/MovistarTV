// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using IpTviewr.UiServices.Configuration.Schema2014.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers
{
    [XmlRoot("TvPlayers", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class TvPlayersSettings : IConfigurationItem
    {
        private TvPlayer DefaultPlayer;
        private Guid DefaultPlayerGuid;

        public TvPlayersSettings()
        {
            PlayerIcons = new ImageList();
            PlayerIcons.ImageSize = new Size(32, 32);
            PlayerIcons.ColorDepth = ColorDepth.Depth32Bit;
        } // constructor

        public Guid DefaultPlayerId
        {
            get
            {
                return DefaultPlayerGuid;
            } // get
            set
            {
                DefaultPlayerGuid = value;
                DefaultPlayer = null;
            } // set
        } // DefaultPlayerId

        public bool DirectLaunch
        {
            get;
            set;
        } // DirectLaunch

        [XmlArray("Players", Namespace = ConfigCommon.ConfigXmlNamespace)]
        [XmlArrayItem("Player")]
        public TvPlayer[] Players
        {
            get;
            set;
        } // Players

        public TvPlayer GetDefaultPlayer()
        {
            if (DefaultPlayer == null)
            {
                foreach (var player in Players)
                {
                    if (player.Id == DefaultPlayerGuid)
                    {
                        DefaultPlayer = player;
                        break;
                    } // if
                } // foreach
            } // if

            return DefaultPlayer;
        } // GetDefaultPlayer

        [XmlIgnore]
        public ImageList PlayerIcons
        {
            get;
            private set;
        } // PlayerIcons

        public string GetPlayerIconKey(string playerPath)
        {
            var key = playerPath.ToLower();
            if (!PlayerIcons.Images.ContainsKey(key))
            {
                var icon = Icon.ExtractAssociatedIcon(playerPath);
                PlayerIcons.Images.Add(key, icon);
            } // if

            return key;
        } // GetPlayerIconKey

        public Image GetPlayerIcon(string playerPath)
        {
            var key = GetPlayerIconKey(playerPath);
            return PlayerIcons.Images[key];
        } // GetPlayerIcon

        #region IConfigurationItem implementation

        bool IConfigurationItem.SupportsInitialization
        {
            get { return false; }
        } // IConfigurationItem.SupportsInitialization

        bool IConfigurationItem.SupportsValidation
        {
            get { return true; }
        } // IConfigurationItem.CanValidate

        InitializationResult IConfigurationItem.Initializate()
        {
            throw new NotSupportedException();
        } // IConfigurationItem.Initializate

        string IConfigurationItem.Validate(string ownerTag)
        {
            string validationError;

            validationError = TvPlayer.ValidateArray(Players, "Player", "Players", ownerTag);
            if (validationError != null) return validationError;

            return null;
        } // IConfigurationItem.Validate

        #endregion
    } // class TvPlayersSettings
} // namespace
