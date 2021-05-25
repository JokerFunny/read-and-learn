using Android.App;
using Android.Content.Res;
using Read_and_learn.PlatformRelatedServices;
using System.IO;
using System.Threading.Tasks;

namespace Read_and_learn.Droid.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IAssetsManager"/>.
    /// </summary>
    public class AndroidAssetsManager : IAssetsManager
    {
        public async Task<string> GetFileContentAsync(string filename)
        {
            var content = string.Empty;

            return await Task.Run(() => {
                var file = Application.Context.Assets.Open(filename, Access.Buffer);

                using (var sr = new StreamReader(file))
                {
                    content = sr.ReadToEnd();
                }

                return content;
            });

        }
    }
}