using _106._2.Admin.Book;
using Microsoft.Win32;
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

namespace _106._2.MainProgram.Admin.Book
{
    /// <summary>
    /// Interaction logic for AddBookPopup.xaml
    /// </summary>
    public partial class AddBookPopup : Window
    {
        /// <summary>
        /// This is a pop-up for confirming the adding a book to the database
        /// </summary>
        public AddBookPopup()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This Cancels the adding of the book 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; this.Close();
        }
        /// <summary>
        /// This  confirms adding a book the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Addbutton_Click_1(object sender, RoutedEventArgs e)
        { 
            this.DialogResult = true; this.Close();
        }  
    }
}
