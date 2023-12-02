using _106._2.MainProgram.Homepage;
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

namespace _106._2.MainProgram.User
{
    /// <summary>
    /// Interaction logic for UserLoginView.xaml
    /// </summary>
    public partial class UserLoginView : Window
    {

        public UserLoginView(string userid )
        {
            InitializeComponent();
            UserId = userid; 

            Main.Content = new Home();
        }
        public static string UserId { get; set; }
        private void BookButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
          //  UserBookView userBookView = new UserBookView(UserId);
          // Main.Content = userBookView;
        }

        private void HomeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Home();

        }

        private void DuedateButton_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            login loginwimdow = new login();
            loginwimdow.Show();
            this.Close();
        }

        
    }
}
