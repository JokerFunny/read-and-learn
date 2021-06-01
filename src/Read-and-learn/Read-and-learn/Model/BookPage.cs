using Read_and_learn.Model.DataStructure;
using System.Collections.Generic;

namespace Read_and_learn.Model
{
    /// <summary>
    /// Model to store info about dedicated page.
    /// </summary>
    public class BookPage
    {
        /// <summary>
        /// Id of related section.
        /// </summary>
        public string SectionId { get; set; }

        /// <summary>
        /// Page number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Position of page start.
        /// </summary>
        public int StartPosition { get; set; }

        /// <summary>
        /// List of elements.
        /// </summary>
        public List<Element> Content { get; set; }
    }
}
