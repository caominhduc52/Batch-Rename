using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic.FileIO;

namespace _1753141_1753115_1753133
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
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var prototype = actionCombobox.SelectedItem as StringAction;

            var instance = prototype.Clone();
            actionListBox.Items.Add(instance);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            actions = new List<StringAction>
            {
                new ReplaceAction(),
                new NewCase(),
                new FulNameNormalizeAction(),
                new MoveAction()
            };

            actionCombobox.ItemsSource = actions;

        }

        private void LoadFiles_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Microsoft.Win32.OpenFileDialog();
            screen.Multiselect = true;

            if (screen.ShowDialog() == true)
            {
                foreach (var file in screen.FileNames)
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
                string[] subDirectory = Directory.GetDirectories(directory);

                foreach (var dir in subDirectory)
                {
                    foldersListView.Items.Add(dir);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var action = actionListBox.SelectedItem as StringAction;

            action.ShowEditDialog();
        }

        public static void CopyAll(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            if (!dir.Exists)
            {
                System.Windows.MessageBox.Show("Source Directory does not exist or could not be found !");
            }

            if (!Directory.Exists(destDirName))
            {
                DirectoryInfo tempFolder = Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach(FileInfo file in files)
            {
                string temppath = System.IO.Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    CopyAll(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static void RemoveDirectory(string sourcePath)
        {
            DirectoryInfo src = new DirectoryInfo(sourcePath);

            foreach (var file in src.GetFiles())
            {
                file.Delete();
            }
            foreach(var dir in src.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        private void startbatchButton_Click(object sender, RoutedEventArgs e) 
        {
            foreach (string filename in filesListView.Items)
            {
                string result = filename;

                foreach (StringAction action in actionListBox.Items)
                {
                    result = action.Processor.Invoke(result);
                }

                var file = new FileInfo(filename);
                file.MoveTo(result);
            }

            foreach (string foldername in foldersListView.Items)
            {
                string result = foldername;

                foreach (StringAction action in actionListBox.Items)
                {
                    result = action.Processor.Invoke(result);
                }

                string tempFolderName = "\\Temp";
                string tempFolderPath = System.IO.Path.GetDirectoryName(foldername) + tempFolderName;
                CopyAll(foldername, tempFolderPath, true);

                if (foldername.Equals(result) == false)
                {
                    RemoveDirectory(foldername);
                    Directory.Delete(foldername);
                    Directory.Move(tempFolderPath, result);
                    
                }
                
            }

            System.Windows.MessageBox.Show("All done");
        }

    }
}
