using CKK.Logic.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.DB.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<IReadOnlyList<Product>> GetByNameAsync(string name);
        public List<Product> GetAll();
        public int Update(Product entity);
    }
}
      /*  List<Product> GetByName(string name);
        int Update(Product entity);*/
