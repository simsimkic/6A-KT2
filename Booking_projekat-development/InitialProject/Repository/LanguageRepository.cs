using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class LanguageRepository
    {
        private const string FilePath = "../../../Resources/Data/languages.txt";

        private readonly Serializer<Language> _serializer;

        private List<Language> _comments;

        public LanguageRepository()
        {
            _serializer = new Serializer<Language>();
            _comments = _serializer.FromCSV(FilePath);
        }


        public List<Language> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

    }
}
