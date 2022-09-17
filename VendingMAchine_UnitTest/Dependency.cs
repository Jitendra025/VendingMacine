#region Using Statements
using InterfaceLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using VendingMachine.BusinessLogic;
using VendingMachine.Common.Common;
using VendingMachine.Common.Validation;
#endregion

namespace VendingMachine_UnitTest
{
    /// <summary>
    /// This class provides all dependecies instances needed
    /// </summary>
    public class Dependency
    {
        #region Member Variables
        static ServiceProvider serviceProvider;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        static Dependency()
        {
            // Setup Configuration
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            // Setup  Dependency Injection
             serviceProvider = new ServiceCollection()
                .AddTransient<IDepositeCoin, DepositeCoin>()
                .AddTransient<ISelectItems, SelectItems>()
                .AddSingleton<IValidation, Validation>()
                .AddSingleton<IFormatCoinsToPoundOrPence, FormatCoinsToPoundOrPence>()
                .AddTransient<IStartVendingMachine, StartVendingMachine>()
                .AddSingleton<IConfiguration>(config)
                .BuildServiceProvider();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method return the instance needed
        /// </summary>
        /// <typeparam name="T">Type of instance needed</typeparam>
        /// <returns>instance</returns>
        public static T GetInstance<T>()
        {
            return serviceProvider.GetService<T>();
        }
        #endregion
    }
}
