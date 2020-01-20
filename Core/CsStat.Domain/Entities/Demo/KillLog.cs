namespace CsStat.Domain.Entities.Demo
{
    public class KillLog
    {
        public long? Killer { get; set; }
        public string KillerName { get; set; }
        public long? Victim { get; set; }
        public string VictimName { get; set; }
        public bool IsHeadshot { get; set; }
        public string Weapon { get; set; }
        public long? Assister { get; set; }
        public string AssisterName { get; set; }
        public bool IsSuicide { get; set; }
        public int RoundNumber { get; set; }
    }
}