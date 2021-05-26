using Autofac;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.Message;
using Read_and_learn.Service.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace Read_and_learn.Model.View.Reader
{
    /// <summary>
    /// VM for bookmark item.
    /// </summary>
    public class BookmarkVM : BaseVM
    {
        private bool _editMode;

        /// <summary>
        /// Target <see cref="Bookmark"/>
        /// </summary>
        public Bookmark Bookmark { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> to handle bookmark opening.
        /// </summary>
        public ICommand OpenBookmarkCommand { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> to handle bookmark deleting.
        /// </summary>
        public ICommand DeleteBookmarkCommand { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> to handle showing of bookmark editing view.
        /// </summary>
        public ICommand ShowEditCommand { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> to handle bookmark saving.
        /// </summary>
        public ICommand SaveCommand { get; set; }

        /// <summary>
        /// Is edited in current moment.
        /// </summary>
        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="bookmark"></param>
        public BookmarkVM(Bookmark bookmark)
        {
            Bookmark = bookmark;

            OpenBookmarkCommand = new Command(_OpenBookmark);
            DeleteBookmarkCommand = new Command(_DeleteBookmark);
            ShowEditCommand = new Command((entry) => _ShowEdit(entry));
            SaveCommand = new Command(_ChangeName);
        }

        private void _OpenBookmark()
        {
            IocManager.Container.Resolve<IMessageBus>().Send(new OpenBookmarkMessage { Bookmark = Bookmark });
            IocManager.Container.Resolve<IMessageBus>().Send(new CloseQuickPanelMessage());
        }

        private void _DeleteBookmark()
            => IocManager.Container.Resolve<IMessageBus>()
                .Send(new DeleteBookmarkMessage { Bookmark = Bookmark });

        public void _ShowEdit(object obj)
        {
            EditMode = true;

            if (obj is Entry entry)
                entry.Focus();
        }

        public void _ChangeName()
        {
            IocManager.Container.Resolve<IMessageBus>().Send(new BookmarkNameChangedMessage { Bookmark = Bookmark });

            EditMode = false;
        }
    }
}
