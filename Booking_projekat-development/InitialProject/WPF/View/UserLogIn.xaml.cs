using InitialProject.Model;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.CustomClasses;
using InitialProject.Repository;
using InitialProject.Domen.RepositoryInterfaces;
using System.Collections.ObjectModel;
using InitialProject.View.Guest1;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserLogIn : Window, INotifyPropertyChanged
    {
        private UserRepository _userRepository;
        public string Email { get; set; }
        public string Password { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public static ObservableCollection<Location> Locations { get; set; }
        private readonly AccommodationRepository _accommodationRepository;



        public UserLogIn()
        {
            InitializeComponent();
            DataContext = this;
            _userRepository = new UserRepository();
            _accommodationRepository = new AccommodationRepository();
            Locations = new ObservableCollection<Location>(_accommodationRepository.GetAllLocationsFromAccommodations());

        }
        private void Btn_LogIn(object sender, RoutedEventArgs e)
        {
            List<User> users = _userRepository.GetAllUsers();
            bool match = false;
            foreach(User user in users)
            {
                if(user.Email == Email && user.Password == Password)
                {
                    match = true;
                    StartWindow startWindow = new StartWindow(user.Id);
                    this.Close();
                    ChooseWindow(user);
                    break;
                }
            }
            if (!match) MessageBox.Show("User don't exist.");
        }
        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void ChooseWindow(User user)
        {
            switch (user.TypeOfUser)
            {
                case (UserType.Guest1):
                    Guest1Window window = new Guest1Window(user.Id, Locations);
                    window.Show();
                    this.Close();
                    break;
                default:
                    StartWindow startWindow = new StartWindow(user.Id);
                    startWindow.Show();
                    this.Close();
                    break;
                
            }
        }
    }
}
