using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    ///<inheritdoc cref="IDbContextRepository{T, TDbContext}"/>.
    public abstract class DbContextRepository<T, TDbContext> : Repository<T>, IDbContextRepository<T, TDbContext>
        where T : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// Creates new instance of a repository.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DbContextRepository(TDbContext context) : base(context.Set<T>())
        {
            Context = context;
        }

        #region IDbContextRepository

        ///<inheritdoc/>
        public TDbContext Context { get; }

        #endregion

        #region IManagementRepository

        ///<inheritdoc/>
        public virtual int Add(T o)
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            Context.Add(o);
            return Context.SaveChanges();
        }

        ///<inheritdoc/>
        public virtual int AddRange(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            if (list.Any() == false)
            {
                return 0;
            }

            Context.AddRange(list);
            return Context.SaveChanges();
        }

        ///<inheritdoc/>
        public virtual int Delete(T o)
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            Context.Remove(o);
            return Context.SaveChanges();
        }

        ///<inheritdoc/>
        public virtual int DeleteRange(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            if (list.Any() == false)
            {
                return 0;
            }

            Context.RemoveRange(list);
            return Context.SaveChanges();
        }

        ///<inheritdoc/>
        public abstract int Update(T o);

        ///<inheritdoc/>
        public abstract int Update(T to, T from);

        #endregion
    }
}
