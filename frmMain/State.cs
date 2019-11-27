using System;

namespace frmMain
{
    class State
    {
        private int i, j;
        private string lhs;
        private RHS rhs;

        public State(string lhs, RHS rhs, int i, int j)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.i = i;
            this.j = j;
        }

        public int I
        {
            get
            {
                return i;
            }
        }

        public int J
        {
            get
            {
                return j;
            }
        }

        public string Lhs
        {
            get
            {
                return lhs;
            }
        }

        public RHS Rhs
        {
            get
            {
                return rhs;
            }
        }

        public string getAfterDot()
        {
            return rhs.getAfterDot();
        }

        public string getBeforeDot()
        {
            return rhs.getBeforeDot();
        }

        public bool isDotLast()
        {
            return rhs.isDotLast();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return this == null;

            State s = (State)obj;
            bool part1 = lhs.CompareTo(s.lhs) == 0;
            bool part2 = rhs.Equals(s.rhs);

            return part1 && part2 && i == s.i && j == s.j;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string r = string.Empty;

            return lhs + " " + rhs.ToString() +
                " " + i.ToString() + " " + j.ToString();
        }
    }
}