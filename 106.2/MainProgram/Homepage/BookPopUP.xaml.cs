using _106._2.Admin.Book;
using _106._2.MainProgram.Admin.Book;
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
using System.Windows.Shapes;

namespace _106._2.MainProgram.Homepage
{
    /// <summary>
    /// Interaction logic for BookPopUP.xaml
    /// </summary>
    public partial class BookPopUP : Window
    {
        public BookPopUP()
        {
            login login = new login();
            Userid = login.LoginId;
            InitializeComponent();
           

        }

        public string Userid {  get; set; }  

        public Book GetBook = new Book();   
        public class Book
        {
            public string BookID { get; set; }
            public string ImagePath { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }

        }
        private AdminBookView BookView { get; set; }
        public DataRow DataRow(string command)
        {
            NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            NpgsqlCommand cmd1 = new NpgsqlCommand(command, SqlCONN);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataRow dataRow = dt.Rows[0];
            return dataRow;
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            string MemberId = Userid;
            DataRow dataRow  =   DataRow($"Select * FROM members WHERE number = {MemberId}");

            MemberId = Userid;
            string
            MemerIDnumber = dataRow[0].ToString(),
            MemberName = dataRow[1].ToString();
           
            string BookInfo = $"Book ID Number :{GetBook.BookID} {Environment.NewLine} " +
                             $"Book Title: {GetBook.Title}   {Environment.NewLine}" +
                             $"Book Author: {GetBook.Author}  {Environment.NewLine}" +
                             $"Book Genre: {GetBook.Genre}  {Environment.NewLine}",
                   MemberInfo = $"Member ID Number :{MemerIDnumber} {Environment.NewLine} " +
                             $"Member Name: {MemberName}   {Environment.NewLine};";

            ReturnBookPopup returnBookPopup = new ReturnBookPopup();

            returnBookPopup.BookInfo.Text = BookInfo;
            returnBookPopup.MemberInfo.Text = MemberInfo;
            returnBookPopup.MemberId = MemberId;
            returnBookPopup.SelectedBookId = GetBook.BookID;
            if (returnBookPopup.ShowDialog() == true) { Close(); }
            
        }

        private void BorrowButton_Click(object sender, RoutedEventArgs e)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {

                try
                {

                    SqlCONN.Open();
                    WithdrawBookPopup withdrawBookPopup = new WithdrawBookPopup();
                    DataRow dataRow = DataRow($"Select * FROM members WHERE number = {Userid}");
                    string
                    MemerIDnumber = dataRow[0].ToString(),
                    MemberName = dataRow[1].ToString();
                    string BookInfo = $"Book ID Number :{GetBook.BookID} {Environment.NewLine} " +
                                     $"Book Title: {GetBook.Title}   {Environment.NewLine}" +
                                     $"Book Author: {GetBook.Author}  {Environment.NewLine}" +
                                     $"Book Genre: {GetBook.Genre}  {Environment.NewLine}",
                           MemberInfo = $"Member ID Number :{MemerIDnumber} {Environment.NewLine} " +
                                     $"Member Name: {MemberName}   {Environment.NewLine};";




                    withdrawBookPopup.BookInfo.Text = BookInfo;
                    withdrawBookPopup.MemberInfo.Text = MemberInfo;
                    withdrawBookPopup.MemberId = Userid;
                    withdrawBookPopup.SelectedBookId = GetBook.BookID;
                    withdrawBookPopup.ShowDialog();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { SqlCONN.Close(); }

            }
        }

        private void HoldButton_Click(object sender, RoutedEventArgs e)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {

                try
                {

                    SqlCONN.Open();
                    HoldBookPopup HoldBookPopup = new HoldBookPopup();
                    DataRow dataRow = DataRow($"Select * FROM members WHERE number = {Userid}");
                    string
                    MemerIDnumber = dataRow[0].ToString(),
                    MemberName = dataRow[1].ToString();
                    string BookInfo = $"Book ID Number :{GetBook.BookID} {Environment.NewLine} " +
                                     $"Book Title: {GetBook.Title}   {Environment.NewLine}" +
                                     $"Book Author: {GetBook.Author}  {Environment.NewLine}" +
                                     $"Book Genre: {GetBook.Genre}  {Environment.NewLine}",
                           MemberInfo = $"Member ID Number :{MemerIDnumber} {Environment.NewLine} " +
                                     $"Member Name: {MemberName}   {Environment.NewLine};";
                    HoldBookPopup.BookInfo.Text = BookInfo;
                    HoldBookPopup.MemberInfo.Text = MemberInfo;
                    HoldBookPopup.MemberId = Userid;
                    HoldBookPopup.SelectedBookId = GetBook.BookID;
                    HoldBookPopup.ShowDialog();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { SqlCONN.Close(); }

            }
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            Image.Source = new BitmapImage(new Uri(GetBook.ImagePath));
            Title.Text = GetBook.Title;
            Author.Text = GetBook.Author;
            Genre.Text = GetBook.Genre;

            string MemberId = Userid;

            DataRow dataRow = DataRow($"Select * FROM Booklog WHERE issuedid = {MemberId}");
            string
            bookissued = dataRow[7].ToString();
            ReturnButton.IsEnabled = (bookissued == MemberId) ? true : false;
            dataRow = DataRow($"Select onhold FROM Booklog WHERE bookid = {GetBook.BookID}");
            string
            Onhold = dataRow[0].ToString();
            HoldButton.IsEnabled = (Onhold == "False") ? true : false;
            dataRow = DataRow($"Select withdrawn FROM Booklog WHERE bookid = {GetBook.BookID}");
            string
            Withdrawn = dataRow[0].ToString();
            BorrowButton.IsEnabled = (Withdrawn == "False") ? true : false;
        }
    }
}
