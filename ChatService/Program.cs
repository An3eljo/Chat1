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
    public class Program
    {
        public static void Main(string[] args)
        {
            Init.Initialize();
        }
    }
}
