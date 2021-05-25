using Android.Content;
using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Service.Interface;

namespace Read_and_learn.Droid.PlatformRelatedServices
{
    /// <summary>
    /// Receiver of change battery status.
    /// </summary>
    public class BatteryBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var messageBus = IocManager.Container.Resolve<IMessageBus>();

            messageBus.Send(new BatteryChangeMessage { });
        }
    }
}