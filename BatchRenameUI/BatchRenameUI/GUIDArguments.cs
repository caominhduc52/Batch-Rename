using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameUI
{
    class GUIDArguments : StringArguments, INotifyPropertyChanged
    {
        public string Details => "Unique name to save in DB";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
