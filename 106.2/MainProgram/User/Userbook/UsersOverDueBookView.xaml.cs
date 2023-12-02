using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _106._2.MainProgram.User.Userbook
{
    /// <summary>
    /// Interaction logic for UsersOverDueBookView.xaml
    /// </summary>
    public partial class UsersOverDueBookView : Page
    {
        private int specificBookId = 0;

        public ObservableCollection<Book> OverDueBooks { get; set; }

        public class Book
        {
            public string BookID { get; set; }
            public string ImagePath { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public object Genre { get; set; }

        }

        public UsersOverDueBookView(string userid)
        {
            InitializeComponent();


            // Initialize Books collection and populate it from the database

            OverDueBooks = new ObservableCollection<Book>();


            NpgsqlConnection connectionString = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");


            string OverDueCommand = $"SELECT book.*, booklog.* FROM booklog INNER JOIN book ON book.bookid = booklog.bookid WHERE issuedid = {userid} AND overdue = TRUE";
            /*
             * OverDue books 
             */
            try
            {
                // Open the connection
                connectionString.Open();

                // Execute the query
                using (NpgsqlCommand command = new NpgsqlCommand(OverDueCommand, connectionString))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the results and populate the Books collection
                        int i = 0;
                        while (reader.Read())
                        {
                            ++i;
                            OverDueBooks.Add(new Book
                            {
                                BookID = reader["bookid"].ToString(),
                                ImagePath = reader["image"].ToString(),
                                Title = reader["bookname"].ToString(),
                                Author = reader["author"].ToString(),
                                Genre = reader["genre"].ToString(),
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
                connectionString.Close();
            }
            // Set the DataContext to this instance
            DataContext = this;
        }
    }
}
