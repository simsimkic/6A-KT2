using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Language : ISerializable
    {
        public string Name { get; set; }

        public Language(string language)
        {
            Name = language;
        }
        public Language()
        {
            Name = "";
        }

        public override string ToString()
        {
            return Name;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Name
            };

            return csvValues;
        }

        public Language fromStringToLanguage(string s)
        {
            return new Language(s);
        }


        public void FromCSV(string[] values)
        {
            Name = values[0];
        }
    }
}
