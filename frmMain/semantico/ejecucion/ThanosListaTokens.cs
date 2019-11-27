using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace frmMain.semantico.ejecucion
{
    public static class ListaThanosTokens
    {
        public static readonly SortedDictionary<string, string> Tokens = new SortedDictionary<string, string>()
        {
            ["COMMENT"] = @"^\/\*[\s\S]*?\*\/",

            ["RESERVED_WORD_PUBLIC"] = @"^public\b",
            ["RESERVED_WORD_CLASS"] = @"^class\b",
            ["RESERVED_WORD_STATIC"] = @"^static\b",
            ["RESERVED_WORD_VOID"] = @"^void\b",
            ["RESERVED_WORD_RETURN"] = @"^return\b",
            ["RESERVED_WORD_NULL"] = @"^null\b",
            ["RESERVED_WORD_TRUE"] = @"^true\b",
            ["RESERVED_WORD_FALSE"] = @"^false\b",

            ["FLOW_CONTROLLER_IF"] = @"^if\b",
            ["FLOW_CONTROLLER_ELSE"] = @"^else\b",
            ["FLOW_CONTROLLER_WHILE"] = @"^while\b",
            ["FLOW_CONTROLLER_FOR"] = @"^for\b",

            ["DATA_TYPE_INT"] = @"^int\b",
            ["DATA_TYPE_FLOAT"] = @"^float\b",
            ["DATA_TYPE_DOUBLE"] = @"^double\b",
            ["DATA_TYPE_BOOLEAN"] = @"^boolean\b",
            ["DATA_TYPE_STRING"] = @"^string\b",

            ["DELIMITER_PARENTHESIS_OPEN"] = @"^\(",
            ["DELIMITER_PARENTHESIS_CLOSE"] = @"^\)",
            ["DELIMITER_BRACKET_OPEN"] = @"^\{",
            ["DELIMITER_BRACKET_CLOSE"] = @"^\}",

            //["MATH_OPERATOR_ADD"] = @"^++\b",
            //["MATH_OPERATOR_SUB"] = @"^--\b",

            ["RELATIONAL_OPERATOR_GREATER_EQUALS"] = @"^>=\b",
            ["RELATIONAL_OPERATOR_LESS_EQULAS"] = @"^<=\b",
            ["RELATIONAL_OPERATOR_EQUALS"] = @"^==\b",
            ["RELATIONAL_OPERATOR_DIFFERENT"] = @"^!=\b",
            ["RELATIONAL_OPERATOR_GREATER"] = @"^\>",
            ["RELATIONAL_OPERATOR_LESS"] = @"^\<",

            ["ARITHMETIC_OPERATOR_ADD"] = @"^\+",
            ["ARITHMETIC_OPERATOR_SUB"] = @"^\-",
            ["ARITHMETIC_OPERATOR_MUL"] = @"^\*",
            ["ARITHMETIC_OPERATOR_DIV"] = @"^\/",
            ["ARITHMETIC_OPERATOR_MOD"] = @"^\%",

            ["ACCESS_OPERATOR_INSTANCE"] = @"^\.",
            ["ACCESS_OPERATOR_INDEX_OPEN"] = @"^\[",
            ["ACCESS_OPERATOR_INDEX_CLOSE"] = @"^\]",

            ["ASSIGNMENT_OPERATOR"] = @"^\=",
            ["COMMA"] = @"^\,",
            ["SEMICOLON"] = @"^\;",
            ["STRING"] = "^\"[^\"]*\"",
            //["MAIN"] = @"^main\b",
            ["NUMBER"] = @"^\d+[f|d]?(\.\d+[f|d]?)?",
            ["IDENTIFIER"] = @"^([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*",

            ["PRINT"] = @"^Console.Write",
            ["PRINTLN"] = @"^Console.WriteLine",

            ["SCAN_INT"] = @"^int.Parse(Console.ReadLine())",
            ["SCAN_FLOAT"] = @"^float.Parse(Console.ReadLine())",
            ["SCAN_DOUBLE"] = @"^double.Parse(Console.ReadLine())",
            ["SCAN_BOOLEAN"] = @"^bool.Parse(Console.ReadLine())",
            ["SCAN_STRING"] = @"^Console.ReadLine()",
        };

        public static int EndOfMatch(string token, string regex)
        {
            Console.WriteLine("================================================================================");
            Console.WriteLine($"Validating '{token}' with regex '{regex}'");

            Match match = Regex.Match(token, regex);

            if (match.Success)
            {
                Console.WriteLine("Match!");
                Console.WriteLine("================================================================================");
                return match.Length;
            }

            Console.WriteLine("No match :(");
            Console.WriteLine("================================================================================");
            return -1;
        }
    }
}
