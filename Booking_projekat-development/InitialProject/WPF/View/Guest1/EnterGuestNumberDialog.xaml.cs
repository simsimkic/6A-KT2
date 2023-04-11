using System;
using System.Collections.Generic;
using System.Linq;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for EnterGuestNumberDialog.xaml
    /// </summary>
    public partial class EnterGuestNumberDialog : Window
    {
        public int NumberOfGuests {get; set; }
        public int MaxAccommodationGuestsNumber { get; set; }
        public EnterGuestNumberDialog(int maxAccommodationGuestsNumber)
        {
            InitializeComponent();
            MaxAccommodationGuestsNumber = maxAccommodationGuestsNumber;
        }

        private void ReserveButtonClick(object sender, RoutedEventArgs e)
        {
            NumberOfGuests = Convert.ToInt32(Input1.Text);
            if (NumberOfGuests > MaxAccommodationGuestsNumber)
            {
                MessageBox.Show("Number of guests must be bellow " + MaxAccommodationGuestsNumber.ToString());
            }
            else
            {
                this.Close();
            }
        }
    }
}
