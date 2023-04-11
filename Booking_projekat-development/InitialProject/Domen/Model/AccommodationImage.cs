using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InitialProject.Serializer;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class AccommodationImage : ISerializable
    {
        
        public int ImageId { get; set; }
        public string Url { get; set; }
        public int ResourceId { get; set; }



        public AccommodationImage()
        {
            ImageId = 0;
            ResourceId = 0;
        }

        public AccommodationImage(int id,string url,int resourceId)
        {
            ImageId= id;
            Url= url;
            ResourceId= resourceId;

            
        }

        public void FromCSV(string[] values)
        {
            ImageId = Convert.ToInt32(values[0]);
            ResourceId = Convert.ToInt32(values[1]);
            Url = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ImageId.ToString(),
                ResourceId.ToString(),
                Url

                
            };
            return csvValues;
        }
    }
}
