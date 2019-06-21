using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameUI
{
    public class Folder : INotifyPropertyChanged
    {
        private string _folder;
        private string _newfolder;
        private string _path;
        private string _erro;

        public string Foldername
        {
            get => _folder; set
            {
                _folder = value;
                NotifyChanged("Folder");
            }
        }

        public string Newfolder
        {
            get => _newfolder;
            set
            {
                _newfolder = value;
                NotifyChanged("Newfolder");
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                NotifyChanged("Path");
            }
        }

        public string Erro
        {
            get => _erro;
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

        public Folder Clone()
        {
            return new Folder()
            {
                Foldername = this._folder,
                Newfolder = this._newfolder,
                Path = this._path,
                Erro = this._erro
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
