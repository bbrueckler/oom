using System;

namespace task6
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

        public Sportbike(string Manufacturer, string Model, int mileage, int ServiceInterval)
        {
            if (string.IsNullOrWhiteSpace(Manufacturer)) throw new ArgumentException("Manufacturer must not be empty.", nameof(Manufacturer));
            if (string.IsNullOrWhiteSpace(Model)) throw new ArgumentException("Model must not be empty.", nameof(Model));
            if (mileage < 0) throw new ArgumentOutOfRangeException("Mileage must not be negative. ", nameof(mileage));

            this.Manufacturer = Manufacturer;
            this.Model = Model;
            this.ServiceInterval = ServiceInterval;
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
}
