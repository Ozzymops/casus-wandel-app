using System;
using System.Collections.Generic;
using System.Text;

namespace WandelApp.Models
{
    public enum Hills { None, Sloped, Steep };
    public enum ForestDensity { None, Thin, Thick };

    public class Preferences
    {
        public Hills hills = Hills.None;
        public ForestDensity forestDensity = ForestDensity.Thick;
        public bool asphaltedRoad = true;

    }
}
