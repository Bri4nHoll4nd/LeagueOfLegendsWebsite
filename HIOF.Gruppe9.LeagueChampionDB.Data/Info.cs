using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Net.Gruppe9.LeaugeChampionDB.Data
{
    public class Info
    {
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Magic { get; set; }
        public int Difficulty { get; set; }
    }
}
