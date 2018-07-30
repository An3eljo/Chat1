using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ChatServer;

namespace ChatService
{
    public class Init
    {
        public static void Initialize()
        {
            CheckAboutOtherInstance();
        }

        static void CheckAboutOtherInstance()
        {
            if (System.Diagnostics.Process
                    .GetProcessesByName(
                        System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly()
                            .Location)).Count() > 1)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }

            var uiThread = new Thread(() =>
            {
                var ui = new Application();
                ui.Run(new MainWindow());
            });

            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.Start();
        }
    }
}
