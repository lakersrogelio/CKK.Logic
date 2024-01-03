using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{

    public class Store
    {
        public List<StoreItem> _items;

        public Store()
        {
            //Id = id;
            //Name = name;
            _items = new List<StoreItem>();
        }

        private int Id;
        private string Name;

        public int GetId()
        {
            return Id;
        }
        public void SetId(int id)
        {
            Id = id;
        }

        public string? GetName()
        {
            return Name;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public StoreItem AddStoreItem(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                return null;
            }

            var existingItem = _items.FirstOrDefault(item => item.GetProduct().Id == product.Id);
            if (existingItem != null)
            {
            existingItem.SetQuantity(existingItem.GetQuantity() + quantity);
                return existingItem;
            }

            var newItem = new StoreItem(product, quantity);
            _items.Add(newItem);
            return newItem;
        }

        public StoreItem RemoveStoreItem(int productId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.GetProduct().Id == productId);
            if (item == null)
            {
                return null;
            }

            item.SetQuantity(item.GetQuantity() - quantity);
            if (item.GetQuantity() < 0)
            {
                item.SetQuantity(0);
            }

            return item;
        }

        public StoreItem? FindStoreItemById(int id)
        {

            return _items.FirstOrDefault(item => item.GetProduct().Id == id);

        }

        public List<StoreItem> GetStoreItems()
        {
            return _items;
        }

        /*public class StoreItem
        {
            public StoreItem(Product product, int quantity)
            {
                Product = product;
                Quantity = quantity;
            }

            public Product Product { get; set; }
            public int Quantity { get; set; }
        */
    } 
}



    /*public class Store
    {
        private int _id;
        private string? _name;
        private Product? _product1;
        private Product? _product2;
        private Product? _product3;

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public string? GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void AddStoreItem(Product prod)
        {
            if (_product1 == null)
            {
                _product1 = prod;
            }
            else if (_product2 == null)
            {
                _product2 = prod;
            }
            else if (_product3 == null)
            {
                _product3 = prod;
            }
        }
        public void RemoveStoreItem(int productNumber)
        {
            if (productNumber == 1)
            {
                _product1 = null;
            }
            else if (productNumber == 2)
            {
                _product2 = null;
            }
            else if (productNumber == 3)
            {
                _product3 = null;
            }
        }
        public Product? GetStoreItem(int productNumber)
        {
            if (productNumber == 1)
            {
                return _product1;
            }
            else if (productNumber == 2)
            {
                return _product2;
            }
            else if (productNumber == 3)
            {
                return _product3;
            }
            else
            {
                return null;
            }
        }
        public Product? FindStoreItemById(int id)
        {
            if (_product1 != null && _product1.GetId() == id)
            {
                return _product1;
            }
            else if (_product2 != null && _product2.GetId() == id)
            {
                return _product2;
            }
            else if (_product3 != null && _product3.GetId() == id)
            {
                return _product3;
            }
            else
            {
                return null;
            }
        }
    }
}*/