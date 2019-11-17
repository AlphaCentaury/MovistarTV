// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration.Schema2014.Config;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers
{
    [XmlRoot("TvPlayers", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class TvPlayersSettings : IConfigurationItem
    {
        private TvPlayer _defaultPlayer;
        private Guid _defaultPlayerGuid;

        public TvPlayersSettings()
        {
            PlayerIcons = new ImageList
            {
                ImageSize = new Size(32, 32),
                ColorDepth = ColorDepth.Depth32Bit
            };
        } // constructor

        public Guid DefaultPlayerId
        {
            get => _defaultPlayerGuid;
            set
            {
                _defaultPlayerGuid = value;
                _defaultPlayer = null;
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
            if (_defaultPlayer == null)
            {
                foreach (var player in Players)
                {
                    if (player.Id == _defaultPlayerGuid)
                    {
                        _defaultPlayer = player;
                        break;
                    } // if
                } // foreach
            } // if

            return _defaultPlayer;
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

        bool IConfigurationItem.SupportsInitialization => false;

        bool IConfigurationItem.SupportsValidation => true;

        InitializationResult IConfigurationItem.Initialize()
        {
            throw new NotSupportedException();
        } // IConfigurationItem.Initialize

        string IConfigurationItem.Validate(string ownerTag)
        {
            var validationError = TvPlayer.ValidateArray(Players, "Player", "Players", ownerTag);
            return validationError ?? null;
        } // IConfigurationItem.Validate

        #endregion
    } // class TvPlayersSettings
} // namespace
