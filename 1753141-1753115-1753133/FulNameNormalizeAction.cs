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
            string token = origin.Substring(path.Length);
            string[] tokens = token.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens == null || tokens.Length == 0)
            {
                return origin;
            }

            string[] pattern = tokens[0].Split(new string[] { " "}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < pattern.Length; i++)
            {
                switch(pattern[i].Length)
                {
                    case 1:
                        break;
                    default:
                        pattern[i] = Char.ToUpper(pattern[i][0]) + pattern[i].Substring(1).ToLower();
                        break;
                }
            }
            for (int i = 0; i < pattern.Length; i++)
            {
                sb.Append(pattern[i]);
                sb.Append(" ");
            }
            
            return String.Concat(path, "\\" + sb.ToString());
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }
    }
}
