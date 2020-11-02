﻿using Improve.Module.StudyRoom.ViewModels;
using System;
using System.Windows.Controls;

namespace Improve.Module.StudyRoom.Views
{
    /// <summary>
    /// Interaction logic for BooksStore
    /// </summary>
    public partial class BooksStore : UserControl
    {
        public BooksStore()
        {
            InitializeComponent();
        }

        private void bdContainer_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var vm = this.DataContext as BooksStoreViewModel;
            vm.PdfViewerVisible = true;
            var bd = sender as Border;
            var item = bd.DataContext as Improve.Module.StudyRoom.Models.Book;
            
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() => pdfViewer.OpenPdf(item.FilePath)));
        }
    }
}
