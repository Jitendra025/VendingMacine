#region  using Statements
using System;
using System.Collections.Generic;
#endregion

namespace VendingMachine.Common.Common
{
    /// <summary>
    /// This class populates the item list and item codes.
    /// </summary>
    public class Items
    {
        #region Private Member
        private static Dictionary<string, decimal> itemList;
        private static Dictionary<string, string> itemCodes;
        #endregion

        #region Public Properties
        public static Dictionary<string, decimal> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
        public static Dictionary<string, string> ItemCodes
        {
            get { return itemCodes; }
            set { itemCodes = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        static Items()
        {
            ItemList = new Dictionary<string, decimal>();
            ItemCodes = new Dictionary<string, string>();
            PopulateItems();
            PopulateItemsCode();
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// This method populates the item list
        /// </summary>
        static void PopulateItems()
        {
            try
            {
                ItemList.Add("Cola", 1.00m);
                ItemList.Add("Crisps", 0.5m);
                ItemList.Add("Chocolate", 0.65m);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method populates the item codes
        /// </summary>
        static void PopulateItemsCode()
        {
            try
            {
                ItemCodes.Add("CO", "Cola");
                ItemCodes.Add("CR", "Crisps");
                ItemCodes.Add("CH", "Chocolate");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

