using System;
using System.Collections.Generic;
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

namespace _106._2.MainProgram.Homepage
{
    /// <summary>
    /// Interaction logic for Booktemplate.xaml
    /// </summary>
    public partial class Booktemplate : UserControl
    {    
        public Book GetBook = new Book();
        public class Book
        {
            public string[] BookID { get; set; }
            public string ImagePath { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }

        }
        public Booktemplate()
        {
             
            InitializeComponent();
           

        }
     
       

        private void CoverImage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            CoverImage.Source =  new BitmapImage(new Uri("/Images/CoverPlaceholder", UriKind.Relative));

        }

        private void btnPopup_Click(object sender, RoutedEventArgs e)
        {   
            Booktemplate booktemplate = this;
            GetBook.BookID = booktemplate.IDBlock.Text.Split(':'); 
            GetBook.ImagePath =  booktemplate.CoverImage.Source.ToString();
            GetBook.Title = booktemplate.TitleBlock.Text;
            GetBook.Author = booktemplate.AuthorBlock.Text;
            GetBook.Genre = booktemplate.GenreBlock.Text;
            Point point = Mouse.GetPosition(null) ;
            BookPopUP bookPopUP = new BookPopUP();
            bookPopUP.Top = point.Y - bookPopUP.Height/2;
            bookPopUP.Left = point.X - bookPopUP.Width / 2;
            bookPopUP.GetBook.BookID = GetBook.BookID[1];
            bookPopUP.GetBook.ImagePath = GetBook.ImagePath;
            bookPopUP.GetBook.Title = GetBook.Title;
            bookPopUP.GetBook.Author = GetBook.Author;
            bookPopUP.GetBook.Genre = GetBook.Genre;
            bookPopUP.ShowDialog();
        }
    }
}
