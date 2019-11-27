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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void Loading()
        {
            Application.Run(new Form1());
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void lateral_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lateral_MouseHover(object sender, EventArgs e)
        {



        }

        private void lateral_MouseLeave(object sender, EventArgs e)
        {


        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = frmMain.Properties.Resources.f;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = frmMain.Properties.Resources.ap_550x550_12x12_1_transparent_t_u2;
        }

        private void bunifuFlatButton1_MouseHover(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_MouseLeave(object sender, EventArgs e)
        {

        }
        bool mnuExpanded = false;
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void MouseDetect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            if (lateral.ClientRectangle.Contains(PointToClient(Control.MousePosition)))
            {
                if (!mnuExpanded)
                {
                    mnuExpanded = true;
                    lateral.Width = 176;
                }
            }
            else
            {
                if (mnuExpanded)
                {
                    mnuExpanded = false;
                    lateral.Width = 66;
                    lateral.Visible = true;
                    // bunifuTransition1.ShowSync(myPanel2);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bunifuTransition1.HideSync(bunifuFlatButton1);
            bunifuTransition1.HideSync(bunifuFlatButton4);
            bunifuTransition1.HideSync(bunifuFlatButton5);
            bunifuTransition1.HideSync(bunifuFlatButton6);
            bunifuTransition1.HideSync(bunifuFlatButton2);
            bunifuTransition1.HideSync(bunifuFlatButton3);
            
            
            
            bunifuTransition1.HideSync(panel1);
            bunifuTransition1.HideSync(lateral);
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            abrirForm2(new Lexico());
            //bunifuFlatButton4.Activecolor = Color.DarkGreen;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
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
                lvToken.Rows.Add(tk.Name,tk.Lexema,tk.Index,tk.Linea,tk.Columna);
                n++;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            abrirForm2(new Inicio());
            this.toolTip1.SetToolTip(pictureBox5, "Copiar");
            this.toolTip1.SetToolTip(pictureBox6, "Pegar");
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            abrirForm2(new Inicio());
            /*Semantico s = new Semantico();
            s.limpiar();
            tbxCode.Text = "";
            lvToken.Rows.Clear();*/
        }
        OpenFileDialog ofd = new OpenFileDialog();
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Información inf = new Información();
            inf.ShowDialog();
            //ofd.Filter = "JAVA|*.java";
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
                
            //    string direccion = ofd.FileName;
            //    StreamReader leer = new StreamReader(@direccion);
            //    string linea;
            //    try
            //    {
            //        tbxCode.Text = "";
            //        lvToken.Rows.Clear();
            //        linea = leer.ReadLine();
            //        while (linea!=null)
            //        {
            //            tbxCode.AppendText(linea + "\n");
            //            linea = leer.ReadLine();
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Error");
            //    }
            //}
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("El analizador sintactico proximamente estara disponible", "Anuncio");
            abrirForm2(new MainForm());
        }
        private void abrirForm2(object Agregar)
        {
            if (this.panel1.Controls.Count > 0)
                this.panel1.Controls.RemoveAt(0);
            Form fh = Agregar as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(fh);
            this.panel1.Tag = fh;
            
            fh.Show();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            abrirForm2(new Semantico());
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JAVA|*.java";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName,tbxCode.Text);
            }
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            pictureBox5.Image = frmMain.Properties.Resources.copybrillante;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = frmMain.Properties.Resources.sinb;
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.Image = frmMain.Properties.Resources.pbrillante;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Image = frmMain.Properties.Resources.pegar;
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

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            
        }

        private void lvToken_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
