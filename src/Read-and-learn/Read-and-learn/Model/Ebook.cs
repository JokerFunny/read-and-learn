using Read_and_learn.Model.DataStructure;
using System;
using System.Collections.Generic;

namespace Read_and_learn.Model
{
    /// <summary>
    /// Model for main entity - Book.
    /// </summary>
    public class Ebook
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Default language (taken from file).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Base64 cover.
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// Book <see cref="Section"/>(s)
        /// </summary>
        public List<Section> Sections { get; set; }

        /// <summary>
        /// Available navigation items.
        /// </summary>
        public List<Navigation> Navigation { get; set; }

        /// <summary>
        /// Fullpath to target file.
        /// </summary>
        public string Path { get; set; }
    }
}
