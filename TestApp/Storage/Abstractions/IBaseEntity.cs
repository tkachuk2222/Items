namespace Storage.Abstractions
{
    /// <summary>
    /// IBaseEntity
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseEntity<TKey> 
    {
        /// <summary>
        /// id
        /// </summary>
        TKey Id { get; set; }
    }
}
