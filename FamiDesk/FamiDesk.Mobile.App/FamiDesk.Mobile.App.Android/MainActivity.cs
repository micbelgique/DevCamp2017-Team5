using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using FamiDesk.Mobile.App.Messages;
using FamiDesk.Mobile.App.Services;
using Xamarin.Forms;

namespace FamiDesk.Mobile.App.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int NotificationId = 9000;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            //MessagingCenter.Unsubscribe<BluetoothLEService>();
            MessagingCenter.Subscribe<NotificationMessage>(this, "NotificationMessage", message => {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowNotification(message.Title, message.Content);
                });
            });

            var ble = DependencyService.Get<BluetoothLEService>();
            ble?.ScanTask(new CancellationToken());
        }


        private void ShowNotification(string title, string content)
        {
            // Build the notification:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                .SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
                //.SetContentIntent(resultPendingIntent)  // Start 2nd activity when the intent is clicked.
                .SetContentTitle("Button Clicked")      // Set its title
                //.SetNumber(count)                       // Display the count in the Content Info
                .SetSmallIcon(Resource.Drawable.beacon_icon_lrg)  // Display this icon
                .SetContentText(content); // The message to display.

            // Finally, publish the notification:
            NotificationManager notificationManager =
                (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(NotificationId, builder.Build());
        }
    }
}
