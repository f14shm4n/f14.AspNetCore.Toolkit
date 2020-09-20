using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace f14.EntityFrameworkCore
{
    /// <summary>
    /// Provides utility methods for <see cref="DbContext"/>.
    /// </summary>
    public static class DbContextUtility
    {
        /// <summary>
        /// Sequentially removes the items from the table represented as a <see cref="DbSet{TEntity}"/>.
        /// <para>
        ///     This method reads items from the table from the top and removes them uses EntityFramework methods, 
        ///     this can be helpful if you want to process items while it removing from the data base or 
        ///     if you need an ability to stop the removal process in the middle.
        /// </para>
        /// </summary>
        /// <typeparam name="T">Type of table items.</typeparam>
        /// <param name="dbContext">The db context.</param>
        /// <param name="itemsPerIteration">The number of items to read and delete per iteration. Should be greater than zero.</param>
        /// <param name="onDeleted">A callback delegate which provides removed items.</param>
        /// <param name="cancellationToken">Cancellation token to stop the removal process.</param>
        /// <returns>Async task.</returns>
        public static async Task ClearTableAsync<T>(DbContext dbContext, int itemsPerIteration, Action<List<T>>? onDeleted = default, CancellationToken cancellationToken = default)
            where T : class
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (itemsPerIteration < 1)
            {
                throw new InvalidOperationException($"Param '{nameof(itemsPerIteration)}' must be greater than zero.");
            }

            List<T> toDel = await ReadItemsAsync().ConfigureAwait(false);
            while (toDel.Count > 0)
            {
                dbContext.RemoveRange(toDel);

                await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                onDeleted?.Invoke(toDel);

                toDel = await ReadItemsAsync().ConfigureAwait(false);
            }

            #region Local

            Task<List<T>> ReadItemsAsync() => dbContext.Set<T>().Take(itemsPerIteration).ToListAsync(cancellationToken);

            #endregion
        }
    }
}
