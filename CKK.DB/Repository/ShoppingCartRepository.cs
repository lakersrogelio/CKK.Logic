using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CKK.DB.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ShoppingCartRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        private IDbConnection Connection => new SqlConnection();

        public int Add(ShoppingCartItem entity)
        {
            using (var conn = Connection)
            {
                string query = "INSERT INTO ShoppingCartItems (ShoppingCartId, ProductId, Quantity) VALUES (@ShoppingCartId, @ProductId, @Quantity); SELECT CAST(SCOPE_IDENTITY() as int)";
                return conn.ExecuteScalar<int>(query, entity);
            }
        }

        public ShoppingCartItem AddToCart(int ShoppingCartId, int ProductId, int quantity)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var productRepository = new ProductRepository(_connectionFactory);
                var item = productRepository.GetById(ProductId);
                var productItems = GetProducts(ShoppingCartId).Find(x => x.ProductId == ProductId);

                var shopItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    ProductId = ProductId,
                    Quantity = quantity
                };

                if (item.Quantity >= quantity)
                {
                    if (productItems != null)
                    {
                        var test = Update(shopItem);
                    }
                    else
                    {
                        var test = Add(shopItem);
                    }
                }
                return shopItem;
            }
        }

        public int ClearCart(int shoppingCartId)
        {
            using (var conn = Connection)
            {
                string query = "DELETE FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
                return conn.Execute(query, new { ShoppingCartId = shoppingCartId });
            }
        }

        public List<ShoppingCartItem> GetProducts(int shoppingCartId)
        {
            using (var conn = Connection)
            {
                string query = "SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
                return conn.Query<ShoppingCartItem>(query, new { ShoppingCartId = shoppingCartId }).ToList();
            }
        }

        public decimal GetTotal(int shoppingCartId)
        {
            using (var conn = Connection)
            {
                string query = "SELECT SUM(p.Price * sci.Quantity) FROM ShoppingCartItems sci JOIN Products p ON sci.ProductId = p.Id WHERE sci.ShoppingCartId = @ShoppingCartId";
                return conn.ExecuteScalar<decimal>(query, new { ShoppingCartId = shoppingCartId });
            }
        }

        public void Ordered(int shoppingCartId)
        {
            using (var conn = Connection)
            {
                string query = "UPDATE ShoppingCartItems SET Ordered = 1 WHERE ShoppingCartId = @ShoppingCartId";
                conn.Execute(query, new { ShoppingCartId = shoppingCartId });
            }
        }

        public int Update(ShoppingCartItem entity)
        {
            var sql = "UPDATE ShoppingCartItems SET ShoppingCartId = @ShoppingCartId, ProductId = @ProductId, Quantity = @Quantity WHERE ShoppingCartId = @ShoppingCartId AND ProductId = @ProductId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }
    }
}


/*using CKK.DB.Interfaces;
using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.DB.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public int Add(ShoppingCartItem entity)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartItem AddToCart(int ShoppingCartId, int ProductId, int quantity)
        {
            throw new NotImplementedException();
        }

        public int ClearCart(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartItem> GetProducts(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotal(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public void Ordered(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public int Update(ShoppingCartItem entity)
        {
            throw new NotImplementedException();
        }
    }
*/
