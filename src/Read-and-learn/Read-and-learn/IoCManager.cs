using Autofac;
using Read_and_learn.Provider;
using Read_and_learn.Repository;
using Read_and_learn.Repository.Interface;
using Read_and_learn.Service;
using Read_and_learn.Service.Interface;
using Read_and_learn.Service.Translation;

namespace Read_and_learn
{
    /// <summary>
    /// Custom IoC container.
    /// </summary>
    public static class IocManager
    {
        private static ContainerBuilder _containerBuilder;
        private static IContainer _container;

        /// <summary>
        /// Get <see cref="ContainerBuilder"/> instance.
        /// </summary>
        public static ContainerBuilder ContainerBuilder
        {
            get
            {
                if (_containerBuilder == null)
                {
                    _containerBuilder = new ContainerBuilder();

                    _SetUpIoC();
                }

                return _containerBuilder;
            }
        }

        /// <summary>
        /// Get <see cref="IContainer"/> instance.
        /// </summary>
        public static IContainer Container
            => _container;

        /// <summary>
        /// Build <see cref="Container"/>
        /// </summary>
        public static void Build()
        {
            if (_container == null)
                _container = ContainerBuilder.Build();
        }

        private static void _SetUpIoC()
        {
            // Repository layer.
            ContainerBuilder.RegisterType<BookRepository>().As<IBookRepository>();
            ContainerBuilder.RegisterType<BookmarkRepository>().As<IBookmarkRepository>();

            // Service layer.
            ContainerBuilder.RegisterType<FileService>().As<IFileService>();
            ContainerBuilder.RegisterType<BookmarkService>().As<IBookmarkService>();
            ContainerBuilder.RegisterType<BookshelfService>().As<IBookshelfService>();
            ContainerBuilder.RegisterType<FB2BookService>().As<IBookService>();
            ContainerBuilder.RegisterType<CryptoService>().As<ICryptoService>();
            ContainerBuilder.RegisterType<DatabaseService>().As<IDatabaseService>().SingleInstance();
            ContainerBuilder.RegisterType<MessageBus>().As<IMessageBus>().SingleInstance();

            // Translation services as keyed.
            ContainerBuilder.RegisterType<GlobalTranslateService>().As<ITranslateService>().SingleInstance();
            ContainerBuilder.RegisterType<ReversoTranslatorService>().Keyed<ITranslatorService>(TranslationServicesProvider.Reverso);
            ContainerBuilder.RegisterType<YandexTranslatorService>().Keyed<ITranslatorService>(TranslationServicesProvider.Yandex_Translator);
            ContainerBuilder.RegisterType<GoogleTranslatorService>().Keyed<ITranslatorService>(TranslationServicesProvider.Google);
        }

    }
}
