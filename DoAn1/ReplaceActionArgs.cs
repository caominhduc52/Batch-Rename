using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DoAn1
{
    public class ReplaceActionArgs : StringArgs, INotifyPropertyChanged
    {
        public string _needle;
        public string _hammer;

        public string Needle { get => _needle; set
            {
                _needle = value;
                NotifyChange("Needle");
                NotifyChange("Details");
            }
        }

        public string Hammer { get => _hammer; set
            {
                _hammer = value;
                NotifyChange("Hammer");
                NotifyChange("Details");
            }
        }
        public string Details => $"Replace {Needle} with {Hammer}";

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }
    }
}
