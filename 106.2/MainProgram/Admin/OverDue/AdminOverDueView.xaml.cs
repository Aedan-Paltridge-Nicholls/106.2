﻿using _106._2.MainProgram.Admin.Book;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
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
using static _106._2.Admin.Book.AdminBookView;

namespace _106._2.MainProgram.Admin.OverDue
{
    /// <summary>
    /// Interaction logic for AdminOverDueView.xaml
    /// </summary>
    public partial class AdminOverDueView : Page
    {
        public AdminOverDueView()
        {

            CultureInfo Dtfi = CultureInfo.CreateSpecificCulture("en-us");
            Dtfi.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            Dtfi.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = Dtfi;
            InitializeComponent();
            LoadDatagrid("");
            LoadGenreBox();
            FindOverdue();
        }

        public void FindOverdue()
        {
            NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
            string comm = "UPDATE booklog " 
                        + "SET overdue = "
                        + $"CASE WHEN duedate < \'{DateTime.Now.ToString("yyyy-MM-dd")}\' THEN TRUE "
                        + "ELSE FALSE "
                        + "END; ";
            NpgsqlCommand cmd = new NpgsqlCommand(comm, SqlCONN);
            SqlCONN.Open();
            cmd.ExecuteNonQuery();
            SqlCONN.Close();


        }
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
                        Dateinput = true;
                        Boolinput = false;
                        Numericinput = false;
                    }
                    break;
                case 6:
                    {
                        Dateinput = false;
                        Boolinput = false;
                        Numericinput = true;
                    }
                    break;
                case 8:
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
        public void Search(string SearchOut)// the start of the search Process 
        {
            if (string.IsNullOrEmpty(SearchOut)) { LoadDatagrid(""); }
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
                        { searchtype = Searchtype.DueDate; }
                        break;
                    case 6:
                        { searchtype = Searchtype.HoldID; }
                        break;
                    case 7:
                        { searchtype = Searchtype.OnHoldTo; }
                        break;
                    case 8:
                        { searchtype = Searchtype.IssuedId; }
                        break;
                    case 9:
                        { searchtype = Searchtype.IssuedTo; }
                        break;
                    default:
                        break;
                }
                LoadDatagrid(SearchOut, searchtype);
            }
        }

        public void LoadDatagrid(string Search, Searchtype type)
        {

            string typeNow = searchtypestringer(type);
            switch (typeNow)
            {

                case "BookID":
                    {
                        int Output;
                            int.TryParse(Search, out Output);
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,book.image,"
                        + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE  (SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) = {Output} AND overdue = 'true'  "
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "Title":
                    {
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,book.image,"
                        + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE {typeNow.ToLower()} ILIKE \'%{Search}%\' AND overdue = 'true'"
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "Author":
                    {

                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,book.image,"
                        + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE {typeNow.ToLower()} ILIKE \'%{Search}%\'  AND overdue = 'true' "
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "Genre":
                    {

                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,book.image,"
                        + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE {typeNow.ToLower()} ILIKE \'%{Search}%\'  AND overdue = 'true'"
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "DueDate":
                    {
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,book.image,"
                        + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE {typeNow.ToLower()} = \'{Search}\'  AND overdue = 'true' "
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
                        + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE   {typeNow.ToLower()} = {Output}  AND overdue = 'true' "
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "OnHoldTo":
                    {
                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,book.image,"
                        + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE (SELECT name FROM  members  WHERE number = holdid ) ILIKE \'%{Search}%\'  AND overdue = 'true'"
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
                        + $" WHERE   {typeNow.ToLower()} = {Output}   AND overdue = 'true' "
                        + " order by Book_id;";
                        LoadDatagrid($"{command}");
                    }
                    break;
                case "IssuedTo":
                    {

                        string command = "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,book.image,"
                        + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + $" WHERE (SELECT name FROM members WHERE number = issuedid  ) ILIKE \'%{Search}%\'  AND overdue = 'true'"
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
            LoadDatagrid("");
        }
        private void GenreOptionBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (SearchOptionBOX.SelectedIndex == 3)
            {
                string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,book.image,"
                            + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + $" WHERE  genre = \'{GenreOptionBOX.SelectedItem}\'  AND overdue = 'true' "
                            + " order by Book_id;";
                LoadDatagrid(command);
            }
            if (GenreOptionBOX.SelectedIndex != -1) { dataStorage.Set_Genre(GenreOptionBOX.SelectedItem.ToString()); }

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
        public void LoadDatagrid(string comm)
        {
            NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
             comm = (comm == "")? "SELECT "
                        + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                        + "book.bookname,book.author,book.genre,book.image,"
                        + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                        + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                        + "booklog.issuedid,"
                        + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                        + " FROM book"
                        + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                        + " WHERE overdue = 'true' "
                        + " order by Book_id;" : comm;
            NpgsqlCommand cmd = new NpgsqlCommand(comm, SqlCONN);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dt.Columns.Add("Owed");
            int i = 0;
            DateTime today = DateTime.Now;
            foreach (DataRow dr in dt.Rows)
            {
                
                dt.Columns[12].Table.Rows[i].BeginEdit();
                var row = dt.Columns[12].Table.Rows[i];
                DateTime DueDate = DateTime.Parse(row["duedate"].ToString());
                TimeSpan span = today -  DueDate  ;
                double owing = span.TotalHours / 2;
                row["Owed"] = (owing).ToString("N2"); 
                dt.Columns[12].Table.Rows[i].EndEdit();
                i++;
            }
         
            Booksdatagrid.ItemsSource = dt.DefaultView;
            Booksdatagrid.DataContext = dt;

        }
        private void Booksdatagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           
            if (Booksdatagrid.SelectedIndex != -1)
            {
                string number ="";
                DataGrid dg = (DataGrid )sender;
                DataRowView selectedRow = dg.SelectedItem as DataRowView;
                number = selectedRow.Row[0].ToString();         
                GenreOptionBOX.Text = selectedRow.Row[3].ToString();
                SelectedBookID.Text = selectedRow.Row[0].ToString();
                SelectedMemberId.Text = selectedRow.Row[10].ToString();
                dataStorage.Set_BookID(selectedRow.Row[0].ToString());
                dataStorage.Set_Title(selectedRow.Row[1].ToString());
                dataStorage.Set_Author(selectedRow.Row[2].ToString());
                dataStorage.Set_Genre(selectedRow.Row[3].ToString());

                
                
            }
          
        }
       
        private void ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchBOX.Clear();
            GenreOptionBOX.SelectedIndex = -1;
            SearchOptionBOX.SelectedIndex = -1;
            StatusBOX.SelectedIndex = -1;
            RefreshGrid();
        }
        // 
        private void ResetBookDataGridSelection_Click(object sender, RoutedEventArgs e)
        {
      
            GenreOptionBOX.SelectedIndex = -1;
            SelectedBookID.Value = null;
            SelectedMemberId.Value = null;
            Booksdatagrid.SelectedIndex = -1;
            RefreshGrid();
        }


       



        private void StatusBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {/* 
          * On Hold
          * Not On Hold
          */
            if (Boolinput)
            {
                switch (StatusBOX.SelectedIndex + 1)
                {
                    // true
                    case 1:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,book.image,"
                            + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  onhold  = true AND overdue = true "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                    case 2:
                        {

                            string command = "SELECT "
                            + "(SELECT bookid FROM  book  WHERE book.bookid = booklog.bookid) AS Book_id,"
                            + "book.bookname,book.author,book.genre,book.image,"
                            + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                            + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                            + "booklog.issuedid,"
                            + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                            + " FROM book"
                            + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                            + " WHERE  onhold  = false  AND overdue = true "
                            + " order by Book_id;";
                            LoadDatagrid($"{command}");
                        }
                        break;
                   
                    default:
                        break;

                }
            }

        }



        public void ReturnBook(String BookID, String Title, String Author, String Genre, string MemberId)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {

                try
                {

                    SqlCONN.Open();
                    ReturnBookPopup returnBookPopup = new ReturnBookPopup();
                    DataTable dataTable = new DataTable();
                    string SelectedBookId = SelectedBookID.Value.ToString();
                    string MemberIdOut = SelectedMemberId.Value.ToString();

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

        

        private void SelectedBookID_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            int i = 0;
            SelectedBookID.Maximum =  commander("SELECT COUNT(*) FROM book", i); 
        }
        private void SelectedMemberId_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {            
            int i = 0;
            SelectedMemberId.Maximum = commander("SELECT COUNT(number) FROM members", i);
        }
        //Excutes sql commands  
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

            /* This searches the BookDataGrid via the DuedateDatepicker
            * when a new date is selected and the SearchOptionBox is set to Duedate
            */
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
                                + "booklog.onhold,booklog.overdue,booklog.duedate,booklog.holdid,"
                                + "(SELECT name FROM  members WHERE number = holdid  ) AS username_holdid,"
                                + "booklog.issuedid,"
                                + "(SELECT name FROM members WHERE number = issuedid  ) AS username_issuedid"
                                + " FROM book"
                                + " INNER JOIN booklog ON booklog.bookid = book.bookid"
                                + $" WHERE  duedate = \'{Date}\'  AND overdue = true "
                                + " order by Book_id;";
                    LoadDatagrid(command);

                }
            }
        }

        
    }
}
