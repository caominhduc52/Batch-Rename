using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameUI
{
    public class File : INotifyPropertyChanged
    {
        private string _filename;
        private string _newfilename;
        private string _path;
        private string _erro;

        public string Filename { get => _filename; set
            {
                _filename = value;
                NotifyChanged("Filename");
            }
        }

        public string Newfilename { get => _newfilename;
            set
            {
                _newfilename = value;
                NotifyChanged("Newfilename");
            }
        }

        public string Path { get => _path;
            set
            {
                _path = value;
                NotifyChanged("Path");
            }
        }

        public string Erro { get => _erro;
            set
            {
                _erro = value;
                NotifyChanged("Erro");
            }
        }

        private void NotifyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public File Clone()
        {
            return new File()
            {
                Filename = this._filename,
                Newfilename = this._newfilename,
                Path = this._path,
                Erro = this._erro
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
