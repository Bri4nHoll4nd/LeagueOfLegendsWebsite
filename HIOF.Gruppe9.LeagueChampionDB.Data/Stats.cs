using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Net.Gruppe9.LeaugeChampionDB.Data
{
    public class Stats
    {
        public float Hp { get; set; }
        public float HpPerLevel { get; set; }
        public float Mp { get; set; }
        public float MpPerLevel { get; set; }
        public float MoveSpeed { get; set; }
        public float Armour { get; set; }
        public float ArmourPerLevel { get; set; }
        public float SpellBlock { get; set; }
        public float SpellBlockPerLevel { get; set; }
        public float AttackRange { get; set; }
        public float HpRegen { get; set; }
        public float HpRegenPerLevel { get; set; }
        public float MpRegen { get; set; }
        public float MpRegenPerLevel { get; set; }
        public float Crit { get; set; }
        public float CritPerLevel { get; set; }
        public float AttackDamage { get; set; }
        public float AttackDamagePerLevel { get; set; }
        public float AttackSpeedPerLevel { get; set; }
        public float AttackSpeed { get; set; }
    }
}
