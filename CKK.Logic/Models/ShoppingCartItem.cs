using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCartItem 
    {
        public Product Product1 { get; set; }
        public int Quantity { get; set; }

       
        public ShoppingCartItem(Product product, int quantity) 
        {
            Product1 = product;
            Quantity = quantity;
        }
  
        public Product GetProduct()
        {
            return Product1;

        }

        public void SetProduct(Product product)
        {
            Product1 = product;
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
            var productPrice = Product1.GetPrice();
            var quantity = GetQuantity();
            productPrice = quantity * productPrice;
            return productPrice;

        }
    }
}



        /*private Product _product;
        private int _quantity;*/