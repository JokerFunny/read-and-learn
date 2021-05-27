using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Provider;
using Read_and_learn.Service.Interface;
using System.Collections.Generic;

namespace Read_and_learn.Model.View
{
    /// <summary>
    /// VM for font size.
    /// </summary>
    public class FontSizeVM : BaseVM
    {
        IMessageBus _messageBus;

        /// <summary>
        /// Available sizes.
        /// </summary>
        public List<int> Items => FontSizeProvider.Sizes;

        /// <summary>
        /// Default ctor.
        /// </summary>
        public FontSizeVM()
        {
            _messageBus = IocManager.Container.Resolve<IMessageBus>();
        }

        /// <summary>
        /// Selected value.
        /// </summary>
        public int Value
        {
            get => UserSettings.Reader.FontSize;
            set
            {
                if (UserSettings.Reader.FontSize == value)
                    return;

                UserSettings.Reader.FontSize = value;
                OnPropertyChanged();
                _messageBus.Send(new ChangeFontSizeMessage());
                _messageBus.Send(new CloseReaderMenuMessage());
            }
        }
    }
}
