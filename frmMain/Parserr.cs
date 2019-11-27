using System;

namespace frmMain
{
    class Parserr
    {
        private string[] sentence;
        private Grammarr grammar;
        private Chart[] charts;

        public Parserr(Grammarr g)
        {
            grammar = g;
        }

        public Grammarr getGrammar()
        {
            return grammar;
        }

        public Chart[] getCharts()
        {
            return charts;
        }

        public bool parseSentence(string[] s)
        {
            sentence = s;
            charts = new Chart[sentence.Length + 1];

            for (int i = 0; i < charts.Length; i++)
                charts[i] = new Chart();

            string[] start1 = { "@", "S" };
            RHS startRHS = new RHS(start1);

            State start = new State("$", startRHS, 0, 0);

            charts[0].addState(start);

            for (int i = 0; i < charts.Length; i++)
            {
                for (int j = 0; j < charts[i].size(); j++)
                {
                    State st = charts[i].getState(j);
                    string next_term = st.getAfterDot();

                    if (st.isDotLast())
                    {
                        completer(st);
                    }
                    else if (grammar.isPartOfSpeech(next_term))
                    {
                        scanner(st);
                    }
                    else
                    {
                        predictor(st);
                    }
                }
            }

            string[] fin = { "S", "@" };
            RHS finRHS = new RHS(fin);
            State finish = new State("$", finRHS, 0, sentence.Length);

            for (int j = 0; j < charts[sentence.Length].size(); j++)
            {
                State state = charts[sentence.Length].getState(j);

                if (state.Equals(finish))
                    return true;
            }

            return false;
        }

        private void predictor(State s)
        {
            string lhs = s.getAfterDot();
            RHS[] rhs = grammar.getRHS(lhs);
            int j = s.J;

            for (int i = 0; i < rhs.Length; i++)
            {
                State ns = new State(lhs, rhs[i].addDot(), j, j);

                charts[j].addState(ns);
            }
        }

        private void scanner(State s)
        {
            string lhs = s.getAfterDot();
            RHS[] rhs = grammar.getRHS(lhs);
            int i = s.I;
            int j = s.J;

            for (int a = 0; a < rhs.Length; a++)
            {
                int count = -1;
                string[] terms = rhs[a].Terms;

                count = terms.Length;

                if (terms.Length == count && j < sentence.Length)
                {
                    for (int k = 0; k < count; k++)
                    {
                        string term = terms[k].ToLower();
                        string sent = sentence[j].ToLower();

                        if (term.CompareTo(sent) == 0)
                        {
                            State ns = new State(lhs, rhs[a].addDotLast(), j, j + 1);

                            charts[j + 1].addState(ns);
                        }
                    }
                }
            }
        }

        private void completer(State s)
        {
            string lhs = s.Lhs;

            for (int a = 0; a < charts[s.I].size(); a++)
            {
                State st = charts[s.I].getState(a);
                string after = st.getAfterDot();

                if (after != string.Empty && lhs.CompareTo(after) == 0)
                {
                    State ns = new State(st.Lhs, st.Rhs.moveDot(), st.I, s.J);

                    charts[s.J].addState(ns);
                }
            }
        }
    }
}
