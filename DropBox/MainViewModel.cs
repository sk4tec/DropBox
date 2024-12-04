using System.ComponentModel;

namespace DropBox
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<string> _items;
        private const string _syncingText = "Syncing";
        private const string _notSyncingText = "Not Syncing";
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {
            Items = new List<string> { "File1", "File2" };
            InputFolder = "C:\\Test\\Input\\";
            OutputFolder = "C:\\Test\\Output\\";
        }

        public List<string> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private string _inputFolder;
        public string InputFolder
        {
            get { return _inputFolder; }
            set
            {
                _inputFolder = value;
                OnPropertyChanged(nameof(InputFolder));
            }
        }

        private string _outputFolder;
        public string OutputFolder
        {
            get { return _outputFolder; }
            set
            {
                _outputFolder = value;
                OnPropertyChanged(nameof(OutputFolder));
            }
        }

        private string _buttonText;
        public string ButtonText
        {
            get { return _isSyncing ? _syncingText : _notSyncingText; }
            set 
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private bool _isSyncing = false;
        public bool IsSyncing
        {
            get { return _isSyncing; }
            set
            {
                _isSyncing = value;
                OnPropertyChanged(nameof(IsSyncing));
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}