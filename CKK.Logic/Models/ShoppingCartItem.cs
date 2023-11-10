﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCartItem
    {
        private Product _product;
        private int _quantity;
        internal int Id;
        internal int Quantity;
        internal int Price;

        public ShoppingCartItem(Product product, int quantity)
        {
            _product = product;
            _quantity = quantity;
        }

        public Product GetProduct()
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
        }
        public decimal GetTotal()
        {
            var productPrice = _product.GetPrice();
            var quantity = GetQuantity();
            productPrice = quantity * productPrice;
            return productPrice;

        
        }
    }
    }

