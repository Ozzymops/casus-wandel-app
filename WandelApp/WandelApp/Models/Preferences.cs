using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace WandelApp.Models
{
    /// <summary>
    /// Enums: these enums are used everywhere in the application.
    /// They determine the available settings for both Preferences and Routes.
    /// </summary>
    public enum HillType { None, Sloped, Steep };
    public enum ForestDensity { None, Thin, Thick };
    public enum RouteFlatness { Flat, Bumpy };
    public enum RoadSigns { None, Some, Many };

    [Table("Preferences")]
    public class Preferences
    {
        [PrimaryKey]public int Id { get; set; }
        public int OwnerId { get; set; }
        public decimal Length { get; set; }
        public HillType HillType { get; set; }
        public bool Marshiness { get; set; }
        public ForestDensity ForestDensity { get; set; }
        public RouteFlatness RouteFlatness { get; set; }
        public bool RouteAsphalted { get; set; }
        public RoadSigns RoadSigns { get; set; }

        // temp constructor
        public Preferences()
        {
            Id = 1;
            OwnerId = 1;
            Length = 5;
            HillType = HillType.None;
            Marshiness = false;
            ForestDensity = ForestDensity.Thick;
            RouteFlatness = RouteFlatness.Bumpy;
            RouteAsphalted = false;
            RoadSigns = RoadSigns.Many;
        }

        /// <summary>
        /// Calculate difficulty based on given Preferences
        /// Maximum score is 10 --> very hard route.
        /// Minimum score = 1 --> very easy route.
        /// </summary>
        /// <param name="route"></param>
        /// <returns>double Difficulty</returns>
        public double CalculateDifficulty(Preferences preferences)
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
