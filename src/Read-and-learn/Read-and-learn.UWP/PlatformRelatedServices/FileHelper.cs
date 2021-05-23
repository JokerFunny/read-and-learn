using Read_and_learn.PlatformRelatedServices;
using System.IO;
using Windows.Storage;

namespace Read_and_learn.UWP.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IFileHelper"/>.
    /// </summary>
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
            => Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
    }
}
