using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Improve.Module.Plan.Controls
{
    public class BackgroundGrid:FrameworkElement
    {
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(BackgroundGrid), new PropertyMetadata(0));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(BackgroundGrid), new PropertyMetadata(0));

        public double RowSpace
        {
            get { return (double)GetValue(RowSpaceProperty); }
            set { SetValue(RowSpaceProperty, value); }
        }
        public static readonly DependencyProperty RowSpaceProperty =
            DependencyProperty.Register("RowSpace", typeof(double), typeof(BackgroundGrid), new PropertyMetadata(double.NaN));

        public double ColumnSpace
        {
            get { return (double)GetValue(ColumnSpaceProperty); }
            set { SetValue(ColumnSpaceProperty, value); }
        }
        public static readonly DependencyProperty ColumnSpaceProperty =
            DependencyProperty.Register("ColumnSpace", typeof(double), typeof(BackgroundGrid), new PropertyMetadata(double.NaN));

        public Brush GridLineBrush
        {
            get { return (Brush)GetValue(GridLineBrushProperty); }
            set { SetValue(GridLineBrushProperty, value); }
        }
        public static readonly DependencyProperty GridLineBrushProperty =
            DependencyProperty.Register("GridLineBrush", typeof(Brush), typeof(BackgroundGrid), new PropertyMetadata(Brushes.Black));

        public double GridLineThickness
        {
            get { return (double)GetValue(GridLineThicknessProperty); }
            set { SetValue(GridLineThicknessProperty, value); }
        }
        public static readonly DependencyProperty GridLineThicknessProperty =
            DependencyProperty.Register("GridLineThickness", typeof(double), typeof(BackgroundGrid), new PropertyMetadata(1.0));

        public Pen Pen
        {
            get { return (Pen)GetValue(PenProperty); }
            set { SetValue(PenProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Pen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PenProperty =
            DependencyProperty.Register("Pen", typeof(Pen), typeof(BackgroundGrid), new PropertyMetadata(null));

        public BackgroundGrid()
        {
            GridLineThickness = 1;
            
            Opacity = 0.4;
            Color color = (Color)ColorConverter.ConvertFromString("#000000");
            Pen = new Pen()
            {
                Brush = new SolidColorBrush(color),
                Thickness = 1,
                //DashStyle = new DashStyle()
                //{
                //    Dashes = new DoubleCollection(new double[] { 20, 20})
                //}
            };
        }

       
        /// <summary>
        /// 通过行间距计算行数。
        /// </summary>
        /// <param name="rowSpace">行间距</param>
        /// <returns>行数</returns>
        protected virtual int CaculateRows(double rowSpace)
        {
            if (rowSpace <= 0 || RenderSize.Height <= 0)
                return 0;

            return (int)Math.Round(RenderSize.Height / rowSpace);
        }

        /// <summary>
        /// 通过列间距计算列数
        /// </summary>
        /// <param name="columnSpace">列间距</param>
        /// <returns>列数</returns>
        protected virtual int CaculateColumns(double columnSpace)
        {
            if (columnSpace <= 0 || RenderSize.Width <= 0)
                return 0;

            return (int)Math.Round(RenderSize.Width / columnSpace);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);
            int rows = Rows != 0 ? Rows : CaculateRows(RowSpace);
            int columns = Columns != 0 ? Columns : CaculateColumns(ColumnSpace);
            Pen pen = Pen == null ? new Pen() { Brush = GridLineBrush, Thickness = GridLineThickness, } : Pen;
            if (rows > 0)
            {
                double offsetY = RenderSize.Height / rows;
                Point startPoint = new Point(0, 0);
                Point endPoint = new Point(RenderSize.Width, 0);
                for (var i = 0; i < rows; i++)
                {
                    startPoint.Y = endPoint.Y = i * offsetY;
                    drawingContext.DrawLine(pen, startPoint, endPoint);
                }
            }

            if (columns > 0)
            {
                double offsetX = RenderSize.Width / columns;
                Point startPoint = new Point(0, 0);
                Point endPoint = new Point(0, RenderSize.Height);
                for (var i = 0; i < columns; i++)
                {
                    startPoint.X = endPoint.X = i * offsetX;
                    drawingContext.DrawLine(pen, startPoint, endPoint);
                }
            }
        }
    }
}
