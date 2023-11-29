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
           
            InitializeComponent();
           

        }
        
        public Book GetBook = new Book();   
        public class Book
        {
            public string ImagePath { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }

        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BorrowButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HoldButton_Click(object sender, RoutedEventArgs e)
        {

        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Image.Source = new BitmapImage(new Uri(GetBook.ImagePath));
            Title.Text = GetBook.Title;
            Author.Text = GetBook.Author;
            Genre.Text = GetBook.Genre;
        }
    }
}
