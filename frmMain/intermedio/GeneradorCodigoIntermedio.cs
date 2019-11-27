using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain.intermedio
{
    class GeneradorCodigoIntermedio
    {
        public TablaTriplos GenerarTriplos(Simbolo simbolo)
        {
            string expresion = simbolo.Valor;
            List<string> infijo = ConvertidorNotacion.TokensDe(expresion);
            List<string> prefijo = ConvertidorNotacion.InfijoPrefijo(infijo);

            TablaTriplos tablaTriplos = TablaTriplosFactory.DeExpresionPrefija(prefijo);

            return tablaTriplos;
        }

        public List<TablaTriplos> GenerarTriplos(TablaSimbolos tablaSimbolos)
        {
            var tablasTriplos = new List<TablaTriplos>();

            foreach (Simbolo simbolo in tablaSimbolos.Simbolos)
            {
                TablaTriplos triplos = GenerarTriplos(simbolo);
                tablasTriplos.Add(triplos);
            }

            return tablasTriplos;
        }

        public List<string> GenerarCodigo(TablaTriplos triplos)
        {
            var lineasCodigo = new List<string>();

            foreach (var i in triplos.Triplos)
            {
                string id = i.Key;
                Triplo triplo = i.Value;

                lineasCodigo.Add($"{triplo.TipoDeTriplo()} {id} = {triplo.Operando1} {triplo.Operador} {triplo.Operando2};");
            }

            return lineasCodigo;
        }

        public List<List<string>> GenerarCodigo(List<TablaTriplos> tablasTriplos)
        {
            var bloquesCodigo = new List<List<string>>();

            foreach (TablaTriplos triplos in tablasTriplos)
            {
                List<string> codigo = GenerarCodigo(triplos);
                bloquesCodigo.Add(codigo);
            }

            return bloquesCodigo;
        }
    }
}
