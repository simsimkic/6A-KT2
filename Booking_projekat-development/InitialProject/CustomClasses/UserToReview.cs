using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Serializer;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.CustomClasses
{
    public class UserToReview: ISerializable
    {
        public int OwnerId { get; set; }
        public int Guest1Id { get; set; }
        public DateTime LeavingDay { get; set; }

        public UserToReview(int ownerId, int guest1Id, DateTime date)
        {
            OwnerId = ownerId;
            Guest1Id = guest1Id;
            LeavingDay = date;
        }
        public UserToReview() { }

        public string[] ToCSV()
        {

            string[] csvValues = {
                OwnerId.ToString(),
                Guest1Id.ToString(),
                LeavingDay.ToString(),

            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            OwnerId = Convert.ToInt32(values[0]);
            Guest1Id = Convert.ToInt32(values[1]);
            LeavingDay = Convert.ToDateTime(values[2]);
        }

    }
}
