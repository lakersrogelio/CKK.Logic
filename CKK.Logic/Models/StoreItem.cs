using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{


    public class StoreItem : InventoryItem
    {
        public StoreItem(Product product, int quantity)
        {
            
            Product1 = product;
            Quantity = quantity;
        }
    }

}


        //private Product _product;
        //private int _quantity;
        //private Product _product;
        //private int _quantity;
        /*public Product GetProduct()
        {
            return _product;

        }

        public void SetProduct(Product product)
        {
             _product = product;
        }

        public int GetQuantity()
        {
            return _quantity;
        }

        public void SetQuantity(int quantity)
        {
            _quantity = quantity;
        }*/
