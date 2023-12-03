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
    /// Interaction logic for ReturnBookPopup.xaml
    /// </summary>
    public partial class ReturnBookPopup : Window
    {
        /// <summary>
        /// This is a popup for Returning a book 
        /// </summary>
        public ReturnBookPopup()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This stores the members id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// This stores the Selected Book Id 
        /// </summary>
        public string SelectedBookId { get; set; }
        /// <summary>
        /// this Returns a book 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {
                SqlCONN.Open();
                string command = "UPDATE  booklog " +
                                       $" SET onhold = 'false', withdrawn = 'false', overdue = 'false', returned ='true' ,holdid = null , duedate = null , issuedid  = null " +
                                       $" WHERE bookID = {SelectedBookId} ";
                var cmd = new NpgsqlCommand(command, SqlCONN);
                cmd.ExecuteNonQuery();
                SqlCONN.Close();

            }
            this.DialogResult = true; this.Close();
        }
        /// <summary>
        /// This Button Cancels Returning a book 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelbutton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; this.Close();
        }
    }
}
