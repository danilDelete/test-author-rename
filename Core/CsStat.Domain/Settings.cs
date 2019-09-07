﻿using System;
using System.Configuration;
using System.IO;
using CsStat.SystemFacade.Extensions;

namespace CsStat.Domain
{
    public static class Settings
    {
        public static string LogsPath => GetSetting(nameof(LogsPath), Defaults.LogsPath);
        public static int FileReadNewLinesInterval => GetSetting(nameof(FileReadNewLinesInterval), Defaults.FileReadNewLinesInterval);
        public static int TimerInterval => GetSetting(nameof(TimerInterval), Defaults.TimerInterval);
        public static int TakeLines => GetSetting(nameof(TakeLines), Defaults.TakeLines);
        public static string PlayersDataApiPath => GetSetting(nameof(PlayersDataApiPath), Defaults.PlayersDataApiPath);
        public static string PlayersDataSteamApiPath => GetSetting(nameof(PlayersDataSteamApiPath), Defaults.PlayersDataSteamApiPath);
        public static long FirstSteamId => GetSetting(nameof(FirstSteamId), Defaults.FirstSteamId);
        public static string ApiKey => GetSetting(nameof(ApiKey), Defaults.ApiKey);



        private static string GetSetting(string settingName, string defaultValue = null)
        {
            var settingValue = ConfigurationManager.AppSettings[settingName];

            if (string.IsNullOrEmpty(settingValue))
            {
                settingValue = defaultValue;
            }

            return settingValue;
        }

        private static int GetSetting(string settingName, int defaultValue)
        {
            var settingValue = ConfigurationManager.AppSettings[settingName];

            return settingValue.ParseOrDefault(defaultValue);
        }

        private static long GetSetting(string settingName, long defaultValue)
        {
            var settingValue = ConfigurationManager.AppSettings[settingName];

            return settingValue.ParseOrDefault(defaultValue);
        }

        private static class Defaults
        {
            public static string LogsPath = Path.Combine(Environment.CurrentDirectory, "logs");
            public const int FileReadNewLinesInterval = 10000;
            public const int TimerInterval = 10000;
            public const int TakeLines = 50;
            public const string PlayersDataApiPath = "api/playersdata";
            public const string PlayersDataSteamApiPath = @"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/";
            public const long FirstSteamId = 76561197960265728;
            public const string ApiKey = "C03F2D79CF2FE20B64A85531031F3983";
        }
    }
}