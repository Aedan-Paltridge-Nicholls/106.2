using Npgsql;
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

namespace _106._2
{
    /// <summary>
    /// Interaction logic for UpdateloginPopup.xaml
    /// </summary>
    public partial class UpdateloginPopup : Window
    {
        
        public UpdateloginPopup()
        { 
            InitializeComponent();
           
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            IdNumberBOX.Text = member_IdBox;
        }
        public static NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
        public string member_IdBox {  get; set; }
        private void update_Click_1(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection conn = SqlCONN;
            conn.Open();
            string idnumber, number = IdNumberBOX.Text, Name = txtUser.Text.Trim(), password = txtPassword.Password.Trim();
            int Removetext = number.IndexOf(':') ;
            idnumber = number.Remove(0, (Removetext + 4));
            if (txtUser.Text != null || txtPassword.Password != null)
            {
                string loginQuery = " UPDATE  logins" +
                                   $" SET  username = \'{Name}\', userpassword = \'{password}\', isadmin = \'{admin.ToString()}\' " +
                                   $" WHERE userid =  {idnumber}  ";
                NpgsqlCommand command = new NpgsqlCommand(loginQuery, conn);
                command.ExecuteNonQuery();
            }
            else
            {
                string message = $"You Must enter a Username and Password \nfor user number : {idnumber} ",
                       caption = "Error Must give Login";
                Xceed.Wpf.Toolkit.MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Stop);
                conn.Close();
                return;
            }
             conn.Close();
             this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public bool admin = false; 
        private void IsAdminCheckBox_Checked(object sender, RoutedEventArgs e)
        {
           admin = true;
            IdNumberBOX.Text = member_IdBox;

        }

        private void IsAdminCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
          admin = false;
        }
    }
}
