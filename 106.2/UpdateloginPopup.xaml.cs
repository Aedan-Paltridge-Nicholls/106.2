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
     
        private void update_Click_1(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection conn = GlobalVariables.SqlCONN;
            conn.Open();
            string idnumber, number = IdNumberBOX.Text, Name = txtUser.Text, password = txtPassword.Text;
            int Removetext = number.IndexOf(':') + 2;
            idnumber = number.Remove(0, Removetext);
            if (txtUser.Text != null || txtPassword.Text != null)
            {
                string loginQuery = "UPDATE  logins" +
                                   $"SET ( Username = \'{Name}\',password = \'{password}\'," +
                                   $"WHERE (SELECT number FROM members WHERE number = {idnumber} ) ";
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
    }
}
