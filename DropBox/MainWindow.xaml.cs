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
using FileMonitor = DropBox.Services.FileMonitor;

namespace DropBox
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }
        private FileMonitor fileMonitor;

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
            }
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            // Start monitoring the input folder

            if (!ViewModel.IsSyncing)
            {
                ViewModel.IsSyncing = true;
                fileMonitor = new FileMonitor(ViewModel.InputFolder);
                fileMonitor.DirectoryChanged += FileMonitor_DirectoryChanged;
            }
            else
            {
                ViewModel.IsSyncing = false;
                fileMonitor?.StopMonitoring();
            }
        }

        private void FileMonitor_DirectoryChanged(object? sender, Services.DirectoryChangedEventArgs e)
        {
            Console.WriteLine(e.FilePath);
            Console.Beep();
        }
    }
}