using System;
using System.Collections.Generic;
using System.Data;
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

namespace Video_Store
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        
        Rent_Cust  Obj_RentCust = new Rent_Cust();
        Login_Video Obj_LoginVideo = new Login_Video();
        

        public int CustID;
        public int MoviedID;
        private object dialogResult;

        public Main()
        {
            InitializeComponent();
            dateissue.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string FirstName = Firsttb.Text;
            string LastName = Lasttb.Text;
            string Address = Addresstb_txt.Text;
            string Phone = Phonetb.Text;
            int CustID = Convert.ToInt32(Custid.Text);
            Obj_RentCust.CustomerUp(CustID ,  FirstName, LastName, Address, Phone);
            
            Customer_data.ItemsSource = Obj_RentCust .Loadcustomer().DefaultView;
            Firsttb.Text = "";
            Lasttb.Text = "";
            Phonetb.Text = "";
            Addresstb_txt.Text = "";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            if (Firsttb.Text != "" && Lasttb.Text != "" && Addresstb_txt.Text != "" && Phonetb.Text != "")
            {
                Obj_RentCust.CustomerInsert(  Firsttb.Text, Lasttb.Text, Addresstb_txt.Text, Phonetb.Text);
                Customer_data.ItemsSource = Obj_RentCust.Loadcustomer().DefaultView;
                Addresstb_txt.Text = "";
                Phonetb.Text = "";
                Firsttb.Text = "";
                Lasttb.Text = "";
                

            }
        }

        private void DeletecustomerClick(object sender, RoutedEventArgs e)
        {
            int CustID = Convert.ToInt32(Custid.Text);
           
                Obj_RentCust.CustomerDelete(CustID);
                Customer_data.ItemsSource = Obj_RentCust.Loadcustomer().DefaultView;
                Firsttb.Text = "";
                Addresstb_txt.Text = "";
                Lasttb.Text = "";
               
                Phonetb.Text = "";
            
        }

        private void Customer_load(object sender, RoutedEventArgs e)
        {
           
            Customer_data.ItemsSource = Obj_RentCust.Loadcustomer().DefaultView;
        }

        private void Select(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)Customer_data.SelectedItems[0];
            Custid.Text = Convert.ToString(row["CustID"]);
            Firsttb.Text = Convert.ToString(row["FirstName"]);
            Lasttb.Text = Convert.ToString(row["Lastname"]);
            Addresstb_txt.Text = Convert.ToString(row["Address"]);
            Phonetb.Text = Convert.ToString(row["Phone"]);

            Customer_data.ItemsSource = Obj_RentCust.Loadcustomer().DefaultView;
        }

        private void AddMovies_Click(object sender, RoutedEventArgs e)
        {
            
            if (Rating_txt.Text != "" && Title_txt.Text != "" && Year_tx.Text != "" &&  Plot_txt.Text != "" && Genre_txt.Text != "" && copies_txt.Text != "")
            {
                int Moyear = Convert.ToInt32(Year_tx.Text);
                int copies = Convert.ToInt32(copies_txt.Text);
                string rent;
                if (2018 - Moyear > 5)
                {
                    rent = "2";
                        
                }
                else
                {
                    rent = "5";
                }

                Obj_LoginVideo.AddVideos(Rating_txt.Text, Title_txt.Text, Year_tx.Text, rent, Plot_txt.Text, Genre_txt.Text, copies);
                Video_data.ItemsSource = Obj_LoginVideo.LoadMovies().DefaultView;
                Title_txt.Text = ""; 
                Rating_txt.Text = "";
                Plot_txt.Text = "";
                Year_tx.Text = "";
                Genre_txt.Text = "";
                copies_txt.Text = "";

            }
        }

        private void UpdateMovies_Click(object sender, RoutedEventArgs e)
        {
            int MoviedID = Convert.ToInt32(Movieidt.Text);
            int copies = Convert.ToInt32(copies_txt.Text);


            string Title = Title_txt.Text;
            string Rating = Rating_txt.Text;
            string Plot = Plot_txt.Text;
            string Genre = Genre_txt.Text;
            int Year = Convert.ToInt32(Year_tx.Text);
            Obj_LoginVideo .UpdateVideo( MoviedID, Rating, Title, Year, Plot, Genre, copies);
            MessageBox.Show("Video Updated");
            Video_data.ItemsSource = Obj_LoginVideo.LoadMovies().DefaultView;
            Title_txt.Text = "";
            Rating_txt.Text = "";
            Plot_txt.Text = "";
            Year_tx.Text = "";
            Genre_txt.Text = "";
            copies_txt.Text = "";
        }

        private void DeleteMovie(object sender, RoutedEventArgs e)
        {
            
                int movie = Convert.ToInt32(Movieidt.Text);



                Obj_LoginVideo .DeleteVideo( movie);
            Video_data.ItemsSource = Obj_LoginVideo.LoadMovies().DefaultView;
                Title_txt.Text = "";
                Rating_txt.Text = "";
                Plot_txt.Text = "";
                Year_tx.Text = "";
                Genre_txt.Text = "";
                Movieidt.Text = "";


            
            


        }

        private void Video_loaded(object sender, RoutedEventArgs e)
        {
            Video_data.ItemsSource = Obj_LoginVideo.LoadMovies().DefaultView;
        }

        private void SelectMovieRow_Edit(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)Video_data.SelectedItems[0];
            Title_txt.Text = Convert.ToString(row["Title"]);
            Plot_txt.Text = Convert.ToString(row["Plot"]);
            Genre_txt.Text = Convert.ToString(row["Genre"]);
            Year_tx.Text = Convert.ToString(row["Year"]);
            Rating_txt.Text = Convert.ToString(row["Rating"]);
            Movieidt.Text = Convert.ToString(row["MovieID"]);
            copies_txt.Text = Convert.ToString(row["copies"]);

            Video_data.ItemsSource = Obj_LoginVideo.LoadMovies().DefaultView;
        }

        private void TabControl_SelectionChanged()
        {

        }

        private void Movieid_txt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Movieid_txt_Copy2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Returned_Click(object sender, RoutedEventArgs e)
        {
            int RMID = Convert.ToInt32(Rmid_txt.Text);
            int MoviedID = Convert.ToInt32(Movieidt.Text);
            


            Obj_RentCust.UpdateRented(RMID, MoviedID, Convert.ToDateTime(dateissue.Text), DateTime.Now);

            Rental_data.ItemsSource = Obj_RentCust .ListRented().DefaultView;
            Video_data.ItemsSource = Obj_LoginVideo .LoadMovies().DefaultView;
            Rental_data.ItemsSource = Obj_RentCust .ListRented().DefaultView;
            Customer_data.ItemsSource = Obj_RentCust.Loadcustomer().DefaultView;
            Movieidt.Text = "";
            Custid.Text = "";
            Title_txt.Text = "";
            Plot_txt.Text = "";
            Genre_txt.Text = "";
            Year_tx.Text = "";
            Rating_txt.Text = "";
            Movieidt.Text = "";
            copies_txt.Text = "";
            Firsttb.Text = "";
            Lasttb.Text = "";
            Addresstb_txt.Text = "";
            Phonetb.Text = "";


        }

        private void Video_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Customer_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Issue_btn_Click(object sender, RoutedEventArgs e)
        {
            if (copies_txt.Text != "0")

            {
                if (Movieidt.Text != "" && Custid.Text != "" && dateissue.Text != "")
                {
                    int MovieID = Convert.ToInt32(Movieidt.Text);
                    int Customerid = Convert.ToInt32(Custid.Text);
                    dateissue.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    int copies = Convert.ToInt32(copies_txt.Text);
                    int Rented = 1;
                   


                    Obj_RentCust.AddRented(MovieID, Customerid, DateTime.Now, copies, Rented);
                    Video_data.ItemsSource = Obj_LoginVideo.LoadMovies().DefaultView;
                    Rental_data.ItemsSource = Obj_RentCust.ListRented().DefaultView;
                    Customer_data.ItemsSource = Obj_RentCust .Loadcustomer().DefaultView;
                    Movieidt.Text = "";
                    Custid.Text = "";
                    Year_tx.Text = "";
                    Rating_txt.Text = "";
                    Movieidt.Text = "";
                    copies_txt.Text = "";
                    Firsttb.Text = "";
                    Title_txt.Text = "";
                    Plot_txt.Text = "";
                    Genre_txt.Text = "";
                   
                    Lasttb.Text = "";
                    Addresstb_txt.Text = "";
                    Phonetb.Text = "";

                }

            }
                else
                {
                    MessageBox.Show("All Copies Are Out Rented");
                }

        }

        private void Rental_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Rented(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)Rental_data.SelectedItems[0];
            Movieidt.Text = Convert.ToString(row["MovieIDFK"]);
            Custid.Text = Convert.ToString(row["CustIDFK"]);
            Rmid_txt.Text = Convert.ToString(row["RMID"]);
            dateissue.Text = Convert.ToString(row["DateRented"]);
            dateretuned.Text = DateTime.Now.ToString("dd-MM-yyyy");



            Rental_data.ItemsSource = Obj_RentCust.ListRented().DefaultView;
        }

        private void video_load(object sender, RoutedEventArgs e)
        {
            Rental_data.ItemsSource = Obj_RentCust.ListRented().DefaultView;

        }

        private void rented(object sender, RoutedEventArgs e)
        {
            Rental_data.ItemsSource = Obj_RentCust.ListRented().DefaultView;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {

            int RMID = Convert.ToInt32(Rmid_txt.Text);
            dateretuned.Text = DateTime.Today.ToString("dd-MM-yyyy");
            int MoviedID = Convert.ToInt32(Movieidt.Text);



            Obj_RentCust.UpdateRented(RMID, MoviedID, Convert.ToDateTime(dateissue.Text), DateTime.Now);
            Video_data.ItemsSource = Obj_LoginVideo.LoadMovies().DefaultView;
            Rental_data.ItemsSource = Obj_RentCust.ListRented().DefaultView;
            Customer_data.ItemsSource = Obj_RentCust.Loadcustomer().DefaultView;
            Movieidt.Text = "";
            Custid.Text = "";
            Genre_txt.Text = "";
            Year_tx.Text = "";
            Rating_txt.Text = "";
            Movieidt.Text = "";
            copies_txt.Text = "";
            Firsttb.Text = "";
            Title_txt.Text = "";
            Plot_txt.Text = "";
            
            Lasttb.Text = "";
            Addresstb_txt.Text = "";
            Phonetb.Text = "";


        }

        private void Topcust_btn_Click(object sender, RoutedEventArgs e)
        {
            Obj_RentCust.BestBuy();
        }

        private void Topmovie_Click(object sender, RoutedEventArgs e)
        {
            Obj_RentCust.Top_Mov();
        }
    }
}
