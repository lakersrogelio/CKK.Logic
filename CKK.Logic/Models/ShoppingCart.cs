using CKK.Logic.Exceptions;
using CKK.Logic.Interfaces;
using System.Xml.Linq;

namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private List<ShoppingCartItem> _products;
        
        public Customer _customer { get; set; }
     
        public ShoppingCart (Customer cust)
        {
            _products = new List<ShoppingCartItem>();
            _customer = cust;

        }




        public int GetCustomerId()
        {
            return _customer.Id;
        }
       
        
        public ShoppingCartItem AddProduct(Product prod, int quantity)
        {
            if (quantity <= 0)
            {
                throw new InventoryItemStockTooLowException("Inventory too low");
            }

            var existingItem = _products.FirstOrDefault(p => p.Product.Id == prod.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                return existingItem;
            }


            var newItem = new ShoppingCartItem(prod, quantity);
            _products.Add(newItem);
            return newItem;
        }





        public ShoppingCartItem RemoveProduct(int id, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException("No Product to remove");
            }

            var item = _products.FirstOrDefault(p => p.Product.Id == id);

            if (item == null)
            {
                throw new ProductDoesNotExistException("Product does not exist");
            }
            
            
            if (item.Quantity - quantity <= 0)
            {
                item.Quantity = 0;
            _products.Remove(item);
            }
            else
            {

            item.Quantity = item.Quantity - quantity;
            }
        

            return item;
        }

        public ShoppingCartItem? GetProductById(int id)
        {

           
          
            if (id < 0)
            {
                
                throw new InvalidIdException("Id is invalid");
            }

                var product = _products.FirstOrDefault(p => p.Product.Id == id);

            if (product == null)
            {
                return null;
            }
            

            if (product.Quantity <= 0)
            {
                product.Quantity = 0;
            }
            return product;
            

        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (var item in _products)
            {
                total += item.Product.Price * item.Quantity;
            }

            return total;
        }

        public List<ShoppingCartItem> GetProducts()
        {
            return _products;
        }
    }
}




















































