using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Improve.Module.Plan.Controls
{
    /// <summary>
    /// DaySchedule.xaml 的交互逻辑
    /// </summary>
    public partial class DaySchedule : UserControl
    {
        public DaySchedule()
        {
            InitializeComponent();
        }

        private void container_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount==2)
            {
                var canvas = e.OriginalSource as Canvas;

                if (canvas!=null)
                {
                    AddTodo(canvas, e.GetPosition(canvas));
                }
            }
        }

        void AddTodo(Canvas container, Point position)
        {
            var todoCard = new TodoCard();
            todoCard.SetTitle("Todo Title");

            container.Children.Add(todoCard);
            Canvas.SetLeft(todoCard, position.X);
            Canvas.SetTop(todoCard, position.Y);
        }
    }
}
