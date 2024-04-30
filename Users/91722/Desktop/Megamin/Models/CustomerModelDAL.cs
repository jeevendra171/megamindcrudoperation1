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
    public class CustomerModelDAL
    {
        string cs = ConfigurationManager.ConnectionStrings["MegaMindEntities1"].ConnectionString.ToString();

        public bool InsertCustomer(tbl_UserInfo customerModel)
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
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public int UpdateCustomer(CustomerModel1 customerModel)
        //{
        //    int rowsAffected = 0;

        //    using (SqlConnection connection = new SqlConnection(cs))
        //    {
        //        SqlCommand command = new SqlCommand("sp_UpdateUserInfo", connection);
        //        command.CommandType = CommandType.StoredProcedure;

        //        command.Parameters.AddWithValue("@UserId", customerModel.UserId);
        //        command.Parameters.AddWithValue("@Name", customerModel.Name);
        //        command.Parameters.AddWithValue("@Email", customerModel.Email);
        //        command.Parameters.AddWithValue("@PhoneNo", customerModel.PhoneNo);
        //        command.Parameters.AddWithValue("@Address", customerModel.Address);
        //        command.Parameters.AddWithValue("@State", customerModel.State);
        //        command.Parameters.AddWithValue("@City", customerModel.City);
        //        command.Parameters.AddWithValue("@Agree", customerModel.Agree);

        //        connection.Open();
        //        rowsAffected = command.ExecuteNonQuery();
        //        connection.Close();
        //        return rowsAffected > 0;
        //    }


        //}

        public List<tbl_UserInfo> GetCustomers()
        {
            List<tbl_UserInfo> customers = new List<tbl_UserInfo>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SelectUserInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sd.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    customers.Add(
                        new tbl_UserInfo
                        {
                            UserId = Convert.ToInt32(dr["UserId"]),
                            Name = Convert.ToString(dr["Name"]),
                            Email = Convert.ToString(dr["Email"]),
                            PhoneNo = Convert.ToString(dr["PhoneNo"]),
                            Address = Convert.ToString(dr["Address"]),
                            State = Convert.ToString(dr["State"]),
                            City = Convert.ToString(dr["City"]),
                            Agree = Convert.ToBoolean(dr["Agree"])
                        });
                }
            }
            return customers;
        }

        //public int DeleteCustomer(int customerId)
        //{
        //    SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerId = @CustomerId", con);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.AddWithValue("@CustomerId", customerId);
        //    con.Open();
        //    int i = cmd.ExecuteNonQuery();
        //    con.Close();
        //    return i;
        //}
    }
}
