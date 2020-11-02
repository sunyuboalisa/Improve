using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Improve.Module.StudyRoom.Helpers
{
    class VideoHelper
    {
        public static BitmapImage GetThumbnail( string videoPath, string thumbnailPath)
        {
            string ffmpeg = @".\Ffmpeg\ffmpeg.exe";
            if (File.Exists(ffmpeg))
            {
            }
            var path = Path.GetFullPath(ffmpeg);
            var cmd = "-itsoffset -1  -i " + '"' + videoPath + '"' + " -vcodec mjpeg -vframes 1 -an -f rawvideo -s 320x240 " + '"' + thumbnailPath + '"';

            var startInfo = new ProcessStartInfo
            {
                FileName = ffmpeg,
                Arguments = cmd,
                CreateNoWindow=true
            };

            var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit(5000);

            return LoadImage(thumbnailPath);
        }

        static BitmapImage LoadImage(string path)
        {
            BitmapImage image = new BitmapImage();
            using (var ms = new MemoryStream(File.ReadAllBytes(path)))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.DecodePixelWidth = 100;
                image.EndInit();
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return image;
        }
    }
}
