using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using InitialProject.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourPoint : ISerializable
    {

        public enum Status { Active = 0, NotActive = 1, Finished = 2, ForceFisnihed = 3}
        public int Id { get; set; }
        public string Name { get; set; }
        public int TourId { get; set; }
        public Status CurrentStatus { get; set; } = Status.NotActive;
        public string Description { get; set; }
        public int Order { get; set; } = 0;
        

        public TourPoint()
        {
            

        }

        public TourPoint(string tourPointName, Status currentActive, string description)
        {
            Name = tourPointName;
            CurrentStatus = currentActive;
            Description = description;

        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                TourId.ToString(),
                CurrentStatus.ToString(),
                Description,
                Order.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            TourId= Convert.ToInt32(values[2]);
            CurrentStatus = (Status)Enum.Parse(typeof(Status), values[3]);
            Description = values[4];
            Order = Convert.ToInt32(values[5]);
        }

        
    }
}
