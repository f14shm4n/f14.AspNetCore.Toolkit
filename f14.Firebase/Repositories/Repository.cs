using f14.Data;
using f14.Firebase.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace f14.Firebase.Repositories
{
    ///<inheritdoc cref="IRepository{T}"/>
    public abstract class Repository<T> : IRepository<T> where T : Document
    {
        /// <summary>
        /// Creates new instance of a repository.
        /// </summary>
        /// <param name="projectId">The Firebase project id.</param>
        /// <param name="collectionName">The working collection name.</param>
        protected Repository(string projectId, string collectionName)
        {
            Database = FirestoreDb.Create(projectId);
            Collection = Database.Collection(collectionName);
        }

        #region Properties

        /// <summary>
        /// Provides the root <see cref="FirestoreDb"/> object.
        /// </summary>
        protected FirestoreDb Database { get; }

        /// <summary>
        /// Provides the collection name with which current repository works.
        /// </summary>
        protected CollectionReference Collection { get; }

        #endregion

        #region Processors

        /// <summary>
        /// Executes before the document will be patched.
        /// </summary>
        /// <param name="id">Entity id to patch.</param>
        /// <param name="patches">A collection of patches.</param>
        protected virtual void OnPatching(string id, IDictionary<string, object?> patches)
        {

        }

        /// <summary>
        /// Executes before the document will be patched.
        /// </summary>
        /// <param name="entity">Entity to patch.</param>
        /// <param name="patches">A collection of patches.</param>
        protected virtual void OnPatching(T entity, IDictionary<string, object?> patches)
        {

        }

        /// <summary>
        /// Executes before the document will be updated.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        protected virtual void OnUpdating(T entity)
        {

        }

        #endregion

        #region IRepository

        ///<inheritdoc/>
        public virtual async Task AddAsync(T entry, CancellationToken cancellationToken = default)
        {
            var dRef = await Collection.AddAsync(entry, cancellationToken).ConfigureAwait(false);
            entry.Id = dRef.Id;
        }

        ///<inheritdoc/>
        public async Task<List<T>?> ExecuteQueryAsync(int offset, int size, Func<Query, Query> beforeExecute, CancellationToken cancellationToken = default)
        {
            // Defines the pagination vars
            int skip = offset;
            int take = 50;
            // Prepare the query
            Query query = Collection;

            query = beforeExecute(query);

            // Define results
            List<T> results = new List<T>();

            // Take first results
            QuerySnapshot snapshot = await query.Limit(take).GetSnapshotAsync(cancellationToken).ConfigureAwait(false);

            while (snapshot.Count > 0)
            {
                results.AddRange(snapshot.Documents.Select(x => x.ConvertTo<T>()));

                if (results.Count < size)
                {
                    skip += take;
                    snapshot = await query.Offset(skip).Limit(take).GetSnapshotAsync(cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    break;
                }
            }

            if (results.Count == 0)
            {
                return null;
            }

            if (results.Count > size)
            {
                return results.Take(size).ToList();
            }

            return results;
        }

        ///<inheritdoc/>
        public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var snapshot = await Collection.Document(id).GetSnapshotAsync(cancellationToken).ConfigureAwait(false);
            if (snapshot.Exists)
            {
                await snapshot.Reference.DeleteAsync(null, cancellationToken).ConfigureAwait(false);
            }
        }

        ///<inheritdoc/>
        public virtual async Task<T?> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var snapshot = await Collection.Document(id).GetSnapshotAsync(cancellationToken).ConfigureAwait(false);
            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<T>();
            }
            return null;
        }

        ///<inheritdoc/>
        public async Task PatchAsync(string id, IReadOnlyCollection<PropertyPatch<T>> patches, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object?> updates = new Dictionary<string, object?>();

            foreach (var ppi in patches)
            {
                var name = FirestoreUtil.GetFirestorePropertyName(ppi.PropertySelector)!;
                var value = ppi.ValueToAssign;

                updates[name] = value;
            }

            OnPatching(id, updates);

            var doc = Collection.Document(id);
            await doc.UpdateAsync(updates, null, cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task PatchAsync(T entity, IReadOnlyCollection<PropertyPatch<T>> patches, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object?> updates = new Dictionary<string, object?>();

            foreach (var ppi in patches)
            {
                var name = FirestoreUtil.GetFirestorePropertyName(ppi.PropertySelector)!;
                var value = ppi.ValueToAssign;

                updates[name] = value;
            }

            OnPatching(entity, updates);

            var doc = Collection.Document(entity.Id);
            await doc.UpdateAsync(updates, null, cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var dRef = Collection.Document(entity.Id);

            OnUpdating(entity);

            await dRef.SetAsync(entity, SetOptions.Overwrite, cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
