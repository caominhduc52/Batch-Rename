using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1753141_1753115_1753133
{
    public class MoveArguments : StringArguments, INotifyPropertyChanged
    {
        public int _choose;
        public string _position;

        public int Choose { get => _choose; set
            {
                _choose = value;
            }
        }

        public string Position { get => _position;
            set
            {
                _position = value;
                NotifyChanged("Positon");
                NotifyChanged("Details"); 
            }
        }

        public string Details => $"Move ISBN to {Position}";
        private void NotifyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
