using System.Collections.Generic;

namespace Utils
{
    public class Define
    {
        public enum StatType
        {
            Damage,
            CriticalChance,
            CriticalDamage,
            Armor,
            MaxHealth,
            Evasion
        }
        public static readonly Dictionary<StatType, string> StatTypeZhName = new ()
        {
            [StatType.Damage] = "物理伤害",
            [StatType.CriticalChance] = "暴击几率",
            [StatType.CriticalDamage] = "暴击伤害",
            [StatType.MaxHealth] = "最大血量",
            [StatType.Armor] = "护甲值",
            [StatType.Evasion] = "闪避"
        };
    }
}