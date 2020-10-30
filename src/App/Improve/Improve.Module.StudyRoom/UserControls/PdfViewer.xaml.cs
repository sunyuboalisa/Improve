using Improve.Module.StudyRoom.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Improve.Module.StudyRoom.UserControls
{
    /// <summary>
    /// Interaction logic for PdfViewer.xaml
    /// </summary>
    public partial class PdfViewer : UserControl
    {
        private PdfDocument pdfDocument;
        /// <summary>
        /// To resize pdf pages.
        /// </summary>
        DispatcherTimer resizeTimer;
        /// <summary>
        /// The indexes of pdf which has loaded source.
        /// </summary>
        private List<int> loadedPdfIndexes = new List<int>();

        public ObservableCollection<PdfProvider> PdfPages
        {
            get;
            set;
        } = new ObservableCollection<PdfProvider>();

        //
        // pdf zoom rate,(100%~300% spec).
        //
        public double ZoomRate => Math.Min(PagesContainer.LayoutTransform.Value.M11
                                            , PagesContainer.LayoutTransform.Value.M22);

        public string PdfPath
        {
            get { return (string)GetValue(PdfPathProperty); }
            set { SetValue(PdfPathProperty, value); }
        }

        public static readonly DependencyProperty PdfPathProperty =
            DependencyProperty.Register("PdfPath", typeof(string), typeof(PdfViewer),
               new PropertyMetadata(null, propertyChangedCallback: OnPdfPathChanged));

        private static void OnPdfPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pdfViewer = (PdfViewer)d;
            if (File.Exists(pdfViewer.PdfPath))
            {
                pdfViewer.OpenPdf(pdfViewer.PdfPath);
            }
        }

        public PdfViewer()
        {
            InitializeComponent();
            resizeTimer = new DispatcherTimer();
            resizeTimer.Interval = TimeSpan.FromMilliseconds(150);
            resizeTimer.Tick += resizeTimer_Tick;
        }

        public void OpenPdf(string pdfPath)
        {
            if (!string.IsNullOrEmpty(pdfPath))
            {
                //
                // Get PdfDocument.
                //
                var path = System.IO.Path.GetFullPath(pdfPath);

                Task<StorageFile> getFile = StorageFile.GetFileFromPathAsync(path).AsTask();
                getFile.Wait();
                Task<PdfDocument> fileLoad = PdfDocument.LoadFromFileAsync(getFile.Result).AsTask();
                fileLoad.Wait();

                pdfDocument = fileLoad.Result;

                LoadPagesWithoutSource(pdfDocument);
                GetVisibleRange(out int firstIndex, out int lastIndex);
                LoadPdfSource(firstIndex, lastIndex);
            }
        }

        private void resizeTimer_Tick(object sender, EventArgs e)
        {
            resizeTimer.Stop();

            ZoomToFixedWidth();
        }

        void ZoomToFixedWidth()
        {
            //
            // the scroll info will change,if you did not calculate the resume position,
            // it will scroll by the scrollview default behavior,then the current page will be changed.
            //
            //var resumePosition = svPdfContainer.VerticalOffset / ZoomRate;
            var resumePosition = svPdfContainer.VerticalOffset;

            double zoomRate = 0.0;

            for (int i = 0; i < PdfPages.Count; i++)
            {
                var bound = PdfPages[i].Bound;
                var baseWidth = bound.Width;
                bound.Width = svPdfContainer.ActualWidth;
                zoomRate = svPdfContainer.ActualWidth / baseWidth;
                bound.Height *= zoomRate;
            }

            resumePosition *= zoomRate;

            //
            // Reset zoom rate to default.
            //
            //ResetZoomRate();

            // 
            // It need update page position because scrollinfo has changed.
            //
            UpdatePagePosition();

            //
            // Scroll to the current page before scrollinfo changed.
            //
            svPdfContainer.ScrollToVerticalOffset(resumePosition);
        }

        /// <summary>
        /// Reset zoom rate to default.
        /// </summary>
        void ResetZoomRate()
        {
            PagesContainer.LayoutTransform = Transform.Identity;
        }

        /// <summary>
        /// Update pdf page offset.
        /// </summary>
        void UpdatePagePosition()
        {
            double pageOffset = 0;

            for (int i = 0; i < PdfPages.Count; i++)
            {
                var page = PdfPages[i];
                page.Bound.VerticalOffset = pageOffset;
                pageOffset += page.Bound.Height * ZoomRate;
            }
        }

        /// <summary>
        /// Load pdf page except pdf source.it only render the page bound.
        /// </summary>
        /// <param name="pdfDoc"></param>
        void LoadPagesWithoutSource(PdfDocument pdfDoc)
        {
            double pageOffset = 0;

            for (uint i = 0; i < pdfDoc.PageCount; i++)
            {
                using (var page = pdfDoc.GetPage(i))
                {
                    var zoomRate = this.ActualWidth / page.Size.Width;

                    var pdfProvider = new PdfProvider
                    {
                        Bound = new Bound
                        {
                            Width = this.ActualWidth,
                            Height = page.Size.Height * zoomRate,
                            VerticalOffset = pageOffset
                        },
                        Index = (int)i
                    };

                    PdfPages.Add(pdfProvider);
                    pageOffset += pdfProvider.Bound.Height;
                }
            }
        }

        /// <summary>
        /// Get the range of pdf in the viewport.
        /// </summary>
        /// <param name="firstIndex">The first pdf index in the viewport.</param>
        /// <param name="lastIndex">The last pdf index in the viewport.</param>
        void GetVisibleRange(out int firstIndex, out int lastIndex)
        {
            var currentPosition = Math.Floor(svPdfContainer.VerticalOffset);
            var lastPosition = Math.Floor(svPdfContainer.VerticalOffset + svPdfContainer.ViewportHeight);

            var pdfRanges = PdfPages.Select(page => new { start = page.Bound.VerticalOffset, end = page.Bound.VerticalOffset + page.Bound.Height * ZoomRate, index = page.Index });

            try
            {
                firstIndex = pdfRanges.Where(para => currentPosition >= para.start && currentPosition <= para.end).FirstOrDefault().index;
                lastIndex = pdfRanges.Where(para => lastPosition >= para.start && lastPosition <= para.end).FirstOrDefault().index;
            }
            catch (Exception) // when last page position is in viewport 
            {
                firstIndex = pdfRanges.Where(para => currentPosition >= para.start && currentPosition <= para.end).FirstOrDefault().index;
                lastIndex = pdfRanges.LastOrDefault().index;
            }
        }

        /// <summary>
        /// Loads pdf source according to a given range.
        /// </summary>
        /// <param name="firstIndex">Beginning of range</param>
        /// <param name="lastIndex">End of range</param>
        void LoadPdfSource(int firstIndex, int lastIndex)
        {
            try
            {
                for (int i = firstIndex; i <= lastIndex; i++)
                {
                    var pdfProvider = PdfPages[i];

                    if (pdfProvider.IsLoaded)
                    {
                        continue;
                    }

                    using (var page = pdfDocument.GetPage((uint)i))
                    {
                        pdfProvider.Source = PageToBitmap(page);
                        pdfProvider.IsLoaded = true;
                        loadedPdfIndexes.Add(i);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error in load pdf resource.");
            }
        }

        private BitmapImage PageToBitmap(PdfPage page)
        {
            BitmapImage image = new BitmapImage();

            using (var stream = new InMemoryRandomAccessStream())
            {
                page.RenderToStreamAsync(stream).AsTask().Wait();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream.AsStream();
                image.EndInit();
            }

            return image;
        }

        private void PagesContainer_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            try
            {
                var pdfContainer = e.Source as FrameworkElement;
                var scrollViewer = pdfContainer.Parent as ScrollViewer;

                if (pdfContainer == null || scrollViewer == null)
                {
                    return;
                }

                if (e.Manipulators.Count() > 1)// Represents zoom in,zoom out.
                {
                    isZooming = true;

                    var deltaManipulation = e.DeltaManipulation;
                    var center = e.ManipulationOrigin;

                    if (e.CumulativeManipulation.Scale.X > 1.13 || e.CumulativeManipulation.Scale.X < 0.87)
                    {
                        var result = ApplyScaleTransform(pdfContainer, deltaManipulation.Scale, center);

                        //
                        // The UI has changed ,it need update page position.
                        //
                        UpdatePagePosition();
                    }
                }

                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - Math.Round(e.CumulativeManipulation.Translation.X));
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - Math.Round(e.CumulativeManipulation.Translation.Y));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool ApplyScaleTransform(FrameworkElement element, Vector scale, Point center)
        {
            Matrix matrix = ((MatrixTransform)element.LayoutTransform).Matrix;
            var scaleFactor = Math.Round(scale.X, 3);
            matrix.ScaleAt(scaleFactor, scaleFactor, center.X, center.Y);
            var scale1 = Math.Min(matrix.M11, matrix.M22);

            //
            // the scale between 100% to 300%. spec.
            //
            if (scale1 >= 1.0 && scale1 <= 3.0)
            {
                element.LayoutTransform = new MatrixTransform(matrix);
                return true;
            }
            return false;
        }

        private void PagesContainer_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
        }

        private void PagesContainer_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
        }

        public void Dispose()
        {
            PdfPages.Clear();
            PdfPages = null;
            resizeTimer.Stop();
            resizeTimer.Tick -= resizeTimer_Tick;
            resizeTimer = null;
            svPdfContainer.ScrollChanged -= svPdfContainer_ScrollChanged;
        }

        void CleanPdfSource(int firstIndex, int lastIndex)
        {
            var needUnloadPdfIndexs = loadedPdfIndexes.Where(index => index < firstIndex || index > lastIndex);

            foreach (var item in needUnloadPdfIndexs)
            {
                PdfPages[item].Source = null;
                PdfPages[item].IsLoaded = false;
            }
        }

        private void PdfViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsLoaded)
            {
                resizeTimer?.Stop();
                resizeTimer?.Start();
            }
        }

        int preFirstIndex = -1;
        int preLastIndex = -1;
        bool isZooming = false;
        private void svPdfContainer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                if (!isZooming)
                {
                    try
                    {
                        GetVisibleRange(out int firstIndex, out int lastIndex);
                        Console.WriteLine($"viewport page range: {firstIndex}---{lastIndex}");
                        if (preFirstIndex != firstIndex || preLastIndex != lastIndex)
                        {
                            preFirstIndex = firstIndex;
                            preLastIndex = lastIndex;
                            LoadPdfSource(firstIndex, lastIndex);
                            CleanPdfSource(firstIndex, lastIndex);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("error in scroll changed.");
                    }

                }
            }
        }

        private void PagesContainer_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            isZooming = false;
        }
    }
}
