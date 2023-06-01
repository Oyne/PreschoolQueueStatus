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
    /// Interaction logic for AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        DataSet dataSet;
        Regex dateRegex = new Regex(@"\d{4}-\d{2}-\d{2}");
        Regex genderRegex = new Regex(@"[1, 2]");
        Regex ageGroupRegex = new Regex(@"від \d{1} до \d{1}");
        Regex benefitRegex = new Regex(@"[ні, так]");
        Regex statustRegex = new Regex(@"[Виконана, Чинна]");

        public AddElement(DataSet dataSet)
        {
            InitializeComponent();
            this.dataSet = dataSet;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxGotFocusEvent(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
            ((TextBox)sender).GotFocus -= TextBoxGotFocusEvent;
        }

        private void Add(object sender, EventArgs e)
        {
            PreschoolStatus status = new PreschoolStatus();

            if (IdentifierNumberTextBox.Text == "")
                MessageBox.Show("Введіть ідентифікатор(наприклад 80151)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (DateReceivedTextBox.Text == "")
                MessageBox.Show("Введіть дату подання(формат YY-MM-DD)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (GenderTextBox.Text == "")
                MessageBox.Show("Введіть стать(1 або 2)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (AgeGroupTextBox.Text == "")
                MessageBox.Show("Введіть вікову группу(формат від X до Y)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (InstitutionNameTextBox.Text == "")
                MessageBox.Show("Введіть назву закладу", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (InstitutionIdentifierTextBox.Text == "")
                MessageBox.Show("Введіть ідентифікатор закладу(наприклад 39152477)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (DateProvidedTextBox.Text == "")
                MessageBox.Show("Введіть дату виконання(формат YY-MM-DD)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (StatusTextBox.Text == "")
                MessageBox.Show("Введіть статус заяви(Виконана або Чинна)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (!dateRegex.IsMatch(DateReceivedTextBox.Text))
                    MessageBox.Show("Неправильна дата подання. Введіть дату подання(формат YY-MM-DD)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (!genderRegex.IsMatch(GenderTextBox.Text))
                    MessageBox.Show("Неправильна стать. Введіть стать(1 або 2)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (!ageGroupRegex.IsMatch(AgeGroupTextBox.Text))
                    MessageBox.Show("Неправильна вікова группа. Введіть вікову группу(формат від X до Y)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (!benefitRegex.IsMatch(BenefitTypeTextBox.Text))
                    MessageBox.Show("Неправильна наявність пільг. Введіть наявність пільг(ні або так)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (!dateRegex.IsMatch(DateProvidedTextBox.Text))
                    MessageBox.Show("Неправильна дата виконання. Введіть дату виконання(формат YY-MM-DD)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (!statustRegex.IsMatch(StatusTextBox.Text))
                    MessageBox.Show("Неправильний статус заяви. Введіть статус заяви(Виконана або Чинна)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (dataSet.GetStatuses.Any(cus => cus.Identifier == int.Parse(IdentifierNumberTextBox.Text)))
                    MessageBox.Show("Такий ідентифікатор та номер у черзі вже зайнятий. Введіть інший ідентифікатор з номером у черзі", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    status.Identifier = int.Parse(IdentifierNumberTextBox.Text);
                    status.DateReceived = DateReceivedTextBox.Text;
                    status.Number = int.Parse(IdentifierNumberTextBox.Text);
                    status.Gender = int.Parse(GenderTextBox.Text);
                    status.AgeGroup = AgeGroupTextBox.Text;
                    status.BenefitType = BenefitTypeTextBox.Text;
                    status.InstitutionName = InstitutionNameTextBox.Text;
                    status.InstitutionIdentifier = int.Parse(InstitutionIdentifierTextBox.Text);
                    status.DateProvided = DateProvidedTextBox.Text;
                    status.Status = StatusTextBox.Text;

                    dataSet.GetStatuses.Add(status);
                    MessageBox.Show("Запис було додано", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
        }
    }
}
