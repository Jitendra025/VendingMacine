#region Using Statements
using InterfaceLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using VendingMachine.Common.Common;
#endregion

namespace VendingMachine.BusinessLogic
{
    /// <summary>
    /// This class handles the operations related to coin deposite or return
    /// </summary>
    public class DepositeCoin : IDepositeCoin
    {
        #region Private Variables
        decimal runningAmount = 0;
        string[] returnCoins;
        decimal money = 0;
        private readonly IValidation _validate;
        private readonly IFormatCoinsToPoundOrPence _formatCoins;
        private readonly IConfiguration _config;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="validate"></param>
        /// <param name="formatCoins"></param>
        /// <param name="config"></param>
        public DepositeCoin(IValidation validate, IFormatCoinsToPoundOrPence formatCoins,
                             IConfiguration config)
        {
            _validate = validate;
            _formatCoins = formatCoins;
            _config = config;

            // Get the excluded coins from config file
            returnCoins = _config
                              .GetSection("exclude")
                              .GetChildren()
                              .Select(x => x.Value)
                              .ToArray();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// This method handles the deposite/insert coins functionality
        /// </summary>
        public decimal DepositCoins(string coin, ref decimal returnChange)
        {
            try
            {
                // Validate if this a valid coin.
                money = _validate.ValidateCoin(coin);
                if (money == 0)
                {
                    // If 1p or 2p coin inserted add that value in the return change field.
                    if (!string.IsNullOrEmpty(coin) && returnCoins.Contains(_formatCoins.ConvertCoins(Convert.ToDecimal(coin) / 100.00m)))
                    {
                        returnChange += Convert.ToDecimal(coin) / 100.00m;
                    }
                }
                else
                {
                    runningAmount += money;
                }
                // Display the running amount.
                Display();
            }
            catch (Exception)
            {
                throw;
            }
            return runningAmount;
        }

        /// <summary>
        /// This method return the change if there is any
        /// </summary>
        /// <param name="itemPrice">decimal, price of the item selected</param>
        public string ReturnChange(decimal itemPrice, decimal runningAmount, decimal returnChange)
        {
            try
            {
                if (!itemPrice.Equals(runningAmount) || returnChange > 0)
                {
                    return $"{Constants.CollectChange} {_formatCoins.ConvertCoins(runningAmount + returnChange - itemPrice)}";
                }
                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// This method display the current ammount on console or insert coin text.
        /// </summary>
        private void Display()
        {
            try
            {
                if (runningAmount == 0)
                {
                    ConsoleOperations.WriteOnConsole(Constants.InsertCoins,true);
                }
                else
                {
                    ConsoleOperations.WriteOnConsole($"{Constants.TotalAmount} {_formatCoins.ConvertCoins(runningAmount)}: "
                                                       ,true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

