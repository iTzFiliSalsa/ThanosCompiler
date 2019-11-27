using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Irony.Parsing;
using ktf;
using frmMain.semantico;
using frmMain.intermedio;
using frmMain.semantico.ejecucion;

namespace frmMain
{
    public partial class Semantico : Form
    {
        int CARACTER;
        RegexLexer csLexer = new RegexLexer();
        List<String> PalabrasReservadas;
        TablaSimbolos ts = new TablaSimbolos();

        public Semantico()
        {
            InitializeComponent();

            using (StreamReader sr = new StreamReader(@"..\..\RegexLexer.cs"))
            {

                //csLexer.AddTokenRule(@"\t+", "TABULACION");//esme
                csLexer.AddTokenRule(@"\s+", "ESPACIO", true);
                csLexer.AddTokenRule("\".*?\"", "CADENA");
                csLexer.AddTokenRule(@"'\\.'|'[^\\]'", "CARACTER");
                csLexer.AddTokenRule("//[^\r\n]*", "COMENTARIOLINEA");
                csLexer.AddTokenRule("/[*].*?[*]/", "COMENTARIOBLOQUE");
                csLexer.AddTokenRule(@"\d+\.\d+", "DECIMAL");//ESME
                csLexer.AddTokenRule(@"\d*\.?\d+", "NUMERO");
                csLexer.AddTokenRule(@"\b[_a-zA-Z][\w]*\.?\b[_a-zA-Z][\w]*\b", "IDENTIFICADOR_FUNCIONAL");//ESME
                csLexer.AddTokenRule(@"[\(\)\{\}\[\];,]", "DELIMITADOR");
                csLexer.AddTokenRule(@"[\.=\+\-/*%]", "OPERADOR");
                csLexer.AddTokenRule(@">|<|==|>=|<=|!|!=", "COMPARADOR");
                csLexer.AddTokenRule(@"\b[_a-zA-Z][\w]*\b", "IDENTIFICADOR");
                csLexer.AddTokenRule(@"\+\+|\-\-", "OPERADOR_INC_O_DEC");//esme
                csLexer.AddTokenRule(@"\+=|\-=|\*=|/=", "OPERADOR_ASIGNACION");//esme
                csLexer.AddTokenRule(@"[\.=\+\-/*%}|\^]", "OPERADOR");
                csLexer.AddTokenRule(@"\&\&|\|\||\!\=", "OPERADOR_LOGICO");//esme

                // csLexer.AddTokenRule();
                //listas  macoy
                PalabrasReservadas = new List<string>() { "abstract","assert","boolean","break","byte", "case", "catch",
                    "char","class","const","continue","default","do","double","else","enum","extends","final","finally",
                    "float","for","goto","if","implements","import","instanceof","int","interface","long","native","new",
                    "package","private","protected","public","return","short","static","stricftp","super","switch","synchronized",
                    "this","throw","throws","transient","try","void","volatile","while"
                };
                //Listas esme
                //List<string> reservadasCiclos;
                //List<string> tipoDeDato;
                //List<string> ambitoNivelDeAcceso;
                //List<string> palabrasReservadas;
                //reservadasCiclos = new List<string>()
                //{
                //     "while","do","for","foreach","if"
                //};
                //tipoDeDato = new List<string>()
                //{
                //    "int","float","double","long","bool","byte", "char",
                //    "decimal", "dynamic","sbyte", "short",
                //    "string", "uint", "ulong", "ushort", "var"
                //};
                //ambitoNivelDeAcceso = new List<string>()
                //{
                //    "public","private", "protected","internal"
                //};
                //palabrasReservadas = new List<string>() {
                //    "abstract","as","async","await","checked",
                //    "const", "continue", "default", "delegate",
                //    "base", "break", "case","else", "enum","event",
                //    "explicit", "extern", "false", "finally",
                //    "fixed","goto","implicit","in", "interface",
                //    "is", "lock", "new","null", "operator",
                //    "catch","out", "override","params","readonly",
                //    "ref", "return", "sealed", "sizeof",
                //    "stackalloc", "static","switch", "this",
                //    "throw","true", "try", "typeof", "namespace",
                //    "unchecked","unsafe", "virtual", "void",
                //    "object","get", "set", "new","partial", "yield",
                //    "add", "remove", "value","alias", "ascending",
                //    "descending", "from","group", "into", "orderby",
                //    "select", "where","join", "equals", "using",
                //     "class", "struct" };
                csLexer.Compile(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("semantico");

            
        }

        public void aCodigo()
        {
            int n = 0;
            int errores = 0;
            int i = 0;
            int j = 0;

            /*foreach (var tk in csLexer.GetTokens(tbxCode.Text))
            {
                if (tk.Name == "ERROR")
                {
                    errores++;
                    j++;
                }else if (tk.Name == "IDENTIFICADOR")
                {
                    tbxCode.ForeColor = Color.White;
                    if (PalabrasReservadas.Contains(tk.Lexema)){
                        tk.Name = "RESERVADO";
                    }
                }
                if (!(tk.Name == "ERROR"||tk.Name=="0"))
                {
                    lvToken.Rows.Insert(i, tk.Name, tk.Lexema, tk.Linea);
                    i++;
                }
                n++;
            }*/

            // SINTACTICO
            bool resultado = Sintactico.analizar(tbxCode.Text);

            if (resultado)
            {
                tbxCode.ForeColor = Color.White;

                // SEMANTICO
                ts.Simbolos.Clear();
                lvToken.Rows.Clear();

                //var raiz = Sintactico.analizarArbol(tbxCode.Text);
                ParseTree parseTree = Sintactico.AnalisisSemantico(tbxCode.Text);

                if (parseTree.Root == null)
                {
                    MessageBox.Show("Error sintactico");
                    return;
                }

                ParseTreeNode raiz = parseTree.Root;

                var nodos = Dfs(raiz);
                for (int k=0;k<nodos.Count();k++)
                {
                    var nodo = nodos[k];
                    if (nodo.Term.ToString().Equals("<tipo>")) { 
                    
                        var id = nodo.FindTokenAndGetText();
                        var variable = nodos[k + 3].FindTokenAndGetText();
                        var resultados = nodos[k + 6].FindTokenAndGetText();
                        ts.AgregarSimbolo(new Simbolo(id, variable, resultados));

                        Console.WriteLine("El tipo es " + id);
                        Console.WriteLine("La variable es " + variable);
                        Console.WriteLine("El resultado es" + resultados);
                        /*
                        for (int l = k; l < nodos.Count; l++)
                        {
                            Console.Write(nodos[l].FindTokenAndGetText() + "_ ");
                        }
                        Console.WriteLine("Salio");
                        Console.WriteLine("000000000000000000000000000000000000000000000000000");
                        Console.WriteLine("El tipo es " + id);
                        //Console.WriteLine("El valor es " + igual);
                        Console.WriteLine("000000000000000000000000000000000000000000000000000");
                        /*var simbolo = new Simbolo(id, tipo, valor);
                        ts.Agregar(simbolo);*/
                    }
                    //Console.WriteLine(nodo.Term.ToString() + " " + nodo.FindTokenAndGetText());
                }
                int q = 0;
                List<String> repetido = new List<string>();
                List<String> temporal = new List<string>();

                // LENAR TABLA DE SIMBOLOS
                foreach(var recorrido in ts.Simbolos)
                {
                    repetido.Add(recorrido.Tipo);
                    lvToken.Rows.Insert(q,recorrido.Id, recorrido.Tipo, recorrido.Valor);
                    q++;
                }

                /* MANEJAR TABLA DE SIMBOLOS AQUI */

                bool duplicados = ChecarDuplicados(ts);

                if (duplicados == false)
                {
                    MessageBox.Show("Variables duplicadas");
                    return;
                }

                bool tipos = ChecarTipos(ts);

                if (tipos == false)
                {
                    MessageBox.Show("Error de tipo");
                    return;
                }

                /* MANEJAR TABLA DE SIMBOLOS FINALIZA AQUI */

                q = 0;
                int re = 0;
                bool enc = false;

                for(int z=0; z < temporal.Count; z++)
                {
                    for(int zz = 0; zz < temporal.Count; z++)
                    {
                        if (temporal[z].Equals(temporal[zz]))
                        {
                            Console.WriteLine("Repetido");
                            re++;
                            break;
                        }
                    }
                }
                Console.WriteLine("Valores: " + re);
                
                /*foreach (var k in repetido)
                {
                    temporal.Add(k);
                    for (int z = 0; z < temporal.Count; z++)
                    {
                        for(int zz = 0; zz < temporal.Count; zz++)
                        {
                            if (temporal[z].Equals(temporal[zz]))
                            {
                                re++;
                                enc = true;
                                break;
                            }
                            if (enc)
                                break;
                        }
                        if (enc)
                            break;
                    }*/
                   /* if (k.Count() > 2)
                    {
                        MessageBox.Show("No se puede declarar dos veces una variable");
                        tbxCode.ForeColor = Color.Red;
                    }
                    Console.WriteLine("Encontrado "+k.Count());
                    Console.WriteLine($" {k.Key} encontrado {k.Count()} veces");*/
               // }
                if (re > 2)
                {
                    MessageBox.Show("No repetir variables");
                    tbxCode.ForeColor = Color.Red;
                }
                re = 0;
               foreach(var mm in temporal)
                {
                    Console.WriteLine($"Valor de temporal {mm}");
                }
                temporal.Clear();
                repetido.Clear();
            }
            else
            {
                MessageBox.Show("Sintactico incorrecto");
                tbxCode.ForeColor = Color.Red;
            }
            /*
            Kuto kt = new Kuto(tbxCode.Text);
            kt = kt.Extract("public static void main(String []args){","");
            string[] separador = kt.ToString().Split(';');
            for(int t = 0;t < separador.Length; t++)
            {
                MessageBox.Show(separador[t]);
            }
            */
        }

        public List<ParseTreeNode> Dfs(ParseTreeNode raiz)
        {
            List<ParseTreeNode> nodos = new List<ParseTreeNode>();
            Dfs(raiz, nodos);
            return nodos;
        }
        public void Dfs(ParseTreeNode raiz, List<ParseTreeNode> nodos)
        {
            nodos.Add(raiz);
            raiz.ChildNodes.ForEach(nodo =>
            {
                Dfs(nodo, nodos);
            });
        }

        private void Semantico_Load(object sender, EventArgs e)
        {
            tbxCode.Text = "";
        }

        #region Semantico

        public void analisisSemantico()
        {
            ParseTree parseTree = Sintactico.AnalisisSemantico(tbxCode.Text);

            if (parseTree.Root == null)
            {
                MessageBox.Show("Error sintactico");
                return;
            }

            // hacer arbol
            var arbol = new Arbol(parseTree);

            // checar tabla de simbolos
            ts = GenerarTablaSimbolos(arbol);

            bool duplicados = ChecarDuplicados(ts);

            if (duplicados == false)
            {
                MessageBox.Show("Variables duplicadas");
                return;
            }

            bool tipos = ChecarTipos(ts);

            if (tipos == false)
            {
                MessageBox.Show("Error de tipo");
                return;
            }

            // llenar tabla de simbolos
            foreach (var s in ts.Simbolos)
            {
                lvToken.Rows.Add(s.Id, s.Tipo, s.Valor);
            }

            var sb = new StringBuilder();

            foreach (var s in ts.Simbolos)
            {
                sb.Append(s.ToString()).Append('\n');
            }

            MessageBox.Show(sb.ToString());
        }

        public TablaSimbolos GenerarTablaSimbolos(Arbol arbol)
        {
            var tabla = new TablaSimbolos();
            List<ParseTreeNode> nodos = arbol.Recorrer(Gramatica.NoTerminales.DeclaracionVariable);

            foreach (ParseTreeNode nodo in nodos)
            {
                List<Simbolo> simbolos = CrearSimbolos(arbol, nodo);
                tabla.AgregarSimbolos(simbolos);
            }

            return tabla;
        }

        private List<Simbolo> CrearSimbolos(Arbol arbol, ParseTreeNode nodo)
        {
            var simbolos = new List<Simbolo>();

            List<ParseTreeNode> tipos = arbol.Recorrer(nodo, Gramatica.NoTerminales.Tipo);
            List<ParseTreeNode> ids = arbol.Recorrer(nodo, Gramatica.ExpresionesRegulares.Id);
            List<ParseTreeNode> asignaciones = arbol.Recorrer(nodo, Gramatica.NoTerminales.Asignable);
            var listaAsignables = new List<List<ParseTreeNode>>();

            asignaciones.ForEach(node =>
            {
                List<ParseTreeNode> hojas = arbol.HojasDe(node);
                listaAsignables.Add(hojas);
            });

            // Crear simbolos
            for (int i = 0; i < ids.Count; i++)
            {
                string tipo = tipos[0].FindTokenAndGetText();
                string id = ids[i].FindTokenAndGetText();

                if (listaAsignables.Count == 0)
                    simbolos.Add(new Simbolo(tipo, id));

                else
                {
                    var sb = new StringBuilder();

                    listaAsignables[i].ForEach(token =>
                    {
                        sb.Append($"{token.FindTokenAndGetText()} ");
                    });

                    string asignable = sb.ToString().Trim();
                    simbolos.Add(new Simbolo(tipo, id, asignable));
                }
            }

            return simbolos;
        }

        public bool ChecarDuplicados(TablaSimbolos tabla)
        {
            var contadores = new Dictionary<string, int>();

            foreach (Simbolo simbolo in tabla.Simbolos)
            {
                string id = simbolo.Id;

                if (!contadores.ContainsKey(id))
                    contadores[id] = 0;

                contadores[id] += 1;

                if (contadores[id] > 1)
                    return false;
            }

            return true;
        }

        public bool ChecarTipos(TablaSimbolos tabla)
        {
            foreach (Simbolo simbolo in tabla.Simbolos)
            {
                string tipo = simbolo.Tipo;
                string valor = simbolo.Valor;

                if (valor == null)
                    continue;

                // Si el valor del simbolo actual es un id
                if (ValidarRegex(valor, Gramatica.ExpresionesRegulares.IdRegex) && !ValidarRegex(valor, Gramatica.ExpresionesRegulares.StringRegex))
                {
                    // Primero, checamos si el identificador existe
                    if (!tabla.ContieneSimbolo(valor))
                        return false;

                    // Despues, tenemos que obtener el valor de dicho id para comprobar su tipo
                    valor = ValorDe(tabla, valor);
                }

                switch (tipo)
                {
                    case Gramatica.Terminales.Int:
                    {
                        if (!ValidarRegex(valor, Gramatica.ExpresionesRegulares.NumeroRegex))
                            return false;

                        if (valor.Contains('.'))
                            return false;

                        break;
                    }

                    case Gramatica.Terminales.Float:
                    {
                        if (!ValidarRegex(valor, Gramatica.ExpresionesRegulares.NumeroRegex))
                            return false;

                        break;
                    }

                    case Gramatica.Terminales.Double:
                    {
                        if (!ValidarRegex(valor, Gramatica.ExpresionesRegulares.NumeroRegex))
                            return false;

                        break;
                    }

                    case Gramatica.Terminales.Bool:
                    {
                        if (!valor.Equals(Gramatica.Terminales.True) || !valor.Equals(Gramatica.Terminales.False))
                            return false;

                        break;
                    }

                    case Gramatica.Terminales.String:
                    {
                        if (!ValidarRegex(valor, Gramatica.ExpresionesRegulares.StringRegex))
                            return false;

                        break;
                    }
                }
            }

            return true;
        }

        public bool ValidarRegex(string cadena, string regex)
        {
            Match validacion = Regex.Match(cadena, regex);
            return validacion.Success;
        }

        private string ValorDe(TablaSimbolos tabla, string id)
        {
            Console.WriteLine("/ = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = / = /");
            Console.WriteLine($"Checando tipo del id '{id}'");

            Simbolo simbolo = tabla.BuscarSimbolo(id);

            Console.WriteLine($"El valor del id actual '{id}' es '{simbolo.Valor}'");

            if (id.Equals(simbolo.Valor))
                return null;

            if (simbolo.Valor == null)
                return null;

            if (ValidarRegex(simbolo.Valor, Gramatica.ExpresionesRegulares.IdRegex) && !ValidarRegex(simbolo.Valor, Gramatica.ExpresionesRegulares.StringRegex))
            {
                Console.WriteLine("Recursando...");
                return ValorDe(tabla, simbolo.Valor);
            }

            Console.WriteLine($"Se encontro el tipo del id '{id}'. Tipo es '{simbolo.Tipo}'");
            return simbolo.Valor;
        }

        #endregion

        private void pictureBoxCodigoIntermedio_Click(object sender, EventArgs e)
        {
            // intermedio
            TablaTriplosFactory.ReiniciarContador();

            var generador = new GeneradorCodigoIntermedio();
            List<TablaTriplos> tablasTriplos = generador.GenerarTriplos(ts);
            List<List<string>> codigoGenerado = generador.GenerarCodigo(tablasTriplos);

            var sb = new StringBuilder();

            foreach (List<string> lineasCodigo in codigoGenerado)
            {
                foreach (string linea in lineasCodigo)
                {
                    sb.Append(linea).Append('\n');
                }
            }

            MessageBox.Show(sb.ToString());
        }

        private void ejecutar()
        {
            // analisis lexico

            string codigo = tbxCode.Text;
            var lexer = new ThanosLexer(codigo);

            while (lexer.HasNext())
            {
                Console.WriteLine($"{lexer.CurrentToken} = {lexer.CurrentLexeme}");
            }

            if (!lexer.IsSuccessful)
            {
                MessageBox.Show("Error de analisis lexico");
                return;
            }

            // analisis semantico

            var parser = new ThanosParser(lexer.Tokens);
            string nuevoCodigo = parser.ParseToSourceCode();

            //MessageBox.Show(nuevoCodigo);

            // ejecucion

            if (!ThanosExecutor.Compilar(nuevoCodigo))
            {
                MessageBox.Show(String.Join("\n\n", ThanosExecutor.Errores));
                return;
            }

           // MessageBox.Show("Ejecutando...");

            var consola = new EjecutarForm();
            consola.Ejecutar();
            consola.ShowDialog();
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

        public void limpiar()
        {
            MessageBox.Show(tbxCode.Text);
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

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            lvToken.Rows.Clear();
            //aCodigo();
            //analisisSemantico();
            ejecutar();
        }

        private void tbxCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox8_Paint_1(object sender, PaintEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }


        private void contador_Paint(object sender, PaintEventArgs e)
        {
            CARACTER = 0;
            int ALTURA = tbxCode.GetPositionFromCharIndex(0).Y;
            if (tbxCode.Lines.Length > 0)
            {
                for (int i = 0; i < tbxCode.Lines.Length; i++)
                {
                    e.Graphics.DrawString(Convert.ToString(i + 1), tbxCode.Font, Brushes.Gold, contador.Width - (e.Graphics.MeasureString(Convert.ToString(i + 1), tbxCode.Font).Width + 10), ALTURA);
                    CARACTER += tbxCode.Lines[i].Length + 1;
                    ALTURA = tbxCode.GetPositionFromCharIndex(CARACTER).Y;
                }
            }
            else
            {
                e.Graphics.DrawString(Convert.ToString(1), tbxCode.Font, Brushes.Gold, contador.Width - (e.Graphics.MeasureString(Convert.ToString(1), tbxCode.Font).Width + 10), ALTURA);
            }
        }

        private void Counter_Tick(object sender, EventArgs e)
        {
            contador.Refresh();
        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox8_MouseHover(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.Red;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(255, 50, 0, 50);
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
        private void pictureBox9_Click(object sender, EventArgs e)
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
