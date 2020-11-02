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
    public class VideoLibraryViewModel : BindableBase
    {
        private object _obj = new object();

        public VideoLibraryViewModel()
        {
            LoadVideoSources(@"C:\Users\SunYubo\Documents\TaskFlow\AssignData\mediafiles");
        }

        private ObservableCollection<VideoProvider> _videos;
        public ObservableCollection<VideoProvider> Videos
        {
            get { return _videos; }
            set { SetProperty(ref _videos, value); }
        }

        void LoadVideoSources(string directory)
        {
            if (Videos == null)
            {
                Videos = new ObservableCollection<VideoProvider>();
            }

            if (Directory.Exists(directory))
            {
                foreach (var filePath in Directory.GetFiles(directory, "*.mp4", SearchOption.AllDirectories))
                {
                    LoadVideoSource(filePath);
                }
            }
        }

        async void LoadVideoSource(string filePath)
        {
            lock (_obj)
            {
                var videoProvider = new VideoProvider();
                videoProvider.Init(filePath);
                Videos.Add(videoProvider);
            }
        }
    }
}
