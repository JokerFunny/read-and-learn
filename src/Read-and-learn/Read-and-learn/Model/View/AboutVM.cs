using Autofac;
using Microsoft.AppCenter.Crashes;
using Read_and_learn.PlatformRelatedServices;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Read_and_learn.Model.View
{
    /// <summary>
    /// VM for about page.
    /// </summary>
    public class AboutVM : BaseVM
    {
        /// <summary>
        /// App version.
        /// </summary>
        public string Version
            => IocManager.Container.Resolve<IVersionProvider>().AppVersion;

        /// <summary>
        /// Copyright info.
        /// </summary>
        public string Copyright { get; } = $"Created by Danylo Novykov, 2021";

        /// <summary>
        /// Command for open url.
        /// </summary>
        public ICommand OpenUrlCommand { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public AboutVM()
        {
            OpenUrlCommand = new Command((url) => _OpenUrl(url));
        }

        private void _OpenUrl(object url)
        {
            if (url != null)
            {
                try
                {
                    var uri = new Uri(url.ToString());

                    Device.OpenUri(uri);
                }
                catch (Exception e)
                {
                    Crashes.TrackError(e, new Dictionary<string, string> 
                    {
                        {"Url", url.ToString() }
                    });
                }
            }
        }
    }
}
