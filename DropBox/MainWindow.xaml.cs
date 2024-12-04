using DropBox.Services;
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
        private Logger logger;

        public MainWindow()
        {
            InitializeComponent();

            logger = new Logger();
            logger.LogCreated += Logger_LogCreated; // Subscribe to the LogCreated event

            ViewModel = new MainViewModel();
            ViewModel.Items = new List<string>();
            this.DataContext = this;
        }

        private void Logger_LogCreated(object? sender, string logEntry)
        {
            // Handle the log entry here
            var lastEntries = ViewModel.Items;
            lastEntries.Add(logEntry);

            ViewModel.Items = new List<string>(lastEntries);
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
                fileMonitor = new FileMonitor(ViewModel.InputFolder, logger);
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