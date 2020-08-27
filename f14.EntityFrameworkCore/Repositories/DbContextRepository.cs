using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    ///<inheritdoc cref="IDbContextRepository{T, TDbContext}"/>.
    public abstract class DbContextRepository<T, TDbContext> : Repository<T>, IDbContextRepository<T, TDbContext>, IManagementRepository<T>
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
            Context.Add(o);
            return Context.SaveChanges();
        }

        ///<inheritdoc/>
        public virtual int AddRange(IEnumerable<T> list)
        {
            Context.AddRange(list);
            return Context.SaveChanges();
        }

        ///<inheritdoc/>
        public virtual int Delete(T o)
        {
            Context.Remove(o);
            return Context.SaveChanges();
        }

        ///<inheritdoc/>
        public virtual int DeleteRange(IEnumerable<T> list)
        {
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
