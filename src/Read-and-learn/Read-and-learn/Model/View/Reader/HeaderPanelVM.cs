using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Provider;
using Read_and_learn.Service.Interface;
using System;
using Xamarin.Forms;

namespace Read_and_learn.Model.View.Reader
{
    /// <summary>
    /// VM for HeaderPanel page.
    /// </summary>
    public class HeaderPanelVM : BaseVM
    {
        private BatteryProvider _batteryProvider;
        private string _pages;
        private string _clock;
        private string _batteryIcon;

        /// <summary>
        /// Current to total page value.
        /// </summary>
        public string Pages
        {
            get => _pages;
            set
            {
                _pages = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Current time.
        /// </summary>
        public string Clock
        {
            get => _clock;
            set
            {
                _clock = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Target battery icon.
        /// </summary>
        public string BatteryIcon
        {
            get => _batteryIcon;
            set
            {
                _batteryIcon = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// If battery should be showen.
        /// </summary>
        public bool ShowBattery
            => Device.RuntimePlatform == Device.Android;

        /// <summary>
        /// Text color related to the NightMode.
        /// </summary>
        public string TextColor
            => UserSettings.Reader.NightMode ? "#eff2f7" : "#000000";

        /// <summary>
        /// Default ctor.
        /// </summary>
        public HeaderPanelVM()
        {
            IocManager.Container.Resolve<IMessageBus>().Subscribe<PageChangeMessage>(_HandlePageChange);
            IocManager.Container.Resolve<IMessageBus>().Subscribe<BatteryChangeMessage>(_HandleBatteryChange);
            _batteryProvider = new BatteryProvider();

            _SetClock();
            _SetBattery();

            Device.StartTimer(new TimeSpan(0, 0, 10), () => {
                _SetClock();

                return true;
            });
        }

        private void _HandlePageChange(PageChangeMessage msg)
            => Pages = $"{msg.CurrentPage} / {msg.TotalPages}";

        private void _HandleBatteryChange(BatteryChangeMessage msg)
            => _SetBattery();

        private void _SetClock()
            => Clock = DateTime.Now.ToString("HH:mm");

        private void _SetBattery()
        {
            var percent = _batteryProvider.RemainingChargePercent;

            if (percent > 0)
            {
                string icon;
                if (percent > 0 && percent < 5)
                    icon = "empty_battery";
                else if (percent <= 30)
                    icon = "low_battery";
                else if (percent <= 55)
                    icon = "half_battery";
                else if (percent <= 85)
                    icon = "battery_almost_full";
                else
                    icon = "full_battery";

                if (UserSettings.Reader.NightMode)
                    icon += "_white";

                icon += ".png";

                if (BatteryIcon != icon)
                    BatteryIcon = icon;
            }
        }
    }
}
