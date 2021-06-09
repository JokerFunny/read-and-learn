using Android.Content;
using Android.Runtime;
using Android.Views;
using Read_and_learn.Droid.Renderers;
using Read_and_learn.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GestureScrollView), typeof(GestureScrollViewRenderer))]
namespace Read_and_learn.Droid.Renderers
{
    /// <summary>
    /// Custom <see cref="ScrollViewRenderer"/> for <see cref="GestureScrollView"/>
    /// </summary>
    [Preserve]
    public class GestureScrollViewRenderer : ScrollViewRenderer
    {
        readonly CustomGestureListener _listener;
        readonly GestureDetector _detector;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="context">Target <see cref="Context"/></param>
        public GestureScrollViewRenderer(Context context) : base(context)
        {
            _listener = new CustomGestureListener();
            _detector = new GestureDetector(context, _listener);
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            if (_detector != null)
            {
                _detector.OnTouchEvent(e);
                base.DispatchTouchEvent(e);
                return true;
            }

            return base.DispatchTouchEvent(e);
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            base.OnTouchEvent(ev);

            if (_detector != null)
                return _detector.OnTouchEvent(ev);

            return false;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                _listener.OnSwipeLeft -= HandleOnSwipeLeft;
                _listener.OnSwipeRight -= HandleOnSwipeRight;
            }

            if (e.OldElement == null)
            {
                _listener.OnSwipeLeft += HandleOnSwipeLeft;
                _listener.OnSwipeRight += HandleOnSwipeRight;
            }
        }

        void HandleOnSwipeLeft(object sender, EventArgs e) =>
            ((GestureScrollView)Element).OnSwipeLeft();

        void HandleOnSwipeRight(object sender, EventArgs e) =>
            ((GestureScrollView)Element).OnSwipeRight();
    }
}