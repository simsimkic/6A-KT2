namespace InitialProject.Model
{
    using InitialProject.Serializer;
    using System;
    using System.Collections.Generic;

    public class TourRate : ISerializable
    {
        public TourRate() { }
        public int GuestId { get; set; }
        public int TourId { get; set; }
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int TourInterest { get; set; }
        public string? Comment { get; set; }
        public List<string>? Images { get; set; }

        public TourRate(int guestId, int tourId, int guideKnowledge, int guideLanguage, int tourInterest, string? comment, List<string>? images)
        {
            GuestId = guestId;
            TourId = tourId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourInterest = tourInterest;
            Comment = comment;
            Images = images;
        }

        public string[] ToCSV()
        {

            string[] csvValues = {
                GuestId.ToString(),
                TourId.ToString(),
                GuideKnowledge.ToString(),
                GuideLanguage.ToString(),
                TourInterest.ToString(),
                Comment,
                ImagesListToString()

            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            GuideKnowledge = Convert.ToInt32(values[2]);
            GuideLanguage = Convert.ToInt32(values[3]);
            TourInterest = Convert.ToInt32(values[4]);
            Comment = values[5];
            Images = StringToImagesList(values[6]);
        }

        private string ImagesListToString()
        {
            string s = "";
            foreach (string image in Images)
            {
                if (Images.IndexOf(image) == Images.Count - 1)
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
