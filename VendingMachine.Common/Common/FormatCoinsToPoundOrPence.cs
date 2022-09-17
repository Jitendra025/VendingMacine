#region Using Statements
using InterfaceLayer.Interfaces;
using System;
#endregion

namespace VendingMachine.Common.Common
{
    /// <summary>
    /// This class handles the conversion of total amount to pound or pence.
    /// </summary>
    public class FormatCoinsToPoundOrPence : IFormatCoinsToPoundOrPence
    {
        #region Public Method
        /// <summary>
        /// This method converts the total amount to pound or pence
        /// </summary>
        /// <param name="amount">decimal,  amount</param>
        /// <returns>string, converted amount with symbol</returns>
        public string ConvertCoins(decimal amount)
        {
            try
            {
                return amount >= 1.00m ? $"{Constants.PoundSymbol}{amount}" : $"{ amount * 100}{Constants.Pence}";
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
