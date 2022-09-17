#region Using Statements
using InterfaceLayer.Interfaces;
using System;
using VendingMachine.Common.Common;
#endregion 

namespace VendingMachine.Common.Validation
{
    /// <summary>
    /// This class handles the validation operations
    /// </summary>
    public class Validation : IValidation
    {
        #region Public Methods
        /// <summary>
        /// This method validates if the the valid coin inserted.
        /// </summary>
        /// <param name="coin">string, type of coin</param>
        /// <returns>decimal,value of the coin</returns>
        public decimal ValidateCoin(string coin)
        {
            try
            {
                if (decimal.TryParse(coin, out decimal value))
                {
                    return TypesOfCoins.ValidCoins.ContainsKey(value / 100) ? value / 100.00m : 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
        #endregion
    }
}
