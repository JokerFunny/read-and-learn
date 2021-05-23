using Read_and_learn.PlatformRelatedServices;
using System;
using System.Threading.Tasks;

namespace Read_and_learn.UWP.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IAssetsManager"/>.
    /// </summary>
    public class UWPAssetsManager : IAssetsManager
    {
        public async Task<string> GetFileContentAsync(string filename)
        {
            var assetsPath = $@"Assets\{filename}";
            var storage = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await storage.GetFileAsync(assetsPath).AsTask();
            return await Windows.Storage.FileIO.ReadTextAsync(file).AsTask();
        }
    }
}
