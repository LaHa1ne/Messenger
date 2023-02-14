using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;

        public BaseRepository(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<T>> GetAll() => await _db.Set<T>().ToListAsync();

        public async Task Create(T Entity)
        {
            await _db.Set<T>().AddAsync(Entity);
            await _db.SaveChangesAsync();
        }

        public Task Update(T Entity)
        {
            _db.Set<T>().Update(Entity);
            _db.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task Delete(T Entity)
        {
            _db.Set<T>().Remove(Entity);
            _db.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
