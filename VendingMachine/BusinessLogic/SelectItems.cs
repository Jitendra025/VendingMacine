#region Using Statements
using InterfaceLayer.Interfaces;
using System;
using System.Text;
using VendingMachine.Common.Common;
#endregion

namespace VendingMachine.BusinessLogic
{
    /// <summary>
    /// This class handles the operations related to dispense the items.
    /// </summary>
    public class SelectItems : ISelectItems
    {
        #region Private Variables
        private readonly IFormatCoinsToPoundOrPence _formatCoins;
        bool blnItemDispensed = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="formatCoins"></param>
        public SelectItems(IFormatCoinsToPoundOrPence formatCoins)
        {
            _formatCoins = formatCoins;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// This method handles the operations related to dispensation of the item
        /// </summary>
        /// <param name="runningAmount">decimal, total ammount of coins inserted by customer</param>
        /// <param name="itemPrice">decimal, actual price of item</param>
        /// <returns>bool, item dispensed or not</returns>
        public bool DispenseItem(decimal runningAmount, ref string itemCode, out decimal itemPrice)
        {
            string selectedItem = string.Empty;
            itemPrice = 0;
            try
            {
                // Keep asking the item code untill customer provided a proper code
                while (string.IsNullOrEmpty(itemCode) || !Items.ItemCodes.ContainsKey(itemCode.ToUpper()))
                {
                    itemCode = Console.ReadLine();
                }
                // Convert the keyed code in to upper case.
                itemCode = itemCode.ToUpper();

                // Try to get price of  item based on selected code.
                Items.ItemCodes.TryGetValue(itemCode, out selectedItem);

                // Display the price of item selected.
                DisplayItems(itemCode, false);
                itemPrice = Items.ItemList[selectedItem];

                // We have the current amount greater than item price, item can dispensed now
                if (itemPrice <= runningAmount)
                {
                    // Item dispensed, return change if any.
                    blnItemDispensed = true;
                }
                else
                {
                    // Item not dispensed, more money needed.
                    blnItemDispensed = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return blnItemDispensed;

        }
        #endregion

        #region Private Methods

        /// <summary>
        /// This method display the list of available items or select item based on bool flag passed.
        /// </summary>
        /// <param name="key">string, item code</param>
        /// <param name="multipleItems">bool, all items to display or only one</param>
        private void DisplayItems(string key, bool multipleItems)
        {
            try
            {
                if (multipleItems)
                {
                    ConsoleOperations.WriteOnConsole($"{key} : {Items.ItemCodes[key]}  " +
                                              $"{_formatCoins.ConvertCoins(Items.ItemList[Items.ItemCodes[key]])}"
                                              ,true);
                }
                else
                {
                    ConsoleOperations.WriteOnConsole($"{Items.ItemCodes[key]}  " +
                                          $"Price {_formatCoins.ConvertCoins(Items.ItemList[Items.ItemCodes[key]])}"
                                          ,true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method will display the list of available items on console
        /// </summary>
        public void ListItems()
        {
            // Display all items when customer inserted first valid coin
            ConsoleOperations.WriteOnConsole(Constants.SelectItem,true);
            ConsoleOperations.WriteOnConsole(Constants.Stars,true);
            foreach (string key in Items.ItemCodes.Keys)
            {
                DisplayItems(key, true);
            }
            ConsoleOperations.WriteOnConsole(Constants.Stars, true);
        }
        #endregion
    }
}
