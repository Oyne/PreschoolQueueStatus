using DocumentFormat.OpenXml.Office2010.ExcelAc;
using PreschoolStatusDataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Queue
{
    /// <summary>
    /// Interaction logic for RemoveElement.xaml
    /// </summary>
    public partial class RemoveElement : Window
    {
        DataSet dataSet;

        public RemoveElement(DataSet statuses)
        {
            this.dataSet = statuses;
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void RemoveGotFocusEvent(object sender, RoutedEventArgs e)
        {
            IdentifierTextBox.Text = "";
            IdentifierTextBox.GotFocus -= RemoveGotFocusEvent;
        }

        private void Remove(object sender, EventArgs e)
        {
            if (IdentifierTextBox.Text != "Введіть ідентифікатор")
            {
                if (dataSet.GetStatuses.Any(cus => cus.Identifier == int.Parse(IdentifierTextBox.Text.Equals(null) ? "-1" : IdentifierTextBox.Text)))
                {
                    dataSet.GetStatuses.RemoveAll((s) => s.Identifier == int.Parse(IdentifierTextBox.Text));
                    MessageBox.Show("Запис за ідентифікатором " + IdentifierTextBox.Text + " було видалено", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                    MessageBox.Show("Немає такого ідентифікатора", "Помилка видалення", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
