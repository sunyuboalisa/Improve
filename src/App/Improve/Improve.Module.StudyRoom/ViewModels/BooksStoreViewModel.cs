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

        public BooksStoreViewModel()
        {
            if (Books==null)
            {
                Books = new ObservableCollection<Book>();
            }
            LoadBookThumbnails(@"C:\Users\SunYubo\Documents\TaskFlow\AssignData\mediafiles");
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
    }
}
