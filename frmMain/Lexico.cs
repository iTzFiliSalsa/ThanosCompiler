using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace frmMain
{
    public partial class Lexico : Form
    {
        public Lexico()
        {
            InitializeComponent();
        }
        

        private void tbxCode_TextChanged(object sender, EventArgs e)
        {
            if (load)
                AnalizeCode();
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

        private void Lexico_Load(object sender, EventArgs e)
        {
            /*this.toolTip1.SetToolTip(pictureBox5, "Copiar");
            this.toolTip1.SetToolTip(pictureBox6, "Pegar");*/
            using (System.IO.StreamReader sr = new StreamReader(@"..\..\RegexLexer.cs"))
            {
                csLexer.AddTokenRule(@"\s+", "ESPACIO", true);
                csLexer.AddTokenRule(@"\b[_a-zA-Z][\w]*\b", "IDENTIFICADOR");
                csLexer.AddTokenRule("\".*?\"", "CADENA");
                csLexer.AddTokenRule(@"'\\.'|'[^\\]'", "CARACTER");
                csLexer.AddTokenRule("//[^\r\n]*", "COMENTARIO1");
                csLexer.AddTokenRule("/[*].*?[*]/", "COMENTARIO2");
                csLexer.AddTokenRule(@"\d*\.?\d+", "NUMERO");
                //csLexer.AddTokenRule(@"\d?", "NUMERO");
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Red;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(255, 50, 0, 50);
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
        private void pictureBox4_Click(object sender, EventArgs e)
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
