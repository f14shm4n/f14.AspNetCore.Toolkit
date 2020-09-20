using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Basic repository that provides entities extraction methods.
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    public abstract class Repository<T> : IRepository<T>, IAsyncRepository<T>
        where T : class
    {
        /// <summary>
        /// Creates new instance of repository.
        /// </summary>
        /// <param name="table">The queryable collection.</param>
        protected Repository(IQueryable<T> table)
        {
            Table = table;
        }

        /// <summary>
        /// Provides <see cref="IQueryable{T}"/> as root collection of entities.
        /// </summary>
        protected IQueryable<T> Table { get; }

        #region IRepository

        ///<inheritdoc cref="IRepository{T}.Count()"/>
        public virtual int Count() => Table.Count();

        ///<inheritdoc cref="IRepository{T}.Count(Expression{Func{T, bool}})"/>
        public virtual int Count(Expression<Func<T, bool>> filter) => Table.Count(filter);

        ///<inheritdoc cref="IRepository{T}.Get(Expression{Func{T, bool}})"/>
        public virtual T Get(Expression<Func<T, bool>> selector) => Table.FirstOrDefault(selector);

        ///<inheritdoc cref="IRepository{T}.GetAll()"/>
        public IEnumerable<T>? GetAll() => Table.ToList();

        ///<inheritdoc cref="IRepository{T}.GetAll(Expression{Func{T, bool}}, int, int)"/>
        public IEnumerable<T>? GetAll(Expression<Func<T, bool>> filter, int skip, int take) => Table.Where(filter).Skip(skip).Take(take).ToList();

        #endregion

        #region IAsyncRepository

        ///<inheritdoc/>
        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await Table.CountAsync(cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await Table.CountAsync(filter, cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task<T?> GetAsync(Expression<Func<T, bool>> selector, CancellationToken cancellationToken = default)
        {
            return await Table.SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await Table.ToListAsync().ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> filter, int skip, int take, CancellationToken cancellationToken = default)
        {
            return await Table.Where(filter).Skip(skip).Take(take).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
