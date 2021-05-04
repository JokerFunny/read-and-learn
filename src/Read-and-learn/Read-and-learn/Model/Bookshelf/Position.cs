namespace Read_and_learn.Model.Bookshelf
{
    /// <summary>
    /// Model for entity position (Book, bookmark etc.)
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Target section of book
        /// </summary>
        public int Section { get; }
        /// <summary>
        /// Current position of <see cref="Section"/>
        /// </summary>
        public int SectionPosition { get; }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Position()
        { }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Position(int section, int sectionPosition)
        {
            Section = section;
            SectionPosition = sectionPosition;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Position(Position position)
        {
            Section = position.Section;
            SectionPosition = position.SectionPosition;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Position p = (Position)obj;
                return Section == p.Section && SectionPosition == p.SectionPosition;
            }
        }
    }
}
