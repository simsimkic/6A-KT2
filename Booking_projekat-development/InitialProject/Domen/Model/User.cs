using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
    public class User : ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType TypeOfUser { get; set; }
        public User(string name, string username, string email, string password, UserType typeOfUser)
        {
            Name = name;
            Username = username;
            Email = email;
            Password = password;
            TypeOfUser = typeOfUser;
        }
        public User() 
        {
            Id = -1;
            Name = "";
            Username = "";
            Email = "";
            Password = "";
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                Username,
                Email,
                Password,
                TypeOfUser.ToString(),    
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Username = values[2];
            Email = values[3];
            Password = values[4];
            TypeOfUser = (UserType)Enum.Parse(typeof(UserType), values[5]);
        }
    }
}
