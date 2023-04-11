using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourImages : ISerializable
    {
        public int TourId { get; set; }
        public int ImageId { get; set; }
        public string Url { get; set; }




        public TourImages() { }

        public TourImages(int tourId, int id, string url)
        {
            TourId = tourId;
            ImageId = id;
            Url = url;



        }

        public void FromCSV(string[] values)
        {
            TourId= Convert.ToInt32(values[0]);
            ImageId = Convert.ToInt32(values[1]);
            Url = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                TourId.ToString(),
                ImageId.ToString(),
                Url,


            };
            return csvValues;
        }
    
    }
}
