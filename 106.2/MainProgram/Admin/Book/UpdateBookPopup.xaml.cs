using Microsoft.Win32;
using Npgsql;
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
    /// Interaction logic for UpdateBookPopup.xaml
    /// </summary>
    public partial class UpdateBookPopup : Window
    {
        /// <summary>
        /// This Stores a Connection string
        /// </summary>
        public NpgsqlConnection Connection { get; set; }
        /// <summary>
        /// This is a Popup to update a Book
        /// </summary>
        public UpdateBookPopup()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This  stores a books id
        /// </summary>
        public string bookid { set; get; }
        /// <summary>
        /// This Button Updates the book
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBook_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; this.Close();

        }
        /// <summary>
        /// This Button cancels Updating the book
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; this.Close();
        }
        /// <summary>
        /// This stores the image path for the cover of a book
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// This is to update  the  book cover
        /// </summary>
        private void GetImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "c:\\";    // Seting a filter for file extensions 
            ofd.Filter = "\"Image files (*.bmp,*.png, *.jpg)|*.bmp;*.png;*.jpg|All files (*.*)|*.*\"'";
            ofd.Multiselect = false;
            ofd.Title = "Select Cover Image";

            if (ofd.ShowDialog() == true)// Opening the file dialog
            {
                // Gets the selected images filename
                ImagePath = ofd.FileName.ToString();

            }
            else
            {
                MessageBox.Show("Must Select Cover Image ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                GetImage();
            }

        }
        /// <summary>
        /// This button updates the books cover by calling 'GetImage()'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Updatecover_Click(object sender, RoutedEventArgs e)
        {
            GetImage();
            NpgsqlCommand cmd = new NpgsqlCommand($"UPDATE book SET image = '{ImagePath}' WHERE bookid = {bookid}", Connection);
            cmd.ExecuteNonQuery();
        }
    }
}
