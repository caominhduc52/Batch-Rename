using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1753141_1753115_1753133
{
    class MoveAction : StringAction
    {
        public string name => "Move";

        public StringProcessor Processor => _move;


        public StringArguments Args { get ; set ; }

        public StringAction Clone()
        {
            return new MoveAction()
            {
                Args = new MoveArguments()
            };
        }

        private string _move(string origin)
        {
            var myArgs = Args as MoveArguments;
            var choose = myArgs.Choose;

            string tmp = "";
            string pattern = @"[+\d-]+";

            string path = Path.GetDirectoryName(origin);
            string filename = Path.GetFileNameWithoutExtension(origin);
            string extension = Path.GetExtension(origin);
            

            Regex regex = new Regex(pattern);
            Match match = regex.Match(origin);
            if (match.Success)
            {
                tmp = match.Value;
            }
            
            if (choose == 0)
            {
                 //caominhduc 99921-58-10-7 A new way to tackle stress, John Smith
                return String.Concat(path,"\\" + tmp, " " + filename.Remove(filename.IndexOf(tmp), tmp.Length) + extension);
             
            }
            if (choose == 1)
            {
               
                return String.Concat(path,"\\" + filename.Remove(0, tmp.Length), " " + tmp + extension);
            }
            return origin;
        }
        public void ShowEditDialog()
        {
            var screen = new MoveActionDialog(Args as MoveArguments);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as MoveArguments;
                myArgs.Choose = screen.Choose;
                myArgs.Position = screen.Position;
            }
        }
    }
}
