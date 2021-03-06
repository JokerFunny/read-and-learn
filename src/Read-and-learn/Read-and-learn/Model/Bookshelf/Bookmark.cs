using SQLite;
using System;

namespace Read_and_learn.Model.Bookshelf
{
    /// <summary>
    /// Model for Bookmark
    /// </summary>
    public class Bookmark
    {
        [PrimaryKey]
        public long Id { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset LastChange { get; set; }
        [NotNull]
        public string Name { get; set; }
        [Indexed]
        [NotNull]
        public string BookId { get; set; }
        public int Section { get; set; }
        public int SectionPosition { get; set; }

        [Ignore]
        public virtual Position Position
        {
            get => new Position(Section, SectionPosition);
            set
            {
                Section = value.Section;
                SectionPosition = value.SectionPosition;
            }
        }
    }
}
