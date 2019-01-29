using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace WandelApp.Models
{
    [Table("Route")]
    public class Route : Preferences
    {
        public double Difficulty { get; set; }
        public string Name { get; set; }
        public decimal StartLong { get; set; }
        public decimal StartLat { get; set; }
        public decimal EndLong { get; set; }
        public decimal EndLat { get; set; }
        public List<POI> POIList { get; set; }
        public List<RouteSequence> SequenceList { get; set; }

        /// <summary>
        /// Calculate difficulty based on given Route.
        /// Maximum score is 10 --> very hard route.
        /// Minimum score = 1 --> very easy route.
        /// </summary>
        /// <param name="route"></param>
        /// <returns>double Difficulty</returns>
        public double CalculateDifficulty(Route route)
        {
            double difficulty = 0;

            try
            {
                // Length
                if (route.Length > 0 && route.Length <= 5)
                {
                    difficulty += 1;
                }
                else if (route.Length > 5 && route.Length <= 10)
                {
                    difficulty += 1.5;
                }
                else if (route.Length > 10)
                {
                    difficulty += 2;
                }

                // Enums:
                // HillType
                if (route.HillType == HillType.Sloped)
                {
                    difficulty += 1;
                }
                else if (route.HillType == HillType.Steep)
                {
                    difficulty += 2;
                }

                // ForestDensity
                if (route.ForestDensity == ForestDensity.Thin)
                {
                    difficulty += 0.5;
                }
                else if (route.ForestDensity == ForestDensity.Thick)
                {
                    difficulty += 1;
                }

                // RouteFlatness
                if (route.RouteFlatness == RouteFlatness.Bumpy)
                {
                    difficulty += 1;
                }

                // RoadSigns
                if (route.RoadSigns == RoadSigns.Some)
                {
                    difficulty += 1;
                }
                else if (route.RoadSigns == RoadSigns.None)
                {
                    difficulty += 2;
                }

                // Bool:
                // Marshiness
                if (route.Marshiness)
                {
                    difficulty += 1;
                }

                // RouteAsphalted
                if (!route.RouteAsphalted)
                {
                    difficulty += 0.5;
                }

            }
            catch (Exception e)
            {
                Models.Logger l = new Models.Logger();
                l.WriteToLog(e.ToString());
            }

            return difficulty;
        }
    }
}
