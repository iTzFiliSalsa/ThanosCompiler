using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain.semantico.ejecucion
{
    public class ThanosToken
    {
        public readonly string Lexeme;
        public readonly string NombreToken;

        public ThanosToken(string lexeme, string nombreToken)
        {
            Lexeme = lexeme;
            NombreToken = nombreToken;
        }

        public override string ToString()
        {
            return $"Token({Lexeme}, {NombreToken})";
        }
    }
}
