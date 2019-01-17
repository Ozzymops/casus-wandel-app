using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace WandelApp.Models
{
    [Table("Route")]
    public class Route
    {
        [PrimaryKey, NotNull] public int Id { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public int StartLocation { get; set; }
        public int EndLocation { get; set; }
        public int Difficulty { get; set; }
        public int Length { get; set; }
        public HillType HillType { get; set; }
        public bool Marshiness { get; set; }
        public ForestDensity ForestDensity { get; set; }
        public RouteFlatness RouteFlatness { get; set; }
        public bool RouteAsphalted { get; set; }
        public bool RouteHardened { get; set; }
        public RoadSigns RoadSigns { get; set; }

        // temp constructor
        public Route()
        {
            Id = 1;
            Name = "aaa route";
            StartLocation = 0;
            EndLocation = 0;
            Difficulty = 0;
            Length = 3;
            HillType = HillType.Steep;
            Marshiness = false;
            ForestDensity = ForestDensity.Thin;
            RouteFlatness = RouteFlatness.Bumpy;
            RouteAsphalted = true;
            RouteHardened = true;
            RoadSigns = RoadSigns.None;
        }
    }
}
