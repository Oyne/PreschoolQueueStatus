using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using PreschoolStatusDataSet;

namespace Queue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet dataSet = new DataSet();
        string filename;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AuthorOpen(object sender, RoutedEventArgs e)
        {
            Author author = new();
            author.Show();
        }

        public void FileOpen(object sender, EventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Workbook"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Excel Workbook (.xlsx)|*.xlsx"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;
                dataSet.ImportDataSet(filename);

                StatusGrid.ItemsSource = dataSet.GetStatuses;
            }
        }

        public void FileSave(object sender, EventArgs e)
        {
            dataSet.ExportDataSet(filename);

        }

        public void FileSaveAs(object sender, EventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Workbook"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Excel Workbook (.xlsx)|*.xlsx"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;
                dataSet.ExportDataSet(filename);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void SearchGotFocusEvent(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            SearchTextBox.GotFocus -= SearchGotFocusEvent;
        }

        private void Search(object sender, EventArgs e)
        {
            if (SearchTextBox.Text == "") Reset(sender, e);
            else
            {
                var filtered = dataSet.GetStatuses.Where(status => status.Identifier.ToString().StartsWith(SearchTextBox.Text));

                StatusGrid.ItemsSource = filtered;
            }
        }

        private void Reset(object sender, EventArgs e)
        {
            SearchTextBox.Text = "Введіть ідентифікатор";
            SearchTextBox.GotFocus += SearchGotFocusEvent;

            StatusGrid.ItemsSource = dataSet.GetStatuses;
        }
    }
}
