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
            NpgsqlConnection connectionString = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");

            InitializeComponent();

        }

        private void HomeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
                                        
        }

        private void AdminBookButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void DuedateButton_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MembersButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new AdminLoginView();

        }

        private void HomeButton_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
