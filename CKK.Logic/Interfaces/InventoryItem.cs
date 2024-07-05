using CKK.Logic.Exceptions;
using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CKK.Logic.Interfaces
{
    [Serializable]
    public abstract class InventoryItem
    {
        private Product Product1; 
        private int _quantity;

        public Product Product
        {
            get
            {
                return Product1;
            }
            set
            {
                Product1 = value;
            }
        }
            
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value < 0)
                {
                    throw new InventoryItemStockTooLowException("Invalid quantity: " + value);
                }
                _quantity = value;
            }
        }
    }
       
        
}
