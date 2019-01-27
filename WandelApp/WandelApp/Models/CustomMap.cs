using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms.Maps;

namespace WandelApp.Models
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }

        public event EventHandler<MapTapEventArgs> Tapped;

        /// <summary>
        /// Allow user to pass coordinates on tap.
        /// </summary>
        /// <param name="coordinate"></param>
        public void OnTap(Position coordinate)
        {
            OnTap(new MapTapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap (MapTapEventArgs e)
        {
            var handler = Tapped;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class CustomPin : Pin
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}
