using f14.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace f14.Firebase.Repositories
{
    /// <summary>
    /// Provides an extensions methods for the <see cref="IRepository{T}"/>.
    /// </summary>
    public static class IRepositoryExtensions
    {
        /// <summary>
        /// Patches the specified entity in a collection.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="repository">Source repository.</param>
        /// <param name="entity">Entity to patch.</param>
        /// <param name="patch">Specific property patch.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation</returns>
        public static Task PatchAsync<T>(
            this IRepository<T> repository,
            T entity,
            PropertyPatch<T> patch,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return repository.PatchAsync(entity, new PropertyPatch<T>[] { patch }, cancellationToken);
        }

        /// <summary>
        /// Patches the specified entity in a collection with error handling.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="repository">Source repository.</param>
        /// <param name="entity">Entity to patch.</param>
        /// <param name="patch">Specific property patch.</param>
        /// <param name="onError">Error handler.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation</returns>
        public static Task TryPatchAsync<T>(
            this IRepository<T> repository,
            T entity,
            PropertyPatch<T> patch,
            Action<Exception>? onError = default,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return repository.TryPatchAsync(entity, new PropertyPatch<T>[] { patch }, onError, cancellationToken);
        }

        /// <summary>
        /// Patches the specified entity in a collection with error handling.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="repository">Source repository.</param>
        /// <param name="entity">Entity to patch.</param>
        /// <param name="patches">A collection of patches.</param>
        /// <param name="onError">Error handler.</param>
        /// <param name="cancellationToken">The cancellation token that uses to stop async operation.</param>
        /// <returns>An async task that represent the execution operation</returns>
        public static async Task TryPatchAsync<T>(
            this IRepository<T> repository,
            T entity,
            IReadOnlyCollection<PropertyPatch<T>> patches,
            Action<Exception>? onError = default,
            CancellationToken cancellationToken = default)
            where T : class
        {
            try
            {
                await repository.PatchAsync(entity, patches, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }
    }
}
