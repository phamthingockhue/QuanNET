using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using QuanNET.GUI;

namespace QuanNET
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Application app = new System.Windows.Application();

            Login_Regist loginWindow = new Login_Regist();

            app.Run(loginWindow);
        }
    }
}
