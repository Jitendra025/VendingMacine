namespace InterfaceLayer.Interfaces
{
    /// <summary>
    /// This interface is implemented by DepositeCoin class.
    /// </summary>
    public interface IDepositeCoin
    {
        decimal DepositCoins(string coin, ref decimal returnChange);
        string ReturnChange(decimal itemPrice,decimal runningAmount, decimal returncChange);
    }
}
