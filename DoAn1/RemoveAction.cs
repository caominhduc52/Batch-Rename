using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1
{
    class RemoveAction : StringAction
    {
        public string name => "Remove action";

        public StringProcessor Processor => _remove;

        public StringArgs Args { get ; set ; }

        public StringAction Clone()
        {
            return new RemoveAction()
            {
                Args =  new RemoveActionArgs()
            };
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }

        private string _remove(string origin)
        {
            var myArgs = Args as RemoveActionArgs;
            int start = int.Parse(myArgs.Start);
            int count = int.Parse(myArgs.Count);
            string res;

            res = origin.Remove(start, count);

            return res;
        }
    }
}
