#region Using Statements
using InterfaceLayer.Interfaces;
using System;
using VendingMachine.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using VendingMachine.Common.Validation;
using VendingMachine.Common.Common;
using Microsoft.Extensions.Configuration;
using System.IO;
using VendingMachine.Common;
#endregion

namespace VendingMachine
{
    /// <summary>
    /// Main entry class of the project
    /// </summary>
    class Program
    {

        /// <summary>
        /// Entry method of the program
        /// </summary>
        /// <param name="args">args, arguments from console</param>
        static void Main(string[] args)
        {
            IConfiguration config;
            bool blnDispenseNextItem = false;
            try
            {
                // Setup Configuration
                config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appSettings.json")
                    .Build();

                // Setup  Dependency Injection
                var serviceProvider = new ServiceCollection()
                    .AddTransient<IDepositeCoin, DepositeCoin>()
                    .AddTransient<ISelectItems, SelectItems>()
                    .AddSingleton<IValidation, Validation>()
                    .AddSingleton<IFormatCoinsToPoundOrPence, FormatCoinsToPoundOrPence>()
                    .AddTransient<IStartVendingMachine, StartVendingMachine>()
                    .AddSingleton<IConfiguration>(config)
                    .BuildServiceProvider();

                // Display welcome message for the first time.
                Console.WriteLine(Messages.Welcome);
                Console.WriteLine(Messages.CoinTypes);

                // Get the instance of StartVendingMachine class
                var startVending = serviceProvider.GetService<IStartVendingMachine>();
                blnDispenseNextItem = startVending.StartMachine();
                while (blnDispenseNextItem)
                {
                    startVending = serviceProvider.GetService<IStartVendingMachine>();
                    blnDispenseNextItem = startVending.StartMachine();
                }
            }
            catch (Exception excp)
            {
                ConsoleOperations.WriteOnConsole(string.Concat(Messages.Issues, Messages.TryAgain, 
                                                 Environment.NewLine, excp.Message)
                                                    ,true);
                ConsoleOperations.ReadFromConsole();
            }
            finally
            {
                // Do clean up if there is any thing.
            }
        }
    }
}

