using FluentAssertions;
using Read_and_learn.Service.Interface;
using Read_and_learn.Service.Translation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Read_and_learn.Tests.Service.Translation
{
    public class GoogleTranslatorServiceTest
    {
        private ITranslatorService _translatorService;
        private Random _randomGenerator;

        public GoogleTranslatorServiceTest()
        {
            _translatorService = new GoogleTranslatorService();
            _randomGenerator = new Random();

            UserSettings.Translation.SelectedLanguage = "uk";
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GoogleTranslatorService")]
        public async Task GoogleTranslatorService_TranslatePart_Should_Works_Fine_For_Supported_Language()
        {
            var result = await _translatorService.TranslatePart("pizza is a good food", "en");

            result.Should().NotBeNull();

            result.Error.Should().BeNull();
            result.Result.Should().Be("піца - це хороша їжа");
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GoogleTranslatorService")]
        public async Task GoogleTranslatorService_TranslatePart_Should_Return_Result_With_Target_Part_For_Not_Supported_Language()
        {
            UserSettings.Translation.SelectedLanguage = "badLanguage";
            var result = await _translatorService.TranslatePart("pizza is a good food", "en");

            result.Should().NotBeNull();

            result.Error.Should().BeNull();
            result.Result.Should().Be("pizza is a good food");
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GoogleTranslatorService")]
        public async Task GoogleTranslatorService_TranslateWord_Should_Works_Fine_For_Supported_Language()
        {
            var result = await _translatorService.TranslateWord("pizza", "en");

            result.Should().NotBeNull();

            result.Error.Should().BeNull();
            result.Result.Should().Be("піца");
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "GoogleTranslatorService")]
        public async Task GoogleTranslatorService_TranslateWord_Should_Return_Result_With_Target_Word_For_Not_Supported_Language()
        {
            UserSettings.Translation.SelectedLanguage = "badLanguage";
            var result = await _translatorService.TranslateWord("pizza", "en");

            result.Should().NotBeNull();

            result.Error.Should().BeNull();
            result.Result.Should().Be("pizza");
        }
    }
}
