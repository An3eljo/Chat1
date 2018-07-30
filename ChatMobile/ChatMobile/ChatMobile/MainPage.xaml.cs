using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using Xamarin.Forms;
using System.Globalization;
using ChatMobile.Languages;

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
		    Init.Initialize();
        }

	    private void Test()
	    {
	        
	    }
	}
}
