using CamDemo.model;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace CamDemo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private VideoReader vr;
        private CancellationTokenSource cts;
        private CancellationToken token;

        public string imageBytes { get; private set; }
        public MainViewModel()
        {
            vr = new VideoReader();
            cts = new CancellationTokenSource();
            token = cts.Token;
            Task task = vr.ReadFrame(new Progress<ProcessedFrame>(ReportProgress), token);
        }

        private void ReportProgress(ProcessedFrame processed)
        {
            if (processed.Bitmap != null)
            {
                imageBytes = GetByteArrayFromImage(processed.Bitmap);
            }
        }

        public static string GetByteArrayFromImage(BitmapSource writeableBitmap)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(writeableBitmap));
            Byte[] bytes;
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                bytes = stream.ToArray();
                return Convert.ToBase64String(bytes);
            }

            
        }

        public override void Cleanup()
        {
            cts.Cancel();
            Task.Delay(1000).Wait();
            base.Cleanup();
        }

    }
}
