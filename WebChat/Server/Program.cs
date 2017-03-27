using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            string porta = "7070";
            
            if (args.Length >= 1) {
                porta = args[0];
            } else if (args.Length != 0) {
                Console.WriteLine("Utilize 0 ou 1 argumentos (porta)");
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerConnectionGUI(porta));
        }
    }
}
