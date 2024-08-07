﻿using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{

    [Serializable]
    public class StoreItem : InventoryItem
    {
        public StoreItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"#{Product.Id} {Product.Name}: {Quantity}";
        }


    }

}


       