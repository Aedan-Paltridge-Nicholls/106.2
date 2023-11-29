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
    using System.Windows.Controls.Primitives;
    using Xceed.Wpf.AvalonDock.Themes;
    using Xceed.Wpf.Toolkit.Primitives;
    using static _106._2.AdminLoginView;
    using System.Reflection;

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
  
       
       

      

            public void LoadDatagrid(string comm)
            {
                NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
                comm = (comm == null) ? "SELECT members.*, logins.username,logins.isadmin  " +
                "FROM members  FULL JOIN logins ON logins.userid  = number ORDER BY number; " : comm;
                int Memberindex = (membersdatagrid.SelectedIndex != -1)? membersdatagrid.SelectedIndex :  -1;

                NpgsqlCommand cmd = new NpgsqlCommand(comm, SqlCONN);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
              adapter.Fill(ds, "members");
                membersdatagrid.ItemsSource = ds.Tables["members"].DefaultView;
                membersdatagrid.DataContext = ds;
           
                membersdatagrid.SelectedIndex = Memberindex;
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
                        return "joindate";
                    case Searchtype.address: 
                        return "address";
                    default:
                    return "";
                
                }
        

            }
            public void RefreshGrid() 
            {
              LoadDatagrid(null);
            }
            public void LoadDatagrid(string Search,Searchtype type)
            {
            
                string typeNow = searchtypestringer(type);
                switch (typeNow)
                {

                    case "number":
                        int Output = int.Parse(Search);
                        LoadDatagrid($"SELECT * FROM Members WHERE {typeNow} = {Output} ");
                        break;
                    case "name":
                        LoadDatagrid($"SELECT * FROM Members WHERE {typeNow} ILIKE \'%{Search}%\' ");
                        break;
                    case "phonenumbers":
                        LoadDatagrid($"SELECT * FROM Members WHERE {typeNow} ILIKE \'%{Search}%\' ");
                        break;
                    case "email":
                        LoadDatagrid($"SELECT * FROM Members WHERE {typeNow} ILIKE \'%{Search}%\' ");
                        break;
                    case "joindate":
                        LoadDatagrid($"SELECT * FROM Members WHERE {typeNow} = \'{Search}\' ");
                        break;
                    case "address":
                        LoadDatagrid($"SELECT * FROM Members WHERE {typeNow} ILIKE \'%{Search}%\' ");
                        break;
                    default:
                        break;
                };
                RefreshGrid();


            }
            public AdminLoginView()
            {
           

                CultureInfo Dtfi = CultureInfo.CreateSpecificCulture("en-us");
                Dtfi.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
                Dtfi.DateTimeFormat.DateSeparator = "-";
                Thread.CurrentThread.CurrentCulture = Dtfi;
                InitializeComponent();
                LoadDatagrid("SELECT members.*, logins.username,logins.isadmin  " +
                    "FROM members FULL JOIN logins ON logins.userid  = number ORDER BY number;");
                JoinDataBOX.SelectedDate = DateTime.Today;
            }
            public class MemberInfoComferm 
            {
                public string number, name, phonenumbers, email, joindate, address;
                public string getinfo() 
                { 
                
                    return number;

                }
        
            }

            public async void addmember(string number, string name, string phonenumbers, string email, string joindate, string address)
            {
                using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;")) 
                {

                    try
                    {                
                        if (number == "") { MessageBox.Show(" Must enter Member id number"); return; }
                        SqlCONN.Open();
                        Addmemberpopup addmemberpopup = new Addmemberpopup();

                        string command = "insert into members (number, name, phonenumbers, email, joindate, address)" +
                            $" values ( {number}, '{name}', '{phonenumbers}', '{email}', '{joindate}', '{address}' );";
                    
                    
                        string info = $"Member ID Number :{number} {Environment.NewLine} " +
                                      $"Member Name: {name}   {Environment.NewLine}" +
                                      $"Member Phonenumber: {phonenumbers}  {Environment.NewLine}" +
                                      $"Member Email: {email}  {Environment.NewLine}" +
                                      $"Member Join-Date: {joindate}   {Environment.NewLine}" +
                                      $"Member Address: {address}   {Environment.NewLine}";
                        addmemberpopup.MemberInfoBox.Text = info;
                        addmemberpopup.IdNumberBOX.Text = $"ID Number :{Environment.NewLine}{number}";
                        bool? NotCanceled = addmemberpopup.ShowDialog();
                        if ( NotCanceled != null && NotCanceled == true)
                        {
                        
                            var cmd = new NpgsqlCommand(command, SqlCONN);
                            await using var reader = await cmd.ExecuteReaderAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally { SqlCONN.Close(); } 
            
                }
            }
            public  void UpdateMember(string number, string name, string phonenumbers, string email, string joindate, string address)
            {

                using (NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;"))
                {
                    try
                    {
                        if (number == "") { MessageBox.Show(" Must enter Member id number"); return; }
                        SqlCONN.Open();
                        Updatememberpopup updatememberpopup = new Updatememberpopup();
                        string command = $"UPDATE members SET name = \'{name}\', phonenumbers = \'{phonenumbers}\', email = \'{email}\', joindate = \'{joindate}\', address = \'{address}\'" +
                                         $" WHERE number = {number} ",

                           NEWinfo = $"Member ID Number :{number} {Environment.NewLine} " +
                                     $"Member Name: {name}   {Environment.NewLine}" +
                                     $"Member Phonenumber: {phonenumbers}  {Environment.NewLine}" +
                                     $"Member Email: {email}  {Environment.NewLine}" +
                                     $"Member Join-Date: {joindate}   {Environment.NewLine}" +
                                     $"Member Address: {address}   {Environment.NewLine}",
                           OLDinfo =  "";
                        var cmdsql = new NpgsqlCommand($"SELECT * FROM members WHERE number  = {number} ", SqlCONN);
                        using NpgsqlDataReader readerOne = cmdsql.ExecuteReader();
                        while (readerOne.Read())
                        {
                            for (int i = 1; i < readerOne.FieldCount; i++)
                            {
                                OLDinfo += (readerOne.GetValue(i)).ToString() + '|' ;
                            }
                        }
                        string[] strings = OLDinfo.Split('|');
                        string OLDname = strings[0], OLDphonenumbers = strings[1], OLDemail = strings[2], OLDjoindate = strings[3], OLDaddress = strings[4];
                        string[] strings1  = OLDjoindate.Split(' ');
                        OLDjoindate = strings1[0];
                        OLDinfo = $"Member ID Number :{number} {Environment.NewLine} " +
                                  $"Member Name: {OLDname}   {Environment.NewLine}" +
                                  $"Member Phonenumber: {OLDphonenumbers}  {Environment.NewLine}" +
                                  $"Member Email: {OLDemail}  {Environment.NewLine}" +
                                  $"Member Join-Date: {OLDjoindate}   {Environment.NewLine}" +
                                  $"Member Address: {OLDaddress}   {Environment.NewLine}";
                        updatememberpopup.NewMemberInfo.Text = NEWinfo;
                        updatememberpopup.OldMemberInfo.Text = OLDinfo;
                        updatememberpopup.IdUpdateNumber = number;
                        SqlCONN.Close();
                        SqlCONN.Open();

                        bool? NotCanceled = updatememberpopup.ShowDialog();
                        if (NotCanceled != null && NotCanceled == true)
                        {
                            var cmd = new NpgsqlCommand(command, SqlCONN);

                            using var reader = cmd.ExecuteReader();
                        }



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                     }
                    finally { SqlCONN.Close(); }

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
                RefreshGrid();

            }
            private void ChangeInfoBUTTON_Click(object sender, RoutedEventArgs e)
            {
               if (membersdatagrid.SelectedIndex != -1) 
               { 
                string OutNumber = Memberdata.Get_number(),
                    OutName = Memberdata.Get_name(),
                    OutPhonenumbers = Memberdata.Get_phonenumbers(),
                    OutEmail = Memberdata.Get_email(),
                    OutJoindate = Memberdata.Get_joindate().Remove(Memberdata.Get_joindate().IndexOf(" ")),
                    OutAddress = Memberdata.Get_address();
                UpdateMember(OutNumber, OutName, OutPhonenumbers, OutEmail, OutJoindate, OutAddress);
               }
                RefreshGrid();
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
                if (membersdatagrid.SelectedIndex == 0)
                {
                    Date = Date.Remove(Date.IndexOf(' '));
                    Memberdata.Set_joindate(Date);
                    if (Dateinput)
                    {
                        Searchtype outtype;
                        outtype = Searchtype.joindate;
                        LoadDatagrid(Date, outtype);
                    }
                }
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
                if (string.IsNullOrEmpty(SearchOut)) {LoadDatagrid("");}
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
                if (Dateinput) { SearchBOX.Clear(); }
                numericinput = (SearchOptionBOX.SelectedIndex == 1) ? true : false;
                Dateinput = (SearchOptionBOX.SelectedIndex == 4) ? true : false;
                if (Dateinput)
                {
                    SearchBOX.Text = "Please use the date time picker";
               

                }
            }

        
       
                
            


            private void membersdatagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
            {

                NpgsqlConnection SqlCONN = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=Nicholls2004;Database=106.2;");
                SqlCONN.Open();
                if(membersdatagrid.SelectedIndex != -1)
                { 
                    DataGrid dg = (DataGrid)sender;
                    DataRowView selectedRow = dg.SelectedItem as DataRowView;
               
                
                
                    AddressBOX.Text = selectedRow[5].ToString();
                    JoinDataBOX.Text = selectedRow[4].ToString();
                    EmailBOX.Text = selectedRow[3].ToString(); 
                    PhonenumberBOX.Text = selectedRow[2].ToString();
                    NameBOX.Text = selectedRow[1].ToString();
                    MembernumberBOX.Text = selectedRow[0].ToString();
                    Memberdata.Set_joindate(selectedRow[4].ToString());
                }
                SqlCONN.Close();
            }

        

            private void ClearSearch_Click(object sender, RoutedEventArgs e)
            {
                SearchBOX.Clear();
                SearchOptionBOX.SelectedIndex = -1;
                RefreshGrid();
            }

            private void ResetMemberDataGridSelection_Click(object sender, RoutedEventArgs e)
            {
                membersdatagrid.SelectedIndex = -1;
                NameBOX.Clear();
                MembernumberBOX.Clear();
                PhonenumberBOX.Clear();
                JoinDataBOX.Text = DateTime.Now.ToString();
                EmailBOX.Clear();
                AddressBOX.Clear();
                RefreshGrid();
            }
        }
    }
