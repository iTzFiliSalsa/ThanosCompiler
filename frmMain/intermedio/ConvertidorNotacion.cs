using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain.intermedio
{
    public class ConvertidorNotacion
    {
        public static List<string> InfijoPrefijo(List<string> tokensInfijo)
        {
            // Voltear expresion
            var pilaTokens = new Stack<string>();
            tokensInfijo.ForEach(token => pilaTokens.Push(token));

            // Iterar sobre tokens volteados
            var operadores = new Stack<string>();
            var operandos = new Stack<string>();

            while (pilaTokens.Count > 0)
            {
                string tokenActual = pilaTokens.Pop();

                // si es operador
                if (EsOperador(tokenActual))
                {
                    operadores.Push(tokenActual);

                    if (operadores.Contains(Gramatica.Terminales.ParentesisAbrir) && operadores.Contains(Gramatica.Terminales.ParentesisCerrar))
                    {
                        bool encontroAbrir = false;
                        bool encontroCerrar = false;

                        while (!encontroAbrir || !encontroCerrar)
                        {
                            string token = operadores.Pop();

                            if (token.Equals(Gramatica.Terminales.ParentesisAbrir))
                            {
                                encontroAbrir = true;
                                continue;
                            }

                            if (token.Equals(Gramatica.Terminales.ParentesisCerrar))
                            {
                                encontroCerrar = true;
                                continue;
                            }

                            operandos.Push(token);
                        }
                    }
                }
                // si es operando
                else
                {
                    operandos.Push(tokenActual);
                }
            }

            // voltear tokens
            var prefijo = new List<string>();

            while (operandos.Count > 0)
            {
                string token = operandos.Pop();
                prefijo.Add(token);
            }

            return prefijo;
        }

        public static bool EsOperador(string token)
        {
            return
                token.Equals(Gramatica.Terminales.Mas) ||
                token.Equals(Gramatica.Terminales.Menos) ||
                token.Equals(Gramatica.Terminales.Por) ||
                token.Equals(Gramatica.Terminales.Entre) ||
                token.Equals(Gramatica.Terminales.ParentesisAbrir) ||
                token.Equals(Gramatica.Terminales.ParentesisCerrar);
        }

        public static bool EsOperando(string token)
        {
            return !EsOperador(token);
        }

        public static int PrecedenciaDe(string token)
        {
            if (!EsOperador(token))
                return -1;

            if (token.Equals(Gramatica.Terminales.ParentesisAbrir) || token.Equals(Gramatica.Terminales.ParentesisCerrar))
                return 1;

            if (token.Equals(Gramatica.Terminales.Mas) || token.Equals(Gramatica.Terminales.Menos))
                return 2;

            if (token.Equals(Gramatica.Terminales.Por) || token.Equals(Gramatica.Terminales.Entre) || token.Equals(Gramatica.Terminales.Modulo))
                return 3;

            if (token.Equals(Gramatica.Terminales.Potencia))
                return 4;

            return -1;
        }

        public static List<string> TokensDe(string expresion)
        {
            if (expresion == null)
                return new List<string>();

            string[] tokens = expresion.Split(' ');
            var listaTokens = new List<string>(tokens);
            return listaTokens;
        }
    }
}
