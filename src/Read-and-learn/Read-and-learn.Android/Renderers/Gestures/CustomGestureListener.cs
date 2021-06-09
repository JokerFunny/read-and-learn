using Android.Views;
using System;

namespace Read_and_learn.Droid.Renderers
{
    /// <summary>
    /// Custom gesture listener.
    /// </summary>
    public class CustomGestureListener : GestureDetector.SimpleOnGestureListener
    {
        private static readonly int _rSWIPE_THRESHOLD = 100;
        private static readonly int _rSWIPE_VELOCITY_THRESHOLD = 100;

        private MotionEvent _lastOnDownEvent;

        /// <summary>
        /// <see cref="EventHandler"/> on swipe left gesture.
        /// </summary>
        public event EventHandler OnSwipeLeft;

        /// <summary>
        /// <see cref="EventHandler"/> on swipe right gesture.
        /// </summary>
        public event EventHandler OnSwipeRight;

        /// <summary>
        /// On down motion event handler.
        /// </summary>
        /// <param name="e">Target <see cref="MotionEvent"/></param>
        /// <returns>
        ///     true.
        /// </returns>
        public override bool OnDown(MotionEvent e)
        {
            _lastOnDownEvent = e;

            return true;
        }

        /// <summary>
        /// On fluing motion events handler.
        /// </summary>
        /// <param name="e1">Target first <see cref="MotionEvent"/></param>
        /// <param name="e2">Target second <see cref="MotionEvent"/></param>
        /// <param name="velocityX">Target X-velocity</param>
        /// <param name="velocityY">Target Y-velocity</param>
        /// <returns>
        ///     base <see cref="OnFling"/>.
        /// </returns>
        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            if (e1 == null)
                e1 = _lastOnDownEvent;

            float diffY = e2.GetY() - e1.GetY();
            float diffX = e2.GetX() - e1.GetX();

            if (Math.Abs(diffX) > Math.Abs(diffY))
            {
                if (Math.Abs(diffX) > _rSWIPE_THRESHOLD && Math.Abs(velocityX) > _rSWIPE_VELOCITY_THRESHOLD)
                {
                    if (diffX > 0)
                        OnSwipeRight?.Invoke(this, null);
                    else
                        OnSwipeLeft?.Invoke(this, null);
                }
            }

            return base.OnFling(e1, e2, velocityX, velocityY);
        }
    }
}