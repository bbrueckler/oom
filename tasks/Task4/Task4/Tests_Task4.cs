using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task4
{
    [TestFixture]
    class Tests_Task4
    {
        [Test]
        public void ManufacturerMustNotBeEmpty()
        {
            Assert.Catch(() =>
            {
                var newBike = new Sportbike("", "Model", 9, 9);
            });
        }

        [Test]
        public void ModelMustNotBeEmpty()
        {
            Assert.Catch(() =>
            {
                var newBike = new Sportbike("Manufacturer", "", 9, 9);
            });
        }

        [Test]
        public void MileageMustNotBeNegative()
        {
            Assert.Catch(() =>
            {
                var newBike = new Sportbike("M", "M", -9, 10);
            });
        }

        [Test]
        public void BikePropertiesSameAfterInitialisation()
        {
            var newBike = new Sportbike("Manufacturer", "Model", 123456, 10);

            Assert.IsTrue(newBike.Manufacturer == "Manufacturer");
            Assert.IsTrue(newBike.Model == "Model");
            Assert.IsTrue(newBike.GetMileage() == 123456);
        }

        [Test]
        public void BikeMileageCanBeIncreased()
        {
            var newBike = new Sportbike("Manufacturer", "Model", 10000, 10);
            newBike.SetMileage(15000);
            Assert.IsTrue(newBike.GetMileage() == 15000);
        }

        [Test]
        public void BikeMileageCannotBeDecreased()
        {
            var newBike = new Sportbike("Manufacturer", "Model", 10000, 9);
           
            Assert.Catch(() =>
            {
                newBike.SetMileage(5000);
            });
        }

        [Test]
        public void GetNextServiceReturnsCorrectValue()
        {
            var newBike = new Sportbike("Manufacturer", "Model", 10000, 100);
            newBike.ServiceInterval = 3000;
            Assert.IsTrue(newBike.GetNextService() == 2000);
        }

        [Test]
        public void DirtbikePropertiesAreSettable()
        {
            var newDirtbike = new Dirtbike("Manufacturer", "Model", 20000, 0);
            newDirtbike.ServiceInterval = 5000;
            newDirtbike.ServiceInterval_Oil = 2000;

            Assert.IsTrue(newDirtbike.ServiceInterval == 5000);
            Assert.IsTrue(newDirtbike.ServiceInterval_Oil == 2000);
        }
    }
}
