using System;
using System.Collections.Generic;
using DriveIT.CarQuery;
using DriveIT.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

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
            var result = await JSONWrapper.Read<TrimArray>("make=ford");

            foreach (var car in result.Trims)
            {
                Console.WriteLine(car.model_make_display + " --- " + car.model_name);
            }
        }

        [Test]
        public async void TestFillCarData()
        {
            var toFill = new CarDto {Make = "Ford", Model = "Focus"};

            await CarQuery.FillCarData(toFill);

            Assert.IsNotNull(toFill.Drive);
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
