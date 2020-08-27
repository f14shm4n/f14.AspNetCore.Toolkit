using System;
using System.Linq.Expressions;

namespace f14.Data
{
    /// <summary>
    /// Provides information about a property that need to be updated and a value to assign to it.
    /// </summary>
    /// <typeparam name="T">Type of object to update.</typeparam>
    public sealed class PropertyPatch<T>
    {
        /// <summary>
        /// Creates new instance of a property patch.
        /// </summary>
        /// <param name="property">The property selector.</param>
        /// <param name="valueToAssign">A value to assign when executing an update action.</param>
        public PropertyPatch(Expression<Func<T, object?>> property, object? valueToAssign)
        {
            PropertySelector = property;
            ValueToAssign = valueToAssign;
        }

        /// <summary>
        /// Gets the property selector.
        /// </summary>
        public Expression<Func<T, object?>> PropertySelector { get; }

        /// <summary>
        /// Gets a value to assign when executing an update action.
        /// </summary>
        public object? ValueToAssign { get; }
    }

    /// <summary>
    /// Provides information about a property that need to be updated and a value to assign to it.
    /// </summary>
    public sealed class PropertyPatch
    {
        /// <summary>
        /// Creates new instance of a property patch.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">New property value.</param>
        public PropertyPatch(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value;
        }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Gets the new property value.
        /// </summary>
        public object Value { get; }
    }
}
