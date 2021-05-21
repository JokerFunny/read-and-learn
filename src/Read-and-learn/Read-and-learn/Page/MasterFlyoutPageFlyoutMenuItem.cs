using System;

namespace Read_and_learn.Page
{
    /// <summary>
    /// Default generated FlyoutMenuItem.
    /// </summary>
    public class MasterFlyoutPageFlyoutMenuItem
    {
        /// <summary>
        /// Target id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Target title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Target type.
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public MasterFlyoutPageFlyoutMenuItem()
        {
            TargetType = typeof(MasterFlyoutPageFlyoutMenuItem);
        }
    }
}