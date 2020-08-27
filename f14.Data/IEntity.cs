namespace f14.Data
{
    /// <summary>
    /// Provides basic interface which represent the data model with <see cref="Id"/> property.
    /// </summary>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        TKey Id { get; set; }
    }
}
