using Read_and_learn.PlatformRelatedServices;
using Windows.ApplicationModel;

namespace Read_and_learn.UWP.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IVersionProvider"/>.
    /// </summary>
    public class VersionProvider : IVersionProvider
    {
        public string AppVersion
        {
            get
            {
                var version = Package.Current.Id.Version;
                return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            }
        }
    }
}
