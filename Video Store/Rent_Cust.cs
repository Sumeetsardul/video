using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Video_Store
{
    class Rent_Cust
    {
        SqlConnection Rent_custcon = new SqlConnection("Data Source=gill-pc\\sqlexpress;Initial Catalog=VSR_System;Integrated Security=True");

        SqlCommand cmdRent_custcon = new SqlCommand();

        SqlDataReader ReaderRent_custcon;

        String Main;
       String Strr;

        public IEnumerable DefaultView { get; internal set; }
        public string S2 { get; private set; }

        public DataTable Loadcustomer()
        {
            DataTable dt = new DataTable();
            try
            {
                cmdRent_custcon.Connection = Rent_custcon;
                Main = "Select * from Coustmer";

                cmdRent_custcon.CommandText = Main;
                //connection   opened
                Rent_custcon.Open();

                // get data stream
                ReaderRent_custcon = cmdRent_custcon.ExecuteReader();

                if (ReaderRent_custcon.HasRows)
                {
                    dt.Load(ReaderRent_custcon);
                }
                return dt;
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
                return null;
            }
            finally
            {
                // close reader
                if (ReaderRent_custcon != null)
                {
                    ReaderRent_custcon.Close();
                }

                // close connection
                if (Rent_custcon != null)
                {
                    Rent_custcon.Close();
                }
            }

        }



        public void CustomerInsert(string FirstName, string LastName, string Address, string Phone)
        {
            try
            {
                cmdRent_custcon.Parameters.Clear();
                cmdRent_custcon.Connection = Rent_custcon;



                Main = "Insert into Coustmer(FirstName, LastName, Address, Phone) Values( @FirstName, @LastName, @Address, @Phone)";


                cmdRent_custcon.Parameters.AddWithValue("@FirstName", FirstName);
                cmdRent_custcon.Parameters.AddWithValue("@LastName", LastName);
                cmdRent_custcon.Parameters.AddWithValue("@Address", Address);
                cmdRent_custcon.Parameters.AddWithValue("@Phone", Phone);

                cmdRent_custcon.CommandText = Main;

                //connection opened
                Rent_custcon.Open();

                // Executed query
                cmdRent_custcon.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Rent_custcon != null)
                {
                    Rent_custcon.Close();
                }
            }
        }

        public void CustomerDelete(Int32 CustID)
        {
            try
            {
                cmdRent_custcon.Parameters.Clear();
                cmdRent_custcon.Connection = this.Rent_custcon;


                String Strr = "";
                Strr = "select Count(*) from RentedMovies where CustIDFK= @CustID and isout ='1' ";
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@CustID", CustID) };
                cmdRent_custcon.Parameters.Add(parameterArray[0]);

                cmdRent_custcon.CommandText = Strr;
                Rent_custcon.Open();
                int count = Convert.ToInt32(cmdRent_custcon.ExecuteScalar());
                if (count == 0)
                {
                    Strr = "Delete from Coustmer where CustID like @CustID";
                    cmdRent_custcon.CommandText = Strr;
                    cmdRent_custcon.ExecuteNonQuery();
                    MessageBox.Show("User Deleted");
                }
                else
                {
                    //display the message if he has a movie on rent 
                    MessageBox.Show("Customer has taken a movie on Rent");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception" + exception.Message);
            }
            finally
            {
                if (this.Rent_custcon != null)
                {
                    this.Rent_custcon.Close();
                }
            }
        }



        public void CustomerUp(int CustID, string FirstName, string LastName, string Address, string Phone)
        {
            try
            {
                cmdRent_custcon.Parameters.Clear();
                cmdRent_custcon.Connection = Rent_custcon;
                Main = "Update Coustmer Set FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone where CustID = @CustID";


                cmdRent_custcon.Parameters.AddWithValue("@CustID", CustID);
                cmdRent_custcon.Parameters.AddWithValue("@FirstName", FirstName);
                cmdRent_custcon.Parameters.AddWithValue("@LastName", LastName);
                cmdRent_custcon.Parameters.AddWithValue("@Address", Address);
                cmdRent_custcon.Parameters.AddWithValue("@Phone", Phone);

                cmdRent_custcon.CommandText = Main;

                //connection opened
                Rent_custcon.Open();

                // Executed query
                cmdRent_custcon.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Rent_custcon != null)
                {
                    Rent_custcon.Close();
                }
            }
        }
        public DataTable ListRented()
        {
            DataTable dt = new DataTable();
            try
            {
                cmdRent_custcon.Connection = Rent_custcon;
                Main = "Select * from RentedMovies";

                cmdRent_custcon.CommandText = Main;
                //connection   opened
                Rent_custcon.Open();

                // get data stream
                ReaderRent_custcon = cmdRent_custcon.ExecuteReader();

                if (ReaderRent_custcon.HasRows)
                {
                    dt.Load(ReaderRent_custcon);
                }
                return dt;
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
                return null;
            }
            finally
            {
                // close reader
                if (ReaderRent_custcon != null)
                {
                    ReaderRent_custcon.Close();
                }

                // close connection
                if (Rent_custcon != null)
                {
                    Rent_custcon.Close();
                }
            }

        }



        public void AddRented(int MovieIDFK, int CustIDFK, DateTime DateRented, int copies, int isout)
        {
            try
            {
                cmdRent_custcon.Parameters.Clear();
                cmdRent_custcon.Connection = Rent_custcon;



                Main = "Insert into RentedMovies(MovieIDFK, CustIDFK, DateRented ,isout) Values( @MovieIDFk, @CustIDFK, @DateRented, @isout)";

                cmdRent_custcon.Parameters.AddWithValue("@MovieIDFK", MovieIDFK);
                cmdRent_custcon.Parameters.AddWithValue("@CustIDFK", CustIDFK);
                cmdRent_custcon.Parameters.AddWithValue("@DateRented", DateRented);
                cmdRent_custcon.Parameters.AddWithValue("@isout", isout);
                cmdRent_custcon.Parameters.AddWithValue("@copies", copies);


                cmdRent_custcon.CommandText = Main;

                //connection opened
                Rent_custcon.Open();

                // Executed query
                cmdRent_custcon.ExecuteNonQuery();

                Main = "Update Movies set copies = copies-1 where MovieID = @MovieIDFK";
                cmdRent_custcon.CommandText = Main;
                cmdRent_custcon.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Rent_custcon != null)
                {
                    Rent_custcon.Close();
                }
            }
        }


        public void UpdateRented(int RMID, int MovieID, DateTime DateRent, DateTime DateReturned)
        {
            try
            {
                cmdRent_custcon.Parameters.Clear();
                cmdRent_custcon.Connection = Rent_custcon;
                int RentTotal = 0, Cost = 0;
                double days = (DateReturned - DateRent).TotalDays;

                string S1 = "Select Rental_Cost from Movies where MovieID = @MovieIDFK";
                cmdRent_custcon.Parameters.AddWithValue("@MovieIDFK", MovieID);

                cmdRent_custcon.CommandText = S1;
                Rent_custcon.Open();
                Cost = Convert.ToInt32(cmdRent_custcon.ExecuteScalar());

                if (Convert.ToInt32(days) == 0)
                {
                    RentTotal = Cost;
                }
                else
                {
                    RentTotal = Cost * Convert.ToInt32(days);
                }


                S2 = "Update RentedMovies Set DateReturned='" + DateReturned + "' where RMID = @RMID";
                cmdRent_custcon.Parameters.AddWithValue("@DateReturned", DateReturned);
                cmdRent_custcon.Parameters.AddWithValue("@RMID", RMID);

                cmdRent_custcon.CommandText = S2;

                cmdRent_custcon.ExecuteNonQuery();


                S2 = "Update Movies set Copies = Copies+1 where MovieID = @MovieIDFK";
                this.cmdRent_custcon.CommandText = this.S2;

                this.cmdRent_custcon.ExecuteNonQuery();

                S2 = "Update RentedMovies set isout = 0 where RMID = @RMID";
                this.cmdRent_custcon.CommandText = this.S2;

                this.cmdRent_custcon.ExecuteNonQuery();

                MessageBox.Show("Total Rent is " + RentTotal);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception " + exception.Message);
            }
            finally
            {
                if (Rent_custcon != null)
                {
                    Rent_custcon.Close();
                }
            }


        }

        public void BestBuy()
        {
            int Best_BuyerID = 0, Max_number = 0, Total_Customer = 0;
            string Strr = "";
            try
            {
                cmdRent_custcon.Parameters.Clear();
                cmdRent_custcon.Connection = Rent_custcon;
                string Strr1 = "Select IDENT_CURRENT('Coustmer')";

                cmdRent_custcon.CommandText = Strr1;
                Rent_custcon.Open();
                Total_Customer = Convert.ToInt32(cmdRent_custcon.ExecuteScalar());

                for (int i = 1; i <= Total_Customer; i++)
                {

                    Strr = "select Count(*) from RentedMovies where CustIDFK= '" + i + "'";


                    cmdRent_custcon.CommandText = Strr;
                    int count = Convert.ToInt32(cmdRent_custcon.ExecuteScalar());
                    if (count > Max_number)
                    {
                        Max_number = count;
                        Best_BuyerID = i;
                    }
                }
                this.S2 = "Select FirstName from Coustmer where CustID ='" + Best_BuyerID + "'";
                this.cmdRent_custcon.CommandText = this.S2;
                String FirstName = Convert.ToString(cmdRent_custcon.ExecuteScalar());
                MessageBox.Show(FirstName + " (CustID " + Best_BuyerID + " ) is maximum movie buyer with " + Max_number + " times");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception " + exception.Message);
            }
            finally
            {
                if (Rent_custcon != null)
                {
                    Rent_custcon.Close();
                }
            }

        }


        public void Top_Mov()
        {
            int Top_MovieID = 0, Max_number = 0, Total_Movies = 0;
            
            try
            {
                cmdRent_custcon.Parameters.Clear();
                cmdRent_custcon.Connection = Rent_custcon;
                string Strr1 = "Select IDENT_CURRENT('Movies')";

                cmdRent_custcon.CommandText = Strr1;
                Rent_custcon.Open();
                Total_Movies = Convert.ToInt32(cmdRent_custcon.ExecuteScalar());

                for (int i = 1; i <= Total_Movies; i++)
                {

                    Strr = "select Count(*) from RentedMovies where MovieIDFK= '" + i + "'";


                    cmdRent_custcon.CommandText = Strr;
                    int count = Convert.ToInt32(cmdRent_custcon.ExecuteScalar());
                    if (count > Max_number)
                    {
                        Max_number = count;
                        Top_MovieID = i;
                    }
                }

                String r = "";
                r = "Select Title from Movies where MovieID ='" + Top_MovieID + "'";
                this.cmdRent_custcon.CommandText = r;
                String Title = Convert.ToString(cmdRent_custcon.ExecuteScalar());
                MessageBox.Show(Title + " (MovieID " + Top_MovieID + " ) is maximum rented movie with " + Max_number + " times");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception " + exception.Message);
            }
            finally
            {
                if (Rent_custcon != null)
                {
                    Rent_custcon.Close();
                }
            }

        }
    }
}
