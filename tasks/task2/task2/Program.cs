using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    public class Bike
    {
        private int m_mileage;
        public string Manufacturer { get; }
        public string Model { get; }
        public int ServiceInterval { get; set; }

        public Bike(string manuf, string model, int mileage)
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

    class Program
    {
        static void Main(string[] args)
        {
            var myBike = new Bike("Yamaha", "MT-07", 13000);
            myBike.ServiceInterval = 10000;

            Console.WriteLine("Data for my " + myBike.Manufacturer + " " + myBike.Model + ": ");
            Console.WriteLine("Current mileage: " + myBike.GetMileage() + "km. Next maintenance in: " + myBike.GetNextService() + "km.");
            int newMileage = 23460;
            Console.WriteLine("Setting mileage to " + newMileage + "km.");
            myBike.SetMileage(newMileage);
            Console.WriteLine("Current mileage: " + myBike.GetMileage() + "km. Next maintenance in: " + myBike.GetNextService() + "km.");
        }
    }
}
