using System;
using System.Collections.Generic;
using System.Text;

namespace WandelApp.Models
{
    public class CalculateDifficulty
    {
        /// <summary>
        /// Difficulty calculation
        /// Enum index determines a certain difficulty number.
        /// All numbers get added up to one single number.
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>

        public int Difficulty(Route route)
        {
            int difficulty = 0;

            // Length
            if(route.Length > 0 && route.Length <= 5)
            {
                difficulty += 1;
            }
            else if (route.Length > 5 && route.Length <= 10)
            {
                difficulty += 2;
            }
            else if (route.Length > 10)
            {
                difficulty += 3;
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
                difficulty += 1;
            }
            else if (route.ForestDensity == ForestDensity.Thick)
            {
                difficulty += 2;
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
                difficulty += 1;
            }

            // RouteHardened
            if (!route.RouteHardened)
            {
                difficulty += 1;
            }

            // hard cap
            if (difficulty >= 10)
            {
                difficulty = 10;
            }

            return difficulty;
        }
    }
}
