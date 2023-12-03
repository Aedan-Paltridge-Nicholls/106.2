using Npgsql;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace _106._2.MainProgram.Admin.Book
{
    /// <summary>
    /// Interaction logic for WithdrawBookPopup.xaml
    /// </summary>
    public partial class WithdrawBookPopup : Window
    {
        /// <summary>
        /// This is a popup for issuing / Withdrawing a book
        /// </summary>
        public WithdrawBookPopup()
        {
            
            
            CultureInfo Dtfi = CultureInfo.CreateSpecificCulture("en-us");
            Dtfi.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            Dtfi.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = Dtfi;
            InitializeComponent();
            DueDatePicker.BlackoutDates.AddDatesInPast();
            DueDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now , DateTime.Now.AddDays(10)));


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
        /// This stores the DueDate 
        /// </summary>
        public string DueDate { get; set; }
        /// <summary>
        /// this Button Issues a book
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WithdrawBook_Click(object sender, RoutedEventArgs e)
        {
            using(NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {
                if(DueDatePicker.Text == "") { MessageBox.Show("Must select a due date."); return; }
                SqlCONN.Open();
                string command = "UPDATE  booklog " +
                                       $" SET withdrawn = 'true',returned = 'false', overdue ='false', issuedid = {MemberId} , duedate = '{DueDate}' " +
                                       $" WHERE bookID = {SelectedBookId} ";
                            var cmd = new NpgsqlCommand(command, SqlCONN);
                            cmd.ExecuteNonQuery();
                SqlCONN.Close();

            }
            this.Close();
        }
        /// <summary>
        /// This Button Cancels issuing a book 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///  This lets a member set the duedate 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DueDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DueDate = DueDatePicker.SelectedDate.ToString();
            DueDate = DueDate.Remove(DueDate.IndexOf(' '));
        }


    }
}
