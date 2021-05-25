using Android.Content;
using Android.Support.Design.Widget;
using Read_and_learn.Droid.Renderers;
using Read_and_learn.Page;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(SettingsPage), typeof(SettingsPageRenderer))]
namespace Read_and_learn.Droid.Renderers
{
    /// <summary>
    /// Custom renderer for <see cref="SettingsPage"/>
    /// </summary>
    class SettingsPageRenderer : TabbedPageRenderer
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="context"></param>
        public SettingsPageRenderer(Context context) : base(context)
        { }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);

            var tabLayout = child as TabLayout;
            if (tabLayout != null)
                tabLayout.TabMode = TabLayout.ModeScrollable;
        }
    }
}