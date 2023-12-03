using _106._2.MainProgram.Homepage;
using _106._2.MainProgram.User.Userbook;
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
using System.Windows.Shapes;

namespace _106._2.MainProgram.User
{
    /// <summary>
    /// Interaction logic for UserLoginView.xaml
    /// </summary>
    public partial class UserLoginView : Window
    {
        /// <summary>
        /// This is the main page for non-admin users
        /// </summary>
        /// <param name="userid">This is the id of the user</param>
        public UserLoginView(string userid )
        {
            InitializeComponent();
            UserId = userid; 

            Main.Content = new Home();
        }
        public static string UserId { get; set; }
        /// <summary>
        /// This lets users look at  their withdrawn & on hold books by opening the 'UsersBooksView' page .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UsersBooksView BookView = new UsersBooksView(UserId);
            Main.Content = BookView;
        }
        /// <summary>
        /// This lets users look at  the libraries books by opening the 'Home' page .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Home();

        }
        /// <summary>
        /// This lets users look at  their overdue books by opening the 'UsersOverDueBookView' page .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DuedateButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UsersOverDueBookView BookView = new UsersOverDueBookView(UserId);
            Main.Content = BookView;
        }
        /// <summary>
        /// This lets users logout of their account and go back to the login screen
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
        /// This holds if the search is numeric or not
        /// </summary>
        public bool numeric { get; set; }
        /// <summary>
        /// This holds the column of the search
        /// </summary>
        public string searchtype { get; set; }
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
            if (numeric ) 
            {
                SearchBOX.Text = Regex.Replace(SearchBOX.Text, "[^0-9]+", "");
            
            }
        }
        /// <summary>
        /// This lets users look at  their Own info by opening the 'MyPage' page .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPage_Click(object sender, RoutedEventArgs e)
        {
            MyPage myPage = new MyPage(UserId);
            Main.Content = myPage;
        }
    }
}
