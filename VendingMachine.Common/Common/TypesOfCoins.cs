#region Using Statements
using System;
using System.Collections.Generic;
#endregion

namespace VendingMachine.Common.Common
{
    /// <summary>
    /// This class populates the valid coins
    /// </summary>
    public class TypesOfCoins
    {
        #region Private Members
        static Dictionary<decimal, string> validCoins;
        #endregion

        #region Public Properties
        public static Dictionary<decimal, string> ValidCoins
        {
            get { return validCoins; }
            set { validCoins = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        static TypesOfCoins()
        {
            ValidCoins = new Dictionary<decimal, string>();
            PolulateCoins();
        }
        #endregion
        /// <summary>
        ///  This method populates the coins with value
        /// </summary>
        static void PolulateCoins()
        {
            try
            {
                ValidCoins.Add(0.05m, "5P");
                ValidCoins.Add(0.1m, "10P");
                ValidCoins.Add(0.2m, "20P");
                ValidCoins.Add(0.5m, "50P");
                ValidCoins.Add(1m, "£1.00");
                ValidCoins.Add(2m, "£2.00");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
