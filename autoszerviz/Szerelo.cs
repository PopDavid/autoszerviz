using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoszerviz
{
    public class Szerelo
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public bool Available { get; set; }

        public Szerelo(string name, int salary)
        {
            Name = name;
            Salary = salary;
            Available = true;
        }
    }
}
