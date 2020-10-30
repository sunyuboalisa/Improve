using Improve.LoggerLib;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Improve.Module.StudyRoom.Helpers
{
    class PdfHelper
    {
        /// <summary>
        /// 获取pdf缩略图
        /// </summary>
        /// <param name="path">pdf文件路径</param>
        /// <param name="index">指定获取缩略图的页面索引</param>
        /// <returns>指定页面的缩略图</returns>
        public static ImageSource GetThumbnail(string path,uint index)
        {
            if (File.Exists(path))
            {
                ImageSource PdfThumbnail(PdfDocument doc, uint index) 
                {
                    BitmapImage bitmap = new BitmapImage();
                    using (var page=doc.GetPage(index))
                    {
                        bitmap = PdfPageToThumbnail(page);
                    }

                    return bitmap; 
                }
                var file = StorageFile.GetFileFromPathAsync(path).AsTask().Result;
                var pdfDoc = PdfDocument.LoadFromFileAsync(file).AsTask().Result;

                ImageSource thumbnail = PdfThumbnail(pdfDoc, index);

                return thumbnail;
            }
            else
            {
                Logger.Log(Level.Warn, "pdf文件不存在。");
                return null;
            }
        }

        private static BitmapImage PdfPageToThumbnail(PdfPage page)
        {
            BitmapImage image = new BitmapImage();

            using (var stream=new InMemoryRandomAccessStream())
            {
                page.RenderToStreamAsync(stream).AsTask().Wait();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream.AsStream();
                image.DecodePixelWidth = 100;
                image.EndInit();
            }

            return image;
        }
    }
}
