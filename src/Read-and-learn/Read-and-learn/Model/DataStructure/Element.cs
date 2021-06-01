using Read_and_learn.Model.Bookshelf;

namespace Read_and_learn.Model.DataStructure
{
    /// <summary>
    /// Model to store dedicated element.
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Target value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Type of current element, see <see cref="ElementType"/>.
        /// </summary>
        public ElementType Type { get; set; }

        /// <summary>
        /// Position of current element.
        /// </summary>
        /// <remarks>
        ///     [TODO]: check ifthis could be removed.
        /// </remarks>
        public Position Position { get; set; }
    }

    public enum ElementType
    {
        Text,
        Image,
        Symbol,
        NewLine
    }
}
