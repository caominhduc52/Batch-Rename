using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;

namespace DoAn1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<StringAction> actions;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            actions = new List<StringAction>
            {
                new ReplaceAction(),
                new RemoveAction()
            };

            actionCombobox.ItemsSource = actions;
           
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var action = actionListBox.SelectedItem as StringAction;

            action.ShowEditDialog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var prototype = actionCombobox.SelectedItem as StringAction;

            var instance = prototype.Clone();
            actionListBox.Items.Add(instance);
        }

        private void LoadFiles_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Microsoft.Win32.OpenFileDialog();
            screen.Multiselect = true;

            if (screen.ShowDialog() == true)
            {
                foreach(var file in screen.FileNames)
                {
                    filesListView.Items.Add(file);
                }
            }
        }

        private void LoadFolders_Click(object sender, RoutedEventArgs e)
        {
            string directory;
            var screen = new FolderBrowserDialog();
            screen.ShowDialog();
            
            if (screen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                directory = screen.SelectedPath;
                DirectoryInfo d = new DirectoryInfo(directory);
                DirectoryInfo[] dirs = d.GetDirectories();

                foreach (var dir in dirs)
                {
                    foldersListView.Items.Add(dir);
                }
            }
        }
    }
}
