using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InitialProject.Serializer;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Model
{
    public class Location : ISerializable
    {
        public int Id { get; set;}
        public string Country { get; set; }
        public string City { get; set; }
        

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }
        public Location()
        {
            City = "";
            Country = "";
        }

        

        public override string ToString()
        {
            return City + ";" + Country;
        }
        public string ToString2()
        {
            return City + " " + Country;
        }
        public Location fromStringToLocation(string s)
        {
            string[] locations = new string[2];
            locations = s.Split(';');
            return new Location(locations[0], locations[1]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                City,
                Country};
            
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }
    }
}
