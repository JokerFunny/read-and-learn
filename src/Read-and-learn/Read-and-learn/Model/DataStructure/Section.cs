using System;
using System.Collections.Generic;
using System.Text;

namespace Read_and_learn.Model.DataStructure
{
    /// <summary>
    /// Datastructure to store book section info
    /// </summary>
    public class Section
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Depth { get; set; }
    }
}
