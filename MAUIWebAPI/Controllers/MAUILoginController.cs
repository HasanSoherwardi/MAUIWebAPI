using MAUIWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MAUIWebAPI.Controllers
{
    public class MAUILoginController : ApiController
    {
        SqlConnection conn;
        private void SqlConn()
        {
            string conString = ConfigurationManager.ConnectionStrings["DBCSFile"].ToString();
            conn = new SqlConnection(conString);
        }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            List<User> fileData = new List<User>();
            SqlConn();
            SqlCommand cmd = new SqlCommand("Select * From TblUser", conn);
            //cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(id));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                    User file1 = new User();
                    file1.Id = Convert.ToInt32(rdr["Id"]);
                    file1.Name = rdr["Name"].ToString();
                    file1.DOB = Convert.ToDateTime(rdr["DOB"].ToString());
                    file1.POB = rdr["POB"].ToString();
                    file1.Email = rdr["Email"].ToString();
                    file1.UserId = rdr["UserId"].ToString();
                    file1.Password = rdr["Password"].ToString();
                    file1.myArray = (byte[])rdr["myArray"];
                    fileData.Add(file1);
            }
            conn.Close();
            return fileData;
        }

        [HttpGet]
        public List<User> GetUser(string UserId, string Password)
        {
                List<User> fileData = new List<User>();
                SqlConn();
                SqlCommand cmd = new SqlCommand("Select * From TblUser Where UserId=@Id and Password=@Pass", conn);
                cmd.Parameters.AddWithValue("@Id", UserId.ToString());
                cmd.Parameters.AddWithValue("@Pass", Password.ToString());
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    User file1 = new User();
                    file1.Id = Convert.ToInt32(rdr["Id"]);
                    file1.Name = rdr["Name"].ToString();
                    file1.DOB = Convert.ToDateTime(rdr["DOB"].ToString());
                    file1.POB = rdr["POB"].ToString();
                    file1.Email = rdr["Email"].ToString();
                    file1.UserId = rdr["UserId"].ToString();
                    file1.Password = rdr["Password"].ToString();
                    file1.myArray = (byte[])rdr["myArray"];
         
                    //Code for Download Image File from Database.
                    //-------------------------------------------
                    //response.Content = new ByteArrayContent(file1.myArray);

                    ////Set the Response Content Length.
                    //response.Content.Headers.ContentLength = file1.myArray.LongLength;

                    ////Set the Content Disposition Header Value and FileName.
                    //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    //response.Content.Headers.ContentDisposition.FileName = file1.Name;

                    ////Set the File Content Type.
                    //response.Content.Headers.ContentType = new MediaTypeHeaderValue("Image");

                    fileData.Add(file1);
                }
                conn.Close();
                return fileData;
        }


        [HttpPost]
        public Response SaveUser(User userdata)
        {
            Response response = new Response();
            SqlConn();
            conn.Open();
            try
            {
                string sSQL = "insert into TblUser (Name,DOB,POB,Email,UserId,Password,myArray)values(@Name,@DOB,@POB,@Email,@UserId,@Password,@myArray)";
                var cmd = new SqlCommand(sSQL, conn);
                cmd.Parameters.AddWithValue("@Name", userdata.Name.ToString());
                cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(userdata.DOB.ToString()));
                cmd.Parameters.AddWithValue("@POB", userdata.POB.ToString());
                cmd.Parameters.AddWithValue("@Email", userdata.Email.ToString());
                cmd.Parameters.AddWithValue("@UserId", userdata.UserId.ToString());
                cmd.Parameters.AddWithValue("@Password", userdata.Password.ToString());
                cmd.Parameters.AddWithValue("@myArray", (byte[])userdata.myArray);

                int temp = 0;

                temp = cmd.ExecuteNonQuery();
                if (temp > 0)
                {
                    conn.Close();
                    response.GetMessage = "Register successfully";
                    response.Status = 1;
                }
                else
                {
                    conn.Close();
                    response.GetMessage = "Registration Failed. Try with different UserId.";
                    response.Status = 0;
                }

            }
            catch (Exception ex)
            {
                conn.Close();
                response.GetMessage = ex.Message;
                response.Status = 0;
            }
            finally
            {
                conn.Close();
            }
            return response;
        }

        [HttpPut]
        public Response UpdateUser(User userdata)
        {
            Response response = new Response();
            SqlConn();
            conn.Open();
            try
            {
                string sSQL = "Update TblUser set Name=@Name,DOB=@DOB,POB=@POB,Email=@Email,UserId=@UserId,Password=@Password,myArray=@myArray Where Id=@Id";
                var cmd = new SqlCommand(sSQL, conn);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(userdata.Id.ToString()));
                cmd.Parameters.AddWithValue("@Name", userdata.Name.ToString());
                cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(userdata.DOB.ToString()));
                cmd.Parameters.AddWithValue("@POB", userdata.POB.ToString());
                cmd.Parameters.AddWithValue("@Email", userdata.Email.ToString());
                cmd.Parameters.AddWithValue("@UserId", userdata.UserId.ToString());
                cmd.Parameters.AddWithValue("@Password", userdata.Password.ToString());
                cmd.Parameters.AddWithValue("@myArray", (byte[])userdata.myArray);

                int temp = 0;

                temp = cmd.ExecuteNonQuery();
                if (temp > 0)
                {
                    conn.Close();
                    response.GetMessage = "Update successfully";
                    response.Status = 1;
                }
                else
                {
                    conn.Close();
                    response.GetMessage = "Updation Failed. Try with different UserId.";
                    response.Status = 0;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                response.GetMessage = ex.Message;
                response.Status = 0;
            }
            finally
            {
                conn.Close();
            }
            return response;
        }

        [HttpDelete]
        public Response DeleteUser(int Uid)
        {
            Response response = new Response();
            SqlConn();
            conn.Open();
            try
            {
                string sSQL = "Delete from TblUser Where Id=@Id";
                var cmd = new SqlCommand(sSQL, conn);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(Uid.ToString()));
                
                int temp = 0;

                temp = cmd.ExecuteNonQuery();
                if (temp > 0)
                {
                    conn.Close();
                    response.GetMessage = "Delete successfully";
                    response.Status = 1;
                }
                else
                {
                    conn.Close();
                    response.GetMessage = "Deletion Failed.";
                    response.Status = 0;
                }

            }
            catch (Exception ex)
            {
                conn.Close();
                response.GetMessage = ex.Message;
                response.Status = 0;
            }
            finally
            {
                conn.Close();
            }
            return response;
        }
    }
}
