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
    /// Interaction logic for Addmemberpopup.xaml
    /// </summary>
    public partial class Addmemberpopup : Window
    {
        public Addmemberpopup()
        {
            InitializeComponent();
            
        }
        
      
        public NpgsqlConnection connection()
        {
            AdminLoginView AdminLoginView = new AdminLoginView();
            string connectionString = AdminLoginView.connectionString;
            return new NpgsqlConnection(connectionString); 
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; this.Close();
            
        }

        private void Addbutton_Click_1(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection conn = connection();
            string idnumber , number = IdNumberBOX.Text , Name = txtUser.Text,password = txtPassword.Text ;
            int Removetext = number.IndexOf(':') + 2;
            idnumber = number.Remove(0, Removetext);
            if (txtUser.Text != null || txtPassword.Text != null )
            {
                string loginQuery = "INSERT INTO Logins(Username,password,Userid)" +
                                   $"VALUES ({Name},{password},(SELECT number FROM members WHERE number = {number} ) )      ";
            }

            this.DialogResult = false; this.Close();


        }
    }
}
