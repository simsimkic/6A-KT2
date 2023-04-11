using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class GuestReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/guestReviews.txt";

        private readonly Serializer<GuestReview> _serializer;

        private List<GuestReview> _guestReviews;


        public GuestReviewRepository()
        {
            _serializer = new Serializer<GuestReview>();
            _guestReviews= _serializer.FromCSV(FilePath);
        }

        public List<GuestReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GuestReview Save(GuestReview guestReview)
        {
            _guestReviews.Add(guestReview);
            _serializer.ToCSV(FilePath, _guestReviews);
            return guestReview;
        }

        public int NextId()
        {
            _guestReviews = _serializer.FromCSV(FilePath);
            if(_guestReviews.Count < 1)
            {
                return 1;
            }
            return _guestReviews.Max(gr => gr.GuestId) + 1;
        }
    }
}
