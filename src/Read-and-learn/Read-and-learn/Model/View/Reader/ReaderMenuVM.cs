using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Service.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace Read_and_learn.Model.View.Reader
{
    /// <summary>
    /// VM for reader menu page.
    /// </summary>
    public class ReaderMenuVM : BaseVM
    {
        private bool _tabContentVisible;

        private bool _tabBookmarksVisible;

        /// <summary>
        /// <see cref="ICommand"/> to handle click on content tab.
        /// </summary>
        public ICommand TabContentTappedCommand { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> to handle click on bookmarks tab.
        /// </summary>
        public ICommand TabBookmarksTappedCommand { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> to handle close current page.
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Is content tab visible.
        /// </summary>
        public bool TabContentVisible
        {
            get => _tabContentVisible;
            set
            {
                _tabContentVisible = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Is bookmarks tab visible.
        /// </summary>
        public bool TabBookmarksVisible
        {
            get => _tabBookmarksVisible;
            set
            {
                _tabBookmarksVisible = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public ReaderMenuVM()
        {
            TabContentVisible = true;

            TabContentTappedCommand = new Command(() => {
                TabContentVisible = true;
                TabBookmarksVisible = false;
            });

            TabBookmarksTappedCommand = new Command(() => {
                TabBookmarksVisible = true;
                TabContentVisible = false;
            });

            CloseCommand = new Command(() => {
                IocManager.Container.Resolve<IMessageBus>().Send(new CloseQuickPanelMessage());
            });
        }
    }
}
