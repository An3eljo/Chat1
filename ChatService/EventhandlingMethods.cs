using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Server;

namespace ChatService
{
    class EventhandlingMethods
    {
        public void OnApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("Message received from " + e.ClientId);
            Console.WriteLine("-------------------");

            Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
            Console.WriteLine("In " + e.ApplicationMessage.Topic);
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        public void OnClientConnect(object sender, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine(e.ClientId + " connected!");
        }

        public void OnClientSubscribe(object sender, MqttClientSubscribedTopicEventArgs e)
        {
            //todo
        }
    }
}
