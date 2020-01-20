namespace ReadFile.ReadDemo.Model
{
    public class Kill
    {
        public Kill(Player killer, Player victim, bool isHeadshot, string weapon, int roundNumber, bool isSuicide)
        {
            Killer = killer;
            Victim = victim;
            IsHeadshot = isHeadshot;
            Weapon = weapon;
            RoundNumber = roundNumber;
            IsSuicide = isSuicide;
        }

        public Player Killer { get; set; }
        public Player Victim { get; set; }
        public bool IsHeadshot { get; set; }
        public string Weapon { get; set; }
        public int RoundNumber { get; set; }

        public bool IsSuicide { get; set; }
        
        public Player Assister { get; set; }
    }
}
