using Microsoft.VisualBasic;
using System.Formats.Tar;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.AccessControl;

namespace autoszerviz
{
    internal class Program
    {
        static DateTime tartok = DateTime.Now;
        static Szerviz szervisz = new Szerviz("Autószerviz", "Budapest", null, null);
        static int szam_bekeres(string kerdes)
        {
            int szam;
            Console.Write(kerdes);
            while (!int.TryParse(Console.ReadLine(), out szam))
            {
                Console.WriteLine("Hibás adat, adjon meg egy számot!");
                Console.Write(kerdes);
            }
            return szam;
        }

        static string[] menu_points = new string[]
        {
            "Új autó érkezett",
            "Szerelők szerződősének módosítása",
            "Szerelők listázása",
            "Szerelendő autók listázása",
            "Visszaadott autók listázása",
            "Bevétel kiírása",
            "Idő pörgetése",
            "Kilépés"
        };
        
        static void Main(string[] args)
        {
            C_clear();
            int valasz = menu(menu_points);
            while (valasz != 0)
            {
                try
                {
                    //Console.WriteLine(valasz);
                    switch (valasz)
                    {
                        case 1:
                            new_car();
                            break;
                        case 2:
                            mod_rep();
                            break;
                        case 3:
                            C_clear();
                            Console.WriteLine(szervisz.PrintSzerelok());
                            break;
                        case 4:
                            C_clear();
                            Console.WriteLine(szervisz.PrintSzerelendo());
                            break;
                        case 5:
                            C_clear();
                            Console.WriteLine(szervisz.PrintMegszerelt());
                            break;
                        case 6:
                            C_clear();
                            Console.WriteLine($"Bevétel: {szervisz.Bevetel}");
                            break;
                        case 7:
                            forward_time();
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            Console.WriteLine("Press any button to continue");
            Console.ReadKey(true);
            C_clear();
            valasz = menu(menu_points);



            }
        }
        
        static int menu(string[] points)
        {
            Console.CursorVisible = false;
            int ans = 0;
            C_clear();
            //Console.Write(' ');
            //Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < points.Length; i++)
            {
                Console.WriteLine(points[i]);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            ConsoleKey pressed;
            do
            {
                pressed = Console.ReadKey(true).Key;
                if ((pressed == ConsoleKey.UpArrow || pressed == ConsoleKey.W) && ans > 0)
                {
                    ans--;
                    
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(0, ans);
                    Console.Write(points[ans]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(0, ans+1);
                    Console.Write(points[ans+1]);
                }
                if ((pressed == ConsoleKey.DownArrow || pressed == ConsoleKey.S) && ans < points.Length-1)
                {
                    ans++;

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(0, ans);
                    Console.Write(points[ans]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(0, ans-1);
                    Console.Write(points[ans - 1]);
                }

            } while (pressed != ConsoleKey.Enter);
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, points.Length);
            return (ans+1)%points.Length;
        }

        static void forward_time()
        {
            Console.WriteLine("Mikorra szeretné pörgetni az időt? (ÉÉÉÉ.HH.NN) ");
            string date = Console.ReadLine()!;
            DateTime new_time = DateTime.Parse(date);
            int hour = 8*((int)(new_time - tartok).TotalDays+1);
            if (hour < 0)
            {
                Console.WriteLine("A Dátumnál nagyobb időt írjon be");
                return;
            }
            Console.WriteLine(szervisz.forward_time(hour));
            if (tartok.Month != new_time.Month)
            {
                Console.WriteLine($"Bevétel: {szervisz.Bevetel}");
                try
                {
                    Console.WriteLine(szervisz.fizetes());
                }
                catch (Exception)
                {
                    Console.WriteLine("Nincs elég pénz kifizetni az alkalmazottakat, csődbe ment a cég.");
                }
            }
            tartok = new_time;
        }

        static void C_clear()
        {
            Console.Clear();
            Console.SetCursorPosition(50, 0);
            Console.Write(tartok.ToShortDateString());
            Console.SetCursorPosition(0, 0);
        }

        static void mod_rep()
        {
            string[] menu_opt = 
            {
                "1. Új szerelő",
                "2. Szerelő eltávolítása",
                "3. Szerelő fizetésének módosítása",
                "4. Szerelő elérhetőségének módosítása",
                "",
            };
            int valasz = menu(menu_opt);

            switch (valasz)
            {
                case 1:
                    Console.Write("Szerelő neve: ");
                    string name = Console.ReadLine()!;

                    int salary = szam_bekeres("Szerelő fizetése: ");
                    szervisz.AddSzerelo(name, salary);
                    Console.WriteLine("Szerelő hozzáadva az adatbázishoz");
                    break;
                case 2:
                    Console.Write("Szerelő neve: ");
                    name = Console.ReadLine()!;
                    Szerelo szerelo = szervisz.szerelok.Find(x => x.Name == name)!;
                    if (szervisz != null)
                    {
                        szervisz.szerelok.Remove(szerelo);
                        Console.WriteLine("Szerelő eltávolítva");
                    }
                    else
                    {
                        Console.WriteLine("Nincs ilyen nevű szerelő!");
                    }
                    break;
                case 3:
                    Console.Write("Szerelő neve: ");
                    name = Console.ReadLine()!;
                    szerelo = szervisz.szerelok.Find(x => x.Name == name)!; 
                    if (szerelo != null)
                    {
                        salary = szam_bekeres("Szerelő új fizetése: ");
                        szerelo.Salary = salary;
                        Console.WriteLine("A fizetésmódosítás megtörtént");

                    }
                    else
                    {
                        Console.WriteLine("Nincs ilyen nevű szerelő!");
                    }
                    break;
                case 4:
                    Console.Write("Szerelő neve: ");
                    name = Console.ReadLine()!;
                    szerelo = szervisz.szerelok.Find(x => x.Name == name)!;
                    if (szerelo != null)
                    {
                        szerelo.Available = !szerelo.Available;
                        Console.WriteLine($"Új elérhetőség: {szerelo.Available}");

                    }
                    else Console.WriteLine("Nincs ilyen nevű szerelő!");
                    
                    break;
            }
        }

        static void new_car()
        {
            C_clear();
            Console.Write("Autó rendszáma: ");
            string rendszam = Console.ReadLine()!;
            Auto auto = szervisz.megszerelt.Find(x => x.LicensePlate == rendszam)!;
            if (auto == null)
            {
                C_clear();
                Console.Write("Autó típusa: ");
                string tipus = Console.ReadLine()!;
                auto = new Auto(rendszam, tipus);
            }
            else
            {
                szervisz.megszerelt.Remove(auto);

            }
            auto.priority = menu(["Nem prioritás", "Prioritás", ""]) - 1 == 1;
            auto.munkalapok.Add(new Munkalap(tartok, auto.get_hiba()));
            szervisz.szerelendo.Add(auto);
            Console.WriteLine("Autó hozzáadva a szerelendők közé");
        }
    }
}