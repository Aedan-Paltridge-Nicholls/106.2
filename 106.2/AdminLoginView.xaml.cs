using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Numerics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace _106._2
{
    /// <summary>
    /// Interaction logic for AdminLoginView.xaml
    /// </summary>
    
    public partial class AdminLoginView : Page
    {
        public class MemberdataStorage 
        {
            string number, name, phonenumbers, email, joindate, address;
            // Setters 
            public void Set_number(string Input)
            {
                number = Input.Trim();

            }
            public void Set_Name(string Input)
            {
                name = Input.Trim();

            }
            public void Set_phonenumbers(string Input)
            {
                phonenumbers = Input.Trim();

            }
            public void Set_email(string Input)
            {
                email = Input.Trim();

            }
            public void Set_joindate(string Input)
            {
                joindate = Input;

            }
            public void Set_address(string Input)
            {
                address = Input.Trim();

            }
            // Getters
            public string Get_number() { return number; }
            public string Get_name() { return name; }
            public string Get_phonenumbers() { return phonenumbers; }
            public string Get_email() { return email; }
            public string Get_joindate() { return joindate; }
            public string Get_address() { return address; }

           public MemberdataStorage() 
           {
                Set_number("");
                Set_Name("");
                Set_phonenumbers("");
                Set_email("");
                Set_joindate("");
                Set_address("");

           }
        }
        public enum Searchtype 
        {
            number, name, phonenumbers, email, joindate, address
        }

        public Searchtype searchtype = new Searchtype();
        
        public MemberdataStorage Memberdata = new();
        
        public string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;";
        public void LoadDatagrid()
        {


            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM Members"; // Replace with your SQL query
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    membersdatagrid.ItemsSource = dt.DefaultView; // Replace 'dataGrid' with your DataGrid's name
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }
            }


        }
        public void LoadDatagrid(int Input)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = $"SELECT * FROM Members WHERE number = {Input} "; // Replace with your SQL query
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    membersdatagrid.ItemsSource = dt.DefaultView; // Replace 'dataGrid' with your DataGrid's name
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }
            }
        }
        public void LoadDatagrid(string Input)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = $"SELECT * FROM Members WHERE joindate::date  = '{Input}' "; // Replace with your SQL query
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    membersdatagrid.ItemsSource = dt.DefaultView; // Replace 'dataGrid' with your DataGrid's name
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }
            }
        }
        public string  searchtypestringer(Searchtype type)
        {
            switch (type)
            {
                case Searchtype.number:
                     return "number";   
                case Searchtype.name:
                  return "name";
                case Searchtype.phonenumbers:
                    return "phonenumbers";
                case Searchtype.email:
                    return "email";
                case Searchtype.joindate:
                    return "joindata";
                case Searchtype.address: 
                    return "address";
                default:
                return "";
                
            }
        

        }
        public void LoadDatagrid(string Search,Searchtype type)
        { 

            if (searchtypestringer(type) == "number") 
            {
                int Output = int.Parse(Search);
                LoadDatagrid(Output);  
            }
            else
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                { 
                    try
                    {
                        con.Open();
                        string searchtypeout = searchtypestringer(type);

                        string sql = $"SELECT * FROM Members WHERE {type} ILIKE \'%{Search}%\' "; // Replace with your SQL query
                        NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);
                        membersdatagrid.ItemsSource = dt.DefaultView; // Replace 'dataGrid' with your DataGrid's name
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally { con.Close(); }
                }
            }
        }
        public AdminLoginView()
        {
            
            CultureInfo Dtfi = CultureInfo.CreateSpecificCulture("en-us");
            Dtfi.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            Dtfi.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = Dtfi;
            InitializeComponent();
            LoadDatagrid();
            JoinDataBOX.SelectedDate = DateTime.Now;
        }
        public async void addmember(string number, string name, string phonenumbers, string email, string joindate, string address)
        {
             
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString)) 
            {
                try
                {
                    con.Open();
                    string command = "insert into members (number, name, phonenumbers, email, joindate, address)" +
                               $" values ( {number}, '{name}', '{phonenumbers}', '{email}', '{joindate}', '{address}' );";
                    var cmd = new NpgsqlCommand(command, con);
                    await using var reader = await cmd.ExecuteReaderAsync();
                    MessageBox.Show(reader.ToString());
                    con.Open();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally {con.Close(); } 
            
            }
        }
        public async void UpdateMember(string number, string name, string phonenumbers, string email, string joindate, string address)
        {

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    string command = $"UPDATE members SET name = {name}, phonenumbers = {phonenumbers}, email = {email}, joindate = {joindate}, address = {address} WHERE number = {number} ";
                    var cmd = new NpgsqlCommand(command, con);
                    await using var reader = await cmd.ExecuteReaderAsync();
                    MessageBox.Show(reader.ToString());
                    con.Open();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }

            }
        }
        public bool numericinput =false;
        public bool Dateinput = false;
        // Buttons
        private void AddMemberBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string OutNumber = Memberdata.Get_number(),
                OutName = Memberdata.Get_name(),
                OutPhonenumbers = Memberdata.Get_phonenumbers(),
                OutEmail = Memberdata.Get_email() ,
                OutJoindate = Memberdata.Get_joindate(),
                OutAddress = Memberdata.Get_address();
            addmember(OutNumber, OutName, OutPhonenumbers, OutEmail, OutJoindate, OutAddress);


        }
        private void ChangeInfoBUTTON_Click(object sender, RoutedEventArgs e)
        {
            string OutNumber = Memberdata.Get_number(),
                OutName = Memberdata.Get_name(),
                OutPhonenumbers = Memberdata.Get_phonenumbers(),
                OutEmail = Memberdata.Get_email(),
                OutJoindate = Memberdata.Get_joindate(),
                OutAddress = Memberdata.Get_address();
            UpdateMember(OutNumber, OutName, OutPhonenumbers, OutEmail, OutJoindate, OutAddress);
        }
        // Textboxes

        private void MembernumberBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            Memberdata.Set_number(MembernumberBOX.Text);
        }

        private void NameBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            Memberdata.Set_Name(NameBOX.Text);
        }
        // Datepicker
        private void JoinDataBOX_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string Date = JoinDataBOX.SelectedDate.ToString();
            Date = Date.Remove(Date.IndexOf(' '));
            Memberdata.Set_joindate(Date);

            if (Dateinput) { LoadDatagrid(Date); }

        }
        // Textboxes
        private void EmailBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
          Memberdata.Set_email(EmailBOX.Text);
        }

        private void PhonenumberBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            Memberdata.Set_phonenumbers(PhonenumberBOX.Text);
        }

        private void AddressBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            Memberdata.Set_address(AddressBOX.Text);
        }
        // Search 
        public void Search(string SearchOut)
        {
            if (string.IsNullOrEmpty(SearchOut)) {LoadDatagrid();}
            else 
            { 
                switch(SearchOptionBOX.SelectedIndex)
                {
                    case 0:
                        {  searchtype = Searchtype.name;}
                        break;
                    case 1:
                        {  searchtype = Searchtype.number;}
                        break;
                    case 2:
                        {  searchtype = Searchtype.phonenumbers;}
                        break;
                    case 3:
                        {  searchtype = Searchtype.email;}
                        break;
                    case 4:
                        {   searchtype = Searchtype.joindate;}
                        break;
                    case 5:
                        {  searchtype = Searchtype.address;}
                        break;
                    default:
                    break;
                }
                LoadDatagrid(SearchOut, searchtype);
            }
        }
        
        private void SearchBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            string SearchOut = "" ;
            if (numericinput)
            {
                int Number;
                bool numeric = int.TryParse(SearchBOX.Text, out Number ); 
                if (numeric)
                {
                     SearchOut = SearchBOX.Text; 
                }
                else 
                { SearchBOX.Text = ""; }
                Search(SearchOut);
            }
            else if (Dateinput) 
            {
                SearchBOX.Text = " Use the date time picker To search";
            }
            else 
            {
                SearchOut = SearchBOX.Text;
                Search(SearchOut);
            }
          
        }
        private void SearchOptionBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                numericinput = (SearchOptionBOX.SelectedIndex == 1) ? true : false;
                Dateinput = (SearchOptionBOX.SelectedIndex == 4) ? true : false;
            if (Dateinput)
            {
                SearchBOX.Text = "Please use the date time picker";


            }
        }

        private void membersdatagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
    }
}
