using CKK.Logic.Exceptions;
using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Store : Entity, IStore
    {
        public List<StoreItem> _items;
        private int _uniqueId = 1;
        public Store()
        {
            _items = new List<StoreItem>();
        }
        public StoreItem AddStoreItem(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                throw new InventoryItemStockTooLowException("Inventory too low");
            }
            if (product.Id == 0)
            {
                product.Id = _uniqueId++;
            }
            var existingItem = _items.FirstOrDefault(item => item.Product.Id == product.Id);
            if (existingItem != null)
            {
            existingItem.Quantity = existingItem.Quantity + quantity;
                return existingItem;
            }
            var newItem = new StoreItem(product, quantity);
            _items.Add(newItem);
            return newItem;
        }
        public void DeleteStoreItem(int id)
        {
            var item = _items.FirstOrDefault(i => i.Product.Id == id);
            if (item != null)
            {
                _items.Remove(item);
            }
        }
        public StoreItem RemoveStoreItem(int productId, int quantity)
        {
            if (quantity < 0) 
            { 

                throw new ArgumentOutOfRangeException("Quantity less than zero");

            }
            var item = _items.FirstOrDefault(i => i.Product.Id == productId);
            if (item == null)
            {
                throw new ProductDoesNotExistException("Product does not exist");
            }
            if (item.Quantity - quantity <= 0)
            {
            item.Quantity = 0;
            }
            else
            {
                item.Quantity = item.Quantity - quantity;
            }
            return item;
        }
        public StoreItem? FindStoreItemById(int id)
        {
            if (id < 0)
            {
               throw new InvalidIdException("Id is invalid");
            }
                
            var item = _items.FirstOrDefault(item => item.Product.Id == id);
            
            if (item == null)
            {
               return null;
            }
            
            if (item.Quantity <= 0)
            {
               item.Quantity = 0;
            }
            return item;
        }
        public List<StoreItem> GetStoreItems()
        {
            return _items;
        }
        public List<StoreItem> GetAllProductsByName(string name)
        {
            return _items.Where(item => item.Product.Name.Contains(name)).ToList();
        }

        public List<StoreItem> GetProductsByQuantity()
        {
            return _items.OrderByDescending(item => item.Quantity).ToList();
        }

        public List<StoreItem> GetProductsByPrice()
        {
            return _items.OrderByDescending(item => item.Product.Price).ToList();
        }
    } 
}

            
            

            

            


            

       

         



    