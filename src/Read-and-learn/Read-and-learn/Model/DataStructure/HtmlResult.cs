using System.Collections.Generic;

namespace Read_and_learn.Model.DataStructure
{
    /// <summary>
    /// Datastructure to handle book content as html
    /// </summary>
    public class HtmlResult
    {
        public string Html { get; set; }
        public IList<Image> Images { get; set; }
    }
}
