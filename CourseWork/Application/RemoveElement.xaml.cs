using PreschoolStatusDataSet;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

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
        private void RemoveGotFocusEvent(object sender, RoutedEventArgs e)
        {
            IdentifierTextBox.Text = "";
            IdentifierTextBox.GotFocus -= RemoveGotFocusEvent;
        }

        /// <summary>
        /// Removes element from the List
        /// </summary>
        private void Remove(object sender, EventArgs e)
        {

            //Checking that TextBoxes not have its default value
            if (IdentifierTextBox.Text != "Введіть ідентифікатор")
            {
                //Checking that element with needed identifier exists in the list
                if (dataSet.GetStatuses.Any(s => s.Identifier == int.Parse(IdentifierTextBox.Text)))
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
