using Microsoft.EntityFrameworkCore;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// The repository that based on the specific <see cref="DbContext"/>.
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
    public interface IDbContextRepository<T, TDbContext> : IRepository<T>
        where T : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// The context which represents the database.
        /// </summary>
        TDbContext Context { get; }
    }
}
