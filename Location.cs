using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterApplication
{
    public class Location
    {
        public double _lat { get; set; }
        public double _long { get; set; }
        public int _radius { get; set; }
        public Location(double lat,double longi, int radius)
        {
            _lat = lat;
            _long = longi;
            _radius = radius; 
        }
    }
}
