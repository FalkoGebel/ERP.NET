using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErpDotNet.Logic;
using ErpDotNet.Repository;
using ErpDotNet.Wpf.Texts;
using Microsoft.Win32;

namespace ErpDotNet.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private SqliteContext? _context;

        [ObservableProperty]
        public partial List<Item> Items { get; set; } = [];

        [ObservableProperty]
        public partial string StatusMessage { get; set; } = MainViewTexts.ViewModel_StatusMessage_Welcome;

        [RelayCommand]
        public void CreateDatabase()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = MainViewTexts.ViewModel_SaveFileDialog_Title,
                Filter = MainViewTexts.ViewModel_FileDialog_Filter
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    _context = DatabaseAdministration.CreateSqliteDatabase(saveFileDialog.FileName);
                    StatusMessage = MainViewTexts.ViewModel_StatusMessage_FileCreated + $"{saveFileDialog.FileName}";
                }
                catch (ArgumentException ex)
                {
                    StatusMessage = MainViewTexts.ViewModel_StatusMessage_FileNotCreated + $"{ex.Message}";
                }
            }
        }

        [RelayCommand]
        public void GetItemsFromDatabase()
        {
            if (!CheckContextAndSetStatusMessage())
                return;

            Items = [.. _context!.Item];
        }

        [RelayCommand]
        public void OpenDatabase()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = MainViewTexts.ViewModel_OpenFileDialog_Title,
                Filter = MainViewTexts.ViewModel_FileDialog_Filter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _context = DatabaseAdministration.OpenSqliteDatabase(openFileDialog.FileName);
                    StatusMessage = MainViewTexts.ViewModel_StatusMessage_FileOpened + $"{openFileDialog.FileName}";
                }
                catch (ArgumentException ex)
                {
                    StatusMessage = MainViewTexts.ViewModel_StatusMessage_FileNotOpened + $"{ex.Message}";
                }
            }
        }

        private bool CheckContextAndSetStatusMessage()
        {
            if (_context == null)
            {
                StatusMessage = MainViewTexts.ViewModel_StatusMessage_NoDatabase;
                return false;
            }

            return true;
        }
    }
}