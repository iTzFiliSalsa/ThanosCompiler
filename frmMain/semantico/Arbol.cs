using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain.semantico
{
    public class Arbol
    {
        private readonly ParseTree arbol;

        public Arbol(ParseTree arbol)
        {
            this.arbol = arbol;
        }

        public bool EsHoja(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes == null)
                return true;

            return nodo.ChildNodes.Count == 0;
        }

        /// <summary>
        /// Obtener las hojas de la raiz especificada
        /// </summary>
        /// <param name="raiz"></param>
        /// <returns></returns>
        public List<ParseTreeNode> HojasDe(ParseTreeNode raiz)
        {
            var nodos = new List<ParseTreeNode>();
            HojasDe(raiz, nodos);
            return nodos;
        }

        private void HojasDe(ParseTreeNode raiz, List<ParseTreeNode> nodos)
        {
            if (EsHoja(raiz))
                nodos.Add(raiz);

            raiz.ChildNodes.ForEach(nodo =>
            {
                HojasDe(nodo, nodos);
            });
        }

        public string ImprimirNodo(ParseTreeNode nodo)
        {
            var sb = new StringBuilder();

            sb
            .Append("Tag                   | ").Append(nodo.Tag).Append('\n')
            .Append("Term                  | ").Append(nodo.Term).Append('\n')
            .Append("Token                 | ").Append(nodo.Token).Append('\n')
            .Append("FindToken()           | ").Append(nodo.FindToken()).Append('\n')
            .Append("FindTokenAndGetText() | ").Append(nodo.FindTokenAndGetText()).Append('\n')
            .Append("ToString()            | ").Append(nodo.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// Obtener todos los nodos del arbol
        /// </summary>
        /// <returns></returns>
        public List<ParseTreeNode> Recorrer()
        {
            var nodos = new List<ParseTreeNode>();
            Recorrer(arbol.Root, nodos);
            return nodos;
        }

        /// <summary>
        /// Obtener todos los nodos del arbol recorriendo desde la raiz especificada
        /// </summary>
        /// <param name="raiz"></param>
        /// <returns></returns>
        public List<ParseTreeNode> Recorrer(ParseTreeNode raiz)
        {
            var nodos = new List<ParseTreeNode>();
            Recorrer(raiz, nodos);
            return nodos;
        }

        private void Recorrer(ParseTreeNode raiz, List<ParseTreeNode> nodos)
        {
            nodos.Add(raiz);
            raiz.ChildNodes.ForEach(nodo =>
            {
                Recorrer(nodo, nodos);
            });
        }

        /// <summary>
        /// Obtener todos los nodos que conforman una el lado derecho de una asignacion (despues del '=')
        /// </summary>
        /// <param name="raiz"></param>
        /// <returns></returns>
        public List<ParseTreeNode> RecorrerAsignables(ParseTreeNode raiz)
        {
            var nodos = new List<ParseTreeNode>();
            RecorrerAsignables(raiz, nodos);
            return nodos;
        }

        private void RecorrerAsignables(ParseTreeNode raiz, List<ParseTreeNode> nodos)
        {
            nodos.Add(raiz);
            raiz.ChildNodes.ForEach(nodo =>
            {
                RecorrerAsignables(nodo, nodos);
            });
        }

        /// <summary>
        /// Obtener todos los nodos que pertenezcan al termino especificado
        /// </summary>
        /// <param name="termino"></param>
        /// <returns></returns>
        public List<ParseTreeNode> Recorrer(string termino)
        {
            List<ParseTreeNode> nodos =
                Recorrer()
                .FindAll(nodo => nodo.Term.ToString().Equals(termino));

            return nodos;
        }

        /// <summary>
        /// Obtener todos los nodos desde recorriendo desde la raiz especificada y que pertenezcan al termino especificado
        /// </summary>
        /// <param name="raiz"></param>
        /// <param name="termino"></param>
        /// <returns></returns>
        public List<ParseTreeNode> Recorrer(ParseTreeNode raiz, string termino)
        {
            List<ParseTreeNode> nodos =
                Recorrer(raiz)
                .FindAll(nodo => nodo.Term.ToString().Equals(termino));

            return nodos;
        }

        public TokenList Tokens()
        {
            return arbol.Tokens;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            Recorrer().ForEach(node =>
            {
                sb.Append(node.ToString()).Append("\n");
            });

            return sb.ToString();
        }
    }
}
