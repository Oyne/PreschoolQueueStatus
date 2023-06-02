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

        /// <summary>
        /// Opens Author window
        /// </summary>
        private void AuthorOpen(object sender, RoutedEventArgs e)
        {
            Author author = new();
            author.Show();
        }

        /// <summary>
        /// Opens RemoveElement window
        /// </summary>
        private void RemoveElementOpen(object sender, RoutedEventArgs e)
        {
            RemoveElement remEl = new(dataSet);
            remEl.Show();

            StatusGrid.ItemsSource = null ;
            StatusGrid.ItemsSource = dataSet.GetStatuses;
        }

        /// <summary>
        /// Opens AddElement window
        /// </summary>
        private void AddElementOpen(object sender, RoutedEventArgs e)
        {
            AddElement addEl = new(dataSet);
            addEl.Show();

            StatusGrid.ItemsSource = null;
            StatusGrid.ItemsSource = dataSet.GetStatuses;
        }

        /// <summary>
        /// Open file with data set
        /// </summary>
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


                StatusGrid.ItemsSource = null;
                StatusGrid.ItemsSource = dataSet.GetStatuses;
            }
        }

        /// <summary>
        /// Save data set in last used file
        /// </summary>
        public void FileSave(object sender, EventArgs e)
        {
            dataSet.ExportDataSet(filename);

        }

        /// <summary>
        /// Save data set as file
        /// </summary>
        public void FileSaveAs(object sender, EventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Workbook"; // Default file name
            dialog.DefaultExt = ".xlsx"; // Default file extension
            dialog.Filter = "Excel Workbook (.xlsx)|*.xlsx"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Save document
                filename = dialog.FileName;
                dataSet.ExportDataSet(filename);
            }
        }

        /// <summary>
        /// Validates input for having only digits
        /// </summary>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Removes text from TextBox when first focused
        /// </summary>
        private void SearchGotFocusEvent(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            SearchTextBox.GotFocus -= SearchGotFocusEvent;
        }

        /// <summary>
        /// Search for element with needed identifier
        /// </summary>
        private void Search(object sender, EventArgs e)
        {
            // Reseting if search field is empty
            if (SearchTextBox.Text == "") Reset(sender, e);
            else
            {
                //Getting elements with parts or full identifier
                var filtered = dataSet.GetStatuses.Where(status => status.Identifier.ToString().StartsWith(SearchTextBox.Text));

                StatusGrid.ItemsSource = filtered;
            }
        }

        /// <summary>
        /// Resets search and DataGrid
        /// </summary>
        private void Reset(object sender, EventArgs e)
        {
            SearchTextBox.Text = "Введіть ідентифікатор";
            SearchTextBox.GotFocus += SearchGotFocusEvent;

            StatusGrid.ItemsSource = dataSet.GetStatuses;
        }
    }
}
