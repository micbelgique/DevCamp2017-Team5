using System;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using FamiDesk.Mobile.App.Messages;
using FamiDesk.Mobile.App.Models;
using FamiDesk.Mobile.App.Services;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Droid;

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

            //catch all exceptions
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
            };

            HandleExtras();

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();

            LoadApplication(new App());

            //MessagingCenter.Unsubscribe<BluetoothLEService>();
            MessagingCenter.Subscribe<BluetoothLEService, NotificationMessage>(this, "NotificationMessage", (sender, message) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowNotification(message.Title, message.Content, message.Extra);
                });
            });

            var ble = DependencyService.Get<BluetoothLEService>();
            ble?.ScanTask(new CancellationToken());
        }

        private void HandleExtras()
        {
            Bundle extra = Intent.Extras;
            if (extra != null)
            {
                string id = extra.GetString("Id");
                if (string.IsNullOrWhiteSpace(id) == false)
                {
                    MessagingCenter.Send((App)Xamarin.Forms.Application.Current, "NotificationClicked",
                        new NotificationClickedMessage(id));
                }
            }
        }


        private async void ShowNotification(string title, string content, KeyValuePair<string, string> extra)
        {
            Bundle extraBundle = new Bundle();
            extraBundle.PutString(extra.Key, extra.Value);

            // Set up an intent so that tapping the notifications returns to this app:
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtras(extraBundle);

            // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
            const int pendingIntentId = 0;
            PendingIntent pendingIntent =
                PendingIntent.GetActivity(this, pendingIntentId, intent, PendingIntentFlags.OneShot);

            // Build the notification:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                .SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
                .SetContentIntent(pendingIntent)  // Start 2nd activity when the intent is clicked.
                .SetContentTitle(title)      // Set its title
                 //.SetNumber(count)                       // Display the count in the Content Info
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetSmallIcon(Resource.Drawable.beacon_icon_lrg)  // Display this icon
                //.SetExtras(extraBundle)
                .SetContentText(content); // The message to display.

            var person = await DependencyService.Get<IDataStore<Person>>().GetItemAsync(extra.Value);
            if (person != null)
            {
                byte[] img = Convert.FromBase64String(person.Avatar);
                Bitmap largeIcon = BitmapFactory.DecodeByteArray(img, 0, img.Length);
                builder.SetLargeIcon(largeIcon);
            }

            // Build the notification:
            Notification notification = builder.Build();

            // Turn on vibrate:
            notification.Defaults |= NotificationDefaults.Vibrate;

            // Finally, publish the notification:
            NotificationManager notificationManager =
                (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(NotificationId, notification);
        }
    }
}
