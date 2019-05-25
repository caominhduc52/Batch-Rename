using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1753141_1753115_1753133
{
    public class NewCaseArguments : StringArguments, INotifyPropertyChanged
    {
        public int _choice;
        public string _newCaseName;
        public int Choice { get => _choice; set
            {
                _choice = value;
               
            }
        }

        public string NewCaseName { get => _newCaseName; set
            {
                _newCaseName = value;
                NotifyChanged("NewCaseName");
                NotifyChanged("Details");
            }
        }

        public string Details => $"New Case with {NewCaseName}";

        public void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
