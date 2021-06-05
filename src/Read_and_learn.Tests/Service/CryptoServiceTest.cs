using FluentAssertions;
using Read_and_learn.Service;
using Read_and_learn.Service.Interface;
using System;
using System.Text;
using Xunit;

namespace Read_and_learn.Tests.Service
{
    public class CryptoServiceTest
    {
        private ICryptoService _cryptoService;

        public CryptoServiceTest()
        {
            _cryptoService = new CryptoService();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "CryptoService")]
        public void CryptoService_GetMd5_Should_Works_Fine()
        {
            var targetPart = "test input";
            var targetBytes = Encoding.Default.GetBytes(targetPart);

            var result = _cryptoService.GetMd5(targetBytes);

            result.Should().NotBeNullOrEmpty()
                .And.NotBe(targetPart);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "CryptoService")]
        public void CryptoService_GetMd5_Should_Return_Same_Result_For_Same_Input()
        {
            var targetPart = "test input";
            var targetBytes = Encoding.Default.GetBytes(targetPart);

            var result1 = _cryptoService.GetMd5(targetBytes);

            result1.Should().NotBeNullOrEmpty()
                .And.NotBe(targetPart);

            var result2 = _cryptoService.GetMd5(targetBytes);

            result2.Should().NotBeNullOrEmpty()
                .And.NotBe(targetPart)
                .And.Be(result1);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Service")]
        [Trait("Category", "CryptoService")]
        public void CryptoService_GetMd5_Should_Throw_For_Null_Input()
        {
            Action act = () => _cryptoService.GetMd5(null);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
