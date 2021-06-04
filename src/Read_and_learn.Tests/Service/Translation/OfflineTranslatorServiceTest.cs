using FluentAssertions;
using Read_and_learn.Service.Interface;
using Read_and_learn.Service.Translation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Read_and_learn.Tests.Service.Translation
{
    public class OfflineTranslatorServiceTest
    {
        private ITranslatorService _translatorService;
        private Random _randomGenerator;

        public OfflineTranslatorServiceTest()
        {
            _translatorService = new OfflineTranslatorService();
            _randomGenerator = new Random();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "OfflineTranslatorServiceTest")]
        public void OfflineTranslatorServiceTest_TranslatePart_Should_Throw()
        {
            Action act = () => _translatorService.TranslatePart("pizza", "en");

            act.Should().Throw<NotImplementedException>();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "OfflineTranslatorServiceTest")]
        public async Task OfflineTranslatorServiceTest_TranslateWord_Should_Works_Fine_For_Supported_Language()
        {
            var result = await _translatorService.TranslateWord("zip", "en");

            result.Should().NotBeNull();

            result.Error.Should().BeNull();
            result.Result.Should().Be("тріск");
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "OfflineTranslatorServiceTest")]
        public async Task OfflineTranslatorServiceTest_TranslateWord_Should_Return_Synonyms_For_Supported_Word()
        {
            var result = await _translatorService.TranslateWord("zip", "en");

            result.Should().NotBeNull();

            result.Error.Should().BeNull();
            result.Result.Should().Be("тріск");
            result.Synonyms.Should().NotBeNull()
                .And.BeEquivalentTo(new List<string>() { "застібка-блискавка" });
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "OfflineTranslatorServiceTest")]
        public async Task OfflineTranslatorServiceTest_TranslateWord_Should_Give_Bad_Result_If_Not_Translation_Exist()
        {
            var result = await _translatorService.TranslateWord("pizza", "en");

            result.Should().NotBeNull();

            result.Error.Should().BeNull();
            result.Result.Should().Be("There is no available translation for target word :(");
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "OfflineTranslatorServiceTest")]
        public async Task OfflineTranslatorServiceTest_TranslateWord_Give_Bad_Result_For_Not_Supported_Language()
        {
            var result = await _translatorService.TranslateWord("pizza", "someTrashLanguage");

            result.Should().NotBeNull();

            result.Error.Should().BeNull();
            result.Result.Should().Be("Source language not supported for translation.");
        }
    }
}
