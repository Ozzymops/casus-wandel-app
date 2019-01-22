using System;
using System.Collections.Generic;
using System.Text;

namespace WandelApp.Models
{
    public class CalculateDifficulty
    {
        /// <summary>
        /// Calculate difficulty based on given Route or Preferences.
        /// Maximum score is 10 --> very hard route.
        /// Minimum score = 1 --> very easy route.
        /// </summary>
        /// <param name="route"></param>
        /// <returns>double Difficulty</returns>
        public double Difficulty(Route route)
        {
            double difficulty = 0;

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

            return difficulty;
        }

        public double Difficulty(Preferences preferences)
        {
            double difficulty = 0;

            // Length
            if (preferences.Length > 0 && preferences.Length <= 5)
            {
                difficulty += 1;
            }
            else if (preferences.Length > 5 && preferences.Length <= 10)
            {
                difficulty += 1.5;
            }
            else if (preferences.Length > 10)
            {
                difficulty += 2;
            }

            // Enums:
            // HillType
            if (preferences.HillType == HillType.Sloped)
            {
                difficulty += 1;
            }
            else if (preferences.HillType == HillType.Steep)
            {
                difficulty += 2;
            }

            // ForestDensity
            if (preferences.ForestDensity == ForestDensity.Thin)
            {
                difficulty += 0.5;
            }
            else if (preferences.ForestDensity == ForestDensity.Thick)
            {
                difficulty += 1;
            }

            // RouteFlatness
            if (preferences.RouteFlatness == RouteFlatness.Bumpy)
            {
                difficulty += 1;
            }

            // RoadSigns
            if (preferences.RoadSigns == RoadSigns.Some)
            {
                difficulty += 1;
            }
            else if (preferences.RoadSigns == RoadSigns.None)
            {
                difficulty += 2;
            }

            // Bool:
            // Marshiness
            if (preferences.Marshiness)
            {
                difficulty += 1;
            }

            // RouteAsphalted
            if (!preferences.RouteAsphalted)
            {
                difficulty += 0.5;
            }

            return difficulty;
        }
    }
}
