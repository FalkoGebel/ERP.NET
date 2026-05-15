using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErpDotNet.Logic;
using ErpDotNet.Wpf.Texts;
using Microsoft.Win32;

namespace ErpDotNet.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial string StatusMessage { get; set; } = MainViewTexts.ViewModel_StatusMessage_Welcome;

        [RelayCommand]
        public void CreateDatabase()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = MainViewTexts.ViewModel_SaveFileDialog_Title,
                Filter = MainViewTexts.ViewModel_SaveFileDialog_Filter,
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var context = DatabaseAdministration.CreateSqliteDatabase(saveFileDialog.FileName);
                    StatusMessage = MainViewTexts.ViewModel_StatusMessage_FileCreated + $"{saveFileDialog.FileName}";
                }
                catch (ArgumentException ex)
                {
                    StatusMessage = MainViewTexts.ViewModel_StatusMessage_FileNotCreated + $"{ex.Message}";
                }
            }
        }
    }
}