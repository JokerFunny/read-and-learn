﻿using System;

namespace Read_and_learn.Model.DataStructure
{
    /// <summary>
    /// Model to handle result of the translation.
    /// </summary>
    public class TranslationResult
    {
        /// <summary>
        /// Result of translation.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Hold exception if it appears during translation.
        /// </summary>
        public Exception Error { get; set; }
    }
}
