using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Net.Gruppe9.LeaugeChampionDB.Data
{
    public class Champion
    {
        public Guid Id { get; set; }
        public string Version { get; set; }
        public string RiotId { get; set; }
        public string RiotKey { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public Info Info { get; set; }
        public Image Image { get; set; }
        public string Tag1 { get; set; }
        public string? Tag2 { get; set; }
        public string Partype { get; set; }
        public Stats Stats { get; set; }
    }
}
