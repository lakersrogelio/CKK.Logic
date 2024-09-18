using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CKK.DB.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ProductRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        private IDbConnection Connection => new SqlConnection();

        public int Add(Product entity)
        {
            var sql = "Insert into Products (Price,Quantity,Name) VALUES (@Price,@Quantity,@Name)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public int Delete(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var conn = Connection)
            {
                return conn.Execute(sql, new { Id = id });
            }
        }

        public List<Product> GetAll()
        {
            var sql = "SELECT * FROM Products";
            using (var conn = Connection)
            {
                return conn.Query<Product>(sql).ToList();
            }
        }

        public Product GetById(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
                return result;
            }
        }

        public List<Product> GetByName(string name)
        {
            var sql = "SELECT * FROM Products WHERE Name LIKE @Name";
            using (var conn = Connection)
            {
                return conn.Query<Product>(sql, new { Name = $"%{name}%" }).ToList();
            }
        }

        public int Update(Product entity)
        {
            var sql = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
            using (var conn = Connection)
            {
                return conn.Execute(sql, entity);
            }
        }
    }
}



/*using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CKK.DB.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ProductRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

       private IDbConnection Connection => new SqlConnection();

         public int Add(Product entity)
        {
            var sql = "Insert into Products (Price,Quantity,Name) VALUES (@Price,@Quantity,@Name)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public int Delete(int id)
        {
            using (var conn = Connection)
            {
                string query = "DELETE FROM Products WHERE Id = @Id";
                return conn.Execute(query, new { Id = id });
            }
        }

        public List<Product> GetAll()
        {
            using (var conn = Connection)
            {
                string query = "SELECT * FROM Products";
                return conn.Query<Product>(query).ToList();
            }
        }

        public Product GetById(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
                return result;
            }
        }

        public List<Product> GetByName(string name)
        {
            using (var conn = Connection)
            {
                string query = "SELECT * FROM Products WHERE Name LIKE @Name";
                return conn.Query<Product>(query, new { Name = $"%{name}%" }).ToList();
            }
        }

        public int Update(Product entity)
        {
            using (var conn = Connection)
            {
                string query = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
                return conn.Execute(query, entity);
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
    public class ProductRepository : IProductRepository
    {

        public int Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}*/

