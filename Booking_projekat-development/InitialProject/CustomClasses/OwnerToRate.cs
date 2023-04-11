using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.CustomClasses
{
    public class OwnerToRate: ISerializable
    {
        public int OwnerId { get; set; }
        public int AccommodationId { get; set; }
        public int UserId { get; set; }
        public DateTime LeavingDay { get; set; }

       
        public OwnerToRate() { }

        public OwnerToRate(int ownerId, int accommodationId, int userId, DateTime leavingDay)
        {
            OwnerId = ownerId;
            AccommodationId = accommodationId;
            UserId = userId;
            LeavingDay = leavingDay;
        }

        public string[] ToCSV()
        {

            string[] csvValues = {
                OwnerId.ToString(),
                AccommodationId.ToString(),
                UserId.ToString(),
                LeavingDay.ToString(),

            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            OwnerId = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            LeavingDay = Convert.ToDateTime(values[3]);
        }
    }
}
