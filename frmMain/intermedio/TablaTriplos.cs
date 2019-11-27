using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain.intermedio
{
    public class TablaTriplos
    {
        private SortedDictionary<string, Triplo> triplos = new SortedDictionary<string, Triplo>();
        public SortedDictionary<string, Triplo> Triplos { get => triplos; }

        public TablaTriplos() { }

        public bool Agregar(string id, Triplo triplo)
        {
            if (Contiene(id))
                return false;

            Triplos[id] = triplo;

            return true;
        }

        public bool Contiene(string id)
        {
            return Triplos.ContainsKey(id);
        }

        public bool Eliminar(string id)
        {
            return Triplos.Remove(id);
        }

        public string EncontrarId(Triplo triplo)
        {
            foreach (var i in Triplos)
            {
                Triplo temp = i.Value;

                if (temp.Equals(triplo))
                    return i.Key;
            }

            return null;
        }

        public Triplo Get(string id)
        {
            if (!Contiene(id))
                return null;

            return Triplos[id];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Triplos.Count == 0)
                return "Triplos: {}";

            sb.Append("Triplos: {\n");

            foreach (var i in Triplos)
            {
                Triplo t = i.Value;
                sb.Append('\t').Append(i.Key).Append(" = ").Append(t).Append('\n');
            }

            sb.Append('}');

            return sb.ToString();
        }
    }
}
