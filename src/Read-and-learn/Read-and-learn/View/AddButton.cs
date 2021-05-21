using System;
using Xamarin.Forms;

namespace Read_and_learn.View
{
    /// <summary>
    /// Custom view for add button.
    /// </summary>
    public class AddButton : Xamarin.Forms.View
    {
        /// <summary>
        /// Handle click event.
        /// </summary>
        public event EventHandler Clicked;

        /// <summary>
        /// Button background color.
        /// </summary>
        public string ButtonBackgroundColor { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public AddButton()
        {
            ButtonBackgroundColor = AppSettings.Color;
            Margin = new Thickness(0, 0, 20, 20);
        }

        /// <summary>
        /// Handle when clicked.
        /// </summary>
        public void TriggerClicked()
        {
            Clicked?.Invoke(this, new EventArgs());
        }
    }
}
