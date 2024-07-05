
using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Interfaces
{
    public interface IShoppingCart
    {
        Customer _customer { get; set; }
        //List<Product> Products { get; set; }
        int GetCustomerId();
        ShoppingCartItem AddProduct(Product prod, int quantity);
        ShoppingCartItem RemoveProduct(int id, int quantity);
        ShoppingCartItem? GetProductById(int id);
        decimal GetTotal();
        List<ShoppingCartItem> GetProducts();
    }
}
