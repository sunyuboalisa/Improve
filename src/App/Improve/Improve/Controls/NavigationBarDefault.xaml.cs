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

namespace Improve.Controls
{
    /// <summary>
    /// Interaction logic for NavigationBarDefault.xaml
    /// </summary>
    public partial class NavigationBarDefault : UserControl
    {
        public NavigationBarDefault()
        {
            InitializeComponent();
        }

        private void HamburgerMenu_ItemInvoked(object sender, MahApps.Metro.Controls.HamburgerMenuItemInvokedEventArgs args)
        {
            var hamburgerMenu = sender as MahApps.Metro.Controls.HamburgerMenu;
            hamburgerMenu.Content = args.InvokedItem;

            if (!args.IsItemOptions && hamburgerMenu.IsPaneOpen)
            {
                // close the menu if a item was selected
                hamburgerMenu.IsPaneOpen = false;
            }
        }
    }
}
