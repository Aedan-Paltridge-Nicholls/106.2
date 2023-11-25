
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

namespace _106._2
{
    /// <summary>
    /// Interaction logic for Updatememberpopup.xaml
    /// </summary>
    public partial class Updatememberpopup : Window
    {
        public Updatememberpopup()
        {
            InitializeComponent();
        }
        public string IdUpdateNumber { get; set; }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; this.Close();
        }

        private void Updatemember_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; this.Close();
        }

        private void UpdateLogin_Click(object sender, RoutedEventArgs e)
        {
            UpdateloginPopup updateloginPopup = new UpdateloginPopup();
            updateloginPopup.member_IdBox = $"ID Number : {Environment.NewLine} {IdUpdateNumber}";
            updateloginPopup.ShowDialog();
        }
    }
}
