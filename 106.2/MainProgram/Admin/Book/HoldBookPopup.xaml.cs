﻿using Npgsql;
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
    /// Interaction logic for HoldBookPopup.xaml
    /// </summary>
    public partial class HoldBookPopup : Window
    {
        /// <summary>
        /// This is a popup for putting a book on hold
        /// </summary>
        public HoldBookPopup()
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
        /// This stores the DueDate 
        /// </summary>
        private void HoldBook_Click(object sender, RoutedEventArgs e)
        {
            using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
            {
                SqlCONN.Open();
                string command = "UPDATE  booklog " +
                                       $" SET onhold = 'true', holdid = {MemberId} " +
                                       $" WHERE bookID = {SelectedBookId} ";
                var cmd = new NpgsqlCommand(command, SqlCONN);
                cmd.ExecuteNonQuery();
                SqlCONN.Close();

            }
            this.Close();
        }
        /// <summary>
        /// This Button Cancels putting a book on hold
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
