using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DropBox
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
            ViewModel.Items = new List<string> { "One", "Two" };
            this.DataContext = this;
        }

        private void OpenFileDialogButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a File",
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };

            // Show the dialog
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected file path
                string filePath = openFileDialog.FileName;
                MessageBox.Show($"Selected file: {filePath}", "File Selected");
            }
        }
    }
}