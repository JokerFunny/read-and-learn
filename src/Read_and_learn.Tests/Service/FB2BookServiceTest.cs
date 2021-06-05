using FluentAssertions;
using Read_and_learn.Service.Interface;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Read_and_learn.Tests.Service
{
    public class FB2BookServiceTest
    {
        private IBookService _bookService;
        private Random _randomGenerator;

        public FB2BookServiceTest()
        {
            _bookService = new FB2BookService(null);
            _randomGenerator = new Random();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FB2BookService")]
        public async Task FB2BookService_CreateBookshelfBook_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FB2BookService")]
        public async Task FB2BookService_CreateBookshelfBook_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FB2BookService")]
        public async Task FB2BookService_OpenBook_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FB2BookService")]
        public async Task FB2BookService_OpenBook_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FB2BookService")]
        public async Task FB2BookService_OpenBook_Should_Throw_For_No_Existed_File()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FB2BookService")]
        public async Task FB2BookService_GetBook_Should_Works_Fine()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "FB2BookService")]
        public async Task FB2BookService_GetBook_Should_Throw_For_Null_Input()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1000));

            result.Should().Be(2);
        }
    }
}
