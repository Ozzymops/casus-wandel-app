﻿using System;
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
            Id = 0;
            OwnerId = 0;
            Length = 0;
            HillType = HillType.None;
            Marshiness = false;
            ForestDensity = ForestDensity.None;
            RouteFlatness = RouteFlatness.Flat;
            RouteAsphalted = false;
            RoadSigns = RoadSigns.None;
        }

        /// <summary>
        /// Calculate difficulty based on given Preferences
        /// Maximum score is 10 --> very hard route.
        /// Minimum score = 1 --> very easy route.
        /// </summary>
        /// <param name="route"></param>
        /// <returns>double Difficulty</returns>
        public int CalculateDifficulty(Preferences preferences)
        {
            int difficulty = 0;

            try
            {
                // Length
                if (preferences.Length > 0 && preferences.Length <= 10)
                {
                    difficulty += 1;
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
                    difficulty += 1;
                }
                else if (preferences.ForestDensity == ForestDensity.Thick)
                {
                    difficulty += 2;
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
                    difficulty += 1;
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
