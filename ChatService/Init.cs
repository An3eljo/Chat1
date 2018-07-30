using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ChatServer;
using MQTTnet;
using MQTTnet.Server;

namespace ChatService
{
    public class Init
    {
        public static void Initialize()
        {
            CheckAboutOtherInstance();
            StartServer();
            PreventClosing();
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

        static void StartServer()
        {
            MqttServerData.Instance.Server = new MqttFactory().CreateMqttServer();
            var options = new MqttServerOptionsBuilder().WithDefaultEndpointPort(1884).Build();

            MqttServerData.Instance.Server.StartAsync(options).Wait();

            var eventhandlingmethods = new EventhandlingMethods();
            MqttServerData.Instance.Server.ApplicationMessageReceived += eventhandlingmethods.OnApplicationMessageReceived;
            MqttServerData.Instance.Server.ClientConnected += eventhandlingmethods.OnClientConnect;
            MqttServerData.Instance.Server.ClientSubscribedTopic += eventhandlingmethods.OnClientSubscribe;
        }

        private static void PreventClosing()
        {
            new Thread(() =>
            {
                var shouldRepeat = true;
                while (shouldRepeat)
                {
                    var end = Console.ReadLine();
                    if (end == "endprogram")
                    {
                        shouldRepeat = false;
                        MqttServerData.Instance.Server.StopAsync().Wait();
                    }
                    else
                    {
                        var message = new MqttApplicationMessageBuilder()
                            .WithTopic("/public/broadcast/messages")
                            .WithPayload(end)
                            .Build();

                        MqttServerData.Instance.Server.PublishAsync(message).Wait();
                    }
                }

            }).Start();
        }
    }
}
