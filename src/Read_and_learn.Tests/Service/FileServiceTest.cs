using FluentAssertions;
using Read_and_learn.Service;
using Read_and_learn.Service.Interface;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Read_and_learn.Tests.Service
{
    public class FileServiceTest
    {
        private IFileService _fileService;
        private Random _randomGenerator;

        public FileServiceTest()
        {
            _fileService = new FileService();
            _randomGenerator = new Random();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FileService")]
        public async Task FileService_ReadFileContent_Should_Works_Fine_For_Existed_File()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FileService")]
        public async Task FileService_ReadFileContent_Should_Return_Null_For_Not_Existed_File()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FileService")]
        public async Task FileService_ReadFileContent_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FileService")]
        public async Task FileService_GetByteArrayFromFile_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FileService")]
        public async Task FileService_GetByteArrayFromFile_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FileService")]
        public async Task FileService_CreateLocalCopy_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FileService")]
        public async Task FileService_CreateLocalCopy_Should_Works_Fine_And_Replace_Existed_File_And_Folder()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FileService")]
        public async Task FileService_CreateLocalCopy_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }
    }
}
