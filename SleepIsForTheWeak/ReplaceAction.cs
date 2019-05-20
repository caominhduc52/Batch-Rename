using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class ReplaceArgs: StringArgs, INotifyPropertyChanged
    {
        private string _needle;
        private string _hammer;

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

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public string Details => $"Replace {Needle} with {Hammer}";

        public event PropertyChangedEventHandler PropertyChanged;


    }

    public class ReplaceAction : StringAction
    {
        public string Name => "Replace action";

        public StringProcessor Processor => _replace;

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new ReplaceAction()
            {
                Args = new ReplaceArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new ReplaceArgsDialog(Args as ReplaceArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as ReplaceArgs;
                myArgs.Needle = screen.Needle;
                myArgs.Hammer = screen.Hammer;
            }
        }

        private string _replace (string origin)
        {
            var myArgs = Args as ReplaceArgs;
            var needle = myArgs.Needle;
            var hammer = myArgs.Hammer;

            string result = origin.Replace(needle, hammer);

            return result;
        }
    }
}
