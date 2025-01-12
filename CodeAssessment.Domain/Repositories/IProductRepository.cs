using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Domain.Entities;

namespace CodeAssessment.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public List<Product> GetAllProducts();

        public bool DeleteProduct(int productId);
    }
}
