using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Model
{
    public class OwnerRate: ISerializable
    {
        public int OwnerRateId { get; set; }
        public int ownerId { get; set; }
        public int AccommodationId { get; set; }
        public int userId { get; set; }
        public int CleanlinessRate { get; set; }
        public int CorrectnessRate { get; set; }
        public string? Comment { get; set; }
        public List<string>? Images { get; set; }
        public OwnerRate(int userId, int ownerId, int accommodationId, int cleanlinessRate, int correctnessRate, string? comment, List<string>? images)
        {
            this.userId = userId;
            this.ownerId = ownerId;
            AccommodationId = accommodationId;
            CleanlinessRate = cleanlinessRate;
            CorrectnessRate = correctnessRate;
            Comment = comment;
            Images = images;
        }
        public OwnerRate()
        {
            userId = -1;
            ownerId = -1;
            AccommodationId = -1;
            CleanlinessRate = 1;
            CorrectnessRate = 1;
            Comment = "";
            Images = new List<string>();
        }
        public string[] ToCSV()
        {

            string[] csvValues = {
                OwnerRateId.ToString(),
                ownerId.ToString(),
                userId.ToString(),
                AccommodationId.ToString(),
                CleanlinessRate.ToString(),
                CorrectnessRate.ToString(),
                Comment,
                ImagesListToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            OwnerRateId = Convert.ToInt32(values[0]);
            ownerId = Convert.ToInt32(values[1]);
            userId = Convert.ToInt32(values[2]);
            AccommodationId = Convert.ToInt32(values[3]);
            CleanlinessRate = Convert.ToInt32(values[4]);
            CorrectnessRate = Convert.ToInt32(values[5]);
            Comment = values[6];
            Images = StringToImagesList(values[7]);
        }
        private string ImagesListToString()
        {
            string s = "";
            foreach(string image in Images)
            {
                if(Images.IndexOf(image) == Images.Count - 1)
                {
                    s += image;
                }
                else
                {
                    s += image + ";";
                }
            }
            return s;
        }
        private List<string> StringToImagesList(string s)
        {
            return new List<string>(s.Split(";"));
        }
    }
}
