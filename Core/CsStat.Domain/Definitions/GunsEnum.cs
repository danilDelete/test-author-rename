using System.ComponentModel;
using CsStat.SystemFacade.Attributes;

namespace CsStat.Domain.Definitions
{
    public enum Guns
    {
        [Description("World")]
        [IsSniperRifle(false)] 
        World, // suicide

        [IsSniperRifle(false)]
        Unknown,

        [IsSniperRifle(false)]
        Null,

        [IsSniperRifle(false)]
        Bomb,

        [Description("Glock-18")]
        [LogValue("glock")]
        [IsSniperRifle(false)]
        Glock,

        [Description("Dual Berettas")]
        [LogValue("elite")]
        [IsSniperRifle(false)]
        Elite,

        [Description("P250")]
        [LogValue("p250")]
        [IsSniperRifle(false)]
        P250,

        [Description("Tec-9")]
        [LogValue("tec9")]
        [IsSniperRifle(false)]
        Tec9,

        [Description("CZ75-Auto")]
        [LogValue("cz75a")]
        [IsSniperRifle(false)]
        CZ75,

        [Description("Desert Eagle")]
        [LogValue("deagle")]
        [IsSniperRifle(false)]
        Deagle,

        [Description("R8 Revolver")]
        [LogValue("revolver")]
        [IsSniperRifle(false)]
        Revolver,

        [Description("USP-S")]
        [LogValue("usp_silencer")]
        [IsSniperRifle(false)]
        Usps,

        [Description("P2000")]
        [LogValue("hkp2000")]
        [IsSniperRifle(false)]
        P2000,


        [Description("Five-Seven")]
        [LogValue("fiveseven")]
        [IsSniperRifle(false)]
        FiveSeven,

        [Description("Nova")]
        [LogValue("nova")]
        [IsSniperRifle(false)]
        Nova,

        [Description("XM1014")]
        [LogValue("xm1014")]
        [IsSniperRifle(false)]
        Xml,

        [Description("Sawed-Off")]
        [LogValue("sawedoff")]
        [IsSniperRifle(false)]
        Sawedoff,

        [Description("M249")]
        [LogValue("m249")]
        [IsSniperRifle(false)]
        M249,

        [Description("Negev")]
        [LogValue("negev")]
        [IsSniperRifle(false)]
        Negev,

        [Description("MAG-7")]
        [LogValue("mag7")]
        [IsSniperRifle(false)]
        Mag,

        [Description("MAC-10")]
        [LogValue("mac10")]
        [IsSniperRifle(false)]
        Mac,

        [Description("MP7")]
        [LogValue("mp7")]
        [IsSniperRifle(false)]
        Mp7,

        [Description("MP5SD")]
        [LogValue("mp5sd")]
        [IsSniperRifle(false)]
        Mp5,

        [Description("Inferno")]
        [LogValue("inferno")]
        [IsSniperRifle(false)]
        Inferno,

        [Description("UMP-45")]
        [LogValue("ump45")]
        [IsSniperRifle(false)]
        Ump,

        [Description("P90")]
        [LogValue("p90")]
        [IsSniperRifle(false)]
        P90,

        [Description("PP-Bizon")]
        [LogValue("bizon")]
        [IsSniperRifle(false)]
        Bizon,

        [Description("MP9")]
        [LogValue("mp9")]
        [IsSniperRifle(false)]
        Mp9,

        [Description("Galil AR")]
        [LogValue("galilar")]
        [IsSniperRifle(false)]
        Galil,

        [Description("AK-47")]
        [LogValue("ak47")]
        [IsSniperRifle(false)]
        Ak,

        [Description("SSG 08")]
        [LogValue("ssg08")]
        [IsSniperRifle(true)]
        Ssg, // sniper

        [Description("SG 553")]
        [LogValue("sg556")]
        [IsSniperRifle(false)]
        Sg,

        [Description("AWP")]
        [LogValue("awp")]
        [IsSniperRifle(true)]
        Awp, //sniper

        [Description("Scar-20")]
        [LogValue("scar20")]
        [IsSniperRifle(true)]
        Scar, //sniper

        [Description("Scout")]
        [LogValue("scout")]
        [IsSniperRifle(true)]
        Scout, //sniper

        [Description("G3SG1")]
        [LogValue("g3sg1")]
        [IsSniperRifle(true)]
        G3sgl, //sniper

        [Description("FAMAS")]
        [LogValue("famas")]
        [IsSniperRifle(false)]
        Famas,

        [Description("M4A1")]
        [LogValue("m4a1")]
        [IsSniperRifle(false)]
        M4,

        [Description("M4A1-S")]
        [LogValue("m4a1_silencer")]
        [IsSniperRifle(false)]
        M4s,

        [Description("AUG")]
        [LogValue("aug")]
        [IsSniperRifle(false)]
        Aug,

        [Description("Zeus x27")]
        [LogValue("taser")]
        [IsSniperRifle(false)]
        Taser,

        [Description("Molotov")]
        [LogValue("molotov")]
        [IsSniperRifle(false)]
        Molotov,

        [Description("Decoy Grenade")]
        [LogValue("decoy")]
        [IsSniperRifle(false)]
        Decoy,

        [Description("Flashbang")]
        [LogValue("flashbang")]
        [IsSniperRifle(false)]
        Flash,

        [Description("High Explosive Grenade")]
        [LogValue("hegrenade")]
        [IsSniperRifle(false)]
        He,

        [Description("Smoke Grenade")]
        [LogValue("smokegrenade")]
        [IsSniperRifle(false)]
        Smoke,

        [Description("Incendiary Grenade")]
        [LogValue("incgrenade")]
        [IsSniperRifle(false)]
        Inc,

        [Description("Knife")]
        [LogValue("knife")]
        [IsSniperRifle(false)]
        Knife,
    }
}

