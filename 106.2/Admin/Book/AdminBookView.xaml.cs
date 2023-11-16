using System;
using System.Collections.Generic;
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
        }

 //    will need to test layter   select book.*, booklog.* , members.name from book
 //inner join booklog on booklog.bookid = book.bookid
 //inner join members on booklog.issuedid = members.number;

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


        private void Booksdatagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
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
    }
}
