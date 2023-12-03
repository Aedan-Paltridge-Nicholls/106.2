using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using _106._2.Admin.Book;
using _106._2.MainProgram;
using _106._2.MainProgram.Admin.OverDue;
using _106._2.MainProgram.Homepage;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;

namespace _106._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// This is the main page for admin users
        /// </summary>
        /// <param name="AdminID">This is the Logged in admin's id</param>
        public MainWindow(string AdminID)
        {

            AdminId = AdminID;
            InitializeComponent();

            Main.Content = new Home();

        }
        /// <summary>
        /// This stores the Logged in admin's id
        /// </summary>
        public static string AdminId { get; set; }

        private void HomeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Home();
        }
        /// <summary>
        /// This lets Admins look at and manage the libraries  books by opening the 'AdminBookView' page .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdminBookButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new AdminBookView();
        }
        /// <summary>
        /// This lets Admins look at the libraries overdue books by opening the 'AdminOverDueView' page to see who has them and how much do they owe.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DuedateButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new AdminOverDueView();


        }
        /// <summary>
        /// This lets Admins look at and manage the libraries  Members & logins by opening the 'AdminLoginView' page .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MembersButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new AdminLoginView();

        }
        /// <summary>
        /// This lets Admins logout of their account and go back to the login screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            login loginwindow = new login();
            loginwindow.Show();
            this.Close();
        }

        /// <summary>
        /// This holds the column of the search
        /// </summary>
        public string searchtype { get; set; }

        /// <summary>
        /// This holds if the search is numeric or not
        /// </summary>
        public bool numeric { get; set; }
        /// <summary>
        /// This sends the search from 'SearchBOX' to a new page called 'BookSearchPage'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBOX.Text == "") { return; }
            BookSearchPage bookSearchPage = new BookSearchPage(SearchBOX.Text, numeric, searchtype);
            Main.Content = bookSearchPage;
        }
        /// <summary>
        /// This Sets if the search is numeric or not by the 'SearchOptionBOX' Selected Index 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchOptionBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SearchOptionBOX.SelectedIndex)
            {
                case 0:
                    {
                        searchtype = "bookid";
                        numeric = true;
                    }
                    break;
                case 1:
                    {
                        searchtype = "bookname";
                        numeric = false;
                    }
                    break;
                case 2:
                    {
                        searchtype = "author";
                        numeric = false;
                    }
                    break;
                case 3:
                    {
                        searchtype = "genre";
                        numeric = false;
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// This checks if a search is numeric and if it is prevents a the imput of non numeric text 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (numeric)
            {
                SearchBOX.Text = Regex.Replace(SearchBOX.Text, "[^0-9]+", "");
            }
        }
        private void MyPage_Click(object sender, RoutedEventArgs e)
        {
            MyPage myPage = new MyPage(AdminId);
            Main.Content = myPage;
        }
    }
}
