using Microsoft.UI.Xaml.Controls;
using OOP_Lab.Entities;
using OOP_Lab.ViewModel;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OOP_Lab.View
{
    public sealed partial class EditDialog : ContentDialog
    {
        EditDialogViewModel ViewModel { get; set; }


        public EditDialog(ApplicationEntitie application)
        {
            this.InitializeComponent();

            ViewModel = new EditDialogViewModel(application);

            this.DataContext = ViewModel;

            ParseInput.Text = application.ToString();
        }

        private void OnClosing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.None)
                return;

            if (!ViewModel.UpdateApplication())
                args.Cancel = true;
        }
    }
}
