using System.Collections.Generic;

namespace f14.AspNetCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Provides the interface to manage objects. This interface include: Insert, Update, Delete methods.
    /// </summary>
    /// <typeparam name="T">Type of repo objects.</typeparam>
    public interface IManagementRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Addes an object into the db.
        /// </summary>
        /// <param name="o">The object to add to the db.</param>
        /// <returns>The affected rows.</returns>
        int Add(T o);

        /// <summary>
        /// Addesan objects into the db.
        /// </summary>
        /// <param name="list">The objects to add to the db.</param>
        /// <returns>The affected rows.</returns>
        int AddRange(IEnumerable<T> list);

        /// <summary>
        /// Updates an object in the db.
        /// </summary>
        /// <param name="o">The data source object.</param>
        /// <returns>The affected rows.</returns>
        int Update(T o);

        /// <summary>
        /// Updates first object using data from second object.
        /// </summary>
        /// <param name="to">Object which accept data from other object.</param>
        /// <param name="from">Object which provides data for update.</param>
        /// <returns>The affected rows.</returns>
        int Update(T to, T from);

        /// <summary>
        /// Deletes an object from db.
        /// </summary>
        /// <param name="o">The object to delete.</param>
        /// <returns>The affected rows.</returns>
        int Delete(T o);

        /// <summary>
        /// Deletes an objects from db.
        /// </summary>
        /// <param name="list">The objects to delete.</param>
        /// <returns>The affected rows.</returns>
        int DeleteRange(IEnumerable<T> list);
    }
}
