using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttControl
{
    public class Topics
    {
        /// <summary>
        /// Shutdown Topic
        /// </summary>
        public static string Shutdown
        {
            get { return "/{0}/shutdown/"; }
        }

        public static string Restart
        {
            get { return "/{0}/restart/"; }
        }

        public static string Lock
        {
            get { return "/{0}/lock/"; }
        }

        public static string Suspend
        {
            get { return "/{0}/suspend/"; }
        }

        public static string Hibernate
        {
            get { return "/{0}/hibernate/"; }
        }

    }
}
