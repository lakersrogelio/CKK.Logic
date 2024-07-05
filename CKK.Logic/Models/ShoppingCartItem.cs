using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    [Serializable]
    public class ShoppingCartItem : InventoryItem
    { 
        public ShoppingCartItem(Product product, int quantity) 
        {
            Product = product;
            Quantity = quantity;
        }
  
        public Product GetProduct()
        {
            return Product;

        }

        public void SetProduct(Product product)
        {
            Product = product;
        }

        public int GetQuantity()
        {
            return Quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
        public decimal GetTotal()
        {
            var productPrice = Product.Price;
            var quantity = GetQuantity();
            productPrice = quantity * productPrice;
            return productPrice;

        }
    }
}



  