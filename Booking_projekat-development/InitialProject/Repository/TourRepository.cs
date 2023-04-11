using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace InitialProject.Repository
{

    public class TourRepository : ITourRepository

    {

        private const string FilePath = "../../../Resources/Data/tours.txt";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public Tour Save(Tour tour)
        {
            tour.TourId = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public void Delete(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour foundedTour = _tours.Find(t => t.TourId == tour.TourId);
            _tours.Remove(foundedTour);
            _serializer.ToCSV(FilePath, _tours);
        }

        public Tour GetById(int id)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour foundedTour = _tours.Find(t => t.TourId == id);
            return foundedTour;
        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(t => t.TourId) + 1;
        }
        public Tour Update(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(t => t.TourId == tour.TourId);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }
    }
}
