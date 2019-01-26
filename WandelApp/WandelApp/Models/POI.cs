using System;
using System.Collections.Generic;
using System.Text;

namespace WandelApp.Models
{
    public class POI
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Long { get; set; }
        public decimal Lat { get; set; }
    }
}
