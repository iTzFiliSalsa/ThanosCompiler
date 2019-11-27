using System;

namespace frmMain

{
    class RHS
    {
        private bool hasDot = false;
        private int dot = -1;
        private const string DOT = "@";
        private string[] terms;

        public RHS(string[] t)
        {
            terms = t;

            for (int i = 0; i < terms.Length; i++)
            {
                if (terms[i].CompareTo(DOT) == 0)
                {
                    dot = i;
                    hasDot = true;
                    break;
                }
            }
        }

        public bool HasDot
        {
            get
            {
                return hasDot;
            }
        }

        public string[] Terms
        {
            get
            {
                return terms;
            }
        }

        public string getBeforeDot()
        {
            if (hasDot && dot > 0)
                return terms[dot - 1];

            return string.Empty;
        }

        public string getAfterDot()
        {
            if (hasDot && dot < terms.Length - 1)
                return terms[dot + 1];

            return string.Empty;
        }

        public bool isDotFirst()
        {
            if (hasDot)
                return dot == 0;

            return false;
        }

        public bool isDotLast()
        {
            if (hasDot)
                return dot == terms.Length - 1;

            return false;
        }

        public RHS addDot()
        {
            string[] t = new string[terms.Length + 1];

            t[0] = DOT;

            for (int i = 1; i < t.Length; i++)
                t[i] = terms[i - 1];

            return new RHS(t);
        }

        public RHS addDotLast()
        {
            string[] t = new string[terms.Length + 1];

            for (int i = 0; i < t.Length - 1; i++)
                t[i] = terms[i];

            t[t.Length - 1] = DOT;

            return new RHS(t);
        }

        public RHS moveDot()
        {
            string[] t = new string[terms.Length];

            for (int i = 0; i < t.Length; i++)
            {
                if (terms[i].CompareTo(DOT) == 0)
                {
                    t[i] = terms[i + 1];
                    t[i + 1] = DOT;
                    i++;
                }

                else
                    t[i] = terms[i];
            }

            return new RHS(t);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return this == null;

            RHS rhs = (RHS)obj;

            if (terms.Length != rhs.terms.Length)
                return false;

            for (int i = 0; i < terms.Length; i++)
                if (terms[i].CompareTo(rhs.terms[i]) != 0)
                    return false;

            return dot == rhs.dot;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string r = string.Empty;

            for (int i = 0; i < terms.Length; i++)
                r += terms[i] + " ";

            return r;
        }
    }
}