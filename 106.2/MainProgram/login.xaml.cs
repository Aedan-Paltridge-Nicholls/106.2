using System;
using Npgsql;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using _106._2.MainProgram.User;

namespace _106._2
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }
        public static string LoginId {  get; set; }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM logins WHERE userpassword = \'{Password.Password.Trim()}\' AND username = \'{Username.Text.Trim()}\' ", SqlCONN);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0 ) 
            {           
                DataRow dataRow = dataTable.Rows[0];

                MainWindow mainWindow = new MainWindow();
                                        
                string IsAdmin = dataRow[3].ToString();
                if (IsAdmin.ToLower() == "true")
                {
                    LoginId = dataRow[0].ToString();
                    mainWindow = new MainWindow();
                    mainWindow.Show (); 
                    this.Close();
                }
                else if (IsAdmin.ToLower() == "false") 
                {
                    LoginId = dataRow[0].ToString();
                    UserLoginView userLoginView = new UserLoginView(LoginId);
                    userLoginView.Show();
                    this.Close();
                }
            }
            else 
            {
               
               MessageBox.Show("Incorect Login","Login fail", MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }
    }
}
