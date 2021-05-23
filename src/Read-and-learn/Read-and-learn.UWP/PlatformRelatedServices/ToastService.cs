using Read_and_learn.PlatformRelatedServices;
using System;
using Windows.UI.Notifications;

namespace Read_and_learn.UWP.PlatformRelatedServices
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IToastService"/>.
    /// </summary>
    public class ToastService : IToastService
    {
        public void Show(string message)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(message));

            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(4);
            ToastNotifier.Show(toast);
        }
    }
}
