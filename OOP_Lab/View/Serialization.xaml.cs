using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using OOP_Lab.Entities;
using OOP_Lab.Service;
using System;
using System.Collections.Generic;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OOP_Lab.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Serialization : Page
    {
        public Serialization()
        {
            this.InitializeComponent();
        }

        private async void SaveCollectionButton_Click(object sender, RoutedEventArgs e)
        {
            var savePicker = new FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);

            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hWnd);

            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.SuggestedFileName = "MyCollection";

            savePicker.FileTypeChoices.Add("JSON File", new List<string>() { ".json" });
            savePicker.FileTypeChoices.Add("CSV File", new List<string>() { ".csv" });

            var file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                var fileType = file.FileType;

                if (fileType == ".json")
                    SerializationHelper.SerializeToJson(DataService.ApplicationModels, file.Path);
                else if (fileType == ".csv")
                    SerializationHelper.SerializeToCsv(DataService.ApplicationModels, file.Path);
            }
        }

        private async void LoadCollectionButton_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);

            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

            openPicker.FileTypeFilter.Add(".json");
            openPicker.FileTypeFilter.Add(".csv");

            var file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                List<ApplicationEntitie> applications;

                if (file.FileType == ".json")
                    applications = SerializationHelper.DeserializeFromJson(file.Path);
                else if (file.FileType == ".csv")
                    applications = SerializationHelper.DeserializeFromCsv(file.Path);
                else
                    return;

                DataService.ApplicationModels.AddRange(applications);
            }
        }
    }
}
