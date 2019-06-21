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

namespace BatchRenameUI
{
    /// <summary>
    /// Interaction logic for preview.xaml
    /// </summary>
    public partial class preview : Window
    {
        public preview(ItemCollection FileItem, ItemCollection FolderItem)
        {
            InitializeComponent();
            FilePreviewTab.ItemsSource = FileItem;
            FolderPreviewTab.ItemsSource = FolderItem;
        }
    }
}
