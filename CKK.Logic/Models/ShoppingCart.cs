using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly List<ShoppingCartItem> _products;
        public Customer _customer { get; set; }
        public List<Product> Products { get; set; }
        public ShoppingCart(Customer cust)
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
                return null;
            }

            var existingItem = _products.FirstOrDefault(p => p.Product1.Id == prod.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                return existingItem;
            }

            
            var newItem = new ShoppingCartItem(prod,quantity );
            _products.Add(newItem);
            return newItem;
        }



            

        public ShoppingCartItem RemoveProduct(int id, int quantity)
        {
            var item = _products.FirstOrDefault(p => p.Product1.Id == id);

            if (item == null)
            {
                return null;
            }

            item.Quantity -= quantity;

            if (item.Quantity <= 0)
            {
                item.Quantity = 0;
                _products.Remove(item);
            }
            
            return item;
        }

        public ShoppingCartItem? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Product1.Id == id);
            
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (var item in _products)
            {
                total += item.Product1.Price * item.Quantity;
            }

            return total;
        }

        public List<ShoppingCartItem> GetProducts()
        {
            return _products;
        }
    }





 
}
    /*public class ShoppingCart
    {
        private Customer _customer;
        private ShoppingCartItem? _product1;
        private ShoppingCartItem? _product2;
        private ShoppingCartItem? _product3;
        public ShoppingCart(Customer cust)
        {
            _customer = cust;
        }
        public int GetCustomerId()
        {
            return _customer.GetId();
        }
        public ShoppingCartItem? GetProductById(int id)
        {
            if (_product1.GetProduct().GetId() == id)
            {
                return _product1;
            }
            else if (_product2.GetProduct().GetId() == id)
            {
                return _product2;
            }
            else if (_product3.GetProduct().GetId() == id)
            {
                return _product3;
            }
            else
            {
                return null;
            }
        }
        public ShoppingCartItem AddProduct(Product prod)
        {
            return AddProduct(prod, 1);
        }
        public ShoppingCartItem AddProduct(Product prod, int quantity)
        {
            ShoppingCartItem item = new ShoppingCartItem(prod, quantity);
            if (quantity <= 0)
            {
                return null;
            }
            if (_product1 != null && _product1.GetProduct().GetId() == prod.GetId())
            {
                int sum = _product1.GetQuantity() + quantity;

                _product1.SetQuantity(sum);

                return _product1;
            }
            else if (_product2 != null && _product2.GetProduct().GetId() == prod.GetId())
            {
                int sum = _product2.GetQuantity() + quantity;

                _product2.SetQuantity(sum);

                return _product2;
            }
            else if (_product3 != null && _product3.GetProduct().GetId() == prod.GetId())

            {
                int sum = _product3.GetQuantity() + quantity;

                _product3.SetQuantity(sum);

                return _product3;
            }
            if (_product1 == null)
            {
                _product1 = item;

                return _product1;
            }
            else if (_product2 == null)
            {
                _product2 = item;

                return _product2;
            }
            else if (_product3 == null)
            {
                _product3 = item;

                return _product3;
            }
            return null;
        }
        public ShoppingCartItem RemoveProduct(Product prod, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }
            ShoppingCartItem? item = GetProductById(prod.GetId());
            if (item != null)
            {
                if (item.GetQuantity() > quantity)
                {
                    int diff = item.GetQuantity() - quantity;
                    item.SetQuantity(diff);
                }
                else
                {
                    if (_product1 == item)
                    {
                        _product1 = null;
                    }
                    else if (_product2 == item)
                    {
                        _product2 = null;
                    }
                    else if (_product3 == item)
                    {
                        _product3 = null;
                    }

                    item = null;
                }
            }
            return item;
        }
        public decimal GetTotal()
        {
            decimal total = 0;

            if (_product1 != null)
            {
                total += _product1.GetTotal();
            }

            if (_product2 != null)
            {
                total += _product2.GetTotal();
            }

            if (_product3 != null)
            {
                total += _product3.GetTotal();
            }
            return total;
        }
        public ShoppingCartItem? GetProduct(int prodNum)
        {
            if (prodNum == 1)
            {
                return _product1;
            }
            else if (prodNum == 2)
            {
                return _product2;
            }
            else if (prodNum == 3)
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


















































