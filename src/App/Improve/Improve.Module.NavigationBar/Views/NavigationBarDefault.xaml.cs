using System.Windows.Controls;

namespace Improve.Module.NavigationBar.Views
{
    /// <summary>
    /// Interaction logic for NavigationBarDefault
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
