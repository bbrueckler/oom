using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace task6
{
    class Program
    {
        static Random rand = new Random();

        class GetRandom
        {

            public static string GetManuf()
            {
                var s = new List<string> { "Aprilia", "Honda", "Yamaha", "Harley Davidson", "Ducati", "Suzuki" };
                return s[rand.Next(0, s.Count)];
            }

            public static string GetModel()
            {
                var s = new List<string> { "XSR", "Tuono", "Hornet", "Sportster", "Hypermotard", "GSX-R" };
                return s[rand.Next(0, s.Count)];
            }
        }

        public static string PrintBike(Sportbike s)
        {
            return s.Manufacturer +
                        " " + s.Model + ": " +
                        s.GetMileage() + " km, " +
                        "Service in: " + s.GetNextService() + " km";

        }

        public static IEnumerable<Sportbike> CreateBike()
        {
            while (true)
            {
                var b = new Sportbike(GetRandom.GetManuf(), GetRandom.GetModel() + " " + (rand.Next(7,13)*100 - 10) , rand.Next(1, 99999), rand.Next(5, 20)*1000);
                yield return b;
            }
        }       

        static void Main(string[] args)
        {
            var newbikes = new Subject<Sportbike>();
            newbikes.Subscribe(o => Console.WriteLine("Observer received: " + PrintBike(o)));

            var t = new Thread(() =>
            {
                var b = CreateBike().GetEnumerator();

                while (b.MoveNext())
                {
                    Console.WriteLine("Enumerator generated: " + PrintBike(b.Current));

                    Console.WriteLine("Pushing to observer...");
                    newbikes.OnNext(b.Current);
                    Console.WriteLine("------------- Sleeping 5 seconds -------------");
                    Thread.Sleep(5000);
                }
            });

            t.Start();
            

        }
    }
}
