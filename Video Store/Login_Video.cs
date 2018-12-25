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
    class Login_Video
    {
        SqlConnection Login_videocon = new SqlConnection("Data Source=gill-pc\\sqlexpress;Initial Catalog=VSR_System;Integrated Security=True");

        SqlCommand cmdLogin_video = new SqlCommand();

        SqlDataReader Readerlogin_video;

        String Login_video;

        public IEnumerable DefaultView { get; internal set; }




        public DataTable LoadMovies()
        {
            DataTable dt = new DataTable();
            try
            {
                cmdLogin_video.Connection = Login_videocon;
                Login_video = "Select * from Movies";

                cmdLogin_video.CommandText = Login_video;
                //connection   opened
                Login_videocon.Open();

                // get data stream
                Readerlogin_video = cmdLogin_video.ExecuteReader();

                if (Readerlogin_video.HasRows)
                {
                    dt.Load(Readerlogin_video);
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
                if (Readerlogin_video != null)
                {
                    Readerlogin_video.Close();
                }

                // close connection
                if (Login_videocon != null)
                {
                    Login_videocon.Close();
                }
            }

        }



        public void AddVideos(string Rating, string Title, string Year, string Rental_Cost, string Plot, string Genre, int copies)
        {
            try
            {
                cmdLogin_video.Parameters.Clear();
                cmdLogin_video.Connection = Login_videocon;



                Login_video = "Insert into Movies(Rating, Title, Year, Rental_Cost, Plot, Genre, copies) Values( @Rating, @Title, @Year, @Rental_Cost, @Plot, @Genre, @copies)";


                cmdLogin_video.Parameters.AddWithValue("@Rating", Rating);
                cmdLogin_video.Parameters.AddWithValue("@Title", Title);
                cmdLogin_video.Parameters.AddWithValue("@Year", Year);
                cmdLogin_video.Parameters.AddWithValue("@Rental_Cost", Rental_Cost);
                cmdLogin_video.Parameters.AddWithValue("@Plot", Plot);
                cmdLogin_video.Parameters.AddWithValue("@Genre", Genre);
                cmdLogin_video.Parameters.AddWithValue("@copies", copies);

                cmdLogin_video.CommandText = Login_video;

                //connection opened
                Login_videocon.Open();

                // Executed query
                cmdLogin_video.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Login_videocon != null)
                {
                    Login_videocon.Close();
                }
            }
        }

        public void DeleteVideo(int MovieID)
        {
            try
            {
                cmdLogin_video.Parameters.Clear();
                cmdLogin_video.Connection = this.Login_videocon;


                //first of the all select the record from the Rented Movie is he already has a movie on rent or not if he has a movie on rent then he can't be able to delete the record from the table
                String check = "";
                check = "select Count(*) from RentedMovies where MovieIDFK= @MovieID and isout ='1' ";
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@MovieID", MovieID) };
                cmdLogin_video.Parameters.Add(parameterArray[0]);

                cmdLogin_video.CommandText = check;
                Login_videocon.Open();
                int count = Convert.ToInt32(cmdLogin_video.ExecuteScalar());
                if (count == 0)
                {
                    check = "Delete from Movies where MovieID like @MovieID";
                    cmdLogin_video.CommandText = check;
                    cmdLogin_video.ExecuteNonQuery();
                    MessageBox.Show("Movie Deleted");
                }
                else
                {
                    //display the message if he has a movie on rent 
                    MessageBox.Show("First Take the movie back");
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception" + exception.Message);
            }
            finally
            {
                if (this.Login_videocon != null)
                {
                    this.Login_videocon.Close();
                }

            }
        }


        
        public void UpdateVideo(int MovieID, string Rating, string Title, int Year, string Plot, string Genre, int copies)
        {
            try
            {
                cmdLogin_video.Parameters.Clear();
                cmdLogin_video.Connection = Login_videocon;
                Login_video = "Update Movies Set Rating = @Rating, Title = @Title, Year = @Year,  Plot = @Plot, Genre = @Genre, copies = @copies where MovieID like @MovieID";


                cmdLogin_video.Parameters.AddWithValue("@MovieID", MovieID);
                cmdLogin_video.Parameters.AddWithValue("@Rating", Rating);
                cmdLogin_video.Parameters.AddWithValue("@Title", Title);
                cmdLogin_video.Parameters.AddWithValue("@Year", Year);
                cmdLogin_video.Parameters.AddWithValue("@Plot", Plot);
                cmdLogin_video.Parameters.AddWithValue("@Genre", Genre);
                cmdLogin_video.Parameters.AddWithValue("@copies", copies);


                cmdLogin_video.CommandText = Login_video;

                //connection opened
                Login_videocon.Open();

                // Executed query
                cmdLogin_video.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Login_videocon != null)
                {
                    Login_videocon.Close();
                }
            }
        }
        public bool Login_method(string username, string password)
        {
            try
            {
                cmdLogin_video.Connection = Login_videocon;

                Login_video = "Select username, password from userdata where UserName =  @UserName  and Password =  @password ";


                cmdLogin_video.Parameters.AddWithValue("@UserName", username);
                cmdLogin_video.Parameters.AddWithValue("@password", password);

                cmdLogin_video.CommandText = Login_video;
                //connection opened
                Login_videocon.Open();

                // get data stream
                Readerlogin_video = cmdLogin_video.ExecuteReader();

                if (Readerlogin_video.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
                return false;
            }
            finally
            {
                // close reader
                if (Readerlogin_video != null)
                {
                    Readerlogin_video.Close();
                }

                // close connection
                if (Login_videocon != null)
                {
                    Login_videocon.Close();
                }
            }
        }
        public void Regis_method(string username, string password)
        {
            try
            {
                cmdLogin_video.Parameters.Clear();
                cmdLogin_video.Connection = Login_videocon;

                Login_video = "Insert into userdata (UserName, Password) Values(@user, @pass)";
                cmdLogin_video.Parameters.AddWithValue("@user", username);
                cmdLogin_video.Parameters.AddWithValue("@pass", password);

                cmdLogin_video.CommandText = Login_video;
                //connection opened
                Login_videocon.Open();

                // get data stream
                cmdLogin_video.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Login_videocon != null)
                {
                    Login_videocon.Close();
                }
            }
        }
    }
}
