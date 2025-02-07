using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoszerviz
{
    internal class Alkatresz
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }

        public Alkatresz(string name, int price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }
        public override string ToString()
        {
            return $"Name: {Name}\n\tPrice: {Price}\n\tStock: {Stock}";
        }
    }
}
