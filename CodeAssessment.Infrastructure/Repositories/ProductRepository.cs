using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Domain.Entities;
using CodeAssessment.Domain.Repositories;
using CodeAssessment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static CodeAssessment.Infrastructure.Repositories.ProductRepository;

namespace CodeAssessment.Infrastructure.Repositories
{

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly CodeAssessmentDBContext _dbContext;

        public ProductRepository(CodeAssessmentDBContext dbContext)
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Product> GetAllProducts()
        {
            return this._dbContext.Products.Where(x => !x.IsDeleted).ToList();
        }

        public bool DeleteProduct(int productId)
        {
            var productDetails = this._dbContext.Products.FirstOrDefault(x => x.ProductId == productId);
            if (productDetails != null)
            {
                productDetails.IsDeleted = true;
                this._dbContext.Products.Update(productDetails);
                this._dbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
