using PCLStorage;
using Microsoft.AppCenter.Analytics;
using Read_and_learn.Service.Interface;
using System;
using System.IO;
using System.Linq;
using System.Text;
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

        public async Task<string> ReadFileData(string fileName)
            => await ReadFileData(fileName, FileSystem.Current.LocalStorage);

        public async Task<string> ReadFileData(string fileName, IFolder folder)
        {
            var file = await OpenFile(fileName, folder);

            return await file.ReadAllTextAsync();
        }

        public async Task<bool> Save(string path, string content)
        {
            var folder = FileSystem.Current.LocalStorage;
            var file = await folder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
            var bytes = Encoding.UTF8.GetBytes(content);

            try
            {
                using (Stream stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                {
                    await stream.WriteAsync(bytes, 0, bytes.Length);

                    return true;
                }
            }
            catch(Exception ex)
            {
                Analytics.TrackEvent($"File save failed. Exception message: {ex.Message}.");
            }

            return false;
        }

        public async Task<bool> CheckFile(string fileName)
        {
            var folder = FileSystem.Current.LocalStorage;

            var fileFolder = await GetFileFolder(fileName, folder);
            var exists = await fileFolder.CheckExistsAsync(GetLocalFileName(fileName));

            return exists == ExistenceCheckResult.FileExists;
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
    }
}
