using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using CKK.Logic.Interfaces;
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
}