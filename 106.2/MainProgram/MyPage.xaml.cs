using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static _106._2.MainProgram.MyPage;

namespace _106._2.MainProgram
{
    /// <summary>
    /// Interaction logic for MyPage.xaml
    /// </summary>
    public partial class MyPage : Page
    {
        /// <summary>
        /// Connection to sql database;
        /// </summary>
        public static NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
        public class Member
        {
            public string Name { get; set; }
            public int Number { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public DateTime JoinDate { get; set; }
            public string Address { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public bool IsAdmin { get; set; }
        }
         public ObservableCollection<Member> Members { get; set; }
        public class MemberViewModel
        {
            public ObservableCollection<Member> Members { get; set; }

            public MemberViewModel(string ID)
            {
                string command = $"SELECT *, logins.* FROM members INNER JOIN logins ON logins.userid = members.number Where number = {ID} ORDER BY number LIMIT  1 ";
                Members = new ObservableCollection<Member>();
                try
                {
                    // Open the connection
                    SqlCONN.Open();

                    // Execute the query
                    using (NpgsqlCommand CMD = new NpgsqlCommand(command, SqlCONN))
                    {
                        using (NpgsqlDataReader reader = CMD.ExecuteReader())
                        {
                            // Iterate through the results and populate the Books collection
                            while (reader.Read())
                            {

                                Members.Add(new Member
                                {
                                    Name = reader.GetString("name"),
                                    Number = reader.GetInt32("number"),
                                    PhoneNumber = reader.GetString("phonenumbers"),
                                    Email = reader.GetString("email"),
                                    JoinDate = reader.GetDateTime("joindate"),
                                    Address = reader.GetString("address"),
                                    Username = reader.GetString("username"),
                                    Password = reader.GetString("userpassword"),
                                    IsAdmin = reader.GetBoolean("isadmin")


                                });

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception details to the console
                    Console.WriteLine($"Error: {ex}");

                    // Display an error message to the user
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    // Close the connection even if an exception occurs
                    SqlCONN.Close();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">User/Admin's ID number</param>
        public MyPage(string ID)
        {
            InitializeComponent();
          string  command = "SELECT "
                       + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                       + "book.bookname,book.author,book.genre,book.image,"
                       + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.duedate,booklog.holdid,"
                       + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                       + "booklog.issuedid,"
                       + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                       + " FROM book"
                       + " INNER JOIN booklog ON booklog.bookid = book.bookid" 
                       +$" WHERE booklog.issuedid  = {ID} OR booklog.holdid = {ID}";
            DataTable BookTable = LoadData(command);
            books.ItemsSource = BookTable.DefaultView;
            this.DataContext = new MemberViewModel(ID);

        }
        public DataTable LoadData(string command)
        {
            try 
            {
                
           
                NpgsqlCommand cmd = new NpgsqlCommand(command, SqlCONN);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Dispose();
                adapter.Fill(dt);
                return dt; 
            }
            catch (Exception ex) 
            {
            }
            DataTable dt2 = new DataTable();  
            return dt2;
        }
    }
}
