using PreschoolStatusDataSet;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Queue
{
    /// <summary>
    /// Interaction logic for AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        DataSet dataSet;

        #region Regex for checking input
        Regex dateRegex = new Regex(@"\d{4}-\d{2}-\d{2}");
        Regex genderRegex = new Regex(@"^[1, 2]$");
        Regex ageGroupRegex = new Regex(@"від \d{1} до \d{1}");
        Regex benefitRegex = new Regex(@"^(ні|так)$");
        Regex statustRegex = new Regex(@"^(Виконана|Чинна)$");
        Regex digitsRegex = new Regex(@"^\d+$");
        #endregion

        public AddElement(DataSet dataSet)
        {
            InitializeComponent();
            this.dataSet = dataSet;
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
        private void TextBoxGotFocusEvent(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
            ((TextBox)sender).GotFocus -= TextBoxGotFocusEvent;
        }

        /// <summary>
        /// Adds element to the List
        /// </summary>
        private void Add(object sender, EventArgs e)
        {
            // New element for list
            PreschoolStatus status = new PreschoolStatus();

            #region Checking that TextBoxes are not empty
            if (IdentifierNumberTextBox.Text == "")
            {
                MessageBox.Show("Введіть ідентифікатор та номер у черзі(наприклад 80151)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                IdentifierNumberTextBox.Text = "Введіть ідентифікатор та номер у черзі(наприклад 80151)";
                IdentifierNumberTextBox.GotFocus += TextBoxGotFocusEvent;
            }

            else if (DateReceivedTextBox.Text == "")
            {
                MessageBox.Show("Введіть дату подання(формат YY-MM-DD)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                DateReceivedTextBox.Text = "Введіть дату подання(формат YY-MM-DD)";
                DateReceivedTextBox.GotFocus += TextBoxGotFocusEvent;
            }

            else if (GenderTextBox.Text == "")
            {
                MessageBox.Show("Введіть стать(1 або 2)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                GenderTextBox.Text = "Введіть стать(1 або 2)";
                GenderTextBox.GotFocus += TextBoxGotFocusEvent;
            }

            else if (AgeGroupTextBox.Text == "")
            {
                MessageBox.Show("Введіть вікову группу(формат від X до Y)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                AgeGroupTextBox.Text = "Введіть вікову группу(формат від X до Y)";
                AgeGroupTextBox.GotFocus += TextBoxGotFocusEvent;
            }


            else if (BenefitTypeTextBox.Text == "")
            {
                MessageBox.Show("Введіть наявність пільг(ні або так)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                BenefitTypeTextBox.Text = "Введіть наявність пільг(ні або так)";
                BenefitTypeTextBox.GotFocus += TextBoxGotFocusEvent;
            }

            else if (InstitutionNameTextBox.Text == "")
            {
                MessageBox.Show("Введіть назву закладу", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                InstitutionNameTextBox.Text = "Введіть назву закладу";
                InstitutionNameTextBox.GotFocus += TextBoxGotFocusEvent;
            }

            else if (InstitutionIdentifierTextBox.Text == "")
            {
                MessageBox.Show("Введіть ідентифікатор закладу(наприклад 39152477)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                InstitutionIdentifierTextBox.Text = "Введіть ідентифікатор закладу(наприклад 39152477)";
                InstitutionIdentifierTextBox.GotFocus += TextBoxGotFocusEvent;
            }

            else if (DateProvidedTextBox.Text == "")
            {
                MessageBox.Show("Введіть дату виконання(формат YY-MM-DD)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                DateProvidedTextBox.Text = "Введіть дату виконання(формат YY-MM-DD)";
                DateProvidedTextBox.GotFocus += TextBoxGotFocusEvent;
            }

            else if (StatusTextBox.Text == "")
            {
                MessageBox.Show("Введіть статус заяви(Виконана або Чинна)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTextBox.Text = "Введіть статус заяви(Виконана або Чинна)";
                StatusTextBox.GotFocus += TextBoxGotFocusEvent;
            }
            #endregion

            else
            {
                #region Checking that all TextBoxes are in needed format
                if (!digitsRegex.IsMatch(IdentifierNumberTextBox.Text))
                    MessageBox.Show("Неправильний ідентифікатор та номер у черзі. Введіть ідентифікатор та номер у черзі(наприклад 80151)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);

                else if (!digitsRegex.IsMatch(InstitutionIdentifierTextBox.Text))
                    MessageBox.Show("Неправильний ідентифікатор закладу. Введіть ідентифікатор закладу(наприклад 39152477)", "Помилка додання", MessageBoxButton.OK, MessageBoxImage.Error);

                else if (!dateRegex.IsMatch(DateReceivedTextBox.Text))
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
                #endregion
                else
                {
                    #region Filling new element that will be added to a list
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
                    #endregion

                    dataSet.GetStatuses.Add(status);
                    MessageBox.Show("Запис було додано", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
        }
    }
}
