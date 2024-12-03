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

        public MainViewModel()
        {
            Items = new List<string> { "File1", "File2" };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}