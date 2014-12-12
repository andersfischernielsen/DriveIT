using System;
using System.Collections.Generic;
using System.Linq;
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
        [Test]
        public async void TestReadMake()
        {
            var result = await JSONWrapper.Read<TrimArray>("make=ford");

            Assert.IsNotNull(result);
            Assert.GreaterOrEqual(result.Trims.Count(), 1);
            Assert.IsTrue(result.Trims[0].make_display.ToLower() == "ford");
        }

        [Test]
        public async void TestReadYearAndMake()
        {
            var result = await JSONWrapper.Read<TrimArray>("make=ford&year=2005");

            Assert.IsNotNull(result);
            Assert.GreaterOrEqual(result.Trims.Count(), 1);
            Assert.IsTrue(result.Trims[0].make_display.ToLower() == "ford");
            Assert.IsTrue(result.Trims[0].model_year.ToLower() == "2005");
        }

        [Test]
        public async void TestReadYearModelAndMake()
        {
            var result = await JSONWrapper.Read<TrimArray>("make=ford&year=2005&model=focus");

            Assert.IsNotNull(result);
            Assert.GreaterOrEqual(result.Trims.Count(), 1);
            Assert.IsTrue(result.Trims[0].make_display.ToLower() == "ford");
            Assert.IsTrue(result.Trims[0].model_year.ToLower() == "2005");
            Assert.IsTrue(result.Trims[0].model_name.ToLower() == "focus");
        }

        [Test]
        public async void TestFillCarData()
        {
            var toFill = new CarDto {Make = "Ford", Model = "Focus"};
            Assert.IsNullOrEmpty(toFill.Drive);
            await CarQuery.FillCarData(toFill);
            Assert.IsNotNull(toFill.Drive);
        }

        [Test]
        public void ThrowsExceptionTest()
        {
            var malformedCarDto = new CarDto {Make = null, Model = null};
            Assert.Throws(typeof(ArgumentNullException), async () => await CarQuery.FillCarData(malformedCarDto));
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
