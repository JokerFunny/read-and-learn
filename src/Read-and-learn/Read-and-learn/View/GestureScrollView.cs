using System;
using Xamarin.Forms;

namespace Read_and_learn.View
{
    /// <summary>
    /// Custom scroll view that support swipe gestures...
    /// </summary>
    /// <remarks>
    ///     DUE TO BAG ON ANDROID WITH DEFAULT <see cref="ScrollView"/>.
    /// </remarks>
    public class GestureScrollView : ScrollView
    {
        /// <summary>
        /// <see cref="EventHandler"/> on swipe left gesture.
        /// </summary>
        public event EventHandler SwipeLeft;

        /// <summary>
        /// <see cref="EventHandler"/> on swipe right gesture.
        /// </summary>
        public event EventHandler SwipeRight;

        /// <summary>
        /// On swipe left invoker.
        /// </summary>
        public void OnSwipeLeft() =>
            SwipeLeft?.Invoke(this, null);

        /// <summary>
        /// On swipe right invoker.
        /// </summary>
        public void OnSwipeRight() =>
            SwipeRight?.Invoke(this, null);
    }
}
