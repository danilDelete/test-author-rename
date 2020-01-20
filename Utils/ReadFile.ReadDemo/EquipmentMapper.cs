using CsStat.Domain.Definitions;
using DemoInfo;

namespace ReadFile.ReadDemo
{
    public static class EquipmentMapper
    {
        public static Guns Map(EquipmentElement weapon)
        {
            switch (weapon)
            {
                case EquipmentElement.AK47:
                    return Guns.Ak;
                case EquipmentElement.AUG:
                    return Guns.Aug;
                case EquipmentElement.AWP:
                    return Guns.Awp;
                case EquipmentElement.Bizon:
                    return Guns.Bizon;
                case EquipmentElement.Bomb:
                    return Guns.Bomb;
                case EquipmentElement.CZ:
                    return Guns.CZ75;
                case EquipmentElement.Deagle:
                    return Guns.Deagle;
                case EquipmentElement.Decoy:
                    return Guns.Decoy;
                case EquipmentElement.DualBarettas:
                    return Guns.Elite;
                case EquipmentElement.Famas:
                    return Guns.Famas;
                case EquipmentElement.FiveSeven:
                    return Guns.FiveSeven;
                case EquipmentElement.Flash:
                    return Guns.Flash;
                case EquipmentElement.G3SG1:
                    return Guns.G3sgl;
                case EquipmentElement.Gallil:
                    return Guns.Galil;
                case EquipmentElement.Glock:
                    return Guns.Glock;
                case EquipmentElement.HE:
                    return Guns.He;
                case EquipmentElement.P2000:
                    return Guns.P2000;
                case EquipmentElement.Incendiary:
                    return Guns.Inc;
                case EquipmentElement.Knife:
                    return Guns.Knife;
                case EquipmentElement.M249:
                    return Guns.M249;
                case EquipmentElement.M4A4:
                    return Guns.M4;
                case EquipmentElement.M4A1:
                    return Guns.M4s;
                case EquipmentElement.Mac10:
                    return Guns.Mac;
                case EquipmentElement.Swag7:
                    return Guns.Mag;
                case EquipmentElement.Molotov:
                    return Guns.Molotov;
                case EquipmentElement.MP5SD:
                    return Guns.Mp5;
                case EquipmentElement.MP7:
                    return Guns.Mp7;
                case EquipmentElement.MP9:
                    return Guns.Mp9;
                case EquipmentElement.Negev:
                    return Guns.Negev;
                case EquipmentElement.Nova:
                    return Guns.Nova;
                case EquipmentElement.P250:
                    return Guns.P250;
                case EquipmentElement.P90:
                    return Guns.P90;
                case EquipmentElement.Revolver:
                    return Guns.Revolver;
                case EquipmentElement.SawedOff:
                    return Guns.Sawedoff;
                case EquipmentElement.Scar20:
                    return Guns.Scar;
                case EquipmentElement.SG556:
                    return Guns.Sg;
                case EquipmentElement.Smoke:
                    return Guns.Smoke;
                case EquipmentElement.Scout:
                    return Guns.Scout;
                case EquipmentElement.Zeus:
                    return Guns.Taser;
                case EquipmentElement.Tec9:
                    return Guns.Tec9;
                case EquipmentElement.UMP:
                    return Guns.Ump;
                case EquipmentElement.USP:
                    return Guns.Usps;
                case EquipmentElement.XM1014:
                    return Guns.Xml;
                case EquipmentElement.World:
                    return Guns.World;
                default:
                    return Guns.Unknown;
            }
        }
    }
}