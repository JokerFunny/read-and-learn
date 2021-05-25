using Read_and_learn.PlatformRelatedServices;
using System.IO;

namespace Read_and_learn.Droid.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IFileHelper"/>.
    /// </summary>
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            return Path.Combine(path, filename);
        }
    }
}