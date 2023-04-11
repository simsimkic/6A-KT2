namespace InitialProject.Services
{
    using InitialProject.Model;
    using InitialProject.Repository;

    public class TourRateService
    {
        private readonly TourRateRepository _repository;
        private TourAttendanceService _tourAttendanceService;

        public TourRateService()
        {
            _repository = new TourRateRepository();
            _tourAttendanceService = new TourAttendanceService();
        }

        public void MakeTourRate(TourRate tourRate)
        {
            _repository.Save(tourRate);
            _tourAttendanceService.AddedComment(tourRate.GuestId, tourRate.TourId);
        }
    }
}
