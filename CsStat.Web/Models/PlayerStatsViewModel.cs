using System.Collections.Generic;
using CsStat.Domain.Entities;

namespace CsStat.Web.Models
{
    public class PlayerStatsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Points { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int FriendlyKills { get; set; }
        public double KillsPerGame { get; set; }
        public double AssistsPerGame { get; set; }
        public double DeathsPerGame { get; set; }
        public int DefusedBombs { get; set; }
        public int ExplodedBombs { get; set; }
        public int TotalGames { get; set; }
        public int HeadShot { get; set; }
        public double KdRatio { get; set; }
        public List<PlayerViewModel> Victims { get; set; }
        public List<PlayerViewModel> Killers { get; set; }
        public List<AchievementViewModel> Achievements { get; set; }
        public List<GunViewModel> Guns { get; set; }
    }
}