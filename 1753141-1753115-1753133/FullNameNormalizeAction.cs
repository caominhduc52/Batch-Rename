using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1753141_1753115_1753133
{
    public class FullNameNormalizeAction : StringAction
    {
        public string name => "Full Name Normalize";

        public StringProcessor Processor => _fullnamenormalize;

        public StringArguments Args { get ; set ; }

        public StringAction Clone()
        {
            return new FullNameNormalizeAction()
            {
                Args = new FullNameNormalizeAction()
            };
        }

        private string _fullnamenormalize(string origin)
        {
            string res;
            res = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(origin);
            return res;
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }
    }
}
