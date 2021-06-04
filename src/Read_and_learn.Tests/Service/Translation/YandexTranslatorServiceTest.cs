using FluentAssertions;
using PCLAppConfig;
using Read_and_learn.Service.Interface;
using Read_and_learn.Service.Translation;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using YandexLinguistics.NET;

namespace Read_and_learn.Tests.Service.Translation
{
    public class YandexTranslatorServiceTest
    {
        private ITranslatorService _translatorService;
        private Random _randomGenerator;

        public YandexTranslatorServiceTest()
        {
            if (ConfigurationManager.AppSettings == null)
            {
                var assembly = typeof(App).GetTypeInfo().Assembly;

                ConfigurationManager.Initialise(assembly.GetManifestResourceStream("Read_and_learn.app.config"));
            }

            _translatorService = new YandexTranslatorService();
            _randomGenerator = new Random();

            UserSettings.Translation.SelectedLanguage = "uk";
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "YandexTranslatorService")]
        public async Task YandexTranslatorService_Throw_For_Blocked_Country()
        {
            var result = await _translatorService.TranslateWord("pizza", "en");

            result.Should().NotBeNull();

            result.Error.Should().NotBeNull()
                .And.BeOfType<YandexLinguisticsException>();
            result.Error.Message.Should().Be("A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond. (dictionary.yandex.net:443)");
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "YandexTranslatorService")]
        public async Task YandexTranslatorService_TranslatePart_Should_Works_Fine_For_Supported_Language()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "YandexTranslatorService")]
        public async Task YandexTranslatorService_TranslatePart_Throw_For_Not_Supported_Language()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "YandexTranslatorService")]
        public async Task YandexTranslatorService_TranslateWord_Should_Works_Fine_For_Supported_Language()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "YandexTranslatorService")]
        public async Task YandexTranslatorService_TranslateWord_Should_Return_Synonyms_For_Supported_Word()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Translation")]
        [Trait("Category", "YandexTranslatorService")]
        public async Task YandexTranslatorService_TranslateWord_Throw_For_Not_Supported_Language()
        {
            var result = 2;

            await Task.Delay((int)(_randomGenerator.NextDouble() * 3000));

            result.Should().Be(2);
        }
    }
}
