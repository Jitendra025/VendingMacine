#region Using Statements
using InterfaceLayer.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion


namespace VendingMachine_UnitTest
{
    /// <summary>
    /// Test class to test SelectItems class.
    /// </summary>
    [TestClass]
    public class SelectItemsTest
    {
        #region Member Variables
        ISelectItems selectItems;
        public SelectItemsTest()
        {
            selectItems = Dependency.GetInstance<ISelectItems>();
        }
        #endregion
        
        #region Test Methods

        /// <summary>
        /// Test method to test that  item should get dispensed
        /// </summary>
        [TestMethod]
        public void TestItemDispensed()
        {
            decimal runningAmount = 0.65m;// Current amount in the machine
            string itemCode = "cr"; // Price of crisps is 50P
            decimal itemPrice = 0.0m;
            bool itemDispensed = selectItems.DispenseItem(runningAmount, ref itemCode, out itemPrice);
            Assert.IsTrue(itemDispensed);
        }

        /// <summary>
        /// Test method to test that  item should not get dispensed as the amount is not enough in the machine.
        /// </summary>
        [TestMethod]
        public void TestItemNotDispensed()
        {
            decimal runningAmount = 0.65m;// Current amount in the machine
            string itemCode = "co"; // Price of cola is £1.00
            decimal itemPrice = 0.0m;
            bool itemDispensed = selectItems.DispenseItem(runningAmount, ref itemCode, out itemPrice);
            Assert.IsFalse(itemDispensed);
        }
        #endregion
    }
}
