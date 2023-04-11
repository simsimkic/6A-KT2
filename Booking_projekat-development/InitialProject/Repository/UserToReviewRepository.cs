using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class UserToReviewRepository
    {

        private const string FilePath = "../../../Resources/Data/userstoreview.txt";

        private readonly Serializer<UserToReview> _serializer;

        private List<UserToReview> UsersToReview;

        public UserToReviewRepository()
        {
            _serializer = new Serializer<UserToReview>();
            UsersToReview = _serializer.FromCSV(FilePath);
        }

        public List<UserToReview> GetByOwnerId(int ownerId)
        {
            List<UserToReview> users = new List<UserToReview>();
            UsersToReview = _serializer.FromCSV(FilePath);
            users.AddRange(UsersToReview.Where(u => u.OwnerId == ownerId));
            return users;
        }

        public List<UserToReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public UserToReview Save(UserToReview user)
        {
            UsersToReview.Add(user);
            _serializer.ToCSV(FilePath, UsersToReview);
            return user;
        }
        public void DeleteByIdAndDate(int Guest1Id, DateTime date)
        {
            UsersToReview = _serializer.FromCSV(FilePath);
            UserToReview userToRemove = UsersToReview.Find(u => u.Guest1Id == Guest1Id && u.LeavingDay == date);
            UsersToReview.Remove(userToRemove);
            _serializer.ToCSV(FilePath, UsersToReview);
        }
    }
}
