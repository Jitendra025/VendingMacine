#region Using Statements
using InterfaceLayer.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

namespace VendingMachine_UnitTest
{
    /// <summary>
    /// Test class to test ValidationTest class.
    /// </summary>
    [TestClass]
    public class ValidationTest
    {
        #region Member Variables
        IValidation validation;
        public ValidationTest()
        {
            validation = Dependency.GetInstance<IValidation>();
        }
        #endregion


        #region Test Methods
        /// <summary>
        /// This method test that valid oins should get accepted.
        /// </summary>
        /// <param name="coin"></param>
        [TestMethod]
        [DataRow("5")] // Value Passed in pence
        [DataRow("10")]
        [DataRow("20")]
        [DataRow("50")]
        [DataRow("100")]
        [DataRow("200")]
        public void ValidateValidCoin(string coin)
        {
            decimal.TryParse(coin, out decimal value);
            decimal expectedValue = value / 100;
            decimal actualValue = validation.ValidateCoin(coin);
            Assert.AreEqual(actualValue, expectedValue);
        }

        /// <summary>
        /// This method test that invalid coins should get accepted.
        /// </summary>
        /// <param name="coin"></param>
        [TestMethod]
        [DataRow("1")] // Value Passed in pence
        [DataRow("2")]
        public void TestInValidCoin(string coin)
        {
            decimal expectedValue = 0;
            decimal actualValue = validation.ValidateCoin(coin);
            Assert.AreEqual(actualValue, expectedValue);
        }
        #endregion
    }
}
