using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using BusinessFacade.Repositories;
using BusinessFacade.Repositories.Implementations;
using CSStat.CsLogsApi.Extensions;
using CsStat.Domain.Entities;
using CsStat.LogApi.Enums;
using CsStat.Web.Helpers;
using CsStat.Web.Models;
using CSStat.WebApp.Tests.Entity;
using DataService;
using DataService.Interfaces;
using MongoDB.Driver;
using NUnit.Framework;
using MongoRepository;

namespace CSStat.WebApp.Tests
{
    public class MongoRepositoryFactoryTests
    {

        private readonly IMongoRepositoryFactory _mongoRepository;
        private readonly IConnectionStringFactory _connectionString;
        private readonly ILogsRepository _logRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IUsefulLinkRepository _usefulLinkRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserRegistrationService _registrationService;

        public  MongoRepositoryFactoryTests()
        {
            _connectionString = new ConnectionStringFactory();
            _mongoRepository = new MongoRepositoryFactory(_connectionString);
            _logRepository = new LogsRepository(_mongoRepository);
            _playerRepository = new PlayerRepository(_mongoRepository);
            _baseRepository = new BaseRepository(_mongoRepository);
            _usefulLinkRepository = new UsefulLinkRepository(_mongoRepository);
            _userRepository = new UserRepository(_mongoRepository);
            _registrationService = new UserRegistrationService(_userRepository);
        }
        [Test]
        public void ReturnRepositoryOfType()
        {
            var repository = _mongoRepository.GetRepository<TestEntity>();
            Assert.True(repository.GetType() == typeof(MongoRepository<TestEntity>));
        }

        [Test]
        public void ConnectToDataBase()
        {
            var connectionString = _connectionString.GetConnectionString();
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                new MongoUrl(connectionString)
            );
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var server = mongoClient.GetServer();
            server.GetDatabaseNames().ToList().ForEach(Console.WriteLine);
            var db = server.GetDatabase("test");
            var collection = db.GetCollection<MongoRepository.Entity>("entities");
            collection.Insert(new Player { NickName = "Jack" });
        }

        [Test]
        public void GetPLayersLogsForPeriod()
        {
            //var startDate = DateTime.ParseExact(@"08/26/2019 - 12:17:29", "MM/dd/yyyy - HH:mm:ss", CultureInfo.InvariantCulture);
            //var endDate = DateTime.ParseExact(@"08/26/2019 - 23:59:32", "MM/dd/yyyy - HH:mm:ss", CultureInfo.InvariantCulture);
            var logs =_playerRepository.GetStatsForAllPlayers();

            if (logs.Any())
            {
                foreach (var logModel in logs)
                {
                    PrintPlayerStat(logModel);
                }
            }
        }

        [Test]
        public void GetPlayersInfo()
        {
            var players = _playerRepository.GetStatsForAllPlayers().OrderByDescending(x=>x.Points);

            foreach (var player in players)
            {
                PrintPlayerStat(player);
                Console.WriteLine(Environment.NewLine);
            }
        }
        private void MappingTest(IOrderedEnumerable<PlayerStatsModel> players)
        {
            foreach (var player in players)
            {
                var id = player.Player.Id;
                var nick = player.Player.NickName;
                var assists = player.Assists;
                var assistsPerGame = player.AssistsPerGame;
                var death = player.Deaths;
                var playerDeathPerGame = player.DeathPerGame;
                var playerDefuse = player.Defuse;
                var playerExplode = player.Explode;
                var friendlyKillsa = player.FriendlyKills;
                var headShot = player.HeadShot;
                var imagePatha = player.Player.ImagePath;
                var kdRatio = player.KdRatio;
                var PlayerKills = player.Kills;
                var killsPerGame = player.KillsPerGame;
                var playerPoints = player.Points;
                var games = player.TotalGames;
                var guns = player.Guns;
                var achieve = player.Achievements;
            }
        }

        [Test]
        [TestCase(@"\Files\Images\test.jpg")]
        public void UpdatePlayer(string imagePath)
        {
            var player = _playerRepository.GetPlayerByNickName("Host");
            _playerRepository.UpdatePlayer(player.Id, "Danil", "Shilov", imagePath);
        }
        [Test]
        public void GetPlayers()
        {
            var players = _playerRepository.GetAllPlayers();
            players.ToList().ForEach(PrintPlayer);
        }

        [Test]
        public void GetSteamId()
        {
            const string idFormLog = "STEAM_1:1:35063322";
            const long firstId = 76561197960265728;
            var idNumber = int.Parse(idFormLog.Split(':').Last());
            var universe = int.Parse(idFormLog.Split(':')[1]);
            var steamId = idNumber * 2 + firstId + universe;
            Console.WriteLine(steamId);
        }

        [Test]
        public void GetLogs()
        {
            var logs = _logRepository.GetAllLogs().Where(x => x.Action == Actions.Defuse && x.Player.NickName=="DJoony").ToList();
            logs.ForEach(PrintLog);
        }
        [Test]
        public void GetSteamAvatars()
        {
            var players = _playerRepository.GetAllPlayers();
        }

        [Test]
        public void AddInfo()
        {
            var info = new UsefulInfo
            {
                Description = "Lorem ipsum dolor sit amet",
                Caption = "Test info",
                PublishDate = DateTime.Now,
                Url = "google.com"
            };

            _usefulLinkRepository.Add(info);

        }

        private static byte[] ReadImage(string path)
        {
          using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
          {
              using (var br = new BinaryReader(fs))
              {
                  var fInfo = new FileInfo(path);
                  return br.ReadBytes((int) fInfo.Length);
              }
          }
        }

        [Test]
        public void SignUp()
        {
            var userModel = new SignInViewModel()
            {
                Name = "admin",
                Password = "salo"
            };
            var result = _registrationService.SignUp(userModel).FirstOrDefault();
            Console.WriteLine(result.Value);
        }

        [Test]
        public void SignIn()
        {
            var userModel = new SignInViewModel
            {
                Name = "admin",
                Password = "123@SX"
            };
            var result = _registrationService.SignIn(userModel);
            Console.WriteLine(result.ToString());
        }
        private static void PrintPlayer(Player player)
        {
            Console.WriteLine($"First Name: {player.FirstName},Second Name: {player.SecondName},Nick Name: {player.NickName},Image: {player.ImagePath}".Replace(',', '\n'));
            Console.WriteLine(Environment.NewLine);
        }

        private static void PrintPlayerStat(PlayerStatsModel log)
        {
            var gun = string.IsNullOrEmpty(log.Guns?.FirstOrDefault()?.Gun.GetDescription())
                ? log.Guns?.FirstOrDefault()?.Gun.ToString() ?? ""
                : log.Guns?.FirstOrDefault()?.Gun.GetDescription() ?? "";

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine(
                ($"PlayerName: {log.Player.NickName},Kills: {log.Kills},Deaths: {log.Deaths},Assists: {log.Assists}," +
                $"Friendly Kills: {log.FriendlyKills},K/D ratio: {log.KdRatio},Total Games: {log.TotalGames},Kills Per Game: {log.KillsPerGame}," +
                $"Points: {log.Points},Acheivements: {string.Join(" | ", log.Achievements.Select(x=>x.Achieve.GetDescription()).ToList())}," +
                $"Death Per Game: {log.DeathPerGame},Favorite Gun: {gun},Head shot: {log.HeadShot}%,Defused bombs: {log.Defuse},Explode Bombs: {log.Explode}").Replace(',', '\n'));
        }

        private static void PrintLog(Log log)
        {
            var action = string.IsNullOrEmpty(log.Action.GetDescription())
                ? log.Action.ToString()
                : log.Action.GetDescription();

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine(
                ($"PlayerName: {log.Player.NickName},PlayerTeam: {log.PlayerTeam.GetDescription()},Action: {action},VictimName: {log.Victim?.NickName},VictimTeam: {log.VictimTeam.GetDescription()}," +
                $"Gun: {log.Gun.GetDescription()},IsHeadshot: {log.IsHeadShot},DateTime: {log.DateTime.ToString(new CultureInfo("ru-RU", false).DateTimeFormat)}")
                    .Replace(',', '\n'));
            Console.WriteLine(Environment.NewLine);
        }
    }
}