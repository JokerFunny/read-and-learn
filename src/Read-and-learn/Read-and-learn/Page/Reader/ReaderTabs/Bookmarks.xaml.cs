using Autofac;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.View.Reader;
using Read_and_learn.Page.Reader.ReaderTabs.BookmarkTab;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Reader.ReaderTabs
{
    /// <summary>
    /// Tab to handle work with bookmarks.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bookmarks : StackLayout
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public Bookmarks()
        {
            InitializeComponent();

            BindingContext = new BookmarksVM();
        }

        /// <summary>
        /// Update target bookmarks list via <paramref name="items"/>.
        /// </summary>
        /// <param name="items">Target items</param>
        public void UpdateBookmarks(List<Bookmark> items)
        {
            Device.BeginInvokeOnMainThread(() => {
                _SetItems(items);
            });
        }

        private void _SetItems(List<Bookmark> items)
        {
            Items.Children.Clear();

            foreach (var item in _GetItems(items))
                Items.Children.Add(item);
        }

        private List<StackLayout> _GetItems(List<Bookmark> items)
        {
            var layouts = new List<StackLayout>();

            foreach (var item in items.Where(o => !o.Deleted)
                    .OrderBy(o => o.Position.Section)
                    .ThenBy(o => o.Position.SectionPosition))
            {
                layouts.Add(new BookmarkItem(item));
            }

            return layouts;
        }
    }
}