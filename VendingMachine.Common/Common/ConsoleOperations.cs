using System;

namespace VendingMachine.Common.Common
{
    public class ConsoleOperations
    {
        #region public Methods
        /// <summary>
        /// This method write the text on console screen
        /// </summary>
        /// <param name="text">string, text to be written on the console</param>
        /// <param name="blnNewLine">bool, flag to decide if need to write in a new line</param>
        public static void WriteOnConsole(string text, bool blnNewLine)
        {
            if (blnNewLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
        }

        /// <summary>
        /// This  method reads the data from console.
        /// </summary>
        /// <returns></returns>
        public static string ReadFromConsole()
        {
            return Console.ReadLine();
        }
        #endregion
    }
}
