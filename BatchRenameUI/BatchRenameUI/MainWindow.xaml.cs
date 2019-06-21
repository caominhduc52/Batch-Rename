using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using Path = System.IO.Path;
using System.Collections.ObjectModel;

namespace BatchRenameUI
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

        private void LoadPreset_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Microsoft.Win32.OpenFileDialog();
            screen.ShowDialog();
            string presetfilename = screen.FileName;
            using (StreamReader reader = new StreamReader(presetfilename))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("New Case"))
                    {
                        NewCase action = new NewCase();
                        NewCaseArguments args = new NewCaseArguments();
                        args.NewCaseName = line.Substring("New Case: New Case with ".Length);
                        action.Args = args;
                        ActionListBox.Items.Add(action);
                    }
                    if (line.Contains("Move"))
                    {
                        MoveAction action = new MoveAction();
                        MoveArguments args = new MoveArguments();
                        args.Position = line.Substring("Move: Move ISBN to ".Length);
                        action.Args = args;
                        ActionListBox.Items.Add(action);
                    }
                    if (line.Contains("GUID Action"))
                    {
                        GUID action = new GUID();
                        GUIDArguments args = new GUIDArguments();
                        action.Args = args;
                        ActionListBox.Items.Add(action);
                    }
                    if (line.Contains("Full Name Normalize"))
                    {
                        FullNameNormalizeAction action = new FullNameNormalizeAction();
                        FulNameNormalizeArguments args = new FulNameNormalizeArguments();
                        action.Args = args;
                        ActionListBox.Items.Add(action);
                    }
                    if (line.Contains("Replace action"))
                    {
                        ReplaceAction action = new ReplaceAction();
                        ReplaceActionArguments args = new ReplaceActionArguments();
                        args.Needle = line.Substring("Replace action: Replace ".Length, line.IndexOf(" with") - "Replace action: Replace ".Length);
                        args.Hammer = line.Substring(line.IndexOf("with") + "with".Length + 1);
                        action.Args = args;
                        ActionListBox.Items.Add(action);
                    }
                }
            }
        }

        private void SavePreset_Click(object sender, RoutedEventArgs e)
        {
            if (ActionListBox.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nothing To Save To Preset", "Erro", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else
            {
               using (StreamWriter writer = new StreamWriter("preset.txt"))
                {
                    foreach (StringAction action in ActionListBox.Items)
                    {
                        string actionData = $"{action.name}: {action.Args.Details}";
                        writer.WriteLine(actionData);
                    }
                }
                System.Windows.Forms.MessageBox.Show("All thing done ^_^", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void AddFileButtons_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Microsoft.Win32.OpenFileDialog();
            screen.Multiselect = true;
            if (screen.ShowDialog() == true)
            {
                foreach (var file in screen.FileNames)
                {
                    FileTab.Items.Add(new File()
                    {
                        Filename = System.IO.Path.GetFileName(file),
                        Path = file
                    });
                }
            }
        }
        private void AddFolderButtons_Click(object sender, RoutedEventArgs e)
        {
            string directory;
            var screen = new FolderBrowserDialog();
            if (screen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                directory = screen.SelectedPath;
                string[] subDirectory = Directory.GetDirectories(directory);

                foreach (var dir in subDirectory)
                {
                    FolderTab.Items.Add(new Folder()
                    {
                        Foldername = dir.Substring(directory.Length + 1),
                        Path = dir
                    });
                }
            }
        }

      
        List<StringAction> actions;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            actions = new List<StringAction>
            {
                new NewCase(),
                new ReplaceAction(),
                new MoveAction(),
                new FullNameNormalizeAction(),
                new GUID()
            };
            actionCombobox.ItemsSource = actions;
        }

        private void AddMethodButton_Click(object sender, RoutedEventArgs e)
        {
            var prototype = actionCombobox.SelectedItem as StringAction;
            var instance = prototype.Clone();
            ActionListBox.Items.Add(instance);
        }

        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            var action = ActionListBox.SelectedItem as StringAction;
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
            foreach (FileInfo file in files)
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
            foreach (var dir in src.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void StartBatchButtonButton_Click(object sender, RoutedEventArgs e)
        {
            bool isDup = false;
            //check input from users;
            if (ActionListBox.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Add Method Before Batching!", "Erro Detected in Input", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (FileTab.Items.Count == 0 && FolderTab.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Choose File Or Folder Before Batching!", "Erro Detected in Input", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else
            {
                ObservableCollection<File> FileList = new ObservableCollection<File>();
                ObservableCollection<Folder> FolderList = new ObservableCollection<Folder>();
                //file process
                foreach (File file in FileTab.Items)
                {
                    string result = file.Filename;

                    foreach (StringAction action in ActionListBox.Items)
                    {
                        result = action.Processor.Invoke(result);
                    }

                    var path = Path.GetDirectoryName(file.Path);
                    try
                    {
                        var tempfile = new FileInfo(file.Path);
                        tempfile.MoveTo(path + "\\" + result);
                        file.Newfilename = result;
                    }
                    catch(Exception k)
                    {
                        isDup = true;
                        file.Newfilename = result;
                        file.Erro = "Duplicate";
                        FileList.Add(file);
                    }
                }
                //folder process
                int count = 0;
                foreach (Folder folder in FolderTab.Items)
                {
                    string result = folder.Foldername;

                    foreach (StringAction action in ActionListBox.Items)
                    {
                        result = action.Processor.Invoke(result);
                    }
                    string newfolderpath = Path.GetDirectoryName(folder.Path) + "\\" + result;
                    string tempFolderName = "\\Temp";
                    string tempFolderPath = Path.GetDirectoryName(folder.Path) + tempFolderName;
                    CopyAll(folder.Path, tempFolderPath, true);
                   
                    if (folder.Path.Equals(newfolderpath) == false)
                    {
                        RemoveDirectory(folder.Path);
                        Directory.Delete(folder.Path);
                        try
                        {
                            Directory.Move(tempFolderPath, newfolderpath);
                            folder.Newfolder = result;
                            folder.Erro = "OK";
                        }
                        catch (Exception exception) //exception when folder name is duplicate
                        {
                            string duplicatestore = Path.GetDirectoryName(folder.Path) + "\\Store" + $"{++count}";
                            CopyAll(tempFolderPath, duplicatestore,true);
                            RemoveDirectory(tempFolderPath);
                            Directory.Delete(tempFolderPath);
                            isDup = true;
                            folder.Newfolder = result;
                            folder.Erro = "Duplicate Foldername";
                            FolderList.Add(folder);
                        }
                    } else
                    {
                        RemoveDirectory(tempFolderPath);
                        Directory.Delete(tempFolderPath);
                    }
                }

                if (isDup == true)
                {
                    var dupscreen = new DuplicateProcess(FileList, FolderList);
                    dupscreen.ShowDialog();
                }
                var screen = new preview(FileTab.Items, FolderTab.Items);
                screen.ShowDialog();
                FolderTab.Items.Refresh();
                FileTab.Items.Refresh();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            FileTab.Items.Refresh();
            FolderTab.Items.Refresh();
        }

        private void DeleteButotn_Click(object sender, RoutedEventArgs e)
        {
            ActionListBox.ItemsSource = null;
            ActionListBox.Items.Clear();
            FileTab.ItemsSource = null;
            FileTab.Items.Clear();
            FolderTab.ItemsSource = null;
            FolderTab.Items.Clear();
        }
    }
}
