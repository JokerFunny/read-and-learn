using System;
using SQLite;

namespace Read_and_learn.Model.Bookshelf
{
    /// <summary>
    /// Model for Book
    /// </summary>
    public class Book
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Cover { get; set; }
        public DateTimeOffset? FinishedReading { get; set; }
        public int Section { get; set; }
        public int SectionPosition { get; set; }

        [Ignore]
        public virtual Position Position
        {
            get =>  new Position(Section, SectionPosition);
            set
            {
                Section = value.Section;
                SectionPosition = value.SectionPosition;
            }
        }
    }
}
