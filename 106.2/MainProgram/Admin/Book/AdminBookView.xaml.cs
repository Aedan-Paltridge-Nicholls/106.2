﻿using _106._2.MainProgram.Admin.Book;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
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
using static _106._2.AdminLoginView;

namespace _106._2.Admin.Book
{
    /// <summary>
    /// Interaction logic for AdminBookView.xaml
    /// </summary>
    public partial class AdminBookView : Page
    {
        public class DataStorage
        {
            string BookID,
                    Title,
                    Author,
                    Genre,
                    OnHold,
                    Withdrawn,
                    Overdue,
                    Returned,
                    DueDate,
                    HoldID,
                    OnHoldTo,
                    IssuedId,
                    IssuedTo;
            // Setters 
            public void Set_BookID(string Input) { BookID = Input.Trim(); }
            public void Set_Title(string Input) { Title = Input.Trim(); }
            public void Set_Author(string Input) { Author = Input.Trim(); }
            public void Set_Genre(string Input) { Genre = Input.Trim(); }
            public void Set_OnHold(string Input) { OnHold = Input.Trim(); }
            public void Set_Withdrawn(string Input) { Withdrawn = Input.Trim(); }
            public void Set_Overdue(string Input) { Overdue = Input.Trim(); }
            public void Set_Returned(string Input) { Returned = Input.Trim(); }
            public void Set_DueDate(string Input) { DueDate = Input.Trim(); }
            public void Set_HoldID(string Input) { HoldID = Input.Trim(); }
            public void Set_OnHoldTo(string Input) { OnHoldTo = Input.Trim(); }
            public void Set_IssuedId(string Input) { IssuedId = Input.Trim(); }
            public void Set_IssuedTo(string Input) { IssuedTo = Input.Trim(); }

            // Getters
            public string Get_BookID() { return BookID; }
            public string Get_Title() { return Title; }
            public string Get_Author() { return Author; }
            public string Get_Genre() { return Genre; }
            public string Get_OnHold() { return OnHold; }
            public string Get_Withdrawn() { return Withdrawn; }
            public string Get_Overdue() { return Overdue; }
            public string Get_Returned() { return Returned; }
            public string Get_DueDate() { return DueDate; }
            public string Get_HoldID() { return HoldID; }
            public string Get_OnHoldTo() { return OnHoldTo; }
            public string Get_IssuedId() { return IssuedId; }
            public string Get_IssuedTo() { return IssuedTo; }

            public DataStorage()
            {
                Set_BookID("");
                Set_Title("");
                Set_Author("");
                Set_Genre("");
                Set_OnHold("");
                Set_Withdrawn("");
                Set_Overdue("");
                Set_Returned("");
                Set_DueDate("");
                Set_HoldID("");
                Set_OnHoldTo("");
                Set_IssuedId("");
                Set_IssuedTo("");
            }
        }
        public enum Searchtype
        {
            BookID,
            Title,
            Author,
            Genre,
            OnHold,
            Withdrawn,
            Overdue,
            Returned,
            DueDate,
            HoldID,
            OnHoldTo,
            IssuedId,
            IssuedTo
        }
        public Searchtype searchtype = new Searchtype();
        public DataStorage dataStorage = new DataStorage();
        public bool Numericinput = false;
        public bool Boolinput = false;
        public bool Dateinput = false;
        public AdminBookView()
        {
            CultureInfo Dtfi = CultureInfo.CreateSpecificCulture("en-us");
            Dtfi.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            Dtfi.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = Dtfi;
            InitializeComponent();
            LoadDatagrid();
            LoadGenreBox();
        }
        public string searchtypestringer(Searchtype type)
        {
            switch (type)
            {
                case Searchtype.BookID:
                    return "BookID";
                case Searchtype.Title:
                    return "Title";
                case Searchtype.Author:
                    return "Author";
                case Searchtype.Genre:
                    return "Genre";
                case Searchtype.OnHold:
                    return "OnHold";
                case Searchtype.Withdrawn:
                    return "Withdrawn";
                case Searchtype.Overdue:
                    return "Overdue";
                case Searchtype.Returned:
                    return "Returned";
                case Searchtype.DueDate:
                    return "DueDate";
                case Searchtype.HoldID:
                    return "HoldID";
                case Searchtype.OnHoldTo:
                    return "OnHoldTo";
                case Searchtype.IssuedId:
                    return "IssuedId";
                case Searchtype.IssuedTo:
                    return "IssuedTo";
                default:
                    return "";

            }


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
        public void LoadDatagrid(string comm)
        {
            NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            if (comm == null)
            {
                LoadDatagrid();
                return;
            }

            NpgsqlCommand cmd = new NpgsqlCommand(comm, SqlCONN);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Booksdatagrid.ItemsSource = dt.DefaultView;
            Booksdatagrid.DataContext = dt;

        }
        public void LoadDatagrid(string Search, Searchtype type)
        {
            /*
            BookID, int
            Title, string
            Author, string
            Genre, string
            OnHold, bool
            Withdrawn, bool
            Overdue,  bool
            Returned, bool
            DueDate,  date
            HoldID,   int
            OnHoldTo, string
            IssuedId, int
            IssuedTo string
          */
            string typeNow = searchtypestringer(type);
            switch (typeNow)
            {

                case "BookID":
                    {
                        int Output = int.Parse(Search);
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE  (SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) = {Output}  "
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "Title":
                    {
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE {typeNow.ToLower()} ILIKE \'%{Search}%\'"
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "Author":
                    {

                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE {typeNow.ToLower()} ILIKE \'%{Search}%\'"
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "Genre":
                    {

                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE {typeNow.ToLower()} ILIKE \'%{Search}%\'"
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "DueDate":
                    {
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE {typeNow.ToLower()} = \'{Search}\' "
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "HoldID":
                    {
                        int Output = int.Parse(Search);
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE   {typeNow.ToLower()} = {Output}  "
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "OnHoldTo":
                    {
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE (SELECT name FROM  members  WHERE number = holdid ) ILIKE \'%{Search}%\'"
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "IssuedId":
                    {
                        int Output = int.Parse(Search);
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE   {typeNow.ToLower()} = {Output}  "
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "IssuedTo":
                    {

                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,"
                        + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE (SELECT name FROM members WHERE number = issuedid  ) ILIKE \'%{Search}%\'"
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                default:
                    break;
            };
            RefreshGrid();


        }
        public void RefreshGrid()
        {
            Booksdatagrid.Items.Refresh();
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



        private void BookIdnumberBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataStorage.Set_BookID(BookIdnumberBOX.Text);
        }

        private void SearchOptionBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SearchOptionBOX.SelectedIndex)
            {
                case 0:
                    {
                        Dateinput = false;
                        Boolinput = false;
                        Numericinput = true;
                    }
                    break;
                case 4:
                    {
                        Dateinput = false;
                        Boolinput = true;
                        Numericinput = false;
                    }
                    break;
                case 5:
                    {
                        Dateinput = false;
                        Boolinput = true;
                        Numericinput = false;
                    }
                    break;
                case 6:
                    {
                        Dateinput = false;
                        Boolinput = true;
                        Numericinput = false;
                    }
                    break;
                case 7:
                    {
                        Dateinput = false;
                        Boolinput = true;
                        Numericinput = false;
                    }
                    break;
                case 8:
                    {
                        Dateinput = true;
                        Boolinput = false;
                        Numericinput = false;
                    }
                    break;
                case 9:
                    {
                        Dateinput = false;
                        Boolinput = false;
                        Numericinput = true;
                    }
                    break;
                case 11:
                    {
                        Dateinput = false;
                        Boolinput = false;
                        Numericinput = true;
                    }
                    break;
                default:
                    {
                        Dateinput = false;
                        Boolinput = false;
                        Numericinput = false;
                    }
                    break;
            }
            if (Boolinput)
            {
                SearchBOX.Clear();
                SearchBOX.Text = "Please use the Statis combobox";
            }
            else if (Dateinput)
            {
                SearchBOX.Clear();
                SearchBOX.Text = "Please use the date time picker";
            }
            else
            {
                SearchBOX.Clear();
            }
        }
        public void Search(string SearchOut)
        {
            if (string.IsNullOrEmpty(SearchOut)) { LoadDatagrid(); }
            else
            {
                switch (SearchOptionBOX.SelectedIndex)
                {
                    case 0:
                        { searchtype = Searchtype.BookID; }
                        break;
                    case 1:
                        { searchtype = Searchtype.Title; }
                        break;
                    case 2:
                        { searchtype = Searchtype.Author; }
                        break;
                    case 3:
                        { searchtype = Searchtype.Genre; }
                        break;
                    case 4:
                        { searchtype = Searchtype.OnHold; }
                        break;
                    case 5:
                        { searchtype = Searchtype.Withdrawn; }
                        break;
                    case 6:
                        { searchtype = Searchtype.Overdue; }
                        break;
                    case 7:
                        { searchtype = Searchtype.Returned; }
                        break;
                    case 8:
                        { searchtype = Searchtype.DueDate; }
                        break;
                    case 9:
                        { searchtype = Searchtype.HoldID; }
                        break;
                    case 10:
                        { searchtype = Searchtype.OnHoldTo; }
                        break;
                    case 11:
                        { searchtype = Searchtype.IssuedId; }
                        break;
                    case 12:
                        { searchtype = Searchtype.IssuedTo; }
                        break;
                    default:
                        break;
                }
                LoadDatagrid(SearchOut, searchtype);
            }
        }
        private void SearchBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            string SearchOut = "";
            if (Numericinput)
            {
                int Number;
                bool numeric = int.TryParse(SearchBOX.Text, out Number);
                if (numeric)
                {
                    SearchOut = SearchBOX.Text;
                }
                else
                { SearchBOX.Text = ""; }
                Search(SearchOut);
            }
            else if (Dateinput)
            {
                SearchBOX.Text = " Use the date time picker To search";
            }
            else if (Boolinput)
            {
                SearchBOX.Text = " Use the Statis combobox To search";
            }
            else
            {
                SearchOut = SearchBOX.Text;
                Search(SearchOut);
            }
        }

        private void TitleBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataStorage.Set_Title(TitleBOX.Text.Trim());
        }
        private void AuthorBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataStorage.Set_Author(AuthorBOX.Text.Trim());

        }
        private void GenreOptionBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchOptionBOX.SelectedIndex == 3)
            {
                string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + $" WHERE  genre = \'{GenreOptionBOX.SelectedItem}\' "
                            + " order by Book_id;";
                LoadDatagrid(command);
            }
        }
        public void Addbook(String BookID,String Title,String Author, String Genre)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {
                if (BookID == "") { MessageBox.Show(" Must enter Book id number"); }
                try
                {
                    SqlCONN.Open();
                    AddBookPopup addBookPopup = new AddBookPopup();

                    string command = "INSERT INTO book (bookID, bookname, author, genre)" +
                        $" VALUES ( {BookID}, '{Title}', '{Author}', '{Genre}'  );" +
                        $" INSERT INTO booklog (bookID) VALUES ({BookID}) ";


                    string info = $"Book ID Number :{BookID} {Environment.NewLine} " +
                                  $"Book Title: {Title}   {Environment.NewLine}" +
                                  $"Book Author: {Author}  {Environment.NewLine}" +
                                  $"Book Genre: {Genre}  {Environment.NewLine}";
                    addBookPopup.BookInfoBox.Text = info;
                    
                    bool? NotCanceled = addBookPopup.ShowDialog();
                    if (NotCanceled != null && NotCanceled == true)
                    {

                        var cmd = new NpgsqlCommand(command, SqlCONN);
                       cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { SqlCONN.Close(); }

            }
        }
        private void AddBookBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string BookIdOut = dataStorage.Get_BookID(),
                BooKTitleOut = dataStorage.Get_Title(),
                BookAuthorOut = dataStorage.Get_Author(),
                BookGenreOut = dataStorage.Get_Genre();
            Addbook(BookIdOut,BooKTitleOut,BookAuthorOut,BookGenreOut);
            Booksdatagrid.Items.Refresh();
        }
        public void withdrawBook(String BookID, String Title, String Author, String Genre, string MemberId)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {

                try
                {

                    SqlCONN.Open();
                    WithdrawBookPopup withdrawBookPopup = new WithdrawBookPopup();
                    string SelectedBookId = SelectedBookID.Text.Substring(SelectedBookID.Text.IndexOf(':') + 2) ; 
                    if (SelectedBookId == "")
                    {
                        throw new Exception(" Must select a book to update ");
                    }
                    DataTable dataTable = new DataTable();

                    NpgsqlCommand cmd1 = new NpgsqlCommand($"Select * FROM book WHERE bookid = {SelectedBookId}", SqlCONN);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dt.Rows[1].ToString();

                    string
                    MemerIDnumber = dt.Rows[0].ToString(),
                    MemberName = dt.Rows[1].ToString();
                    string Newinfo = $"Book ID Number :{BookID} {Environment.NewLine} " +
                                     $"Book Title: {Title}   {Environment.NewLine}" +
                                     $"Book Author: {Author}  {Environment.NewLine}" +
                                     $"Book Genre: {Genre}  {Environment.NewLine}",
                           Oldinfo = $"Member ID Number :{MemerIDnumber} {Environment.NewLine} " +
                                     $"Member Name: {MemberName}   {Environment.NewLine};";




                    withdrawBookPopup.BookInfo.Text = Oldinfo;
                    withdrawBookPopup.MemberInfo.Text = Newinfo;
                    bool? NotCanceled = withdrawBookPopup.ShowDialog();
                    string command = "UPDATE  booklog " +
                        $" SET withdrawn = 'true',returned = 'false', issueid = {MemberId}  " +
                        $" WHERE bookID = ({SelectedBookId}) ";
                    if (NotCanceled != null && NotCanceled == true)
                    {

                        var cmd = new NpgsqlCommand(command, SqlCONN);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { SqlCONN.Close(); }

            }
        }
        private void WithdrawBookBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string BookIdOut = dataStorage.Get_BookID(),
                   BooKTitleOut = dataStorage.Get_Title(),
                   BookAuthorOut = dataStorage.Get_Author(),
                   BookGenreOut = dataStorage.Get_Genre(),
                   MemberIdOut = SelectedMemberId.Text.Substring(SelectedBookID.Text.IndexOf(':') + 2);
            withdrawBook(BookIdOut, BooKTitleOut, BookAuthorOut, BookGenreOut, MemberIdOut);
                  
        }
        public void Updatebook(String BookID, String Title, String Author, String Genre)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {
                
                try
                {

                    SqlCONN.Open();
                    UpdateBookPopup UpadteBookPopup = new UpdateBookPopup();

                    string command = "UPDATE  book " +
                        $" SET bookname = '{Title}',author = '{Author}',genre = '{Genre}'  " +
                        $" WHERE bookID = ({BookID}) ", SelectedBookId = SelectedBookID.Text.Substring(SelectedBookID.Text.IndexOf(':')+2 ) ;
                    if (SelectedBookId == "") 
                    {
                        throw new Exception(" Must select a book to update ");
                    }
                    DataTable dataTable = new DataTable();

                    NpgsqlCommand cmd1 = new NpgsqlCommand($"Select * FROM book WHERE bookid = {SelectedBookId}", SqlCONN);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dt.Rows[1].ToString();
                    
                    string
                    OldBookId = dt.Rows[0].ToString(),
                    OldTitle = dt.Rows[1].ToString(),
                    OLdAuthor = dt.Rows[2].ToString(),
                    OldGenre = dt.Rows[3].ToString(); 
                 string Newinfo = $"Book ID Number :{BookID} {Environment.NewLine} " +
                                  $"Book Title: {Title}   {Environment.NewLine}" +
                                  $"Book Author: {Author}  {Environment.NewLine}" +
                                  $"Book Genre: {Genre}  {Environment.NewLine}",
                        Oldinfo = $"Book ID Number :{OldBookId} {Environment.NewLine} " +
                                  $"Book Title: {OldTitle}   {Environment.NewLine}" +
                                  $"Book Author: {OLdAuthor}  {Environment.NewLine}" +
                                  $"Book Genre: {OldGenre}  {Environment.NewLine}";




                    UpadteBookPopup.OldBookInfo.Text = Oldinfo;
                    UpadteBookPopup.NewBookInfo.Text = Newinfo;
                    bool? NotCanceled = UpadteBookPopup.ShowDialog();
                    if (NotCanceled != null && NotCanceled == true)
                    {

                        var cmd = new NpgsqlCommand(command, SqlCONN);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { SqlCONN.Close(); }

            }
        }
        private void ChangeBookInfoBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string BookIdOut = dataStorage.Get_BookID(),
                 BooKTitleOut = dataStorage.Get_Title(),
                 BookAuthorOut = dataStorage.Get_Author(),
                 BookGenreOut = dataStorage.Get_Genre();
            Updatebook(BookIdOut, BooKTitleOut, BookAuthorOut, BookGenreOut);
            RefreshBookGrid();

        }
        private void RefreshBookGrid() 
        {
            LoadDatagrid();
        }
        private void HoldBookBUTTON_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnBookBUTTON_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Booksdatagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           
            if (Booksdatagrid.SelectedIndex != -1)
            {
                string number ="";
                DataGrid dg = (DataGrid )sender;
                DataRowView selectedRow = dg.SelectedItem as DataRowView;
                number = selectedRow.Row[0].ToString();
                BookIdnumberBOX.Text = number;
                TitleBOX.Text = selectedRow.Row[1].ToString();
                AuthorBOX.Text = selectedRow.Row[2].ToString();
                GenreOptionBOX.Text = selectedRow.Row[3].ToString();
                DuedateDatepicker.Text = selectedRow.Row[8].ToString();
                SelectedBookID.Text = $"Book ID :{Environment.NewLine}{number}";
                
            }
          
        }

        private void DuedateDatepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string Date = DuedateDatepicker.SelectedDate.ToString();
            if (Booksdatagrid.SelectedIndex == -1)
            {
                Date = Date.Remove(Date.IndexOf(' '));

                dataStorage.Set_DueDate(Date);
                if (Dateinput)
                {
                    string command = "SELECT "
                                + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                                + "book.bookname,book.author,book.genre,"
                                + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                                + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                                + "booklog.issuedid,"
                                + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                                + " FROM book"
                                + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                                + $" WHERE  duedate = \'{Date}\' "
                                + " order by Book_id;";
                    LoadDatagrid(command);
                    
                }
            }
        }

        private void StatisBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {/* - true
             On Hold
             Withdrawn
             Overdue 
             On Hold Withdrawn 
             On Hold Overdue 
             Returned 
             On Hold Returned 
            - False
             On Hold 
             Withdrawn 
             Overdue 
             On Hold Withdrawn 
             On Hold Overdue 
             Returned 
             On Hold Returned 
          */
           if (Boolinput)
           { 
                switch (StatisBOX.SelectedIndex)
                {
                    // true
                    case 1:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  onhold  = true "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 2:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  withdrawn  = true "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 3:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  overdue  = true "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 4:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  onhold  = true AND withdrawn  = true "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 5:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE   onhold  = true AND overdue  = true "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 6:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE returned = true "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 7:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  onhold  = true AND returned = true "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    //   false
                    case 9:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  onhold  = false "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 10:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  withdrawn  = false "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 11:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  overdue  = false "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 12:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  onhold  = false AND withdrawn  = false "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 13:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE   onhold  = false AND overdue  = false "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 14:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE returned = false "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 15:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,"
                            + "booklog.onhold,booklog.withdrawn,booklog.overdue,booklog.returned,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  onhold  = false AND returned = false "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    default:
                        break;

                }
           }

        }

        private void ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchBOX.Clear();
            SearchOptionBOX.SelectedIndex = -1;
        }

        private void ResetBookDataGridSelection_Click(object sender, RoutedEventArgs e)
        {
            Booksdatagrid.SelectedIndex = -1;
        }
    }
}