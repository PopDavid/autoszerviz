using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoszerviz
{
    internal class Szerviz
    {
        public string Name { get; set; }
        public string Address { get; set; }
        private List<Alkatresz> Alkatreszek { get; set; }
        private List<Szerelo> Szerelok { get; set; }

        public Szerviz(string name, string address)
        {
            Name = name;
            Address = address;
            Alkatreszek = new List<Alkatresz>();
            Szerelok = new List<Szerelo>();
        }

        public void AddAlkatresz(Alkatresz alkatresz)
        {
            Alkatreszek.Add(alkatresz);
        }

        public void AddAlkatresz(string name, int price, int stock)
        {
            Alkatreszek.Add(new Alkatresz(name, price, stock));
        }

        public void AddSzerelo(string name, int salary)
        {
            Szerelok.Add(new Szerelo(name, salary));
        }

        public void AddAlkatresz(List<Alkatresz> alkatreszek)
        {
            foreach (var i in alkatreszek)
            {
                this.AddAlkatresz(i);
            }
        }

        public void AddSzerelo(List<Szerelo> szerelok)
        {
            foreach (var i in szerelok)
            {
                this.AddSzerelo(i);
            }
        }

        public void AddSzerelo(Szerelo szerelo)
        {
            Szerelok.Add(szerelo);
        }

        public string PrintAlkatreszek()
        {
            string ans = "";
            foreach (var alkatresz in Alkatreszek)
            {
                    ans += alkatresz;
            }
            return ans;
        }

        public string PrintSzerelok()
        {
            string ans = "";
            foreach (var szerelo in Szerelok)
            {
                ans += szerelo;
            }
            return ans;
        }
    }
}
