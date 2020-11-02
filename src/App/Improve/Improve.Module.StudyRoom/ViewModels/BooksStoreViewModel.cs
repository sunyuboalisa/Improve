using Improve.Module.StudyRoom.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Improve.Module.StudyRoom.ViewModels
{
    public class BooksStoreViewModel : BindableBase
    {
        private object obj = new object();

        private string _currentPdfPath;
        public string CurrentPdfPath
        {
            get { return _currentPdfPath; }
            set { SetProperty(ref _currentPdfPath, value); }
        }

        private bool _pdfViewerVisible;
        public bool PdfViewerVisible
        {
            get { return _pdfViewerVisible; }
            set { SetProperty(ref _pdfViewerVisible, value); }
        }

        public BooksStoreViewModel()
        {
            if (Books==null)
            {
                Books = new ObservableCollection<Book>();
            }
            LoadBookThumbnails(@"C:\Users\SunYubo\Documents\TaskFlow\AssignData\mediafiles");
            PdfViewerVisible = false;
        }

        public void LoadBookThumbnails(string directory)
        {
            if (Directory.Exists(directory))
            {
                foreach (var pdfPath in Directory.GetFiles(directory, "*.pdf", SearchOption.AllDirectories))
                {
                    LoadBookThumbnailAsync(pdfPath);
                }
            }
        }

        private async void LoadBookThumbnailAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                lock (obj)
                {
                    var book = new Book();
                    book.Init(filePath);
                    Books.Add(book);
                }
            }
        }

        public ObservableCollection<Book> Books { get; set; }

        private DelegateCommand _openPdfCmd;
        public DelegateCommand OpenPdfCmd =>
            _openPdfCmd ?? (_openPdfCmd = new DelegateCommand(ExecuteOpenBooksCmd));

        void ExecuteOpenBooksCmd()
        {
            CurrentPdfPath = "";
        }
    }
}
