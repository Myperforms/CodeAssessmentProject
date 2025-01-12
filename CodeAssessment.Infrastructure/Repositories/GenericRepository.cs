using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Domain.Repositories;
using CodeAssessment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessment.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
          where T : class
    {
        protected readonly DbSet<T> Table = null;
        protected readonly CodeAssessmentDBContext MarketplaceDbContext;

        public GenericRepository(CodeAssessmentDBContext marketplaceDbContext)
        {
            this.MarketplaceDbContext = marketplaceDbContext;
            this.Table = this.MarketplaceDbContext.Set<T>();
        }

        public T GetById(object id)
        {
            return this.Table.Find(id);
        }

        public virtual void Insert(T obj)
        {
            this.Table.Add(obj);
        }

        public virtual void Update(T obj)
        {
            this.Table.Attach(obj);
            this.MarketplaceDbContext.Entry(obj).State = EntityState.Modified;
        }

        public int SaveChanges()
        {
            return this.MarketplaceDbContext.SaveChanges();
        }
    }
}
