using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Exceptions
{

    [Serializable]
        public class InvalidIdException : Exception
        {
        
            public InvalidIdException(string message) : base(message)
            {
               
            }
        }

     [Serializable]
        public class InventoryItemStockTooLowException : Exception
        {
            public InventoryItemStockTooLowException(string message) : base(message)
            {
            }
        }

    [Serializable]
        public class ProductDoesNotExistException : Exception
        {
            public ProductDoesNotExistException(string message) : base(message)
            {
            }
        }
    
}
