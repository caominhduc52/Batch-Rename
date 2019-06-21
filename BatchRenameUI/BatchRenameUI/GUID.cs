using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameUI
{
    public class GUID : StringAction
    {
        public string name => "GUID Action";

        public StringProcessor Processor => _guid;

        public StringArguments Args { get; set; }

        public StringAction Clone()
        {
            return new GUID()
            {
                Args = new GUIDArguments()
            };
        }

        private string _guid(string origin)
        {
            Guid myGuid;
            string result = "";
            string extension = Path.GetExtension(origin);
            myGuid = Guid.NewGuid();
            result = myGuid.ToString() + extension;
            return result;
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }
    }
}
