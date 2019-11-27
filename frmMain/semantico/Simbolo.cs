using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain
{
    public class Simbolo
    {
        public string Tipo;
        public string Id;
        public string Valor;

        public Simbolo(string tipo, string identificador)
        {
            this.Tipo = tipo;
            this.Id = identificador;
        }

        public Simbolo(string tipo, string identificador, string valor)
        {
            this.Tipo = tipo;
            this.Id = identificador;
            this.Valor = valor;
        }

        public override string ToString()
        {
            return "Simbolo{'" + Tipo + "', '" + Id + "', '" + Valor + "'}";
        }
    }
}
