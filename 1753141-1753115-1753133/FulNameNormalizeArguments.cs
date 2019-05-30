using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1753141_1753115_1753133
{
    public class FulNameNormalizeArguments : StringArguments, INotifyPropertyChanged
    {
        public string Details => "Normalize The Fullname";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
