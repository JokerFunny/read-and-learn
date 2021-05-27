using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Provider;
using Read_and_learn.Service.Interface;
using System.Collections.Generic;

namespace Read_and_learn.Model.View
{
    /// <summary>
    /// VM for margin settings.
    /// </summary>
    public class MarginVM : BaseVM
    {
        IMessageBus _messageBus;

        /// <summary>
        /// Available values.
        /// </summary>
        public List<int> Items => MarginProvider.Margins;

        /// <summary>
        /// Default ctor.
        /// </summary>
        public MarginVM()
        {
            _messageBus = IocManager.Container.Resolve<IMessageBus>();
        }

        /// <summary>
        /// Selected value.
        /// </summary>
        public int Value
        {
            get => UserSettings.Reader.Margin;
            set
            {
                if (UserSettings.Reader.Margin == value)
                    return;

                UserSettings.Reader.Margin = value;
                OnPropertyChanged();
                _messageBus.Send(new ChangeMarginMessage());
                _messageBus.Send(new CloseReaderMenuMessage());
            }
        }
    }
}
