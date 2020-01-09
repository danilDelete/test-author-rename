using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using BusinessFacade.Repositories.Implementations;
using CSStat.CsLogsApi.Extensions;
using CsStat.Domain.Definitions;
using CsStat.Domain.Entities;
using CsStat.SystemFacade;
using DataService;
using NUnit.Framework;

namespace CSStat.WebApp.Tests
{
    public static class ApiTests
    {
        private static readonly CsStat.LogApi.CsLogsApi _api = new CsStat.LogApi.CsLogsApi();

        [Test]
        [TestCase(@"d:\Projects\counterstrikestat\Latest\CSStat.WebApp.Tests\TestParse\logs.txt")]
        public static void ParseLogs(string file)
        {
            var logs = string.Empty;

            using (var sr = new StreamReader(file))
            {
                logs = sr.ReadToEnd();
            }

            var parsedLogs = _api.ParseLogs(logs.Split('\n').ToList());

            if (parsedLogs == null)
            {
                return;
            }

            //var logRepository = new BaseRepository(new MongoRepositoryFactory(new ConnectionStringFactory()));
            //logRepository.InsertBatch(parsedLogs);

        }
        [Test]
        [TestCase(@"d:\Projects\counterstrikestat\Latest\CSStat.WebApp.Tests\TestParse\logs.txt")]
        public static void GetWeapons(string file)
        {
            string logs;

            using (var sr = new StreamReader(file))
            {
                logs = sr.ReadToEnd();
            }

            var splitLogs = logs.Split('\n').ToList();
            var a = new List<string>();

            foreach (var log in splitLogs)
            {
                var splitLog = log.Split('"');
                if (splitLog.Length < 6)
                {
                    continue;
                }

                a.Add(splitLog[5]);
            }

            a.FilterInt().Distinct().ToList().ForEach(Console.WriteLine);

        }

        [Test]
        public static void AttributeTest()
        {
            var gun = Guns.Ak;
            var attributeList = gun.GetAttributeList();

            foreach (var attribute in attributeList)
            {
                Console.WriteLine($"{attribute.Key},{attribute.Value}");
            }

        }



        private static IEnumerable<string> FilterInt(this IEnumerable<string> list)
        {
            foreach (var item in list)
            {
                if (!int.TryParse(item, out var i))
                {
                    yield return item;
                }
            }
        }
            private static void PrintLog(Log log)
            {
                var action = string.IsNullOrEmpty(log.Action.GetDescription())
                    ? log.Action.ToString()
                    : log.Action.GetDescription();

                Console.WriteLine(Environment.NewLine);

                Console.WriteLine(
                    ($"PlayerName: {log?.Player.NickName},PlayerTeam: {log.PlayerTeam.GetDescription()},Action: {action},VictimName: {log?.Victim.NickName},VictimTeam: {log.VictimTeam.GetDescription()}," +
                     $"Gun: {log.Gun.GetDescription()},IsHeadshot: {log.IsHeadShot},DateTime: {log.DateTime.ToString(new CultureInfo("ru-RU", false).DateTimeFormat)}")
                    .Replace(',', '\n'));
                Console.WriteLine(Environment.NewLine);
            }
    }
}

