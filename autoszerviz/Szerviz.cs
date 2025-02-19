using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace autoszerviz
{
    internal class Szerviz
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Alkatresz> alkatreszek { get; set; }
        public List<Szerelo> szerelok { get; set; }
        public List<Auto> szerelendo { get; set; }
        public List<Auto> megszerelt { get; set; }
        public int Bevetel { get; set; }


        public Szerviz(string name, string address, List<Alkatresz>? alkatreszek, List<Szerelo>? szerelok)
        {
            Name = name;
            Address = address;
            if (alkatreszek != null) this.alkatreszek = alkatreszek;
            else this.alkatreszek = new List<Alkatresz>();
            if (szerelok != null) this.szerelok = szerelok;
            else this.szerelok = new List<Szerelo>();
            szerelendo = new List<Auto>();
            megszerelt = new List<Auto>();
        }

        public void AddAlkatresz(Alkatresz alkatresz)
        {
            alkatreszek.Add(alkatresz);
        }

        public void AddAlkatresz(string name, int price, int stock)
        {
            alkatreszek.Add(new Alkatresz(name, price, stock));
        }

        public void AddSzerelo(string name, int salary)
        {
            szerelok.Add(new Szerelo(name, salary));
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
            szerelok.Add(szerelo);
        }

        public string PrintSzerelendo()
        {
            string ans = "";
            this.szerelendo = this.szerelendo.OrderBy(x => !x.priority).ToList();
            foreach (var auto in szerelendo)
            {
                ans += $"Rendszámtábla: {auto.LicensePlate}\n\tTípus:{auto.tipus}\n\tHiba: {auto.get_hiba()}\n\tPrioritás: {auto.priority}\n";
            }
            return ans;
        }

        public string PrintMegszerelt()
        {
            string ans = "";
            foreach (var auto in megszerelt)
            {
                ans += $"Rendszámtábla: {auto.LicensePlate}\n\tTípus:{auto.tipus}";
            }
            return ans;
        }

        public string PrintAlkatreszek()
        {
            string ans = "";
            foreach (var alkatresz in alkatreszek)
            {
                ans += alkatresz;
            }
            return ans;
        }

        public string PrintSzerelok()
        {
            string ans = "";
            foreach (var szerelo in szerelok) ans += szerelo;
            return ans;
        }

        public override string ToString()
        {
            return $"Name: {Name}\nAddress: {Address}\nAlkatreszek: {PrintAlkatreszek()}\nSzerelok: {PrintSzerelok()}\n";
        }
            
        public string forward_time(int hour)
        {
            int jav_auto = 0;
            this.szerelendo = this.szerelendo.OrderBy(x => !x.priority).ToList();
            int repair_time = this.szerelok.Where(x => x.Available).Count() * hour;
            while (szerelendo.Count != 0 && repair_time > szerelendo[0].munkalapok.Last().repair_time  )
            {
                repair_time -= szerelendo[0].munkalapok.Last().repair_time;
                szerelendo[0].munkalapok.Last().done = true;
                megszerelt.Add(szerelendo[0]);
                szerelendo.RemoveAt(0);
                jav_auto++;
                Bevetel += 10;
            }
            string ans = $"{jav_auto} autó javítva\n";
            return ans;
        }
        public string fizetes()
        {
            int fizetes = szerelok.Where(x => x.Available).Sum(x => x.Salary);
            if (fizetes > Bevetel) throw new Exception("Nincs elég pénz a fizetésre");
            Bevetel -= fizetes;
            return $"A fizetés összege: {fizetes}";
        }
    }
}