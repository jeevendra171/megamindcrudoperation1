using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.Helpers;

namespace MINDCRUD1.Models
{


    public class CustomerModelDal
    {
     
        string cs = ConfigurationManager.ConnectionStrings["MegaMindEntities1"].ConnectionString.ToString();
        public int InsertCustomer(CustomerModel customerModel)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", customerModel.Name);
                cmd.Parameters.AddWithValue("@Email", customerModel.Email);
                cmd.Parameters.AddWithValue("@PhoneNo", customerModel.PhoneNo);
                cmd.Parameters.AddWithValue("@Address", customerModel.Address);
                cmd.Parameters.AddWithValue("@State", customerModel.State);
                cmd.Parameters.AddWithValue("@City", customerModel.City);
                cmd.Parameters.AddWithValue("@Agree", customerModel.Agree);

                con.Open();
                cmd.ExecuteScalar();
                // ExecuteNonQuery is used for operations that do not return rows
                // ExecuteScalar can be used to get the value returned by the stored procedure
                //id = Convert.ToInt32(cmd.ExecuteScalar()); // Assuming the stored procedure returns the ID of the inserted record
                con.Close();
            }
            return 1;
        }


        public int UpdateCustomer(CustomerModel customerModel)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateUserInfos", con); 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", customerModel.UserId);
                    cmd.Parameters.AddWithValue("@Name", customerModel.Name);
                    cmd.Parameters.AddWithValue("@Email", customerModel.Email);
                    cmd.Parameters.AddWithValue("@PhoneNo", customerModel.PhoneNo);
                    cmd.Parameters.AddWithValue("@Address", customerModel.Address);
                    cmd.Parameters.AddWithValue("@State", customerModel.State);
                    cmd.Parameters.AddWithValue("@City", customerModel.City);
                    cmd.Parameters.AddWithValue("@Agree", customerModel.Agree);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            return rowsAffected;
        }


        public int DeleteCustomer(int UserId)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_DeleteUserInfos", con); 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log them, or rethrow
                    // For simplicity, let's just log the exception message
                    Console.WriteLine("Error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            return rowsAffected;
        }


        public List<CustomerModel> GetCustomers()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectUserInfos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        customers.Add(new CustomerModel
                        {
                            UserId = Convert.ToInt32(dr["UserId"]),
                            Name = dr["Name"].ToString(),
                            Email = dr["Email"].ToString(),
                            PhoneNo = dr["PhoneNo"].ToString(),
                            Address = dr["Address"].ToString(),
                            State = dr["State"].ToString(),
                            City = dr["City"].ToString(),
                            Agree = Convert.ToBoolean(dr["Agree"])
                        });
                    }
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine("Error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            return customers;
        }

    }
}
        