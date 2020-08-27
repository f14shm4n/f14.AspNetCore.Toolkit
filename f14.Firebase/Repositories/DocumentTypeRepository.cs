using f14.Data;
using f14.Firebase.Utils;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace f14.Firebase.Repositories
{
    ///<inheritdoc cref="IDocumentTypeRepository"/>
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        /// <summary>
        /// Creates new instance of a repository.
        /// </summary>
        /// <param name="projectId">The Firebase project id.</param>
        /// <param name="collectionName">The working collection name.</param>
        public DocumentTypeRepository(string projectId, string collectionName)
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
        /// <typeparam name="T">Type of document.</typeparam>
        /// <param name="patches">A collection of patches.</param>
        protected virtual void OnPatching<T>(IDictionary<string, object?> patches) where T : class, IDocument
        {

        }

        /// <summary>
        /// Executes before the document will be updated.
        /// </summary>
        /// <typeparam name="T">Type of document.</typeparam>
        /// <param name="entity">Entity to update.</param>
        protected virtual void OnUpdating<T>(T entity) where T : class, IDocument
        {

        }

        #endregion

        #region IDocumentTypeRepository

        ///<inheritdoc/>
        public async Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, IDocument
        {
            var dRef = Collection.Document(entity.DocumentType);
            entity.Id = entity.DocumentType;

            await dRef.SetAsync(entity, SetOptions.Overwrite, cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task DeleteAsync<T>(CancellationToken cancellationToken = default) where T : class, IDocument
        {
            var snapshot = await Collection.Document(IDocument.GetDocumentType<T>()).GetSnapshotAsync(cancellationToken).ConfigureAwait(false);
            if (snapshot.Exists)
            {
                await snapshot.Reference.DeleteAsync(null, cancellationToken).ConfigureAwait(false);
            }
        }

        ///<inheritdoc/>
        public async Task<T?> GetAsync<T>(CancellationToken cancellationToken = default) where T : class, IDocument
        {
            var snapshot = await Collection.Document(IDocument.GetDocumentType<T>()).GetSnapshotAsync(cancellationToken).ConfigureAwait(false);
            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<T>();
            }
            return null;
        }

        ///<inheritdoc/>
        public async Task PatchAsync<T>(IReadOnlyCollection<PropertyPatch<T>> patches, CancellationToken cancellationToken = default) where T : class, IDocument
        {
            Dictionary<string, object?> updates = new Dictionary<string, object?>();

            foreach (var ppi in patches)
            {
                var name = FirestoreUtil.GetFirestorePropertyName(ppi.PropertySelector)!;
                var value = ppi.ValueToAssign;

                updates[name] = value;
            }

            OnPatching<T>(updates);

            var doc = Collection.Document(IDocument.GetDocumentType<T>());
            await doc.UpdateAsync(updates, null, cancellationToken).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, IDocument
        {
            var dRef = Collection.Document(entity.DocumentType);

            OnUpdating(entity);

            await dRef.SetAsync(entity, SetOptions.Overwrite, cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
