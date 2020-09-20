using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// The repository that provides extract methods.
    /// </summary>
    /// <typeparam name="T">Type of repository objects.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities in the related table.
        /// </summary>
        /// <returns>A set of entities or null.</returns>
        IEnumerable<T>? GetAll();

        /// <summary>
        /// Searches for an entities using specified filter.
        /// </summary>
        /// <param name="filter">The entity filter.</param>
        /// <param name="skip">The number of an entities that need to skip in filterd collection.</param>
        /// <param name="take">The number of an entities that need to take from the filtered collection.</param>
        /// <returns>A set of entities or null.</returns>
        IEnumerable<T>? GetAll(Expression<Func<T, bool>> filter, int skip, int take);

        /// <summary>
        /// Searches the single object using specified selector.
        /// </summary>
        /// <param name="selector">The entity selector.</param>
        /// <returns>The entity or null.</returns>
        T? Get(Expression<Func<T, bool>> selector);

        /// <summary>
        /// Returns the number of entities in the collection.
        /// </summary>
        /// <returns>The number of objects.</returns>
        int Count();

        /// <summary>
        /// Returns the number of entities in the collection using specified filter.
        /// </summary>
        /// <param name="filter">The entity filter.</param>
        /// <returns>The count of filtered objects.</returns>
        int Count(Expression<Func<T, bool>> filter);
    }
}
