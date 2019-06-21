using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;

namespace BatchRenameUI
{
    /// <summary>
    /// Interaction logic for DuplicateProcess.xaml
    /// </summary>
    public partial class DuplicateProcess : Window
    {
        public DuplicateProcess(ObservableCollection<File> duplicateFilesList, ObservableCollection<Folder> duplicateFoldersList)
        {
            InitializeComponent();
            FolderDuplicateTab.Items.Clear();
            FileDuplicateTab.Items.Clear();
            if (duplicateFilesList.Count == 0)
            {
                FileDuplicateTab.ItemsSource = null;
            } else
            {
                FileDuplicateTab.ItemsSource = duplicateFilesList;
            }
            if(duplicateFoldersList.Count == 0)
            {
                FolderDuplicateTab.ItemsSource = null;
            } else
            {
                FolderDuplicateTab.ItemsSource = duplicateFoldersList;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) //Number, Sorry for naming, I'm in a hurry
        {
            if (FileDuplicateTab.Items.Count != 0)
            {
                int count = 0;
                foreach (File file in FileDuplicateTab.ItemsSource)
                {
                    string newfilepath =file.Path;
                    string newfilename = "";
                    while (System.IO.File.Exists(newfilepath))
                    {
                        ++count;
                        newfilename = file.Newfilename.Substring(0,file.Newfilename.IndexOf(".")) + " (" + $"{count}" + ")";
                        newfilepath = System.IO.Path.GetDirectoryName(file.Path) + "'\\" + newfilename + System.IO.Path.GetExtension(file.Path);
                    }
                    file.Newfilename = newfilename;
                    var tempfile = new FileInfo(file.Path);
                    tempfile.MoveTo(System.IO.Path.GetDirectoryName(file.Path) + "\\" + newfilename + System.IO.Path.GetExtension(file.Path));
                }
            }
            if (FolderDuplicateTab.Items.Count != 0)
            {
                int count = 0;
                foreach (Folder folder in FolderDuplicateTab.ItemsSource)
                {
                    string newfolderpath = System.IO.Path.GetDirectoryName(folder.Path) + "\\" + folder.Newfolder;
                    string newfoldername = "";
                    while (System.IO.Directory.Exists(newfolderpath))
                    {
                        ++count;
                        newfoldername = folder.Newfolder + "(" + $"{count}" + ")";
                        newfolderpath = System.IO.Path.GetDirectoryName(folder.Path) + "\\" + newfoldername;
                    }
                    string tempFolderPath = System.IO.Path.GetDirectoryName(folder.Path) + "\\Store" + $"{count}";
                    folder.Newfolder = newfoldername;
                    folder.Erro = "OK";
                    Directory.Move(tempFolderPath, newfolderpath);
                }
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e) //Old name
        {
            if (FileDuplicateTab.Items.Count != 0)
            {
                foreach (File file in FileDuplicateTab.ItemsSource)
                {
                    file.Newfilename = file.Filename;
                    file.Erro = "OK";
                }
                return;
            }
            if (FolderDuplicateTab.Items.Count != 0)
            {
                foreach(Folder folder in FolderDuplicateTab.ItemsSource)
                {
                    folder.Newfolder = folder.Foldername;
                    folder.Erro = "No";
                }
                return;
            }
        }
    }
}
