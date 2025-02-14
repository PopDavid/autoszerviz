using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace autoszerviz
{
    internal class Munkalap
    {
        Random rnd = new Random();
        public DateTime arrive_time { get; set; }
        public DateTime leave_time { get; set; }
        public string hiba { get; set; }
        public int repair_time { get; set; }
        public bool done { get; set; }
        public Munkalap(DateTime arrive_time, string hiba)
        {
            this.arrive_time = arrive_time;
            this.hiba = hiba;
            done = false;
            switch (hiba)
            {
                case "hűtőfolyadék":
                case "Szélvédő":
                case "Olajcsere":
                    repair_time = rnd.Next(2, 4);
                    break;

                case "kipufogó":
                case "Fék":
                case "Kormány":
                    repair_time = rnd.Next(3, 5);
                    break;
                case "Kérek":
                case "Izzó":
                    repair_time = rnd.Next(1, 2);
                    break;
            }
        }

    }
}
