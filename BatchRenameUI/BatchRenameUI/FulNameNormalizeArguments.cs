using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameUI
{
    public class FulNameNormalizeArguments : StringArguments, INotifyPropertyChanged
    {
        public string Details => "Normalize The Fullname";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
