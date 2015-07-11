/*

 * Date           : 10/05/2012
 * Programmers    : Georgian Costea
 * Description    : The purpose of this file is to execute the program
 *
 **/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SETPaint
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSETPaint());
        }
    }
}
