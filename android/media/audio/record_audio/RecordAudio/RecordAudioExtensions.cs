using System;
using System.IO;
using Android;
using Android.App;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using System.Linq;
using Android.Views;

namespace RecordAudio
{
    /// <summary>
    /// Encapsulate the business of checking for permissions and such as much as possible, so that we can focus on 
    /// the recording APIs.
    /// </summary>
    static class RecordAudioExtensions
    {
        public static readonly string[] REQUIRED_PERMISSIONS = { Manifest.Permission.WriteExternalStorage, Manifest.Permission.RecordAudio };
        public static readonly int REQUEST_ALL_PERMISSIONS = 1200;

        public static Boolean HasPermissionToRecord(this Context context)
        {

            foreach (var permission in REQUIRED_PERMISSIONS)
            {
                var result = ContextCompat.CheckSelfPermission(context, permission);
                if (Android.Content.PM.Permission.Denied.Equals(result))
                {
                    return false;
                }
            }

            return true;
        }

        public static void PerformRuntimePermissionsCheckForRecording(this Activity activity)
        {
            if (activity.ShouldShowUserPermissionRationle())
            {
                var rationale = Snackbar.Make(activity.GetLayoutForSnackbar(),
                                              Resource.String.permissions_rationale,
                                              Snackbar.LengthIndefinite);
                Action<View> action = new Action<View>((obj) => ActivityCompat.RequestPermissions(activity, REQUIRED_PERMISSIONS, REQUEST_ALL_PERMISSIONS));
                rationale.SetAction(Resource.String.ok, action);

                rationale.Show(); ;
            }
            else
            {
                ActivityCompat.RequestPermissions(activity, REQUIRED_PERMISSIONS, REQUEST_ALL_PERMISSIONS);
            }
        }

        public static bool ShouldShowUserPermissionRationle(this Activity activity)
        {
            bool showRecording = ActivityCompat.ShouldShowRequestPermissionRationale(activity, Manifest.Permission.RecordAudio);
            bool showWrite = ActivityCompat.ShouldShowRequestPermissionRationale(activity, Manifest.Permission.WriteExternalStorage);
            return showRecording && showWrite;
        }

        public static View GetLayoutForSnackbar(this Activity activity)
        {
            return activity.FindViewById(Android.Resource.Id.Content);
        }

        public static bool IsExternalStorageWriteable(this Context context)
        {
            String state = Android.OS.Environment.ExternalStorageState;
            bool writeable = Android.OS.Environment.MediaMounted.Equals(state, StringComparison.OrdinalIgnoreCase) &&
                                    !Android.OS.Environment.MediaMountedReadOnly.Equals(state, StringComparison.OrdinalIgnoreCase);

            return writeable;
        }

        public static string GetFileNameForRecording(this Context context)
        {
            string dir = context.GetExternalFilesDir(Android.OS.Environment.DirectoryMusic).AbsolutePath;
            string file = Path.Combine(dir, "test.3gpp");
            return file;
        }

        public static bool AllPermissionsGranted(this Android.Content.PM.Permission[] grantResults)
        {
            return !grantResults.Any(r => r == Android.Content.PM.Permission.Denied);
        }

    }
}