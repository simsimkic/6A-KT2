using InitialProject.Serializer;
using System;

namespace InitialProject.CustomClasses
{
    public class TourAttendance : ISerializable
    {
        public enum AttendanceStatus { Present = 1, OnHold = 2, NotPresent = 3 }
        public int TourAttendanceId { get; set; }
        public int TourId { get; set; } 
        public int TourPointId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public AttendanceStatus UserAttended { get; set; }
        public bool CanGiveReview { get; set; }

        public TourAttendance(int tourAttendanceId, int tourId, int tourPointId, int userId, string username, AttendanceStatus status)
        {
            TourAttendanceId = tourAttendanceId;
            TourId = tourId;
            TourPointId = tourPointId;
            UserId = userId;
            Username = username;
            UserAttended = status;
            CanGiveReview = false;
        }


        public TourAttendance()
        {

        }

       
        public string[] ToCSV()
        {

            string[] csvValues = {
                TourAttendanceId.ToString(),
                TourId.ToString(),
                TourPointId.ToString(),
                UserId.ToString(),
                Username,
                UserAttended.ToString(),
                CanGiveReview.ToString()

            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            TourAttendanceId = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            TourPointId = Convert.ToInt32(values[2]);
            UserId = Convert.ToInt32(values[3]);
            Username = values[4];
            UserAttended = (AttendanceStatus)Enum.Parse(typeof(AttendanceStatus), values[5]);
            CanGiveReview = Convert.ToBoolean(values[6]);
        }
    }
}
