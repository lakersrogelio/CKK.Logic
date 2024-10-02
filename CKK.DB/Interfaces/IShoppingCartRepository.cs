using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.DB.Interfaces
{
    public interface IShoppingCartRepository
    {
        public ShoppingCartItem AddtoCart(int ShoppingCardId, int itemId, int quantity);
        public int ClearCart(int shoppingCartId);
        public decimal GetTotal(int ShoppingCartId);
        public List<ShoppingCartItem> GetProducts(int shoppingCartId);
        public void Ordered(int shoppingCartId);
    }
}

      /*  ShoppingCartItem AddToCart(int ShoppingCartId, int ProductId, int quantity);
        int ClearCart(int shoppingCartId);
        decimal GetTotal(int shoppingCartId);
        List<ShoppingCartItem> GetProducts(int shoppingCartId);
        void Ordered(int shoppingCartId);
        int Update(ShoppingCartItem entity);
        int Add(ShoppingCartItem entity);*/