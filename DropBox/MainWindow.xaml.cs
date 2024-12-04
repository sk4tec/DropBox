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
        private FileSupport fileSupport;
        private Logger logger;

        public MainWindow()
        {
            InitializeComponent();

            logger = new Logger();
            logger.LogCreated += Logger_LogCreated; // Subscribe to the LogCreated event

            fileSupport = new FileSupport(logger);

            ViewModel = new MainViewModel();
            ViewModel.Items = new List<string>();

            fileMonitor = new FileMonitor(ViewModel.InputFolder, ViewModel.OutputFolder, logger, fileSupport);

            this.DataContext = this;
        }

        private void Logger_LogCreated(object? sender, string logEntry)
        {
            // Handle the log entry here
            var lastEntries = ViewModel.Items;
            lastEntries.Add(logEntry);

            ViewModel.Items = new List<string>(lastEntries);
        }

        private void OpenFolderDialog(string title, Action<string> onFolderSelected)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog
            {
                Title = title
            };

            if (openFolderDialog.ShowDialog() == true)
            {
                onFolderSelected(openFolderDialog.FolderName);
            }
        }

        private void OpenFolderDialogButtonInput_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog("Select a Folder", folderName =>
            {
                fileMonitor.pathToMonitor = folderName;
                ViewModel.InputFolder = folderName;
            });
        }

        private void OpenFolderDialogButtonOutput_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog("Select a Folder", folderName =>
            {
                fileMonitor.pathTarget = folderName;
                ViewModel.OutputFolder = folderName;
            });
        }


        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            // Start monitoring the input folder

            if (!ViewModel.IsSyncing)
            {
                ViewModel.IsSyncing = true;
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