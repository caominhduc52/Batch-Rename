using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace _1753141_1753115_1753133
{
    internal class FulNameNormalizeAction : StringAction
    {
        public string name => "Full Name Normalize";

        public StringProcessor Processor => _fullnamenormalize;

        public StringArguments Args { get; set; }

        public StringAction Clone()
        {
            return new FulNameNormalizeAction
            {
                Args = new FulNameNormalizeArguments()
            };
        }

        private string _fullnamenormalize(string origin)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string path = Path.GetDirectoryName(origin);
            string filename = Path.GetFileNameWithoutExtension(origin);
            string extension = Path.GetExtension(origin);
            string[] tokens = filename.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens == null || tokens.Length == 0)
            {
                return origin;
            }

            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Substring(0, 1).ToUpper() + tokens[i].Substring(1).ToLower();
            }
            for (int i = 0; i < tokens.Length; i++)
            {
                sb = sb.Append(tokens[i]);
                sb.Append(" ");
            }
            return String.Concat(path, "\\" + sb.ToString(), extension);
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }
    }
}
