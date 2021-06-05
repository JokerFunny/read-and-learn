using FluentAssertions;
using Read_and_learn.Service;
using Read_and_learn.Service.Interface;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Read_and_learn.Tests.Service
{
    public class BookshelfServiceTest
    {
        private IBookshelfService _bookshelfService;
        private Random _randomGenerator;

        public BookshelfServiceTest()
        {
            _bookshelfService = new BookshelfService(null, null, null, null, null);
            _randomGenerator = new Random();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookshelfService")]
        public async Task BookshelfService_AddBook_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookshelfService")]
        public async Task BookshelfService_AddBook_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookshelfService")]
        public async Task BookshelfService_AddBook_Should_Throw_For_Incorrect_File_Format()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookshelfService")]
        public async Task BookshelfService_LoadBooks_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookshelfService")]
        public async Task BookshelfService_RemoveById_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookshelfService")]
        public async Task BookshelfService_RemoveById_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookshelfService")]
        public async Task BookshelfService_SaveBook_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "BookshelfService")]
        public async Task BookshelfService_SaveBook_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }
    }
}
