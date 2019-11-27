using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain
{
    class Token
    {
        public Token(string name, string lexema, int index, int linea, int columna)
        {
            Name = name;
            Lexema = lexema;
            Index = index;
            Linea = linea;
            Columna = columna;
        }

        public string Name { get; set; }
        public string Lexema { get; private set; }

        public int Index { get; private set; }
        public int Linea { get; private set; }
        public int Columna { get; private set; }

        public int Lenght { get { return Lexema.Length; } }
    }
}
