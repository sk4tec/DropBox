using System.ComponentModel;

namespace DropBox
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<string> _items;
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

        public MainViewModel()
        {
            Items = new List<string> { "File1", "File2" };
            InputFolder = "C:\\TEST\\INPUT\\FOLDER";
            OutputFolder = "C:\\TEST\\OUTPUT\\FOLDER";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}