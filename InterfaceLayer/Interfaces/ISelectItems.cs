namespace InterfaceLayer.Interfaces
{
    /// <summary>
    /// This interface is implemented by SelectItems class in Common project.
    /// </summary>
    public interface ISelectItems
    {
        bool DispenseItem(decimal runninItem,ref string itemCode, out decimal itemPrice);
        void ListItems();
    }
}
