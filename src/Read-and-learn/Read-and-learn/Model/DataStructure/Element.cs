using Read_and_learn.Model.Bookshelf;

namespace Read_and_learn.Model.DataStructure
{
    /// <summary>
    /// Model to store dedicated element.
    /// </summary>
    public class Element
    {
        public string Value { get; set; }
        public ElementType Type { get; set; }
        public Position Position { get; set; }
    }

    public enum ElementType
    {
        Text,
        Image
    }
}
