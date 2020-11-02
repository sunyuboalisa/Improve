using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Improve.Module.StudyRoom.Models
{
    interface IMediaProvider
    {
        string Name { get; set; }
        string FilePath { get; set; }
        ImageSource Thumbnail { get; set; }
        void Init(string filePath);
    }
}
