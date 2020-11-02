using Improve.Module.StudyRoom.Helpers;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Improve.Module.StudyRoom.Models
{
    public class VideoProvider:BindableBase,IMediaProvider
    {
        private bool _isInitilized;

        #region 属性

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }

        private ImageSource _thumbnail;
        public ImageSource Thumbnail
        {
            get { return _thumbnail; }
            set { SetProperty(ref _thumbnail, value); }
        }

        #endregion

        #region 方法

        public void Init(string filePath)
        {
            if (!_isInitilized)
            {
                FilePath = filePath;
                Thumbnail = VideoHelper.GetThumbnail(FilePath,$"thumbnail.jpeg");
                Name = System.IO.Path.GetFileNameWithoutExtension(FilePath);
            };
        }

        #endregion
    }
}
