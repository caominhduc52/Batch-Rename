using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _1753141_1753115_1753133
{
    /// <summary>
    /// Interaction logic for NewCaseDialog.xaml
    /// </summary>
    public partial class NewCaseDialog : Window
    {
        public string NewCaseName;
        public int Choice;
        public NewCaseDialog(NewCaseArguments args)
        {
            InitializeComponent();
            newcaseCombobox.SelectedIndex = args.Choice;
            newcaseCombobox.SelectedValue = args.NewCaseName;
        }

        private void NewcaseCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        BindingList<NewCaseArguments> _choices;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _choices = new BindingList<NewCaseArguments>
            {
                new NewCaseArguments() {_choice = 1, _newCaseName = "All Upper Case"},
                new NewCaseArguments() {_choice = 2, _newCaseName = "All Lower Case"},
                new NewCaseArguments() {_choice = 3, _newCaseName = "String Title Case"},
            };

            newcaseCombobox.ItemsSource = _choices;

            newcaseCombobox.SelectedValuePath = "_newCaseName";
            
            DataContext = this;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            Choice = newcaseCombobox.SelectedIndex;
            NewCaseName = _choices[Choice].NewCaseName;
            MessageBox.Show(NewCaseName);
            this.DialogResult = true;
        }
    }
}
