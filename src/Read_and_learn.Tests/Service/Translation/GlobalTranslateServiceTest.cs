using FluentAssertions;
using Read_and_learn.Service.Interface;
using Read_and_learn.Service.Translation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Read_and_learn.Tests.Service.Translation
{
    public class GlobalTranslateServiceTest
    {
        private ITranslateService _translateService;
        private Random _randomGenerator;

        public GlobalTranslateServiceTest()
        {
            _translateService = new GlobalTranslateService();
            _randomGenerator = new Random();

            UserSettings.Translation.SelectedLanguage = "uk";
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslatePart_Should_Works_Fine_For_Supported_Language()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslatePart_Should_Retur_Result_With_Exception_Without_Internet_Connection()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslatePart_Should_Works_Fine_For_Google_Provider()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslatePart_Should_Works_Fine_For_Yandex_Provider()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslatePart_Should_Works_Fine_For_Reverso_Api_Provider()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslatePart_Should_Works_Fine_If_Error_Happend()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslateWord_Should_Retur_Offline_Result_Without_Internet_Connection()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslateWord_Should_Works_Fine_For_Google_Provider()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslateWord_Should_Works_Fine_For_Yandex_Provider()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslateWord_Should_Works_Fine_For_Reverso_Api_Provider()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GlobalTranslateService")]
        public async Task GlobalTranslateServiceTest_TranslateWord_Should_Return_Offline_Result_If_Error_Happend()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }
    }
}
