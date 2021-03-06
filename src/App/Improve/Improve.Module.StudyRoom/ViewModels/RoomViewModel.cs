﻿using Improve.LoggerLib;
using Improve.Module.StudyRoom.Models;
using Improve.Module.StudyRoom.Views;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Improve.Module.StudyRoom.ViewModels
{
    public class RoomViewModel : BindableBase
    {
        public RoomViewModel()
        {
            Init();
        }

        void Init()
        {
            ShowTagView();
        }

        void ShowTagView()
        {
            TagPanelVidible = true;
            ContentPanelVisible = false;
        }



        void ShowFunctionView()
        {
            TagPanelVidible = false;
            ContentPanelVisible = true;
        }

        private bool _contentPanelVisible;
        public bool ContentPanelVisible
        {
            get { return _contentPanelVisible; }
            set { SetProperty(ref _contentPanelVisible, value); }
        }

        private UserControl _contentView;
        public UserControl ContentView
        {
            get { return _contentView; }
            set { SetProperty(ref _contentView, value); }
        }

        private bool _tagPanelVisible;
        public bool TagPanelVidible
        {
            get { return _tagPanelVisible; }
            set { SetProperty(ref _tagPanelVisible, value); }
        }

        private DelegateCommand _openBooksCmd;
        public DelegateCommand OpenBooksCmd =>
            _openBooksCmd ?? (_openBooksCmd = new DelegateCommand(ExecuteOpenBooksCmd));

        void ExecuteOpenBooksCmd()
        {
            ShowFunctionView();
            ContentView = new BooksStore();
        }

        private DelegateCommand _backCmd;
        public DelegateCommand BackCmd =>
            _backCmd ?? (_backCmd = new DelegateCommand(ExecuteBackCmd));

        void ExecuteBackCmd()
        {
            // 临时代码
            var vm = ContentView?.DataContext as BooksStoreViewModel;
            if (ContentView?.Visibility==Visibility.Visible&&vm?.PdfViewerVisible==true)
            {
                vm.PdfViewerVisible = false;
            }
            else if(ContentView?.Visibility == Visibility.Visible)
            {
                ShowTagView();
                ContentView = null;
            }
        }

        private DelegateCommand _forwardCmd;
        public DelegateCommand ForwardCmd =>
            _forwardCmd ?? (_forwardCmd = new DelegateCommand(ExecuteForwardCmd));

        void ExecuteForwardCmd()
        {
            // 临时代码
            if (ContentView?.Visibility==Visibility.Visible)
            {

            }
        }

        private DelegateCommand _openVideoLibCmd;
        public DelegateCommand OpenVideoLibCmd =>
            _openVideoLibCmd ?? (_openVideoLibCmd = new DelegateCommand(ExecuteOpenVideoLibCmd));

        void ExecuteOpenVideoLibCmd()
        {
            ShowFunctionView();
            ContentView = new VideoLibrary();
        }
    }
}
