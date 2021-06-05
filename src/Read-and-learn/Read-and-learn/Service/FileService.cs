using PCLStorage;
using Microsoft.AppCenter.Analytics;
using Read_and_learn.Service.Interface;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using FileSystem = PCLStorage.FileSystem;

namespace Read_and_learn.Service
{
    /// <summary>
    /// Implementation of <see cref="IFileService"/>.
    /// </summary>
    public class FileService : IFileService
    {
        public const string fileDesiredName = "book.fb2";

        public async Task<string> ReadFileContent(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentNullException(nameof(folderName));

            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.GetFolderAsync(folderName);
            var contentFile = await folder.GetFileAsync(fileDesiredName);

            return await contentFile?.ReadAllTextAsync() ?? string.Empty;
        }

        public async Task<bool> DeleteFolder(string path)
        {
            try
            {
                var folder = await FileSystem.Current.LocalStorage.GetFolderAsync(path);

                await folder.DeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent($"File deleting failed. Exception message: {ex.Message}.");
            }

            return false;
        }

        public async Task<byte[]> GetByteArrayFromFile(FileResult targetFile)
        {
            if (targetFile == null)
                throw new ArgumentNullException(nameof(targetFile));

            Stream fileStream = await targetFile.OpenReadAsync();

            byte[] result = new byte[fileStream.Length];

            await fileStream.ReadAsync(result, 0, (int)fileStream.Length);

            return result;
        }

        public async Task<string> CreateLocalCopy(Stream fileDate, string id)
        {
            if (fileDate == null)
                throw new ArgumentNullException(nameof(fileDate));
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync(id, CreationCollisionOption.ReplaceExisting);
            var contentFile = await folder.CreateFileAsync(fileDesiredName, CreationCollisionOption.ReplaceExisting);

            using (Stream stream = await contentFile.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                await fileDate.CopyToAsync(stream);
            }

            return contentFile.Path;
        }
    }
}
