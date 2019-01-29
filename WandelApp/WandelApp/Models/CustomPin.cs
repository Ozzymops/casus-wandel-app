using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace WandelApp.Models
{
    public class CustomPin : Pin
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// 0: Start, 1: End, 2: Step, 3: POI
        /// </summary>
        public int ImageId { get; set; }
    }
}
