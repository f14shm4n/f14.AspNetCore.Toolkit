using Google.Cloud.Firestore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace f14.Firebase.Utils
{
    /// <summary>
    /// Provides utility methods for working with reflection, attributes, time stamps and others.
    /// </summary>
    public static class FirestoreUtil
    {
        /// <summary>
        /// Gets the <see cref="FirestorePropertyAttribute.Name"/> of desired property that defines as <paramref name="propertySelector"/>.
        /// </summary>
        /// <typeparam name="T">Type of object from which you need to select the property.</typeparam>
        /// <param name="propertySelector">Property selector.</param>
        /// <returns>
        /// If the specified property exists and contains the <see cref="FirestorePropertyAttribute"/>,
        /// then this method will return the <see cref="FirestorePropertyAttribute.Name"/> value or null.
        /// </returns>
        public static string? GetFirestorePropertyName<T>(Expression<Func<T, object?>> propertySelector)
        {
            var attr = AttributeUtil.GetAttributes<FirestorePropertyAttribute, T, PropertyInfo>(propertySelector, true);
            return attr.FirstOrDefault()?.Name;
        }
    }
}
