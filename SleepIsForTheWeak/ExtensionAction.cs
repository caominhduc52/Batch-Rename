using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class ExtensionArgs: StringArgs {
        public string NewExtension { get; set; }

        public string Details => $"Change extension to {NewExtension}";
    }

    class ExtensionAction : StringAction
    {
        public string Name => "New extension";

        public StringProcessor Processor => _changeExtension;
        
        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new ExtensionAction()
            {
                Args = new ExtensionArgs()
            };
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }

        private string _changeExtension(string origin)
        {
            var myArgs = Args as ExtensionArgs;
            var newExtension = myArgs.NewExtension;

            var foundPos = origin.LastIndexOf(".");
            var beginning = origin.Substring(0, foundPos);

            
            return $"{beginning}.{newExtension}";
        }
    }
}
