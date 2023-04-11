namespace InitialProject.Model
{
    using InitialProject.Serializer;
    using System;

    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime Received { get; set; }
        public DateTime ValidUntil { get; set; }

        public Voucher(int id, int guideId, int userId, string name, DateTime received, DateTime validUntil)
        {
            Id = id;
            GuideId = guideId;
            UserId = userId;
            Name = name;
            Received = received;
            ValidUntil = validUntil;
        }

        public Voucher() { }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuideId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            Name = values[3];
            Received = Convert.ToDateTime(values[4]);
            ValidUntil = Convert.ToDateTime(values[5]);


        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                GuideId.ToString(),
                UserId.ToString(),
                Name,
                Received.ToString(),
                ValidUntil.ToString(),
            };
            return csvValues;
        }
    }


}
