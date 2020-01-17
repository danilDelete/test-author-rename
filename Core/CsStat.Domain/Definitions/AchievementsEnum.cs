using System.ComponentModel;
using System.Security.Cryptography;
using CsStat.SystemFacade.Attributes;

namespace CsStat.Domain.Definitions
{
    public enum AchievementsEnum
    {
        [Caption("MVP")]
        [Description("Player with most points")]
        Mvp,

        [Caption("First by k/d")]
        [Description("First player by k/d")]
        First,

        [Caption("Second by k/d ")]
        [Description("Second player by k/d")]
        Second,

        [Caption("Third by k/d")]
        [Description("Third player by k/d")]
        Third,

        [Caption("Killer")]
        [Description("Player with most kills")]
        Killer,

        [Caption("Team player")]
        [Description("Player with most assists")]
        TeamPlayer,

        [Caption("Head Hunter")]
        [Description("Player with most head shots")]
        HeadHunter,

        [Caption("Sniper")]
        [Description("Player with most kills with sniper rifles")]
        Sniper,

        [Caption("Kenny")]
        [Description("They Killed Kenny! You Bastards!\nPlayer with most deaths")]
        Kenny,
        
        [Caption("Brutus")]
        [Description("Et tu, Brute? Player with most friendly kills")]
        Brutus, 

        [Caption("Pitcher")]
        [Description("Player with most HE grenade kills")]
        Pitcher,

        [Caption("Firebug")]
        [Description("Player with most fire grenade kills")]
        Firebug,

        [Caption("Sapper")]
        [Description("Player with most defused bombs")]
        Sapper,

        [Caption("Bomberman")]
        [Description("5-4-3-2-1 Boom! Player with most exploded bombs")]
        Bomberman

    }
}