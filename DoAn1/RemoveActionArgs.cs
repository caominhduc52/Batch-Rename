using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DoAn1
{
    class RemoveActionArgs : StringArgs, INotifyPropertyChanged
    {
        public string _start;
        public string _count;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Start { get => _start; set
            {
                _start = value;
                NotifyChange(_start);
                NotifyChange(Details);
            }
        }

        public string Count { get => _count; set
            {
                _count = value;
                NotifyChange(_count);
                NotifyChange(Details);
            }
        }



        private void NotifyChange(string num)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs($"{num}"));
            }
        }

        public string Details => $"Remove {Count} index from {Start}";
    }
}
