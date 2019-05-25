using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1753141_1753115_1753133
{
    public class NewCase : StringAction
    {
        public string name => "New case";

        public StringProcessor Processor => _newcase;

        public StringArguments Args { get; set; }

        public StringAction Clone()
        {
            return new NewCase()
            {
                Args = new NewCaseArguments()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new NewCaseDialog(Args as NewCaseArguments);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as NewCaseArguments;
                myArgs.Choice = screen.Choice;
                myArgs.NewCaseName = screen.NewCaseName;
            }
        }
        private string _newcase(string origin)
        {
            var myArgs = Args as NewCaseArguments;
            var choice = myArgs.Choice;
            string res = "";

            if (choice == 0)
            {
                res = origin.ToUpper();
            }
            if (choice == 1)
            {
                res = origin.ToLower();
            }
            if (choice == 2)
            {
                res = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(origin);
            }

            return res;
        }
    }
}

