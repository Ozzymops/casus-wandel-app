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
        public int OwnerId { get; set; }
        public int StartLocation { get; set; }
        public int EndLocation { get; set; }
        public double Difficulty { get; set; }
        public decimal Length { get; set; }
        public HillType HillType { get; set; }
        public bool Marshiness { get; set; }
        public ForestDensity ForestDensity { get; set; }
        public RouteFlatness RouteFlatness { get; set; }
        public bool RouteAsphalted { get; set; }
        public RoadSigns RoadSigns { get; set; }
    }
}
