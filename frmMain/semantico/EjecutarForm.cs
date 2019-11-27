using frmMain.semantico.ejecucion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMain.semantico
{
    public partial class EjecutarForm : Form
    {
        public EjecutarForm()
        {
            InitializeComponent();

            consoleControl.OnConsoleOutput += (s, e) =>
            {
                if (consoleControl.IsProcessRunning)
                    return;

                consoleControl.WriteOutput("Proceso Terminado", Color.White);
                
            };
            
        }

        public void Ejecutar()
        {
            consoleControl.StartProcess(ThanosExecutor.Exe, null);
        }
    }
}
