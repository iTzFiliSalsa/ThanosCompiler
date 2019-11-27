using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace frmMain
{
    public partial class MainForm : Form
    {
        int CARACTER;
        public MainForm()
        {
            InitializeComponent();
            Test();
        }

        private void Test()
        {
            /*string[] tokens1 = { "if", "(", "var9", "==", "3", ")", "{", "string", "var5", ";", "}" };
            string[] tokens2 = { "if", "(", "var9", "<=", "3", ")", "{", "bool", "var5", "=", "var2", ";", "}" };
            string[] tokens3 = { "if", "(", "var5", ">=", "var1", ")", "{", "char", "var1", "=", "4", ";", "}" };
            string[] tokens4 = { "if", "(", "var5", "!=", "var8", ")", "{", "int", "var1", "=", "var0", "(", ")", ";", "}" };
            string[] tokens5 = { "if", "(", "var0", "<", "5", ")", "{", "double", "var7", "=", "var0", ".", "var9", "(", ")", ";", "}" };

            string[] error1 = { "var9", "==", "3", "then", "string", "var5", ";", "end" };
            string[] error2 = { "if", "var10", "<=", "3", "then", "bool", "var5", "=", "var2", ";", "end" };
            string[] error3 = { "if", "var5", ">=", "var1", "then", "char", "=", "4", ";", "end" };
            string[] error4 = { "if", "var5", "!=", "var8", "then", "int", "var1", "=", "var0", "(", ")", "end" };
            string[] error5 = { "if", "var0", "<", "5", "then", "double", "var7", "=", "var0", ".", "var9", "(", ")", ";" };*/

            var grammar = new AleGrammar(this);
            var parser = new Parserr(grammar);

           /* Parse(tokens1, parser, false);
            Parse(tokens2, parser, false);
            Parse(tokens3, parser, false);
            Parse(tokens4, parser, false);
            Parse(tokens5, parser, false);

            Parse(error1, parser, false);
            Parse(error2, parser, false);
            Parse(error3, parser, false);
            Parse(error4, parser, false);
            Parse(error5, parser, false);*/
        }

        private void Parse(string[] tokens, Parserr parser, bool clear)
        {
            if (clear)
                textBox1.Clear();

            var output = new StringBuilder();

            for (int i = 0; i < tokens.Length - 1; i++)
                output.Append(tokens[i] + " ");

            string sentence = output.ToString();
            //textBox1.Text += "Parsing: " + sentence + "\r\n";

            bool success = parser.parseSentence(tokens);

            if (success)
            {
                textBox1.ForeColor = Color.Lime;
                textBox1.Text += "Compilacion sintactica con exito";
            }

            else
            {
                textBox1.ForeColor = Color.Red;
                textBox1.Text += "Compilacion sintactica fallida";
            }
            

            /*Chart[] charts = parser.getCharts();
            textBox1.Text += "Charts produced by the sentence: " + sentence + "\r\n";

            for (int i = 0; i < charts.Length; i++)
            {
                textBox1.Text += "Chart[" + i.ToString() + "] :\r\n";
                textBox1.Text += charts[i].ToString() + "\r\n";
            }*/

            /*textBox1.Text += "========================================================";
            textBox1.Text += "\r\n";
            textBox1.Text += "========================================================";
            textBox1.Text += "\r\n";*/
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }
        RegexLexer csLexer = new RegexLexer();
        bool load;
        List<string> palabrasReservadas;

        private void AnalizeCode()
        {
            lvToken.Rows.Clear();
            
            int n = 0, e = 0;

            foreach (var tk in csLexer.GetTokens(tbxCode.Text))
            {
                if (tk.Name == "ERROR") e++;

                if (tk.Name == "IDENTIFICADOR")
                    if (palabrasReservadas.Contains(tk.Lexema))
                        tk.Name = "RESERVADO";
                lvToken.Rows.Add(tk.Name, tk.Lexema, tk.Index, tk.Linea, tk.Columna);
                n++;
            }

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonParse_Click_1(object sender, EventArgs e)
        {
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Start();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(@"..\..\RegexLexer.cs"))
            {
                csLexer.AddTokenRule(@"\s+", "ESPACIO", true);
                csLexer.AddTokenRule(@"\b[_a-zA-Z][\w]*\b", "IDENTIFICADOR");
                csLexer.AddTokenRule("\".*?\"", "CADENA");
                csLexer.AddTokenRule(@"'\\.'|'[^\\]'", "CARACTER");
                csLexer.AddTokenRule("//[^\r\n]*", "COMENTARIO1");
                csLexer.AddTokenRule("/[*].*?[*]/", "COMENTARIO2");
                csLexer.AddTokenRule(@"\d*\.?\d+", "NUMERO");
                csLexer.AddTokenRule(@"[\(\)\{\}\[\];,]", "DELIMITADOR");
                csLexer.AddTokenRule(@"[\.=\+\-/*%]", "OPERADOR");
                csLexer.AddTokenRule(@">|<|==|>=|<=|!", "COMPARADOR");

                palabrasReservadas = new List<string>() { "abstract", "as", "assert", "async", "await",
                "checked", "const", "continue", "default", "delegate", "base", "break", "case",
                "do", "else", "enum", "event", "explicit", "extern","extends", "false", "finally",
                "fixed", "for", "foreach", "goto", "if", "implicit","implements", "in", "interface",
                "internal", "is", "lock", "new", "null", "operator","catch",
                "out", "override", "params", "private", "protected", "public", "readonly",
                "ref", "return", "sealed", "sizeof", "stackalloc", "static",
                "switch", "this", "throw", "true", "try", "typeof", "namespace",
                "unchecked", "unsafe", "virtual", "void", "while", "float", "int", "long", "object",
                "get", "set", "new", "partial", "yield", "add", "remove", "value", "alias", "ascending",
                "descending", "from", "group", "into", "orderby", "select", "where",
                "join", "equals", "using","boolean", "byte", "char", "decimal", "double", "dynamic",
                "sbyte", "short", "string", "uint", "ulong", "ushort", "var", "class", "struct","sizeof"
                ,"LinkedList","import","final","package","instanceof","native",
                "strictfp","super","synchronized","throws","transient","volatile"};

                csLexer.Compile(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);

                load = true;
                AnalizeCode();
                tbxCode.Focus();
            }
        }

        public List<string> getIds()
        { 
            var lista = new List<string>();

           // MessageBox.Show(lvToken.Rows.Count.ToString());

            for (int i = 0; i < lvToken.Rows.Count - 1; i++)
            {
                if (lvToken.Rows[i].Cells["Nombre"].Value.ToString() == "IDENTIFICADOR")
                {
                    lista.Add(lvToken.Rows[i].Cells["Lexema"].Value.ToString());
                }
            }

            return lista;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            CARACTER = 0;
            int ALTURA = tbxCode.GetPositionFromCharIndex(0).Y;
            if (tbxCode.Lines.Length>0)
            {
                for (int i = 0; i < tbxCode.Lines.Length; i++)
                {
                    e.Graphics.DrawString(Convert.ToString(i + 1), tbxCode.Font, Brushes.Gold, pictureBox1.Width - (e.Graphics.MeasureString(Convert.ToString(i + 1), tbxCode.Font).Width + 10), ALTURA);
                    CARACTER += tbxCode.Lines[i].Length + 1;
                    ALTURA = tbxCode.GetPositionFromCharIndex(CARACTER).Y;
                }
            }
            else
            {
               e.Graphics.DrawString(Convert.ToString(1), tbxCode.Font, Brushes.Gold, pictureBox1.Width - (e.Graphics.MeasureString(Convert.ToString(1), tbxCode.Font).Width + 10), ALTURA);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            lvToken.Rows.Clear();
            if (load)
                AnalizeCode();
            string input = tbxCode.Text;
            string[] tokens = input.Split(null);

            var grammar = new AleGrammar(this);
            ////////////

            var idsList = getIds();
            var str = "";

            for (int i = 0; i < idsList.Count - 1; i++)
            {
                str += idsList[i] + " ";
            }
            str += idsList[idsList.Count - 1];

            Console.WriteLine(str);

            grammar.addProductionRule("<id>", str);
            var parser = new Parserr(grammar);
            Parse(tokens, parser, true);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (tbxCode.SelectedText != "")
            {
                Clipboard.SetDataObject(tbxCode.SelectedText);
            }
            else
            {
                Clipboard.SetDataObject(tbxCode.Text);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                if (tbxCode.SelectedText != "")
                {
                    tbxCode.SelectedText = Clipboard.GetText();
                }
                else
                {
                    tbxCode.Text += Clipboard.GetText();
                }

            }
        }

        private void pictureBox10_MouseHover(object sender, EventArgs e)
        {
            pictureBox10.BackColor = Color.Red;
        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox10.BackColor = Color.FromArgb(255, 50, 0, 50);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JAVA|*.java";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, tbxCode.Text);
            }
        }
        OpenFileDialog ofd = new OpenFileDialog();
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JAVA|*.java";
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                string direccion = ofd.FileName;
                StreamReader leer = new StreamReader(@direccion);
                string linea;
                try
                {
                    tbxCode.Text = "";
                    lvToken.Rows.Clear();
                    linea = leer.ReadLine();
                    while (linea != null)
                    {
                        tbxCode.AppendText(linea + "\n");
                        linea = leer.ReadLine();
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }
    }
}
