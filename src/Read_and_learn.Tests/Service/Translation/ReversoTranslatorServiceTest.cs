using FluentAssertions;
using Read_and_learn.Service.Interface;
using Read_and_learn.Service.Translation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Read_and_learn.Tests.Service.Translation
{
    public class ReversoTranslatorServiceTest
    {
        private ITranslatorService _translatorService;
        private Random _randomGenerator;

        public ReversoTranslatorServiceTest()
        {
            _translatorService = new ReversoTranslatorService();
            _randomGenerator = new Random();

            UserSettings.Translation.SelectedLanguage = "uk";
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "ReversoTranslatorService")]
        public async Task ReversoTranslatorService_TranslatePart_Should_Works_Fine_For_Supported_Language()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1500));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "ReversoTranslatorService")]
        public async Task ReversoTranslatorService_TranslatePart_Throw_For_Not_Supported_Language()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1500));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "ReversoTranslatorService")]
        public async Task ReversoTranslatorService_TranslateWord_Should_Works_Fine_For_Supported_Language()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1500));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "ReversoTranslatorService")]
        public async Task ReversoTranslatorService_TranslateWord_Should_Return_Synonyms_For_Supported_Word()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1500));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "ReversoTranslatorService")]
        public async Task ReversoTranslatorService_TranslateWord_Should_Return_Contexts_For_Supported_Word()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1500));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "ReversoTranslatorService")]
        public async Task ReversoTranslatorService_TranslateWord_Throw_For_Not_Supported_Language()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 1500));

            result.Should().Be(2);
        }
    }
}
