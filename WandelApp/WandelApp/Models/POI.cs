using System;
using System.Collections.Generic;
using System.Text;

namespace WandelApp.Models
{
    public class POI
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StepNumber { get; set; }
        public decimal Long { get; set; }
        public decimal Lat { get; set; }
    }
}
