namespace InitialProject.Services
{
    using InitialProject.Model;
    using InitialProject.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourPointService
    {
        private TourPointRepository _tourPointRepository;
        private List<TourPoint> _tourPoints;

        public TourPointService()
        {
            _tourPointRepository = new TourPointRepository();
            _tourPoints = new List<TourPoint>(_tourPointRepository.GetAll());
        }

        public bool TourStartedAndFinished(int tourId)
        {
            return _tourPoints.Where(t=> t.TourId==tourId).Where(t => t.CurrentStatus == TourPoint.Status.Active || t.CurrentStatus == TourPoint.Status.NotActive).Count() == 0;
        }

        public TourPoint GetActiveTourPointOnTour(int tourId)
        {
           return _tourPoints.Where(t => t.TourId == tourId).Where(t => t.CurrentStatus == TourPoint.Status.Active).FirstOrDefault();
        }
    }
}
