#region Using Statements
using InterfaceLayer.Interfaces;
using System;
using VendingMachine.Common.Common;
#endregion

namespace VendingMachine.BusinessLogic
{
    public class StartVendingMachine : IStartVendingMachine
    {
        #region Private Members
        private readonly IDepositeCoin _depositeCoin;
        private readonly ISelectItems _selectItems;
        decimal runningAmount;
        decimal returnChange;
        string itemCode = string.Empty;
        decimal itemPrice = 0;
        bool blnItemDispensed = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="depositeCoin">Reference of IDepositeCoin </param>
        /// <param name="selectItems">Reference of ISelectItems</param>
        public StartVendingMachine(IDepositeCoin depositeCoin, ISelectItems selectItems)
        {
            _depositeCoin = depositeCoin;
            _selectItems = selectItems;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method start the process of dispensing items
        /// </summary>
        /// <returns>bool, is item dispensed</returns>
        public bool StartMachine()
        {
            bool blnDispenseNextItem;
            try
            {
                ConsoleOperations.WriteOnConsole(Constants.InsertCoins, false);
                while (!blnItemDispensed)
                {
                    runningAmount = _depositeCoin.DepositCoins(ConsoleOperations.ReadFromConsole(), ref returnChange);
                    if (string.IsNullOrEmpty(itemCode) && runningAmount > 0)
                    {
                        // Display all items, untill user not selected an item.
                        _selectItems.ListItems();
                        itemCode = ConsoleOperations.ReadFromConsole();
                    }
                    if (runningAmount > 0)
                    {
                        // Dispense Item
                        blnItemDispensed = _selectItems.DispenseItem(runningAmount, ref itemCode, out itemPrice);
                    }
                }
                // Item dispensed, display THANK YOU
                ConsoleOperations.WriteOnConsole(Constants.ThankYou,true);

                // Item dispensed,Return change if any
                ConsoleOperations.WriteOnConsole(_depositeCoin.ReturnChange(itemPrice,runningAmount, returnChange),true);

                // Ready to dispense next item
                blnDispenseNextItem = blnItemDispensed;
            }
            catch (Exception)
            {
                blnDispenseNextItem = false;
                throw;
            }
            finally
            {
                // Do clean up if any.
            }
            return blnDispenseNextItem;
        }
        #endregion
    }
}
