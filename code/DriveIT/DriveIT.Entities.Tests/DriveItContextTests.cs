using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DriveIT.Entities.Tests
{
    [TestClass]
    public class DriveItContextTests
    {
        private IPersistentStorage _toTest;
        [TestInitialize]
        private void SetUp()
        {
            _toTest = new EntityAdapter();
        }

        [TestMethod]
        public void GetAllCarsTest()
        {
            
        }

        public void GetCarsWithIdTest()
        {

        }
    }
}
