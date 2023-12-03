using _106._2.MainProgram.Admin.Book;
using _106._2.MainProgram.Admin.OverDue;
using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        private DataRow dr;

        public AdminBookView()
        {
            CultureInfo Dtfi = CultureInfo.CreateSpecificCulture("en-us");
            Dtfi.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            Dtfi.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = Dtfi;
            InitializeComponent();
            LoadDatagrid();
            LoadGenreBox();
            AdminOverDueView overDueView = new AdminOverDueView();
            overDueView.FindOverdue();
        }
        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)e.OriginalSource;
         
             


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
                        + "book.bookname,book.author,book.genre,book.image,"
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
                        + "book.bookname,book.author,book.genre,book.image,"
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
                        + "book.bookname,book.author,book.genre,book.image,"
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
                        + "book.bookname,book.author,book.genre,book.image,"
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
                        + "book.bookname,book.author,book.genre,book.image,"
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
                        + "book.bookname,book.author,book.genre,book.image,"
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
                        + "book.bookname,book.author,book.genre,book.image,"
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
                        + "book.bookname,book.author,book.genre,book.image,"
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
                        + "book.bookname,book.author,book.genre,book.image,"
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
                        + "book.bookname,book.author,book.genre,book.image,"
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
        /// <summary>
        /// This refreshes the grid. 
        /// </summary>
        public void RefreshGrid()
        {
            LoadDatagrid();
        }
        /// <summary>
        /// This Loads the genre box with all the genres in the 'book' table
        /// </summary>
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


        /// <summary>
        /// This sets the data storage to the user input for book id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookIdnumberBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataStorage.Set_BookID(BookIdnumberBOX.Text);
        }
        /// <summary>
        /// This Sets the input type according to the selected 'SearchOptionBOX' item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                SearchBOX.Text = "Please use the Status combobox";
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
        /// <summary>
        /// This sets the the search type according to the selected 'SearchOptionBOX' item
        /// and checks if the search string 'SearchOut' is equal to null or is empty 
        /// then if it is not sends the search to 'LoadDatagrid(SearchOut, searchtype)'
        /// </summary>
        /// <param name="SearchOut"></param>
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
        /// <summary>
        /// This checks the input type of a search and sends out the searches accordingly  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                SearchBOX.Text = " Use the Status combobox To search";
            }
            else
            {
                SearchOut = SearchBOX.Text;
                Search(SearchOut);
            }
        }

        /// <summary>
        /// This sets the data storage to the user input for book title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataStorage.Set_Title(TitleBOX.Text.Trim());
        }

        /// <summary>
        /// This sets the data storage to the user input for book Author
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
            if (GenreOptionBOX.SelectedIndex != -1) {  dataStorage.Set_Genre(GenreOptionBOX.SelectedItem.ToString()); }
           
        }
        public string ImagePath {  get; set; } 
        private void GetImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "c:\\";    // Setting a filter for file extensions 
            ofd.Filter = "\"Image files (*.bmp, *.jpg)|*.bmp;*.jpg|All files (*.*)|*.*\"'";
            ofd.Multiselect = false;
            ofd.Title = "Select Cover Image";

            if (ofd.ShowDialog() == true)// Opening the file dialog
            {
                // Gets the selected images filename
                 ImagePath = ofd.FileName.ToString();

            }
            else
            {
                MessageBox.Show("Must Select Cover Image ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);//Error message
                GetImage();
            }

        }
        public void Addbook(string BookID,string Title,string Author, string Genre)
        {
            ImagePath = " sdsdsdsd";
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {
                try
                {
                    if (BookID == ""){throw new Exception("Must enter Book id number ");}
                    SqlCONN.Open();
                    AddBookPopup addBookPopup = new AddBookPopup();
                    string info = $"Book ID Number :{BookID} {Environment.NewLine} " +
                                  $"Book Title: {Title}   {Environment.NewLine}" +
                                  $"Book Author: {Author}  {Environment.NewLine}" +
                                  $"Book Genre: {Genre}  {Environment.NewLine}";
                    addBookPopup.BookInfoBox.Text = info;
                    bool? NotCanceled = addBookPopup.ShowDialog();

                    if (NotCanceled != null && NotCanceled == true)
                    {
                        GetImage();
                        string command = "INSERT INTO book (bookID, bookname, author, genre, image)" +
                        $" VALUES ( {BookID}, '{Title}', '{Author}', '{Genre}', '{ImagePath}' );" +
                        $" INSERT INTO booklog (bookID) VALUES ({BookID}) ";
                        var cmd = new NpgsqlCommand(command, SqlCONN);
                       cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { SqlCONN.Close(); }
                RefreshGrid();
            }
        }
        private void AddBookBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string BookIdOut = dataStorage.Get_BookID(),
                BooKTitleOut = dataStorage.Get_Title(),
                BookAuthorOut = dataStorage.Get_Author(),
                BookGenreOut = dataStorage.Get_Genre();
            Addbook(BookIdOut,BooKTitleOut,BookAuthorOut,BookGenreOut);
            RefreshGrid();
        }
        public void withdrawBook(string BookID, string Title, string Author, string Genre, string MemberId)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {

                try
                {

                    SqlCONN.Open();
                    WithdrawBookPopup withdrawBookPopup = new WithdrawBookPopup();
                    string SelectedBookId = SelectedBookID.Value.ToString(),
                     MemberIdOut = SelectedMemberId.Value.ToString();

                    DataTable dataTable = new DataTable();

                    NpgsqlCommand cmd1 = new NpgsqlCommand($"Select * FROM members WHERE number = {MemberId}", SqlCONN);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataRow dataRow = dt.Rows[0];

                    string
                    MemerIDnumber = dataRow[0].ToString(),
                    MemberName = dataRow[1].ToString();
                    string BookInfo = $"Book ID Number :{BookID} {Environment.NewLine} " +
                                     $"Book Title: {Title}   {Environment.NewLine}" +
                                     $"Book Author: {Author}  {Environment.NewLine}" +
                                     $"Book Genre: {Genre}  {Environment.NewLine}",
                           MemberInfo = $"Member ID Number :{MemerIDnumber} {Environment.NewLine} " +
                                     $"Member Name: {MemberName}   {Environment.NewLine};";




                    withdrawBookPopup.BookInfo.Text = BookInfo;
                    withdrawBookPopup.MemberInfo.Text = MemberInfo ;
                    withdrawBookPopup.MemberId = MemberId;
                    withdrawBookPopup.SelectedBookId = SelectedBookId;
                    withdrawBookPopup.ShowDialog();
                    
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { SqlCONN.Close(); }

            }
            RefreshGrid();
        }
        private void WithdrawBookBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string SelectedBookId = SelectedBookID.Value.ToString(),
                     MemberIdOut = SelectedMemberId.Value.ToString();
            if (SelectedBookId == ""){ MessageBox.Show(" Must select a book to Issuse "); return; }
            string BookIdOut = dataStorage.Get_BookID(),
                   BooKTitleOut = dataStorage.Get_Title(),
                   BookAuthorOut = dataStorage.Get_Author(),
                   BookGenreOut = dataStorage.Get_Genre();
                   
            if (MemberIdOut == "") { MessageBox.Show(" Must select a Member Issue the book to "); return; }
            withdrawBook(BookIdOut, BooKTitleOut, BookAuthorOut, BookGenreOut, MemberIdOut);
                  
        }
        public void Updatebook(string BookID, string Title, string Author, string Genre)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {
                
                try
                {

                    SqlCONN.Open();
                    UpdateBookPopup UpadteBookPopup = new UpdateBookPopup();
                    string
                        SelectedBookId = SelectedBookID.Value.ToString(), MemberIdOut = SelectedMemberId.Value.ToString() ;
                    string command = "UPDATE  book " +
                        $" SET bookname = '{Title}',author = '{Author}',genre = '{Genre}'  " +
                        $" WHERE bookID = ({SelectedBookId}) ";
                   
                    if (SelectedBookId == "") 
                    {
                        throw new Exception(" Must select a book to update ");
                    }
                    DataTable dataTable = new DataTable();

                    NpgsqlCommand cmd1 = new NpgsqlCommand($"Select * FROM book WHERE bookid = {SelectedBookId}", SqlCONN);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataRow dataRow =  dt.Rows[0];
                    
                    string
                    OldBookId = dataRow[0].ToString(),
                    OldTitle = dataRow[1].ToString(),
                    OLdAuthor = dataRow[2].ToString(),
                    OldGenre = dataRow[3].ToString(); 
                 string Newinfo = $"Book ID Number: {SelectedBookId} {Environment.NewLine} " +
                                  $"Book Title: {Title}   {Environment.NewLine}" +
                                  $"Book Author: {Author}  {Environment.NewLine}" +
                                  $"Book Genre: {Genre}  {Environment.NewLine}",
                        Oldinfo = $"Book ID Number: {SelectedBookId} {Environment.NewLine} " +
                                  $"Book Title: {OldTitle}   {Environment.NewLine}" +
                                  $"Book Author: {OLdAuthor}  {Environment.NewLine}" +
                                  $"Book Genre: {OldGenre}  {Environment.NewLine}";


                    UpadteBookPopup.Connection = SqlCONN;
                    UpadteBookPopup.bookid = SelectedBookId;
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
                RefreshGrid();
            }
          
        }
       
        private void UpdateBookInfoBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string BookIdOut = dataStorage.Get_BookID(),
                 BooKTitleOut = dataStorage.Get_Title(),
                 BookAuthorOut = dataStorage.Get_Author(),
                 BookGenreOut = dataStorage.Get_Genre();
            Updatebook(BookIdOut, BooKTitleOut, BookAuthorOut, BookGenreOut);
            RefreshBookGrid();

        }
        public void HoldBook(string BookID, string Title, string Author, string Genre, string MemberId)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {

                try
                {

                    SqlCONN.Open();
                    HoldBookPopup HoldBookPopup = new HoldBookPopup();
                    DataTable dataTable = new DataTable();
                    string SelectedBookId = SelectedBookID.Value.ToString(), MemberIdOut = SelectedMemberId.Value.ToString();

                    NpgsqlCommand cmd1 = new NpgsqlCommand($"Select * FROM members WHERE number = {MemberId}", SqlCONN);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataRow dataRow = dt.Rows[0];

                    string
                    MemerIDnumber = dataRow[0].ToString(),
                    MemberName = dataRow[1].ToString();
                    string BookInfo = $"Book ID Number :{BookID} {Environment.NewLine} " +
                                     $"Book Title: {Title}   {Environment.NewLine}" +
                                     $"Book Author: {Author}  {Environment.NewLine}" +
                                     $"Book Genre: {Genre}  {Environment.NewLine}",
                           MemberInfo = $"Member ID Number :{MemerIDnumber} {Environment.NewLine} " +
                                     $"Member Name: {MemberName}   {Environment.NewLine};";




                    HoldBookPopup.BookInfo.Text = BookInfo;
                    HoldBookPopup.MemberInfo.Text = MemberInfo;
                    HoldBookPopup.MemberId = MemberId;
                    HoldBookPopup.SelectedBookId = SelectedBookId;
                    HoldBookPopup.ShowDialog();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { SqlCONN.Close(); }

            }
            RefreshGrid();
        }
       
    
        private void HoldBookBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string SelectedBookId = SelectedBookID.Value.ToString(),
                     MemberIdOut = SelectedMemberId.Value.ToString();
            if (SelectedBookId == "") { MessageBox.Show(" Must select a book to return "); return; }
            string BookIdOut = dataStorage.Get_BookID(),
                   BooKTitleOut = dataStorage.Get_Title(),
                   BookAuthorOut = dataStorage.Get_Author(),
                   BookGenreOut = dataStorage.Get_Genre();

            if (MemberIdOut == "") { MessageBox.Show(" Must select the Member the book is Issued too "); return; }
            HoldBook(BookIdOut, BooKTitleOut, BookAuthorOut, BookGenreOut, MemberIdOut);
        }
        public void ReturnBook(string BookID, string Title, string Author, string Genre, string MemberId)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {

                try
                {

                    SqlCONN.Open();
                    ReturnBookPopup returnBookPopup = new ReturnBookPopup();
                    DataTable dataTable = new DataTable();
                    string SelectedBookId = SelectedBookID.Value.ToString(), MemberIdOut = SelectedMemberId.Value.ToString();

                    NpgsqlCommand cmd1 = new NpgsqlCommand($"Select * FROM members WHERE number = {MemberId}", SqlCONN);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataRow dataRow = dt.Rows[0];

                    string
                    MemerIDnumber = dataRow[0].ToString(),
                    MemberName = dataRow[1].ToString();
                    string BookInfo = $"Book ID Number :{BookID} {Environment.NewLine} " +
                                     $"Book Title: {Title}   {Environment.NewLine}" +
                                     $"Book Author: {Author}  {Environment.NewLine}" +
                                     $"Book Genre: {Genre}  {Environment.NewLine}",
                           MemberInfo = $"Member ID Number :{MemerIDnumber} {Environment.NewLine} " +
                                     $"Member Name: {MemberName}   {Environment.NewLine};";




                    returnBookPopup.BookInfo.Text = BookInfo;
                    returnBookPopup.MemberInfo.Text = MemberInfo;
                    returnBookPopup.MemberId = MemberId;
                    returnBookPopup.SelectedBookId = SelectedBookId;
                    returnBookPopup.ShowDialog();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { SqlCONN.Close(); }

            }
            RefreshGrid();
        }
        private void ReturnBookBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string SelectedBookId = SelectedBookID.Value.ToString(),
                     MemberIdOut = SelectedMemberId.Value.ToString();
            if (SelectedBookId == "") { MessageBox.Show(" Must select a book to return "); return; }
            string BookIdOut = dataStorage.Get_BookID(),
                   BooKTitleOut = dataStorage.Get_Title(),
                   BookAuthorOut = dataStorage.Get_Author(),
                   BookGenreOut = dataStorage.Get_Genre();

            if (MemberIdOut == "") { MessageBox.Show(" Must select the Member the book is Issued too "); return; }
            ReturnBook(BookIdOut, BooKTitleOut, BookAuthorOut, BookGenreOut, MemberIdOut);
        }
        private void RefreshBookGrid() 
        {
            LoadDatagrid();
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
                SelectedBookID.Text = selectedRow.Row[0].ToString();
                SelectedMemberId.Text = (selectedRow.Row[12].ToString() != "" && selectedRow.Row[12].ToString() != "0") 
                    ? selectedRow.Row[12].ToString() : selectedRow.Row[10].ToString();

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
                                + "book.bookname,book.author,book.genre,book.image,"
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

        private void StatusBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                switch (StatusBOX.SelectedIndex)
                {
                    // true
                    case 1:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
                            + "book.bookname,book.author,book.genre,book.image,"
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
            RefreshGrid();
        }

        private void ResetBookDataGridSelection_Click(object sender, RoutedEventArgs e)
        {  
            BookIdnumberBOX.Clear() ;
            TitleBOX.Clear();
            GenreOptionBOX.SelectedIndex = -1;
            AuthorBOX.Clear();
            
            Booksdatagrid.SelectedIndex = -1;
            RefreshGrid();
        }

        private void SelectedBookID_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int i = 0;
            SelectedBookID.Maximum = Convert.ToInt32(commander("SELECT bookid FROM book ORDER BY bookid DESC LIMIT 1 "));
        }
        private void SelectedMemberId_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
       
            SelectedMemberId.Maximum = Convert.ToInt32(commander("SELECT number FROM members ORDER BY number DESC LIMIT 1 "));
        }
        //Excutes sql commands
        public string commander(string CMD)
        {
            NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            SqlCONN.Open();
            using var command = new NpgsqlCommand(CMD, SqlCONN);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataRow dataRow = dt.Rows[0];
            string Output = dataRow[0].ToString();
            return Output;
        }
        public int commander(string CMD, int input)
        {
            NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            SqlCONN.Open();
            using var command = new NpgsqlCommand(CMD, SqlCONN);
            var count = (long)command.ExecuteScalar();
            SqlCONN.Close();
            input = (int)count;
            return input;
        }
    }
}
