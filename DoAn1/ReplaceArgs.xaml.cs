using System;
using System.Collections.Generic;
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

namespace DoAn1
{
    /// <summary>
    /// Interaction logic for ReplaceArgs.xaml
    /// </summary>
    public partial class ReplaceArgs : Window
    {
        public string Needle;
        public string Hammer;
        public ReplaceArgs(ReplaceActionArgs args)
        {
            InitializeComponent();
            needleTextBox.Text = args.Needle;
            hammerTextBox.Text = args.Hammer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Needle = needleTextBox.Text;
            Hammer = hammerTextBox.Text;

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
