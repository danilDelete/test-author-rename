using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using BusinessFacade.Repositories;
using CSStat.CsLogsApi.Extensions;
using CsStat.Domain.Entities.Demo;
using CsStat.SystemFacade;
using CsStat.SystemFacade.Extensions;
using DemoInfo;
using ReadFile.ReadDemo.Model;

namespace ReadFile.ReadDemo
{
    public class DemoReader : BaseWatcher
    {
        private const string SuqadA = "Team A";
        private const string SuqadB = "Team B";
        private const int SwapRoundNumber = 15;

        private static DemoParser _parser;
        private static Result _results;

        private static string _demoFileName;
        private static string _fullDemoFileName;
        
        private static bool _matchStarted;
        private static Round _currentRound;
        private static int _currentRoundNumber;
        private static int _roundEndedCount;

        private static int _lastTScore;
        private static int _lastCTScore;

        private static int _squadAScore;
        private static int _squadBScore;

        #region inint

        private readonly string path;
        private readonly IFileRepository<DemoFile> demoFileRepository;
        private readonly IBaseRepository demoRepository;
        private readonly IMapper mapper;

        public DemoReader(string path, IFileRepository<DemoFile> demoFileRepository, IBaseRepository demoRepository, IMapper mapper)
        {
            this.path = path;
            this.demoFileRepository = demoFileRepository;
            this.demoRepository = demoRepository;
            this.mapper = mapper;
        }

        #endregion

        protected override void ReadFile()
        {
            var allFiles = Directory.GetFiles(path, "*.dem");
            var newFiles = allFiles.Except(demoFileRepository.GetFiles().Select(x => x.Path)).ToArray();

            foreach (var file in newFiles)
            {
                var isSuccessfully = true;
                var error = string.Empty;
                try
                {
                    var fileStream = File.OpenRead(file);

                    _fullDemoFileName = fileStream.Name;
                    _demoFileName = Path.GetFileName(fileStream.Name);

                    if (_demoFileName.IsEmpty())
                        return;

                    Reset();
                    ParseDemo(fileStream);
                }
                catch (Exception e)
                {
                    isSuccessfully = false;
                    Console.WriteLine(e);
                    error = e.ToString();
                }
                finally
                {
                    demoFileRepository.AddFile(new DemoFile
                    {
                        Created = File.GetCreationTime(file),
                        Path = file,
                        IsSuccessfully = isSuccessfully,
                        Message = error
                    });
                }
            }
        }

        private static void Reset()
        {
            var players = new Dictionary<long, Model.Player>();
            if (_results != null && _results.Players.Any())
            {
                foreach (var player in _results.Players)
                {
                    players.Add(player.Key, new Model.Player(player.Value.Name, player.Value.SteamID));
                }
            }
            _results = new Result(_demoFileName) {Players = players};

            _matchStarted = default;
            _currentRound = null;
            _currentRoundNumber = 1;
            _roundEndedCount = default;

            _lastCTScore = default;
            _lastTScore = default;
            
            _squadAScore = default;
            _squadBScore = default;
        }

        private void ParseDemo(FileStream file)
        {
            _parser = new DemoParser(file);
            _parser.ParseHeader();
            _parser.MatchStarted += Parser_MatchStarted;
            _parser.RoundStart += Parser_RoundStart;
            _parser.PlayerKilled += Parser_PlayerKilled;
            _parser.RoundEnd += Parser_RoundEnd;
            _parser.TickDone += Parser_TickDone;
            _parser.BombPlanted += Parser_BombPlanted;
            _parser.BombDefused += Parser_BombDefused;
            _parser.BombExploded += Parser_BombExploded;
            _parser.PlayerBind += Parser_PlayerBind;
            
            Console.WriteLine(
                $"Parse file: \"{_demoFileName}\" Size: {new FileInfo(file.Name).Length.ToSize(LongExtension.SizeUnits.MB)}Mb");

            var sw = new Stopwatch();
            sw.Start();
            _parser.ParseToEnd();
            sw.Stop();

            MatchFinish();

            Console.WriteLine($"It took: {sw.Elapsed:mm':'ss':'fff}");
        }

        private static DateTime? GetDemoDate(string fileName)
        {
            var sections = fileName?.Split('-');
            if (sections?.Length == 6)
            {
                if (DateTime.TryParseExact(string.Join(" ", sections.Skip(1).Take(2)),
                    "yyyyMMdd hhmmss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var date))
                    return date;
            }

            return null;
        }

        private void MatchFinish()
        {
            var demoLog = new DemoLog
            {
                Map = _parser.Map,
                MatchDate = GetDemoDate(_demoFileName),
                Size = new FileInfo(_fullDemoFileName).Length,
                DemoFileName = _results.DemoFileName,
                ParsedDate = DateTime.Now,
                TotalSquadAScore = _squadAScore,
                TotalSquadBScore = _squadBScore,
                Players = _results.Players.Select(x => new PlayerLog
                {
                    Name = x.Value.Name,
                    SteamID = x.Value.SteamID,
                    Assists = x.Value.Assists?.Select(z => mapper.Map<KillLog>(z)).ToList(),
                    Deaths = x.Value.Deaths?.Select(z => mapper.Map<KillLog>(z)).ToList(),
                    Kills = x.Value.Kills?.Select(z => mapper.Map<KillLog>(z)).ToList(),
                    Teamkills = x.Value.Teamkills?.Select(z => mapper.Map<KillLog>(z)).ToList(),
                    BombDefuses = x.Value.BombDefuses?.Select(z => z.RoundNumber).ToList(),
                    BombExplosions = x.Value.BombExplosions?.Select(z => z.RoundNumber).ToList(),
                    BombPlants = x.Value.BombPlants?.Select(z => z.RoundNumber).ToList()
                }).ToList(),
                Rounds = _results.Rounds?.Select(x => new RoundLog
                {
                    BombPlanter = x.Value.BombPlanter?.SteamID,
                    BombPlanterName = x.Value.BombPlanter?.Name,
                    BombDefuser = x.Value.BombDefuser?.SteamID,
                    BombDefuserName = x.Value.BombDefuser?.Name,
                    IsBombExploded = x.Value.IsBombExploded,
                    Reason = (CsStat.Domain.Definitions.RoundEndReason) (int) x.Value.Reason,
                    ReasonTitle = x.Value.Reason.ToString(),
                    RoundNumber = x.Value.RoundNumber,
                    Winner = x.Value.Winner == Team.CounterTerrorist
                        ? CsStat.Domain.Definitions.Teams.Ct
                        : CsStat.Domain.Definitions.Teams.T,
                    WinnerTitle = x.Value.Winner.ToString(),
                    TScore = x.Value.TScore,
                    CTScore = x.Value.CTScore,
                    Squads = x.Value.Squads.Select(z => new SquadLog()
                    {
                        Team = z.Team.ToString(),
                        SquadTitle = z.Title,
                        Players = z.Players.Select(k => new PlayerLog
                        {
                            Name = k.Name,
                            SteamID = k.SteamID,
                            Kills = k.Kills.Where(p => p.RoundNumber == x.Value.RoundNumber)
                                .Select(v => mapper.Map<KillLog>(v)).ToList(),
                            Assists = k.Assists.Where(p => p.RoundNumber == x.Value.RoundNumber)
                                .Select(v => mapper.Map<KillLog>(v)).ToList(),
                            Deaths = k.Deaths.Where(p => p.RoundNumber == x.Value.RoundNumber)
                                .Select(v => mapper.Map<KillLog>(v)).ToList(),
                            Teamkills = k.Teamkills.Where(p => p.RoundNumber == x.Value.RoundNumber)
                                .Select(v => mapper.Map<KillLog>(v)).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            demoRepository.Insert(demoLog);
        }

        private static void Parser_PlayerBind(object sender, PlayerBindEventArgs e)
        {
            _results = _results ?? new Result(_demoFileName);

            if (e.Player == null || e.Player.SteamID == 0 || _results.Players.ContainsKey(e.Player.SteamID))
            {
                return;
            }

            _results.Players.Add(e.Player.SteamID, new Model.Player(e.Player.Name, e.Player.SteamID));
        }

        private void Parser_MatchStarted(object sender, MatchStartedEventArgs e)
        {
            if (_matchStarted) // if we here again it means that the demo file contains two or more matches
            {
                MatchFinish();
                Reset();
            }

            _matchStarted = true;

            _results = _results ?? new Result(_demoFileName);

            _currentRound = new Round {RoundNumber = _currentRoundNumber};
        }

        private static void Parser_RoundEnd(object sender, RoundEndedEventArgs e)
        {
            if (!_matchStarted) 
                return;

            _currentRound.Reason = e.Reason;
            _roundEndedCount = 1;
        }

        private static void Parser_RoundStart(object sender, RoundStartedEventArgs e)
        {
            if (!_matchStarted)
                return;

            _results = _results ?? new Result(_demoFileName);

            _currentRound = new Round();
            _currentRoundNumber++;
            _currentRound.RoundNumber = _currentRoundNumber;
        }

        private static void Parser_TickDone(object sender, TickDoneEventArgs e)
        {
            if (_roundEndedCount == 0)
            {
                return;
            }

            _roundEndedCount++;

            if (_roundEndedCount < _parser.TickRate * 2)
            {
                return;
            }

            _roundEndedCount = 0;
            RoundEnd();
        }

        private static void RoundEnd()
        {
            Console.WriteLine($"Round number: {_currentRound.RoundNumber}");

            if (_lastCTScore + _lastTScore > SwapRoundNumber - 1)
            {
                if (_lastTScore != _parser.TScore)
                {
                    _squadBScore++;
                }
                else if (_lastCTScore != _parser.CTScore)
                {
                    _squadAScore++;
                }
            }
            else
            {
                if (_lastTScore != _parser.TScore)
                {
                    _squadAScore++;
                }
                else if (_lastCTScore != _parser.CTScore)
                {
                    _squadBScore++;
                }
            }

            var winningTeam = Team.Spectate;
            if (_lastTScore != _parser.TScore)
            {
                winningTeam = Team.Terrorist;
            }
            else if (_lastCTScore != _parser.CTScore)
            {
                winningTeam = Team.CounterTerrorist;
            }

            _lastCTScore = _parser.CTScore;
            _lastTScore = _parser.TScore;

            _currentRound.Winner = winningTeam;

            _currentRound.TScore = _parser.TScore;
            _currentRound.CTScore = _parser.CTScore;

            _currentRound.Squads = _parser.Participants
                .Where(x => x.SteamID != 0 && x.Team != Team.Spectate) // skip spectators
                .GroupBy(x => new {x.Team})
                .Select(x => new Squad
                {
                    Team = x.Key.Team,
                    Title = GetSquadName(x.Key.Team),
                    Players = x.Select(z => _results.Players[z.SteamID]).ToList()
                })
                .ToList();

            _results.Rounds.Add(_currentRoundNumber, _currentRound);
        }

        private static string GetSquadName(Team team)
        {
            if (_lastCTScore + _lastTScore > SwapRoundNumber)
            {
                return team == Team.CounterTerrorist ? SuqadA : SuqadB;
            }
            else
            {
                return team == Team.CounterTerrorist ? SuqadB : SuqadA;
            }
        }

        private static void Parser_PlayerKilled(object sender, PlayerKilledEventArgs e)
        {
            if(!_matchStarted)
                return;

            if (e.Killer != null && e.Killer.SteamID != 0 && e.Victim != null && e.Victim.SteamID != 0)
            {
                var kill = new Kill(_results.Players[e.Killer.SteamID], _results.Players[e.Victim.SteamID],
                    e.Headshot, EquipmentMapper.Map(e.Weapon.Weapon).GetDescription(), _currentRoundNumber,
                    e.Killer.SteamID == e.Victim.SteamID);

                if (e.Assister != null)
                {
                    kill.Assister = _results.Players[e.Assister.SteamID];
                    kill.Assister.Assists.Add(kill);
                }

                _results.Players[e.Killer.SteamID].Kills.Add(kill);
                _results.Players[e.Victim.SteamID].Deaths.Add(kill);

                if (e.Killer.Team == e.Victim.Team)
                {
                    _results.Players[e.Killer.SteamID].Teamkills.Add(kill);
                }
            }

            // suicide
            if (e.Killer == null && e.Victim != null && e.Victim.SteamID != 0)
            {
                var kill = new Kill(null, _results.Players[e.Victim.SteamID],
                    e.Headshot, EquipmentMapper.Map(e.Weapon.Weapon).GetDescription(),
                    _currentRoundNumber, true);

                if (e.Assister != null)
                {
                    kill.Assister = _results.Players[e.Assister.SteamID];
                    kill.Assister.Assists.Add(kill);
                }

                _results.Players[e.Victim.SteamID].Deaths.Add(kill);
            }
        }

        private static void Parser_BombDefused(object sender, BombEventArgs e)
        {
            _results.Players[e.Player.SteamID].BombDefuses.Add(_currentRound);
            _currentRound.BombDefuser = _results.Players[e.Player.SteamID];
        }

        private static void Parser_BombPlanted(object sender, BombEventArgs e)
        {
            _results.Players[e.Player.SteamID].BombPlants.Add(_currentRound);
            _currentRound.BombPlanter = _results.Players[e.Player.SteamID];
        }

        private static void Parser_BombExploded(object sender, BombEventArgs e)
        {
            _results.Players[e.Player.SteamID].BombExplosions.Add(_currentRound);
            _currentRound.IsBombExploded = true;
        }
    }
}