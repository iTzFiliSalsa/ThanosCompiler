using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain
{
    public class TablaSimbolos
    {
        public List<Simbolo> Simbolos = new List<Simbolo>();

        public TablaSimbolos() { }

        public void AgregarSimbolo(Simbolo simbolo)
        {
            Simbolos.Add(simbolo);
        }

        public void AgregarSimbolos(List<Simbolo> simbolos)
        {
            Simbolos.AddRange(simbolos);
        }

        public Simbolo BuscarSimbolo(string identificador)
        {
            return Simbolos.Find((simbolo) => simbolo.Id.Equals(identificador)) ?? null;
        }

        public bool ContieneSimbolo(string identificador)
        {
            foreach (Simbolo simbolo in Simbolos)
                if (simbolo.Id.Equals(identificador))
                    return true;

            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("TablaSimbolos{\n");

            foreach (Simbolo simbolo in Simbolos)
            {
                sb.Append('\t').Append(simbolo.ToString()).Append('\n');
            }

            sb.Append('}');

            return sb.ToString();
        }
    }
}
