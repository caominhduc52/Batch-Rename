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

namespace BatchRenameUI
{
    /// <summary>
    /// Interaction logic for MoveActionDialog.xaml
    /// </summary>
    public partial class MoveActionDialog : Window
    {
        public string Position;
        public int Choose;
        public MoveActionDialog(MoveArguments args)
        {
            InitializeComponent();
            positionCombobox.SelectedIndex = args.Choose;
            positionCombobox.SelectedValue = args.Position;
        }

        BindingList<MoveArguments> _chooses;
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Choose = positionCombobox.SelectedIndex;
            Position = _chooses[Choose].Position;
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _chooses = new BindingList<MoveArguments>
            {
                new MoveArguments() {_choose = 1, _position = "First"},
                new MoveArguments() {_choose = 2, _position = "Last"},
            };

            positionCombobox.ItemsSource = _chooses;

            positionCombobox.SelectedValuePath = "_position";

            DataContext = this;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
