using System;

namespace Read_and_learn.Helpers.Exceptions
{
    /// <summary>
    /// Custom exception to prevent incorrect file type.
    /// </summary>
    public class UnknownFileFormatException : Exception
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public UnknownFileFormatException() : base()
        { }

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="format">name of the file that has incorrect format</param>
        public UnknownFileFormatException(string fileName) : base($"Unknow format, provided file: {fileName}")
        { }
    }
}
