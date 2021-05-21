using System.Collections.Generic;

namespace Read_and_learn.Model.DataStructure
{
    /// <summary>
    /// Datastructure to store book section info
    /// </summary>
    public class Section
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public string Epigraph { get; set; }
        public List<Element> Elements { get; set; }
    }
}
