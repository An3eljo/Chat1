using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChatMobile
{
    class Data
    {
        #region Singleton

        private static volatile Data _instance;
        private static object syncRoot = new object();

        public static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new Data();
                    }
                }

                return _instance;
            }
        }

        #endregion

        public CultureInfo MyCultureInfo;
    }
}
