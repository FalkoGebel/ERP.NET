using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErpDotNet.Logic;
using ErpDotNet.Repository;
using Microsoft.Win32;

namespace ErpDotNet.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private SqliteContext? _context;

        [ObservableProperty]
        public partial Type ListType { get; set; }

        [ObservableProperty]
        public partial List<object> Lines { get; set; } = [];

        [ObservableProperty]
        public partial Item? CurrentItem { get; set; }

        [ObservableProperty]
        public partial bool GeneralListVisible { get; set; }

        [ObservableProperty]
        public partial int GeneralListSelectedIndex { get; set; }

        [ObservableProperty]
        public partial bool ItemCardVisible { get; set; }

        [ObservableProperty]
        public partial string StatusMessage { get; set; } = Texts.MainView.ViewModel_StatusMessage_Welcome;

        /// <summary>
        /// Opens a save file dialog to create a new SQLite database. If the database is created successfully,
        /// the context is initialized and a success message is set in the status message. If there is an error during
        /// creation, an error message is set in the status message.
        /// </summary>
        [RelayCommand]
        public void CreateDatabase()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = Texts.MainView.ViewModel_SaveFileDialog_Title,
                Filter = Texts.MainView.ViewModel_FileDialog_Filter
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    _context = DatabaseAdministration.CreateSqliteDatabase(saveFileDialog.FileName);
                    StatusMessage = Texts.MainView.ViewModel_StatusMessage_FileCreated + $"{saveFileDialog.FileName}";
                }
                catch (ArgumentException ex)
                {
                    StatusMessage = Texts.MainView.ViewModel_StatusMessage_FileNotCreated + $"{ex.Message}";
                }
            }
        }

        /// <summary>
        /// Loads items from the database into the Items property and updates the Lines property to display them.
        /// If the database context is not set, an error message is set in the status message.
        /// </summary>
        [RelayCommand]
        public void GetItemsFromDatabase()
        {
            if (!CheckContextAndSetStatusMessage())
                return;

            ListType = typeof(Item);
            Lines = [.. _context!.Item.Select(i => (object)i)];
            GeneralListVisible = true;
        }

        /// <summary>
        /// Opens an existing SQLite database file and initializes the context. If the file cannot be opened, an error message is set in the status message.
        /// </summary>
        [RelayCommand]
        public void OpenDatabase()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = Texts.MainView.ViewModel_OpenFileDialog_Title,
                Filter = Texts.MainView.ViewModel_FileDialog_Filter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _context = DatabaseAdministration.OpenSqliteDatabase(openFileDialog.FileName);
                    StatusMessage = Texts.MainView.ViewModel_StatusMessage_FileOpened + $"{openFileDialog.FileName}";
                }
                catch (ArgumentException ex)
                {
                    StatusMessage = Texts.MainView.ViewModel_StatusMessage_FileNotOpened + $"{ex.Message}";
                }
            }
        }

        [RelayCommand]
        public void OpenCard()
        {
            if (ListType.Name == nameof(Item))
            {
                if (GeneralListSelectedIndex >= 0)
                {
                    CurrentItem = (Item)Lines[GeneralListSelectedIndex];
                    GeneralListVisible = false;
                    ItemCardVisible = true;
                }
                else
                {
                    StatusMessage = Texts.MainView.ViewModel_StatusMessage_NoItemSelectedForCard;
                }
            }
            else
            {
                StatusMessage = string.Format(Texts.MainView.ViewModel_StatusMessage_NoCardForListType, ListType.Name);
            }
        }

        [RelayCommand]
        public void ItemCardOk()
        {
            _context!.SaveChanges();
            CloseItemCard();
        }

        [RelayCommand]
        public void ItemCardCancel() => CloseItemCard();

        private void CloseItemCard()
        {
            _context!.Entry(CurrentItem!).Reload();
            CurrentItem = null;
            ItemCardVisible = false;
            GetItemsFromDatabase();
        }

        private bool CheckContextAndSetStatusMessage()
        {
            if (_context == null)
            {
                StatusMessage = Texts.MainView.ViewModel_StatusMessage_NoDatabase;
                return false;
            }

            return true;
        }
    }
}