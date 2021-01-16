using System;
using System.Collections.Generic;
using System.Text;

namespace ProgMob.Models
{
    public class DaysInWeek
    {
        public int n { get; set; }
        public Dictionary<string, string> CardInWeek = new Dictionary<string, string>();
        public Boolean ifSet { get; set; }
        public DaysInWeek()
        {

            CardInWeek.Add("LUN", "");
            CardInWeek.Add("MAR", "");
            CardInWeek.Add("MER", "");
            CardInWeek.Add("GIO", "");
            CardInWeek.Add("VEN", "");
            CardInWeek.Add("SAB", "");

        }
    }
}
