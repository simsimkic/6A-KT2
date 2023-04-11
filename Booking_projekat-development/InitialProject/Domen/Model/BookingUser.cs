using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public enum UserType { Owner = 0, Guest1, Guest2, Guide }
    public class BookingUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        internal UserType UserType { get; set; }

        public BookingUser(int userId, string username,
            string password, string email,
            string phoneNumber, UserType userType)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            UserType = userType;
        }
        public BookingUser()
        {
            UserId = 0;
            Username = "";
            Password = "";
            Email = "";
            PhoneNumber = "";
            UserType = 0;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                UserId.ToString(),
                Username,
                Password,
                Email,
                PhoneNumber,
                UserType.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            UserId = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Email = values[3];
            PhoneNumber = values[4];
            UserType = (UserType)Enum.Parse(typeof(UserType), values[5]);
        }
    }
}
