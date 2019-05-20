using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class RemoveArgs: StringArgs
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }

        public string Details => $"Remove {Count} characters from index: {StartIndex}";
    }

    public class RemoveAction : StringAction
    {
        public string Name => "Remover";

        public StringProcessor Processor => _remove;
        
        public StringArgs Args { get ; set ; }

        public StringAction Clone()
        {
            return new RemoveAction()
            {
                Args = new RemoveArgs()
            };
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }

        private string _remove(string origin)
        {
            var myArgs = Args as RemoveArgs;
            var startIndex = myArgs.StartIndex;
            var count = myArgs.Count;

            return origin.Remove(startIndex, count);
        }
    }
}
