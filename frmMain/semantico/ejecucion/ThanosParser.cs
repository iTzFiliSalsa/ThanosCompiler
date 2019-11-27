using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain.semantico.ejecucion
{
    public class ThanosParser
    {
        public static readonly Dictionary<string, string> ParseTokens = new Dictionary<string, string>()
        {
            ["Main"] = "ThanosProgram",
            ["main"] = "Main",
            ["print"] = "Console.Write",
            ["println"] = "Console.WriteLine",
            ["readInt"] = "int.Parse(Console.ReadLine())",
            ["readFloat"] = "float.Parse(Console.ReadLine())",
            ["readDouble"] = "double.Parse(Console.ReadLine())",
            ["readBoolean"] = "bool.Parse(Console.ReadLine())",
            ["read"] = "Console.ReadLine",
        };

        public List<ThanosToken> NeoSourceCode { get; }

        public ThanosParser(List<ThanosToken> neoSourceCode)
        {
            
            NeoSourceCode = neoSourceCode;
        }

        public List<string> ParseToTokenList()
        {
            var tokens = new List<string>()
            {
                "using", "System", ";",
            };

            for (int i = 0; i < NeoSourceCode.Count; i++)
            {
                string currentToken = NeoSourceCode[i].NombreToken;

                if (ParseTokens.ContainsKey(currentToken))
                    currentToken = ParseTokens[currentToken];

                tokens.Add(currentToken);
            }

            return tokens;
        }

        public string ParseToSourceCode()
        {
            var sb = new StringBuilder();

            List<string> tokens = ParseToTokenList();

            for (int i = 0; i < tokens.Count; i++)
            {
                string currentToken = tokens[i];
                string nextToken = "";

                if (i != tokens.Count - 1)
                    nextToken = tokens[i + 1];

                sb.Append(currentToken);

                // check for <=, >=, ==, !=
                if (currentToken == "<" && nextToken == "=") { }

                else if (currentToken == ">" && nextToken == "=") { }

                else if (currentToken == "=" && nextToken == "=") { }

                else if (currentToken == "!" && nextToken == "=") { }

                // check for {, }, ;
                else if (currentToken == "{" || currentToken == "}" || currentToken == ";")
                    sb.Append("\n");

                // check for read
                else if (
                    currentToken == "int.Parse(Console.ReadLine())" ||
                    currentToken == "float.Parse(Console.ReadLine())" ||
                    currentToken == "double.Parse(Console.ReadLine())" ||
                    currentToken == "bool.Parse(Console.ReadLine())")
                {
                    i += 2;
                }

                else
                {
                    sb.Append(" ");
                }
                /**
                 * ["readInt"] = "int.Parse(Console.ReadLine())",
            ["readFloat"] = "float.Parse(Console.ReadLine())",
            ["readDouble"] = "double.Parse(Console.ReadLine())",
            ["readBoolean"] = "bool.Parse(Console.ReadLine())",
            ["read"] = "Console.ReadLine",
                 */
            }

            return sb.ToString();
        }
    }
}
