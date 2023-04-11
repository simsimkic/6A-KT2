namespace InitialProject.Repository
{
    using InitialProject.Model;
    using InitialProject.Serializer;
    using System.Collections.Generic;
    using System.Linq;

    public class TourRateRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRates.txt";
        private readonly Serializer<TourRate> _serializer;

        private List<TourRate> _tourRates;
        public TourRateRepository()
        {
            _serializer = new Serializer<TourRate>();
            _tourRates = new List<TourRate>(GetAll());
        }
        public List<TourRate> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public TourRate Save(TourRate TourRate)
        {
            _tourRates.Add(TourRate);
            _serializer.ToCSV(FilePath, _tourRates);
            return TourRate;
        }
    }
}
