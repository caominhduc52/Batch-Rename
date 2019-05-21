using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1
{
    class ReplaceAction : StringAction
    {
        public string name => "Replace action";

        public StringProcessor Processor => _replace;

        public StringArgs Args { get ; set ; }

        public StringAction Clone()
        {
            return new ReplaceAction()
            {
                Args = new ReplaceActionArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new ReplaceArgs(Args as ReplaceActionArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as ReplaceActionArgs;
                myArgs.Needle = screen.Needle;
                myArgs.Hammer = screen.Hammer;
            }
        }

        private string _replace(string origin)
        {
            var myArgs = Args as ReplaceActionArgs;
            var needle = myArgs.Needle;
            var hammer = myArgs.Hammer;

            string res = origin.Replace(needle, hammer);

            return res;
        }
    }
}
