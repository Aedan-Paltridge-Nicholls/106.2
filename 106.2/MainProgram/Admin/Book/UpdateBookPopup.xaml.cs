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
        public UpdateBookPopup()
        {
            InitializeComponent();
        }

        private void UpdateBook_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; this.Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; this.Close();
        }

        private void Updatecover_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
