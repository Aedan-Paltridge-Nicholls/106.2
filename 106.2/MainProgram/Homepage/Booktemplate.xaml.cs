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

namespace _106._2.MainProgram.Homepage
{
    /// <summary>
    /// Interaction logic for Booktemplate.xaml
    /// </summary>
    public partial class Booktemplate : UserControl
    {
        public Booktemplate()
        {
            InitializeComponent();
        }



        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void btnPopup_Click(object sender, RoutedEventArgs e)
        {
            BookPopUP bookPopUP = new BookPopUP();
           
            bookPopUP.ShowDialog();
        }
    }
}
