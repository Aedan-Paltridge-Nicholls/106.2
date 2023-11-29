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
        public NpgsqlConnection Connection { get; set; }
        public UpdateBookPopup()
        {
            InitializeComponent();
        }
        public string bookid { set; get; }
        private void UpdateBook_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; this.Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; this.Close();
        }
        public string ImagePath { get; set; }
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
        private void Updatecover_Click(object sender, RoutedEventArgs e)
        {
            GetImage();
            NpgsqlCommand cmd = new NpgsqlCommand($"UPDATE book SET image = '{ImagePath}' WHERE bookid = {bookid}", Connection);
            cmd.ExecuteNonQuery();
        }
    }
}
