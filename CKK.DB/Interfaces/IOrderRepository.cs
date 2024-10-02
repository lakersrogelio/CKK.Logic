using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Models;

namespace CKK.DB.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Order GetOrderByCustomer(int Id);
        public int Delete(int id);
        public int Add(Order entity);

        public Order GetById(int id);
    }
}
