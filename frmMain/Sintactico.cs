using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace frmMain
{
    class Sintactico : Grammar
    {
        public static bool analizar(string cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;
            if (raiz == null)
                return false;
            return true;
        }

        public static ParseTreeNode analizarArbol(string cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;
            return arbol.Root;
        }

        public static ParseTree AnalisisSemantico(string entrada)
        {
            var gramatica = new Gramatica();
            var sintactico = new Parser(gramatica);

            return sintactico.Parse(entrada);
        }
    }
}
