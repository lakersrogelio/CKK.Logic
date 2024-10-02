using CKK.DB.Interfaces;
using CKK.DB.Repository;
using CKK.DB.UOW;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;
using Dapper;
using CKK.DB;


namespace CKK.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Retrieve connection string from configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Initialize connection factory
            var connectionFactory = new Conn(connectionString);

            // Initialize repositories
            var productRepository = new ProductRepository(connectionFactory);

            // Pass the repository to the form
            Application.Run(new InventoryManagementForm(productRepository));
        }
    }
}
/*using CKK.Logic.Models;
using System;
using System.Collections.Generic;
//using CKK.Logic.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Store store = new Store();            
            Application.Run(new InventoryManagementForm(store));
            
            
          

            
        }
    }
}*/