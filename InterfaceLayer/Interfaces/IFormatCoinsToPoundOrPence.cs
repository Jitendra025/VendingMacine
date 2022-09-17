namespace InterfaceLayer.Interfaces
{
    /// <summary>
    /// This interface is implemented by FormatCoinsToPoundOrPence class.
    /// </summary>
    public interface IFormatCoinsToPoundOrPence
    {
      string ConvertCoins(decimal amount);
    }
}
