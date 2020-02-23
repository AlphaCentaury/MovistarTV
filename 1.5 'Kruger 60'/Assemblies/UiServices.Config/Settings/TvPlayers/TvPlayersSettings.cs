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

using IpTviewr.UiServices.Configuration.Schema2014.Config;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using IpTviewr.Common.Configuration;
using IpTviewr.UiServices.Configuration.Properties;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers
{
    [XmlRoot("TvPlayers", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class TvPlayersSettings : IConfigurationItem
    {
        internal const string PlayerNoIconKey = "<empty>";
        internal const string PlayerNotFoundKey = "<null>";

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
            if (PlayerIcons.Images.ContainsKey(playerPath)) return playerPath;

            Icon icon = null;
            var key = playerPath;

            if (File.Exists(playerPath))
            {
                try
                {
                    icon = Icon.ExtractAssociatedIcon(playerPath);
                }
                catch
                {
                    // ignore
                } // try-catch

                if (icon == null)
                {
                    key = PlayerNoIconKey;
                    if (PlayerIcons.Images.ContainsKey(key)) return key;

                    icon = Resources.GenericFile;
                }
            }
            else
            {
                key = PlayerNotFoundKey;
                if (PlayerIcons.Images.ContainsKey(key)) return key;

                icon = Resources.NotFound;
            } // if-else

            PlayerIcons.Images.Add(key, icon);

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
