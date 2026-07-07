using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Net.Gruppe9.LeaugeChampionDB.Data
{
    public class Image
    {
        public string Full { get; set; }
        public string Sprite { get; set; }
        public string Group { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


    }
}
