using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace _106._2
{
    /// <summary>
    /// Interaction logic for AdminLoginView.xaml
    /// </summary>
    public partial class AdminLoginView : Page
    {
        public class memberdata 
        {
            string number, name, phonenumbers, email, joindate, address;

        } 

        public AdminLoginView()
        {
            InitializeComponent();

            string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM Members"; // Replace with your SQL query
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    membersdatagrid.ItemsSource = dt.DefaultView; // Replace 'dataGrid' with your DataGrid's name
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            
        }
        private void AddMemberBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string comand = "  insert into members (number, name, phonenumbers, email, joindate, address) values (2, 'Krysta Rizziello', '581-231-2360', 'krizziello1@nhs.uk', '2022-11-25', '2890 1st Parkway');";


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
