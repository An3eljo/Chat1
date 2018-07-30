using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using Xamarin.Forms;

namespace ChatMobile
{
	public partial class MainPage : ContentPage
	{
	    private string _message1;

	    public string Message1
	    {
	        get { return _message1; }
	        set
	        {
	            _message1 = value;
	            OnPropertyChanged();
	        }
	    }

	    public MainPage()
		{
			InitializeComponent();
		    Test();
        }

	    private void Test()
	    {
	        var client = new MqttFactory().CreateMqttClient();

	        var options = new MqttClientOptionsBuilder().WithClientId("testuser1").WithTcpServer("192.168.43.91", 1884)
	            .Build();

	        client.ConnectAsync(options).Wait();
            //Task.Run(async () => await client.ConnectAsync(options));

	        client.SubscribeAsync("TestTopic").Wait();

	        client.ApplicationMessageReceived += (sender, e) =>
	        {
	            Message1 = e.ClientId + "has written: " + Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
	        };

            var message = new MqttApplicationMessageBuilder().WithTopic("TestTopic").WithPayload("testmessageClient").Build();

	        client.PublishAsync(message).Wait();

            //Task.Run(async () => await client.PublishAsync(message));
            //Task.Run(
            //    async () => await client.SubscribeAsync(new TopicFilterBuilder().WithTopic("testmessage1").Build()));

	        

	        //client.DisconnectAsync().Wait();
	        //Task.Run(async () => await client.DisconnectAsync());
	    }
	}
}
