using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet.Client;

namespace ChatMobile
{
    class MqttClientData
    {
        #region Singleton

        private static volatile MqttClientData _instance;
        private static object syncRoot = new object();

        public static MqttClientData Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new MqttClientData();
                    }
                }

                return _instance;
            }
        }

        #endregion

        public MqttClientData() { }

        public IMqttClient Server;
    }
}
