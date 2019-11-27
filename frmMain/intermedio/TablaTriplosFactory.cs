using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain.intermedio
{
    public class TablaTriplosFactory
    {
        private static int idContador = -1;
        private static int IdContador
        {
            get {
                idContador++;
                return idContador;
            }
        }

        public static TablaTriplos DeExpresionPrefija(List<string> prefijo)
        {
            var tabla = new TablaTriplos();

            // (2 + 4) * 1
            // * + 2 4 1
            //

            /* 2 */
            // (1 + 4)
            // t1 + 2 * t0

            // voltear tokens
            var pilaTokens = new Stack<string>();
            prefijo.ForEach(token => pilaTokens.Push(token));

            // iterar sobre tokens
            var operandos = new Stack<string>();

            while (pilaTokens.Count > 0)
            {
                string tokenActual = pilaTokens.Pop();

                // si el token actual es operando
                if (ConvertidorNotacion.EsOperando(tokenActual))
                    operandos.Push(tokenActual);

                // si el token actual es operador
                else
                {
                    string operando1 = operandos.Pop();
                    string operando2 = operandos.Pop();

                    Triplo triplo1 = DeExpresion(operando1);
                    Triplo triplo2 = DeExpresion(operando2);

                    if (triplo1 == null && triplo2 == null)
                    {
                        tabla.Agregar("t" + IdContador, new Triplo(tokenActual, operando1, operando2));

                        string nuevoOperando = $"( {operando1} {tokenActual} {operando2} )";
                        operandos.Push(nuevoOperando);
                    }
                    else if (triplo1 != null && triplo2 != null)
                    {
                        string id1 = tabla.EncontrarId(triplo1);
                        string id2 = tabla.EncontrarId(triplo2);
                        tabla.Agregar("t" + IdContador, new Triplo(tokenActual, id1, id2));

                        string nuevoOperando = $"( {id1} {tokenActual} {id2} )";
                        operandos.Push(nuevoOperando);
                    }
                    else if (triplo1 != null && triplo2 == null)
                    {
                        string id1 = tabla.EncontrarId(triplo1);
                        tabla.Agregar("t" + IdContador, new Triplo(tokenActual, id1, operando2));

                        string nuevoOperando = $"( {id1} {tokenActual} {operando2} )";
                        operandos.Push(nuevoOperando);
                    }
                    else if (triplo1 == null && triplo2 != null)
                    {
                        string id2 = tabla.EncontrarId(triplo2);
                        tabla.Agregar("t" + IdContador, new Triplo(tokenActual, operando1, id2));

                        string nuevoOperando = $"( {operando1} {tokenActual} {id2} )";
                        operandos.Push(nuevoOperando);
                    }
                }
            }

            return tabla;
        }

        private static Triplo DeExpresion(string expresion)
        {
            try
            {
                string[] tokens = expresion.Split(' ');

                // ( A + B )
                string operador = tokens[2];
                string operando1 = tokens[1];
                string operando2 = tokens[3];
                return new Triplo(operador, operando1, operando2);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void ReiniciarContador()
        {
            idContador = -1;
        }
    }
}
