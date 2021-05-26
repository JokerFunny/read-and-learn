using System;
using Xamarin.Forms;

namespace Read_and_learn.View.Buttons
{
    /// <summary>
    /// Custom button with "toggle" feature.
    /// </summary>
    public class ToggleButton : Button
    {
        /// <summary>
        /// <see cref="EventHandler"/> for toggle action.
        /// </summary>
        public event EventHandler<ToggledEventArgs> Toggled;

        /// <summary>
        /// Check if toggled.
        /// </summary>
        public static BindableProperty IsToggledProperty =
            BindableProperty.Create("IsToggled", typeof(bool), typeof(ToggleButton), false,
                                    propertyChanged: _OnIsToggledChanged);

        /// <summary>
        /// Default ctor.
        /// </summary>
        public ToggleButton()
        {
            Clicked += (sender, args) => IsToggled ^= true;
        }

        /// <summary>
        /// Check if toggled.
        /// </summary>
        public bool IsToggled
        {
            set { SetValue(IsToggledProperty, value); }
            get { return (bool)GetValue(IsToggledProperty); }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            VisualStateManager.GoToState(this, "ToggledOff");
        }

        private static void _OnIsToggledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ToggleButton toggleButton = (ToggleButton)bindable;
            bool isToggled = (bool)newValue;

            // Fire event
            toggleButton.Toggled?.Invoke(toggleButton, new ToggledEventArgs(isToggled));

            // Set the visual state
            VisualStateManager.GoToState(toggleButton, isToggled ? "ToggledOn" : "ToggledOff");
        }
    }
}
