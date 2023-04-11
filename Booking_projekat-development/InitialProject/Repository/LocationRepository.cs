using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    internal class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.txt";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }
        public List<Location> getAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Location Save(Location locations)
        {
            _locations.Add(locations);
            _serializer.ToCSV(FilePath, _locations);
            return locations;
        }
       
        public void Delete(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location foundedLocation = _locations.Find(loc => loc.City == location.City && location.Country == location.Country);
            _locations.Remove(foundedLocation);
            _serializer.ToCSV(FilePath, _locations);
        }

        public Location GetById(int id)
        {
            return _locations.Find(l => l.Id == id);
        }
    }
}
