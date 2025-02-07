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
        private string licensePlate;
        public string LicensePlate 
        { 
            get => licensePlate;
            set
            {
                // TODO: implement validation
                licensePlate = value;
            }
        }
        public Auto(string licensePlate) 
        {
            LicensePlate = licensePlate;
        }

    }
}
