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
    /// TodoCard.xaml 的交互逻辑
    /// </summary>
    public partial class TodoCard : UserControl
    {
        public TodoCard()
        {
            InitializeComponent();
        }

        public void SetTitle(string title)
        {
            tbkTitle.Text = title;
        }
    }
}
