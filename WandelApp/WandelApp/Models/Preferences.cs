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
    }
}
