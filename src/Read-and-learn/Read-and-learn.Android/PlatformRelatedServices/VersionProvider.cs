using Android.App;
using Read_and_learn.PlatformRelatedServices;

namespace Read_and_learn.Droid.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IAssetsManager"/>.
    /// </summary>
    public class VersionProvider : IVersionProvider
    {
        public string AppVersion
        {
            get
            {
                var context = Application.Context;

                var manager = context.PackageManager;
                var info = manager.GetPackageInfo(context.PackageName, 0);

                return info.VersionName;
            }
        }
    }
}