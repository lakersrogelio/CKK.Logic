using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CKK.Logic.Models
{
    class ShoppingCart
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
            return _customer.Id;
        }

        public ShoppingCartItem? GetProductById(int id)
        {
            if (_product1.Id == id)
            {
                return _product1;
            }
            else if (_product2.Id == id)
            {
                return _product2;
            }
            else if (_product3.Id == id)
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
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            ShoppingCartItem? item = GetProductById(prod.Id);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new ShoppingCartItem(prod, quantity);

                if (_product1 == null)
                {
                    _product1 = item;
                }
                else if (_product2 == null)
                {
                    _product2 = item;
                }
                else if (_product3 == null)
                {
                    _product3 = item;
                }
                else
                {
                    throw new InvalidOperationException("Shopping cart is full.");
                }
            }

            return item;
        }

        public ShoppingCartItem RemoveProduct(Product prod, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            ShoppingCartItem? item = GetProductById(prod.Id);

            if (item != null)
            {
                if (item.Quantity > quantity)
                {
                    item.Quantity -= quantity;
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
                total += _product1.Price * _product1.Quantity;
            }

            if (_product2 != null)
            {
                total += _product2.Price * _product2.Quantity;
            }

            if (_product3 != null)
            {
                total += _product3.Price * _product3.Quantity;
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

}
