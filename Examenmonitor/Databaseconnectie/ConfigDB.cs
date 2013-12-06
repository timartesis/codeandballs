using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examenmonitor
{
    public class ConfigDB
    {
        private static string pad = AppDomain.CurrentDomain.BaseDirectory + "\\Database\\db";

        public static string getPad()
        {
            return pad;
        }

    }
}