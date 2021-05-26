using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Reader.ReaderTabs
{
    /// <summary>
    /// Tab to handle navigation via chapters.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigation : StackLayout
    {
        /// <summary>
        /// Event handler to handle change chapter request.
        /// </summary>
        public event EventHandler<Model.DataStructure.Navigation> OnChapterChange;
        
        /// <summary>
        /// Default ctor.
        /// </summary>
        public Navigation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set naviagtion items.
        /// </summary>
        /// <param name="items"></param>
        public void SetNavigation(List<Model.DataStructure.Navigation> items)
        {
            Device.BeginInvokeOnMainThread(() => {
                _SetItems(items);
            });
        }

        private void _SetItems(List<Model.DataStructure.Navigation> items)
        {
            Items.Children.Clear();

            foreach (var item in _GetItems(items))
            {
                Items.Children.Add(item);
            }
        }

        private List<Label> _GetItems(List<Model.DataStructure.Navigation> items)
        {
            var labels = new List<Label>();

            foreach (var item in items)
            {
                var label = new Label
                {
                    StyleId = item.Id,
                    Text = item.Title,
                    Margin = new Thickness(item.Depth * 20, 0),
                    FontSize = Device.GetNamedSize(Device.RuntimePlatform == Device.Android ? NamedSize.Large : NamedSize.Medium, typeof(Label)),
                    TextColor = Color.White,
                };

                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => _ClickToItem(item);
                label.GestureRecognizers.Add(tgr);

                labels.Add(label);
            }

            return labels;
        }

        private void _ClickToItem(Model.DataStructure.Navigation item)
            => OnChapterChange?.Invoke(this, item);
    }
}