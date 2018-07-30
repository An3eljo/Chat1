using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet;
using MQTTnet.Client;

namespace ChatMobile
{
    class Init
    {
        public static void Initialize()
        {
            StartClient();
        }

        private static void StartClient()
        {
            MqttClientData.Instance.Server = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId(1.ToString())
                .WithTcpServer("192.168.1.105", 1884)
                .Build();

            MqttClientData.Instance.Server.ConnectAsync(options);

            //MqttClientData.Instance.Server.SubscribeAsync("TestTopic").Wait();

            MqttClientData.Instance.Server.ApplicationMessageReceived += (sender, e) =>
            {
                var t = e.ClientId + "has written: " + Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            };

            //var message = new MqttApplicationMessageBuilder().WithTopic("TestTopic").WithPayload("testmessageClient").Build();

            //MqttClientData.Instance.Server.PublishAsync(message).Wait();
        }
    }
}
