using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CamDemo.model
{
    class VideoReader
    {
        private Mat frameMat;
        private bool ended = false;
        private readonly VideoCapture videoCapture;

        public VideoReader()
        {
            videoCapture = new VideoCapture(1);
            videoCapture.Set(VideoCaptureProperties.FrameWidth, 1920);
            videoCapture.Set(VideoCaptureProperties.FrameHeight, 1080);
            videoCapture.Set(VideoCaptureProperties.Fps, 60);

        }

        public async Task ReadFrame(IProgress<ProcessedFrame> progress, CancellationToken token)
        {
            await Task.Run(async () =>
            {
                 while (!(token.IsCancellationRequested || ended))
                {
                    using (frameMat = videoCapture.RetrieveMat())
                    {
                        ended = frameMat.Empty();
                        var result = WriteableBitmapConverter.ToWriteableBitmap(frameMat);
                        result.Freeze();
                        progress.Report(new ProcessedFrame { Bitmap = result });
                    }
                }
                await Task.Delay(5);
                Console.WriteLine("done");
            });
        }
    }

    class ProcessedFrame
    {
        public WriteableBitmap Bitmap;
        //more properties
    }
}
