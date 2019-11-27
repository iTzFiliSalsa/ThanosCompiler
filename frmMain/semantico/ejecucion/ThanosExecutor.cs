using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMain.semantico.ejecucion
{
    public class ThanosExecutor
    {
        public static readonly string Exe = "ThanosProgram.exe";
        public static List<CompilerError> Errores = new List<CompilerError>();

        public static bool Compilar(string codigoFuente)
        {
            Errores.Clear();

            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, Exe, true)
            {
                GenerateExecutable = true,
                OutputAssembly = Exe,
            };

            CompilerResults results = csc.CompileAssemblyFromSource(parameters, codigoFuente);

            Errores = results.Errors.Cast<CompilerError>().ToList();

            foreach (var error in Errores)
            {
                MessageBox.Show(error.ErrorText);
            }

            if (Errores.Count != 0)
            {
                return false;
            }

            return true;
        }
    }
}
