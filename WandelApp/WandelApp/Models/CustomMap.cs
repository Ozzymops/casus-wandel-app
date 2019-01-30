using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms.Maps;

namespace WandelApp.Models
{
    public class CustomMap : Map
    {
        Models.Logger l = new Models.Logger();
        public List<CustomPin> CustomPins { get; set; }

        public event EventHandler<MapTapEventArgs> Tapped;

        public CustomMap()
        {
            CustomPins = new List<CustomPin>();
        }

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

        public List<CustomPin> CreatePinList(Route route)
        {
            // Create CustomPins
            CustomPin startPin = new CustomPin()
            {
                Position = new Position((double)route.StartLat, (double)route.StartLong),
                Label = "s",
                Name = "Startpunt !",
                Description = "START: " + route.Name,
                ImageId = 0,
                Type = PinType.Generic
            };
            l.WriteToLog("StartPin gemaakt!");

            CustomPin endPin = new CustomPin()
            {
                Position = new Position((double)route.EndLat, (double)route.EndLong),
                Label = "e",
                Name = "Eindpunt !",
                Description = "EIND: " + route.Name,
                ImageId = 1,
                Type = PinType.Generic
            };
            l.WriteToLog("EindPin gemaakt!");

            List<CustomPin> sequenceList = new List<CustomPin>();
            if (route.SequenceList != null || route.SequenceList.Count != 0)
            {
                foreach (RouteSequence seq in route.SequenceList)
                {
                    CustomPin stepPin = new CustomPin()
                    {
                        Position = new Position((double)seq.Lat, (double)seq.Long),
                        Label = "st",
                        Name = "Stap " + seq.StepNumber + " !",
                        Description = "STAP: " + seq.StepNumber,
                        ImageId = 2,
                        Type = PinType.Generic
                    };
                    sequenceList.Add(stepPin);
                }
                l.WriteToLog("SequenceList gemaakt!");
            }

            List<CustomPin> poiList = new List<CustomPin>();
            if (route.POIList != null || route.POIList.Count != 0)
            {
                foreach (POI poi in route.POIList)
                {
                    CustomPin poiPin = new CustomPin()
                    {
                        Position = new Position((double)poi.Lat, (double)poi.Long),
                        Label = "p",
                        Name = "POI: " + poi.Name + " !",
                        Description = "DESC: " + poi.Description,
                        ImageId = 3,
                        Type = PinType.Generic
                    };
                    poiList.Add(poiPin);
                }
                l.WriteToLog("POIList gemaakt!");
            }

            // Create return list
            List<CustomPin> returnedList = new List<CustomPin>();
            returnedList.Add(startPin);
            returnedList.Add(endPin);

            foreach (CustomPin s in sequenceList)
            {
                returnedList.Add(s);
            }

            foreach (CustomPin p in poiList)
            {
                returnedList.Add(p);
            }

            l.WriteToLog("ReturnedList gemaakt!");

            return returnedList;
        }
    }

    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}
