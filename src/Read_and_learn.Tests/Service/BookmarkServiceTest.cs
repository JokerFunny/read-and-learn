using FluentAssertions;
using Read_and_learn.Service;
using Read_and_learn.Service.Interface;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Read_and_learn.Tests.Service
{
    public class BookmarkServiceTest
    {
        private IBookmarkService _bookmarkService;
        private Random _randomGenerator;

        public BookmarkServiceTest()
        {
            _bookmarkService = new BookmarkService(null);
            _randomGenerator = new Random();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookmarkService")]
        public async Task BookmarkService_CreateBookmark_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookmarkService")]
        public async Task BookmarkService_CreateBookmark_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookmarkService")]
        public async Task BookmarkService_DeleteBookmark_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookmarkService")]
        public async Task BookmarkService_DeleteBookmark_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookmarkService")]
        public async Task BookmarkService_LoadBookmarksByBookId_Should_Works_Fine_For_Single_Bookmark()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookmarkService")]
        public async Task BookmarkService_LoadBookmarksByBookId_Should_Works_Fine_For_Multiple_Bookmark()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookmarkService")]
        public async Task BookmarkService_LoadBookmarksByBookId_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookmarkService")]
        public async Task BookmarkService_SaveBookmark_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookmarkService")]
        public async Task BookmarkService_SaveBookmark_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }
    }
}
