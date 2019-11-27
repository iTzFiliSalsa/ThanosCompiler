using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace frmMain
{
    class Chart
    {
        List<State> chart;

        public Chart()
        {
            chart = new List<State>();
        }

        public void addState(State s)
        {
            if (!chart.Contains(s))
                chart.Add(s);
        }

        public State getState(int i)
        {
            if (i < 0 || i >= chart.Count)
                return null;

            return chart[i];
        }

        public int size()
        {
            return chart.Count;
        }

        public override string ToString()
        {
            string r = string.Empty;

            for (int i = 0; i < chart.Count; i++)
                r += chart[i].ToString() + "\r\n";

            return r;
        }
    }
}
