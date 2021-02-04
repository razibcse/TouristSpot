using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Remove(T entity);
        T GetById<Y>(Y id);
        ICollection<T> GetAll();
    }
}
