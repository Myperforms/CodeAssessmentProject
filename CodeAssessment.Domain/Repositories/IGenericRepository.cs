using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeAssessment.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(object id);

        void Insert(T obj);

        void Update(T obj);

        int SaveChanges();

    }
}
