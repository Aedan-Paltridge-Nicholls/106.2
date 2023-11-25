﻿using System;
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
using _106._2.Admin.Book;
using _106._2.MainProgram.Homepage;
using Npgsql;
namespace _106._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        
        public MainWindow()
        {   
            

            InitializeComponent();
            Main.Content = new Home();

        }

        private void HomeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Home();
        }

        private void AdminBookButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new AdminBookView();
        }

        private void DuedateButton_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MembersButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new AdminLoginView();

        }

        
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new login();
        }

        
    }
}