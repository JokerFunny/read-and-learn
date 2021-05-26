using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Service.Interface;
using System.Windows.Input;
using Xamarin.Forms;

namespace Read_and_learn.Model.View.Reader
{
    /// <summary>
    /// VM for bookmarks page.
    /// </summary>
    public class BookmarksVM
    {
        /// <summary>
        /// <see cref="ICommand"/> for handle add new bookmark.
        /// </summary>
        public ICommand AddBookmarkCommand { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public BookmarksVM()
        {
            AddBookmarkCommand = new Command(_AddBookmark);
        }

        private void _AddBookmark()
            => IocManager.Container.Resolve<IMessageBus>().Send(new AddBookmarkMessage());
    }
}
