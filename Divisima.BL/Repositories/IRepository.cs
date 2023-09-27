using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.BL.Repositories
{
    public interface IRepository<T> // IReposiyory<Admin> -- IRepository<Slide>
    {
        public IQueryable<T> GetAll();

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression); // linq sorgusu atılabilir halde bir yapı yani sorguya göre getir.
        public T GetBy(Expression<Func<T, bool>> expression); // tek bir kayıt getirmek için(herhangi bir özelliğine göre)

        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
