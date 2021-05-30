using PCLStorage;
using Microsoft.AppCenter.Analytics;
using Read_and_learn.Service.Interface;
using System;
using System.IO;
using System.Linq;
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

        public async Task<IFile> OpenFile(string name, IFolder folder)
        {
            folder = await GetFileFolder(name, folder);

            return await folder.GetFileAsync(GetLocalFileName(name));
        }

        public async Task<IFolder> GetFileFolder(string name, IFolder folder)
        {
            if (name.StartsWith("/"))
            {
                name = name.Substring(1);
            }
            while (name.Contains("/"))
            {
                var path = name.Split(new char[] { '/' }, 2);
                var folderName = path[0];
                name = path[1];
                folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            }

            return folder;
        }

        public string GetLocalFileName(string path)
            => path.Split('/').Last();

        public async Task<string> ReadFileContent(string folderName)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.GetFolderAsync(folderName);
            var contentFile = await folder.GetFileAsync(fileDesiredName);

            return await contentFile.ReadAllTextAsync();
        }

        public async Task<bool> DeleteFolder(string path)
        {
            var folder = await FileSystem.Current.LocalStorage.GetFolderAsync(path);

            try
            {
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
            Stream fileStream = await targetFile.OpenReadAsync();

            byte[] result = new byte[fileStream.Length];

            await fileStream.ReadAsync(result, 0, (int)fileStream.Length);

            return result;
        }

        public async Task<string> CreateLocalCopy(Stream fileDate, string id)
        {
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
