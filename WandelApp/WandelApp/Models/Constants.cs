using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WandelApp.Models
{
    public class Constants
    {
        // This class contains all static, constant data used by other classes.
        // Edit this to your Conveyor local IP, edit this later to IP of live server.
        // Roy: 192.168.2.151
        // Justin: 192.168.56.1
        public const string ApiAddress = "http://192.168.56.1:45455/api";
    }
}
