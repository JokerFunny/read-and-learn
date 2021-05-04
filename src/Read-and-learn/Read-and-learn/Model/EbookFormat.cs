using System;
using System.Collections.Generic;
using System.Text;

namespace Read_and_learn.Model
{
    /// <summary>
    /// Enum with all supported book formats.
    /// </summary>
    public enum EbookFormat
    {
        FB2,
        Epub,
        Txt,
        Html,
    }

    /// <summary>
    /// Enum with supported Epub versions.
    /// </summary>
    public enum EpubVersion
    {
        V200,
        V300,
        V301
    }
}
