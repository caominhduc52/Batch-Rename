using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<StringAction> _protypes = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string origin = "unsplash.com Anatoly - New dawn - 1920x1080.jpg";

            //var action1 = new ReplaceAction()
            //{
            //    Args = new ReplaceArgs()
            //    {
            //        Needle = "unsplash",
            //        Hammer = "newin5days"
            //    }
            //};

            //var action2 = new RemoveAction()
            //{
            //    Args = new RemoveArgs()
            //    {
            //        StartIndex = "newin5days.com".Length,
            //        Count = 10
            //    }
            //};

            //var action3 = new ExtensionAction()
            //{
            //    Args = new ExtensionArgs()
            //    {
            //        NewExtension = "png"
            //    }
            //};

            //var result = action1.Processor.Invoke(origin);
            //result = action2.Processor.Invoke(result);
            //result = action3.Processor.Invoke(result);

            //MessageBox.Show(result);

            _protypes = new List<StringAction>()
            {
                new ReplaceAction(),
                new RemoveAction(),
                new ExtensionAction()
            };

            actionsPrototypeComboBox.ItemsSource = _protypes;
        }

        private void AddAction_Click(object sender, RoutedEventArgs e)
        {
            var prototype = actionsPrototypeComboBox.SelectedItem 
                as StringAction;

            var instance = prototype.Clone();
            actionsListBox.Items.Add(instance);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var action = actionsListBox.SelectedItem as StringAction;

            action.ShowEditDialog();
        }

        private void BatchExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(string filename in filesListView.Items)
            {
                string result = filename;

                foreach (StringAction action in actionsListBox.Items)
                {
                    result = action.Processor.Invoke(result);
                }

                var file = new FileInfo(filename);
                file.MoveTo(result);
                //MessageBox.Show(result);
            }

            MessageBox.Show("All done");
        }

        private void LoadFiles_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Multiselect = true;

            if (screen.ShowDialog() == true)
            {
                foreach(var file in screen.FileNames)
                {
                    filesListView.Items.Add(file);
                }
            }
        }
    }
}
