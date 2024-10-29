using Microsoft.UI.Xaml.Controls;
using OOP_Lab.Entities;
using OOP_Lab.Enums;
using OOP_Lab.ViewModel;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OOP_Lab.View
{
    public sealed partial class AddDialog : ContentDialog
    {
        AddDialogViewModel ViewModel { get; set; }

        public AddDialog()
        {
            this.InitializeComponent();

            ViewModel = new AddDialogViewModel();

            this.DataContext = ViewModel;

            ConstructorComboBox.SelectedIndex = 0;
        }

        private void OnClosing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.None)
                return;

            if (!ViewModel.AddApplication())
                args.Cancel = true;
        }

        private void ComboBoxConstructor_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e
        )
        {
            ComboBox comboBox = sender as ComboBox;
            int selectedIndex = comboBox.SelectedIndex;

            ViewModel.Name = string.Empty;
            ViewModel.Description = string.Empty;
            ViewModel.SelectedCategoryIndex = (int)ApplicationCategory.Other;
            ViewModel.Rating = 1;
            ViewModel.Version = 1;

            switch (selectedIndex)
            {
                case 0:
                    NameTextBox.IsEnabled = true;
                    DescriptionTextBox.IsEnabled = true;
                    CategoryComboBox.IsEnabled = true;
                    RatingControl.IsEnabled = true;
                    VersionNumberBox.IsEnabled = true;

                    ParseStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ConstructorStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;

                    break;

                case 1:
                    NameTextBox.IsEnabled = true;
                    DescriptionTextBox.IsEnabled = true;
                    CategoryComboBox.IsEnabled = false;
                    RatingControl.IsEnabled = false;
                    VersionNumberBox.IsEnabled = false;

                    ParseStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ConstructorStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;

                    break;

                case 2:
                    NameTextBox.IsEnabled = true;
                    DescriptionTextBox.IsEnabled = true;
                    CategoryComboBox.IsEnabled = true;
                    RatingControl.IsEnabled = false;
                    VersionNumberBox.IsEnabled = false;

                    ParseStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ConstructorStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;

                    break;

                case 3:
                    NameTextBox.IsEnabled = true;
                    DescriptionTextBox.IsEnabled = true;
                    CategoryComboBox.IsEnabled = false;
                    RatingControl.IsEnabled = false;
                    VersionNumberBox.IsEnabled = false;

                    ParseStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    ConstructorStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;

                    ViewModel.Name = "Unknow";
                    ViewModel.Description = "Unknow";
                    break;

                case 4:

                    ParseStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                    ConstructorStackPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;

                    break;

                default:
                    break;
            }

            ViewModel.Error = null;
            ViewModel.SelectedConstructorIndex = selectedIndex;
        }
    }
}