namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Interface for realization of cryptography.
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Get hash of target input <paramref name="bytes"/>.
        /// </summary>
        /// <param name="bytes">Target input</param>
        /// <returns>
        ///     Hash of <paramref name="bytes"/>. If <paramref name="bytes"/> empty - string.Empty.
        /// </returns>
        string GetMd5(byte[] bytes);
    }
}
