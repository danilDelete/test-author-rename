using System;
using System.Collections.Generic;
using CsStat.Domain.Models;
using MongoRepository;

namespace CsStat.Domain.Entities
{
    public class PlayerStatsModel : Entity, IBaseEntity
    {
        public Player Player { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int HeadShot { get; set; }
        public int TotalGames { get; set; }
        public int Defuse { get; set; }
        public int Explode { get; set; }
        public int Points { get; set; }
        public int FriendlyKills { get; set; }
        public int SniperRifleKills { get; set; }
        public int GrenadeKills { get; set; }
        public int MolotovKills { get; set; }
        public List<AchieveModel> Achievements { get; set; }
        public List<GunModel>Guns { get; set; }
        public List<PlayerModel> Victims { get; set; }
        public List<PlayerModel> FriendVictims { get; set; }
        public List<PlayerModel> Killers { get; set; }
        public List<PlayerModel> FriendKillers { get; set; }
        public double KdRatio
        {
            get
            {
                if (Deaths > 0)
                {
                    return Math.Round((double)Kills / Deaths,2);
                }

                return Kills;
            }
        }
        public double KillsPerGame
        {
            get
            {
                if (TotalGames == 0)
                {
                    return 0;
                }

                return Math.Round((double)Kills / TotalGames,2);
            }
        }
        public double AssistsPerGame
        {
            get
            {
                if (TotalGames == 0)
                {
                    return 0;
                }

                return Math.Round((double)Assists / TotalGames,2);
            }
        }
        public double DeathPerGame
        {
            get
            {
                if (TotalGames == 0)
                {
                    return 0;
                }

                return Math.Round((double)Deaths / TotalGames,2);
            }
        }
    }
}