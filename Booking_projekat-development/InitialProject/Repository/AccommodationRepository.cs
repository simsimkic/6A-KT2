using InitialProject.CustomClasses;
using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.txt";

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = new List<Accommodation>();
        }
        public List<Accommodation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Accommodation Save(Accommodation accommodation)
        {
             accommodation.AccommodationID = NextId();
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }
        public List<Location> GetAllLocationsFromAccommodations()
        {
            List<Location> locations = new List<Location>();
            _accommodations = _serializer.FromCSV(FilePath);
            foreach(Accommodation a in _accommodations)
            {
                locations.Add(a.Location);
            }
            return locations;
        }
        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if(_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(acm => acm.AccommodationID) + 1;
        }

        public int GetLastAccommodationId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(acm => acm.AccommodationID);

        }
        public void Delete(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation foundedAccommodation = _accommodations.Find(acm => acm.AccommodationID == accommodation.AccommodationID);
            _accommodations.Remove(foundedAccommodation);
            _serializer.ToCSV(FilePath, _accommodations);
        }
        public Accommodation Update(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation current = _accommodations.Find(acm => acm.AccommodationID == accommodation.AccommodationID);
            int index = _accommodations.IndexOf(current);
            _accommodations.Remove(current);
            _accommodations.Insert(index, accommodation);   
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }
    }
}
