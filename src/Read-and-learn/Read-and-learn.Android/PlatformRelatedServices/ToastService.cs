using Android.App;
using Android.Widget;
using Read_and_learn.PlatformRelatedServices;

namespace Read_and_learn.Droid.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IToastService"/>.
    /// </summary>
    public class ToastService : IToastService
    {
        public void Show(string message)
            => Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
    }
}