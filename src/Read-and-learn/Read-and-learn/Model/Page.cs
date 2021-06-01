using Read_and_learn.Model.DataStructure;
using System.Collections.Generic;

namespace Read_and_learn.Model
{
    /// <summary>
    /// Model to store info about dedicated page.
    /// </summary>
    public class Page
    {
        public int Number { get; set; }
        public List<Element> Content { get; set; }
    }
}
