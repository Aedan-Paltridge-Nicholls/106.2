﻿using Npgsql;
using Xceed.Wpf.Toolkit;
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

namespace _106._2
{
    /// <summary>
    /// Interaction logic for Addmemberpopup.xaml
    /// </summary>
    public partial class Addmemberpopup : Window
    {
        public Addmemberpopup()
        {
            InitializeComponent();
            
        }

        public static NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
        public bool Admin = false;

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false; this.Close();
            
        }
        public string CMD {  get; set; }
        private void Addbutton_Click_1(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection conn = SqlCONN;
            conn.Open();

            string idnumber , number = IdNumberBOX.Text , Name = txtUser.Text.Trim(),password = txtPassword.Password.Trim();
            int Removetext = number.IndexOf(':') + 3;
            idnumber = number.Remove(0, Removetext);
            if (txtUser.Text != "" || txtPassword.Text != ""|| number == "")
            {
                string loginQuery = CMD + " " + "INSERT INTO logins(username,userpassword,isadmin,Userid)" +
                                   $"VALUES (\'{Name}\',\'{password}\',\'{Admin.ToString()}\',(SELECT number FROM members WHERE number = {idnumber}  ))";
                NpgsqlCommand command = new NpgsqlCommand(loginQuery, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                string message = $"You Must enter a Username and Password \nfor user number : {idnumber} ",
                       caption = "Error Must give Login";
                Xceed.Wpf.Toolkit.MessageBox.Show(message, caption,MessageBoxButton.OK,MessageBoxImage.Stop);
                conn.Close();
                return;
            }
            conn.Close();
            this.DialogResult = true; this.Close();


        }

        private void IsAdminCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Admin = true;
        }

        private void IsAdminCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Admin = false;
        }
    }
}
