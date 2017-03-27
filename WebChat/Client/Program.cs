using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            string name = "Cliente";
            int status = 0;
            string ip = "127.0.0.1";
            string porta = "7070";

            if (args.Length == 4) {
                name = args[0];
                if (!int.TryParse(args[1], out status)) {
                    status = 0;
                }
                ip = args[2];
                porta = args[3];
            } else if (args.Length != 0) {
                Console.WriteLine("Utilize 0 ou 4 argumentos (nome, status (int), ip e porta)");
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConnectionGUI(name, status, ip, porta));
        }
    }
}
