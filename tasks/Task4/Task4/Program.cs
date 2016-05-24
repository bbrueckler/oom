using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Task4
{
    interface Bike
    {
        string Manufacturer { get; }
        string Model { get; }
        int ServiceInterval { get; set; }

        int GetMileage();
        int GetNextService();
        void SetMileage(int new_mileage);
    }

    public class Sportbike : Bike
    {
        private int m_mileage;
        public string Manufacturer { get; }
        public string Model { get; }
        public int ServiceInterval { get; set; }

        public Sportbike(string manuf, string model, int mileage)
        {
            if (string.IsNullOrWhiteSpace(manuf)) throw new ArgumentException("Manufacturer must not be empty.", nameof(manuf));
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model must not be empty.", nameof(model));
            if (mileage < 0) throw new ArgumentOutOfRangeException("Mileage must not be negative. ", nameof(mileage));

            Manufacturer = manuf;
            Model = model;
            m_mileage = mileage;
        }

        public int GetNextService()
        {
            return ServiceInterval - (m_mileage % ServiceInterval);
        }

        public void SetMileage(int new_mileage)
        {
            if (new_mileage < m_mileage) throw new ArgumentOutOfRangeException("New mileage may not be less than the current mileage. ");
            m_mileage = new_mileage;
        }

        public int GetMileage()
        {
            return m_mileage;
        }
    }

    public class Dirtbike : Bike
    {
        private int m_mileage;
        public string Manufacturer { get; }
        public string Model { get; }
        public int ServiceInterval { get; set; }
        public int ServiceInterval_Oil { get; set; }

        public Dirtbike(string manuf, string model, int mileage)
        {
            if (string.IsNullOrWhiteSpace(manuf)) throw new ArgumentException("Manufacturer must not be empty.", nameof(manuf));
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model must not be empty.", nameof(model));
            if (mileage < 0) throw new ArgumentOutOfRangeException("Mileage must not be negative. ", nameof(mileage));

            Manufacturer = manuf;
            Model = model;
            m_mileage = mileage;
        }

        public int GetNextService()
        {
            return ServiceInterval - (m_mileage % ServiceInterval);
        }

        public int GetNextService_Oil()
        {
            return ServiceInterval - (m_mileage % ServiceInterval_Oil);
        }

        public void SetMileage(int new_mileage)
        {
            if (new_mileage < m_mileage) throw new ArgumentOutOfRangeException("New mileage may not be less than the current mileage. ");
            m_mileage = new_mileage;
        }

        public int GetMileage()
        {
            return m_mileage;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var bikes = new Bike[] {
                new Sportbike ("Yamaha", "MT-07", 13000),
                new Dirtbike ("Yamaha", "WR250", 4500)
            };

            bikes[0].ServiceInterval = 10000;
            bikes[1].ServiceInterval = 5000;

            var settings = new JsonSerializerSettings() { Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.Auto };
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filename = Path.Combine(desktop, "bikes.json");
            var json_strings = JsonConvert.SerializeObject(bikes, settings);

            File.WriteAllText(filename, json_strings);

            // Read JSON from file
            var json_text = File.ReadAllText(filename);
            var json_items = JsonConvert.DeserializeObject<Bike[]>(json_text, settings);

            // Print deserialized objects
            foreach (var b in json_items)
            {
                Console.WriteLine(b.Manufacturer + " " + b.Model + ": KM-Stand " + b.GetMileage() + " km.");
            }
        }
    }
}
