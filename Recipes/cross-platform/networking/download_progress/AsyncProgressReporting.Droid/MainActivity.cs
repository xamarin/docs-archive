using Android.App;
using Android.Widget;
using Android.OS;
using System;
using AsyncProgressReporting.Common;
using System.Threading.Tasks;

namespace AsyncProgressReporting.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ProgressBar _progressBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            _progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += StartDownloadHandler;
        }

        async void StartDownloadHandler(object sender, System.EventArgs e)
        {
            _progressBar.Progress = 0;
            Progress<DownloadBytesProgress> progressReporter = new Progress<DownloadBytesProgress>();
            progressReporter.ProgressChanged += (s, args) => _progressBar.Progress = (int)(100 * args.PercentComplete);

            Task<int> downloadTask = AsyncProgressReporting.Common.DownloadHelper.CreateDownloadTask(DownloadHelper.ImageToDownload, progressReporter);
            int bytesDownloaded = await downloadTask;
            System.Diagnostics.Debug.WriteLine("Downloaded {0} bytes.", bytesDownloaded);
        }
    }
}


