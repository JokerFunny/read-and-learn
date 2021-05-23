using Read_and_learn.Model.Bookshelf;
using System;

namespace Read_and_learn.Provider
{
    /// <summary>
    /// Provider to get proper pseudo-unique id for <see cref="Bookmark"/>.
    /// </summary>
    public static class BookmarkIdProvider
    {
        private static DateTimeOffset _constDate = new DateTimeOffset(new DateTime(2020, 3, 17, 0, 0, 0));

        /// <summary>
        /// Get a proper id. Based on time when it was created.
        /// </summary>
        public static long Id
            => (long)(DateTimeOffset.UtcNow - _constDate).TotalSeconds;
    }
}
