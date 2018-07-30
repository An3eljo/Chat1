using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Server;

namespace ChatService
{
    class Program
    {
        static void Main(string[] args)
        {
            var mqttServer = new MqttFactory().CreateMqttServer();
            var options = new MqttServerOptionsBuilder().WithDefaultEndpointPort(1884).Build();

            Task.Run(async () => await mqttServer.StartAsync(options));

            mqttServer.ApplicationMessageReceived += (sender, e) =>
            {
                Console.WriteLine("Message recceived from " + e.ClientId);
                Console.WriteLine("-------------------");

                Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                Console.WriteLine("In " + e.ApplicationMessage.Topic);
                Console.WriteLine("-------------------");
                Console.WriteLine();
            };

            mqttServer.ClientConnected += (sender, e) =>
            {
                Console.WriteLine(e.ClientId + " connected!");
            };
            
            Console.WriteLine("Press any key to exit.");

            new Thread(() =>
            {
                var shouldRepeat = true;
                while (shouldRepeat)
                {
                    var end = Console.ReadLine();
                    if (end == "end")
                    {
                        shouldRepeat = false;
                        Task.Run(async () => await mqttServer.StopAsync());
                    }
                    else
                    {
                        var message = new MqttApplicationMessageBuilder().WithTopic("TestTopic").WithPayload("testmessageServer").Build();
                        mqttServer.PublishAsync(message).Wait();
                    }
                }
                
            }).Start();
        }
    }
}
