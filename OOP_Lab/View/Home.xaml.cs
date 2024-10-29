using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using OOP_Lab.Entities;
using OOP_Lab.Service;
using OOP_Lab.ViewModels;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OOP_Lab.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    ///

    public sealed partial class Home : Page
    {
        private HomeViewModel ViewModel { get; set; }

        public Home()
        {
            this.InitializeComponent();

            ViewModel = new HomeViewModel();

            this.DataContext = ViewModel;
        }

        private async void OnOpenAddDialog(object sender, RoutedEventArgs e)
        {
            AddDialog dialog = new AddDialog()
            {
                XamlRoot = this.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                ViewModel.UpdateView.Execute(null);
        }

        private async void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            EditDialog dialog = new EditDialog(ViewModel.SelectedItem)
            {
                XamlRoot = this.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                ViewModel.UpdateView.Execute(null);
        }
    }
}
