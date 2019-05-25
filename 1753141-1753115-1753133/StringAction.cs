using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1753141_1753115_1753133
{
    public delegate string StringProcessor(string origin);
    public interface StringAction
    {
        string name { get; }
        StringProcessor Processor { get; }
        StringArguments Args { get; set; }
        StringAction Clone();

        void ShowEditDialog();
    }
}
