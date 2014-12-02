using System;
using System.Collections.Generic;
using DriveIT.CarQuery;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit;
using NUnit.Framework;

namespace DriveIT.CarQuery.Tests
{
    [TestFixture]
    public class CarQueryTests
    {
        [TestFixtureSetUp]
        public void SetUp()
        {

        }

        [Test]
        public async void TestRead()
        {
            var result = await JSONWrapper.ReadList<Trim>("make=ford");

            foreach (var car in result)
            {
                Console.WriteLine(car.model_body + " & " + car.model_doors);
            }
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
