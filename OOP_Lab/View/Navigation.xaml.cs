using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OOP_Lab.View
{
    public sealed partial class Navigation : UserControl
    {
        public Navigation()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            NavView.SelectedItem = NavView.MenuItems[0];
            NavigateToPage("Home");
        }

        public void OnNavigationViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string pageName = args.InvokedItem.ToString();
            NavigateToPage(pageName);
        }

        private void NavigateToPage(string pageName)
        {
            Type pageType = Type.GetType($"OOP_Lab.View.{pageName}");
            if ((pageType != null) && (contentFrame.SourcePageType != pageType))
            {
                contentFrame.Navigate(pageType);
            }        
        }
    }
}
