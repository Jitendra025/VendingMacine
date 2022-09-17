#region Using Statements
using InterfaceLayer.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VendingMachine.Common.Common;
#endregion

namespace VendingMachine_UnitTest
{
    /// <summary>
    /// Test class to test DepositeCoin class.
    /// </summary>
    [TestClass]
    public class DepositeCoinTest
    {
        #region Member Variables
        IDepositeCoin depositeCoin;
        static decimal expectedRunningAmount;
        static decimal expectedReturncChange;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DepositeCoinTest()
        {
            depositeCoin = Dependency.GetInstance<IDepositeCoin>();
        }
        #endregion

        #region Test Methods
        /// <summary>
        /// This method test that 1p cping is rejected and amount is added in the return amount
        /// </summary>
        [TestMethod]
        public void Test1PCoinRejected()
        {
            decimal returnChange = 0.00m;
            string coin = "1";
            decimal runningAmount;
            runningAmount = depositeCoin.DepositCoins(coin, ref returnChange);
            Assert.AreEqual(0, runningAmount);// No value added in the running amount
            Assert.AreEqual(0.01m, returnChange);// Value of 1p added in the coin return
        }

        /// <summary>
        /// This method test that 2p cping is rejected and amount is added in the return amount
        /// </summary>
        [TestMethod]
        public void Test2PCoinRejected()
        {
            decimal returnChange = 0.00m;
            string coin = "2";
            decimal runningAmount;
            runningAmount = depositeCoin.DepositCoins(coin, ref returnChange);
            Assert.AreEqual(0, runningAmount);// No value added in the running amount
            Assert.AreEqual(0.02m, returnChange);// Value of 2p added in the coin return
        }

        /// <summary>
        /// This method test that any invalid coin get rejected.
        /// </summary>
        /// value passed in pence
        [TestMethod]
        [DataRow("3")] // No 3p coin exists.
        [DataRow("0.01")]
        [DataRow("300")]
        [DataRow("1000")]
        public void TestInvalidCoin(string coin)
        {
            decimal returnChange = 0.00m;
            decimal runningAmount;
            runningAmount = depositeCoin.DepositCoins(coin, ref returnChange);
            Assert.AreEqual(0, runningAmount);// No value added in the running amount
            Assert.AreEqual(0.00m, returnChange);// no value added in the coin return
        }

        /// <summary>
        /// This method test that any valid coin should get accepted.
        /// </summary>
        /// value passed in pence
        [TestMethod]
        [DataRow("5")]
        [DataRow("10")]
        [DataRow("20")]
        [DataRow("100")]
        public void TestValidCoins(string coin)
        {
            decimal returnChange = 0.00m;
            decimal runningAmount;
            runningAmount = depositeCoin.DepositCoins(coin, ref returnChange);
            expectedRunningAmount = Convert.ToDecimal(coin) / 100;
            Assert.AreEqual(expectedRunningAmount, runningAmount);
            Assert.AreEqual(0.00m, returnChange);// no value added in the coin return
        }

        /// <summary>
        /// This method test that the correct change return to customer
        /// </summary>
        /// value passed in pence
        [TestMethod]
        [DataRow("1")]
        [DataRow("2")]
        [DataRow("0.01")]
        [DataRow("100")]
        public void TestChangeReturn(string coin)
        {
            decimal returnChange = 0.00m;
            decimal itemPrice = 0.65m;
            decimal runningAmount;
            runningAmount = depositeCoin.DepositCoins(coin, ref returnChange);
            expectedReturncChange += returnChange;
            if (runningAmount >= itemPrice)
            {
                string result = depositeCoin.ReturnChange(itemPrice, runningAmount, expectedReturncChange);
                // Expected return change is 100+(1+2)-65=38P . 1 +2 is for 1p and 2p passed.
                Assert.AreEqual(result, $"{Constants.CollectChange} 38.00P");
            }
        }
        #endregion
    }
}
