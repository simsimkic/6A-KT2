using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace InitialProject.Repository
{
    public  class AccommodationImageRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationImages.txt";

        private readonly Serializer<AccommodationImage> _serializer;

        private List<AccommodationImage> _accommodationImages;


        public AccommodationImageRepository()
        {
            _serializer = new Serializer<AccommodationImage>();
            _accommodationImages= new List<AccommodationImage>();
        }

        public List<AccommodationImage> getAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationImage Save(AccommodationImage accommodationImages,int resourceId)
        {
            accommodationImages.ImageId = NextId();
            accommodationImages.ResourceId = resourceId;
            _accommodationImages.Add(accommodationImages);
            _serializer.ToCSV(FilePath, _accommodationImages);
            return accommodationImages;

        }

        public int NextId()
        {
            _accommodationImages = _serializer.FromCSV(FilePath);
            if (_accommodationImages.Count < 1)
            {
                return 1;
            }
            return _accommodationImages.Max(acm => acm.ImageId) + 1;
        }
    }
}
