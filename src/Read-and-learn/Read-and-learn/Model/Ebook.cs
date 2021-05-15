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
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<Section> Sections { get; set; }
        public IEnumerable<File> Files { get; set; }
        public string Folder { get; set; }
        public string ContentBasePath { get; set; }
    }
}
