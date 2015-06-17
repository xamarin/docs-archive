using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using AsyncProgressReporting.Common;

namespace AsyncProgressReporting.IOS
{
	// ReSharper disable once InconsistentNaming
	public partial class AsyncProgressReporting_IOSViewController : UIViewController
	{
		public AsyncProgressReporting_IOSViewController() : base ("AsyncProgressReporting_IOSViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			StartDownloadButton.TouchUpInside += StartDownloadHandler;			
		}

		async void StartDownloadHandler(object sender, EventArgs e)
		{
			ProgressBar.Progress = 0f;

			Progress<DownloadBytesProgress> progressReporter = new Progress<DownloadBytesProgress>();
			progressReporter.ProgressChanged += (s, args) => ProgressBar.Progress = args.PercentComplete;

			Task<int> downloadTask = DownloadHelper.CreateDownloadTask(DownloadHelper.ImageToDownload, progressReporter);
			int bytesDownloaded = await downloadTask;
			Debug.WriteLine("Downloaded {0} bytes.", bytesDownloaded);
		}
	}
}

