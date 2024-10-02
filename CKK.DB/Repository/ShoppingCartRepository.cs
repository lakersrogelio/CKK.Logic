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

        private readonly string _shopcart = "ShoppingCartItems";


        public ShoppingCartRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public ShoppingCartItem AddtoCart(int ShoppingCardId, int ProductId, int quantity)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                ProductRepository _productRepository = new ProductRepository(_connectionFactory);
                var item = _productRepository.GetById(ProductId);
                var ProductItems = GetProducts(ShoppingCardId).Find(x => x.ProductId == ProductId);

                var shopitem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCardId,
                    ProductId = ProductId,
                    Quantity = quantity
                };

                if (item.Quantity >= quantity)
                {
                    if (ProductItems != null)
                    {
                        //Product already in cart so update quantity
                        var test = UpdateAsync(shopitem);
                    }
                    else
                    {
                        //New product for the cart so add it
                        var test = AddAsync(shopitem);
                    }
                }
                return shopitem;
            }
        }
        public int ClearCart(int shoppingCartId)
        {
            var sql = $"DELETE FROM {_shopcart} WHERE ShoppingCartId = @ShoppingCartId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, new { ShoppingCartId = shoppingCartId });
                return 1;
            }
        }
        public int AddAsync(ShoppingCartItem entity)
        {
            var sql = "Insert into ShoppingCartItems (ShoppingCartId,ProductId,Quantity) VALUES (@ShoppingCartId,@ProductId,@Quantity)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }
        public int UpdateAsync(ShoppingCartItem entity)
        {
            var sql = "UPDATE ShoppingCartItems SET ShoppingCartId = @ShoppingCartId, ProductId = @ProductId, Quantity = @Quantity WHERE ShoppingCartId = @ShoppingCartId AND ProductId = @ProductId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public List<ShoppingCartItem> GetProducts(int shoppingCartId)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                string sql = @"SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
                var result = SqlMapper.Query<ShoppingCartItem>(conn, sql, new { ShoppingCartId = shoppingCartId }).ToList();
                return result;
            }
        }

        public decimal GetTotal(int shoppingCartId)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var items = SqlMapper.Query<ShoppingCartItem>(conn, @"SELECT * FROM ShoppingCartItems WHERE dbo.ShoppingCartItems.ShoppingCartId = @ShoppingCartId", new { ShoppingCartId = shoppingCartId }).ToList();
                List<decimal> total = new List<decimal>();
                ProductRepository _productRepository = new ProductRepository(_connectionFactory);

                foreach (var item in items)
                {
                    var product = _productRepository.GetById(item.ProductId);
                    total.Add(product.Price * (decimal)item.Quantity);
                }
                return total.Sum();

            }

            //The following is a more simple and clean version if you want to share this with a student
            //var sql = "SELECT SUM(Price * ShoppingCartItems.Quantity) FROM Products JOIN ShoppingCartItems ON ProductId = Products.Id WHERE ShoppingCartId = @shoppingCartId";

            //using (var connection = _connectionFactory.GetConnection)
            //{
            //    connection.Open();
            //    var result = connection.QuerySingleOrDefault<Decimal>(sql, new { ShoppingCartId = shoppingCartId });
            //    return result;
            //}
        }

        public void Ordered(int shoppingCartId)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                SqlMapper.Execute(conn, $"DELETE FROM {_shopcart} WHERE ShoppingCartId=ShoppingCartId", new { ShoppingCartId = shoppingCartId });
            }
        }
    }
}

/*namespace CKK.DB.Repository
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

        public ShoppingCartItem AddtoCart(int ShoppingCardId, int itemId, int quantity)
        {
            throw new NotImplementedException();
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
}*/


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
