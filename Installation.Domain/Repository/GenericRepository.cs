﻿using Installation.Domain.Context;
using Installation.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.Repository
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private readonly InstallationContext _context;

        public GenericRepository(InstallationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _context.Set<T>().Take(10).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result; 
        }

        public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            return result;
        }

        public async Task AddAsync(T? entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

             await _context.AddAsync(entity);
             await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T? entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

    }
}
