using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CKK.DB.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public OrderRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        private IDbConnection Connection => new SqlConnection();

        public int Add(Order entity)
        {
            using (var conn = Connection)
            {
                string query = "INSERT INTO Orders (CustomerId, OrderDate) VALUES (@CustomerId, @OrderDate); SELECT CAST(SCOPE_IDENTITY() as int)";
                return conn.ExecuteScalar<int>(query, entity);
            }
        }

        public int Delete(int id)
        {
            using (var conn = Connection)
            {
                string query = "DELETE FROM Orders WHERE Id = @Id";
                return conn.Execute(query, new { Id = id });
            }
        }

        public List<Order> GetAll()
        {
            using (var conn = Connection)
            {
                string query = "SELECT * FROM Orders";
                return conn.Query<Order>(query).ToList();
            }
        }

        public Order GetById(int id)
        {
            using (var conn = Connection)
            {
                string query = "SELECT * FROM Orders WHERE Id = @Id";
                return conn.QuerySingleOrDefault<Order>(query, new { Id = id });
            }
        }

        public Order GetOrderByCustomerId(int id)
        {
            using (var conn = Connection)
            {
                string query = "SELECT * FROM Orders WHERE CustomerId = @CustomerId";
                return conn.QuerySingleOrDefault<Order>(query, new { CustomerId = id });
            }
        }

        public int Update(Order entity)
        {
            using (var conn = Connection)
            {
                string query = "UPDATE Orders SET CustomerId = @CustomerId, OrderDate = @OrderDate WHERE Id = @Id";
                return conn.Execute(query, entity);
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
    public class OrderRepository : IOrderRepository
    {
        public int Add(Order entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByCustomerId(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }

}*/
