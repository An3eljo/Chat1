using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Server;

namespace ChatService
{
    class MqttServerData
    {
        #region Singleton

        private static volatile MqttServerData _instance;
        private static object syncRoot = new object();

        public static MqttServerData Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new MqttServerData();
                    }
                }

                return _instance;
            }
        }

        #endregion

        public MqttServerData() { }       

        public IMqttServer Server;

    }
}
