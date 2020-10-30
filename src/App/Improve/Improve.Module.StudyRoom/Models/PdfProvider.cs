using Improve.Module.StudyRoom.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Improve.Module.StudyRoom.Models
{
    public class PdfProvider : BindBase
    {
        public bool IsLoaded { get; set; }
        public int Index { get; set; }
        public double ZoomRate { get; set; } = 1;

        private ImageSource _source;
        public ImageSource Source
        {
            get => _source;
            set
            {
                if (value != _source)
                {
                    _source = value;
                    OnPropertyChanged();
                }
            }
        }

        private Bound _bound;
        public Bound Bound
        {
            get => _bound;
            set
            {
                if (value != _bound)
                {
                    _bound = value;
                    OnPropertyChanged();
                }
            }
        }
    }

    public class Bound : BindBase
    {
        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged();
                }
            }
        }

        public double VerticalOffset { get; set; }
    }
}
