using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositires
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepositry(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> GetByAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            var entity = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}