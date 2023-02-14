using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }
}
