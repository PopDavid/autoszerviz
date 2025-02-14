using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoszerviz
{
    internal class Auto
    {
        Random rnd = new Random();
        private string licensePlate;
        public string tipus { get; set; }
        public List<Munkalap> munkalapok;
        public bool priority { get; set; }
        public string LicensePlate 
        { 
            get => licensePlate;
            set
            {
                // TODO: implement validation
                licensePlate = value;
            }
        }
        public Auto(string licensePlate, string tipus, bool priority = false)
        {
            this.tipus = tipus;
            LicensePlate = licensePlate;
            munkalapok = new List<Munkalap>();
            this.priority = priority;
        }

        public string get_hiba()
        {
            switch (this.tipus)
            {
                case "Volkswagen":
                    if (rnd.Next(1, 3) == 1) return "Olajcsere";
                    else return "Fék";
                case "BMW":
                    return "hűtőfolyadék";
                case "Ford":
                    if (rnd.Next(1, 2) == 1) return "hűtőfolyadék";
                    else return "Kerék";
                case "Audi":
                    if (rnd.Next(1, 4) == 1) return "izzó";
                    else return "kipufogó";
                case "Opel":
                    return "Kormány";
            }
            throw new Exception("Nem javítunk ilyen típusú autót");
        }
    }
}
