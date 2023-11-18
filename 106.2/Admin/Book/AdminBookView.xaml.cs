using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;

namespace _106._2.Admin.Book
{
    /// <summary>
    /// Interaction logic for AdminBookView.xaml
    /// </summary>
    public partial class AdminBookView : Page
    {
        public AdminBookView()
        {
            InitializeComponent();
            LoadDatagrid();
            LoadGenreBox();
            LoadSearchOptionBOX();
        }
        public void LoadDatagrid()
        {
            NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            string comm = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " inner join booklog on booklog.bookid = book.bookid"
                        + " order by Book_id;";
            NpgsqlCommand cmd = new NpgsqlCommand(comm, SqlCONN);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Booksdatagrid.ItemsSource = dt.DefaultView;
            Booksdatagrid.DataContext = dt;

        }
        public void LoadGenreBox()
        {
            NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            SqlCONN.Open();
            var cmdsql = new NpgsqlCommand("SELECT DISTINCT genre FROM book ", SqlCONN);
            using NpgsqlDataReader readerOne = cmdsql.ExecuteReader();
            List<string> Genres = new List<string>();
            while (readerOne.Read())
            {
                for (int i = 0; i < readerOne.FieldCount; i++)
                {
                    Genres.Add((readerOne.GetValue(i)).ToString());
                }
            }
           GenreOptionBOX.ItemsSource = Genres;
            SqlCONN.Close();
        }
        public void LoadSearchOptionBOX()
        {
            List<string> Searches = new List<string>();
           SearchOptionBOX.ItemsSource = Booksdatagrid.Columns.header;
          //  Searches = Booksdatagrid.Columns.ToList().ToString();
        }
        private void AddBookBUTTON_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BookIdnumberBOX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchOptionBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchBOX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TitleBOX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }





        private void AuthorBOX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

     
        

        private void GenreOptionBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WithdrawBookBUTTON_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeBookInfoBUTTON_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HoldBookBUTTON_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnBookBUTTON_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Booksdatagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            //SqlCONN.Open();
            //if (Booksdatagrid.SelectedIndex != 0)
            //{
            //    string number = Booksdatagrid.SelectedIndex.ToString(), seleceted = "";
            //    var cmdsql = new NpgsqlCommand($"SELECT * FROM members WHERE number  = {number} ", SqlCONN);
            //    using NpgsqlDataReader readerOne = cmdsql.ExecuteReader();
            //    while (readerOne.Read())
            //    {
            //        for (int i = 0; i < readerOne.FieldCount; i++)
            //        {
            //            seleceted += (readerOne.GetValue(i)).ToString() + '|';
            //        }
            //    }
            //    string[] strings = seleceted.Split('|');
            //    string OLDBookID = strings[0], OLDTitle = strings[1], OLDAuthor = strings[2], OLDGenre = strings[3], OLDDuedate = strings[4];
            //    string[] strings1 = OLDDuedate.Split(' ');
            //    OLDDuedate = strings1[0];

            //    GenreOptionBOX.Text = OLDGenre;
            //    .Text = OLDAuthor;
            //    .Text = OLDTitle;
            //    .Text = OLDBookID;
            //    .Set_joindate(OLDDuedate);

            //}
            //SqlCONN.Close();
        }

    }
}
