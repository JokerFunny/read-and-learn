using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Read_and_learn.Droid.Renderers;
using Read_and_learn.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AddButton), typeof(FloatingActionButtonRenderer))]
namespace Read_and_learn.Droid.Renderers
{
    /// <summary>
    /// Custom <see cref="ViewRenderer{TView, TNativeView}"/> for <see cref="AddButton"/>
    /// </summary>
    [Preserve]
    public class FloatingActionButtonRenderer : ViewRenderer<AddButton, FloatingActionButton>
    {
        private FloatingActionButton _fab;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="context">Target <see cref="Context"/></param>
        public FloatingActionButtonRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<AddButton> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _fab = new FloatingActionButton(Context);
                _fab.LayoutParameters = new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                _fab.Clickable = true;
                _fab.SetImageDrawable(ContextCompat.GetDrawable(Context, Resource.Drawable.add));
                _fab.BackgroundTintList = new ColorStateList(new int[][] { new int[0] }, new int[] { Android.Graphics.Color.ParseColor(e.NewElement.ButtonBackgroundColor) });
                SetNativeControl(_fab);
            }

            if (e.NewElement != null)
                _fab.Click += _Fab_Click;

            if (e.OldElement != null)
                _fab.Click -= _Fab_Click;
        }

        private void _Fab_Click(object sender, EventArgs e)
        {
            if (Element != null)
                Element.TriggerClicked();
        }
    }
}