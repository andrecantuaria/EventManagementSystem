using System;
using EMS_ENTITIES;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EMS_DAL
{
    public class RegistrationRepository
    {
        public List<Registration> GetRegistrations(int EventID)
        {
            // Create a list
            List<Registration> registrations = new List<Registration>();

            // Connect to DB with 'using' block.
            // The connection will be opened and used to interact with the database, and upon exiting the using block,
            // the SqlConnection connection will be automatically closed.
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                // Comamand to execute the store procedure to get all events.
                string commandText = "usp_GetRegistrations";
                SqlCommand sqlCommand = new SqlCommand(commandText, conn); //// creating an new sql command object
                sqlCommand.CommandType = CommandType.StoredProcedure; ////it says that the command type is a store procedure

                sqlCommand.Parameters.AddWithValue("@EventID", EventID);


                // at this point, ADO.NET will fire query using command.
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                // at this point, query is fired and the data table has been filled with data/records
                adapter.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    registrations.Add(
                        new Registration
                        {
                            EventID = Convert.ToInt32(dr["EventID"]),
                            RegistrationID = Convert.ToInt32(dr["RegistrationID"]),
                            FullName = Convert.ToString(dr["FullName"]),
                            Email = Convert.ToString(dr["Email"]),
                            RegistrationDate = Convert.ToDateTime(dr["RegistrationDate"]),
                            PaymentStatus = Convert.ToString(dr["PaymentStatus"]),


                        });
                }

                return registrations;

            }

        }

        public bool AddRegistration(Registration registrations) // bool is useful for indicating whether the addition operation was successful or not (true or false)
        {
            // Declares a connection to the database within a using block, ensuring that the connection will be automatically closed at the end of the block.
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                // parameter to be accepeted by store procedure
                SqlCommand sqlCommand = new SqlCommand("usp_InsertRegistration", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EventID", registrations.EventID);
                sqlCommand.Parameters.AddWithValue("@FullName", registrations.FullName);  
                sqlCommand.Parameters.AddWithValue("@Email", registrations.Email);
                sqlCommand.Parameters.AddWithValue("@RegistrationDate", registrations.RegistrationDate);
                sqlCommand.Parameters.AddWithValue("@PaymentStatus", registrations.PaymentStatus);

                conn.Open();

                // The ExecuteNonQuery() method will trigger the execution of the query or stored procedure (to add or store the data).
                // and will return 1 if the operation is successful, 0 if unsuccessful
                // The ExecuteNonQuery() method is used in SqlCommand objects in ADO.NET to execute SQL commands that do not return data, such as insertion,
                // deletion, or update commands - as the name suggests, it is not a query.
                int i = sqlCommand.ExecuteNonQuery();
                conn.Close();

                // ExecuteNonQuery() returns the number of rows affected. If no rows are affected, it returns 0.

                // We use ExecuteNonQuery() when we want to perform DML Operations (Data Manipulation Language) - INSERT, UPDATE, DELETE, RECOVER data

                if (i > 0) // Remember that "i" represents the result of the sqlCommand.ExecuteNonQuery() command.
                    return true; // Success - 1 or more rows were affected
                else
                    return false; // if it's 0 it returns "unsuccessful" because no rows were affected.

            }
        }


        public bool UpdateRegistration(Registration registrations)
        {
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                string commandText = "usp_UpdateRegistration";
                SqlCommand sqlCommand = new SqlCommand(commandText, conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@EventID", registrations.EventID);
                sqlCommand.Parameters.AddWithValue("@RegistrationID", registrations.RegistrationID);
                sqlCommand.Parameters.AddWithValue("@FullName", registrations.FullName);
                sqlCommand.Parameters.AddWithValue("@Email", registrations.Email);
                sqlCommand.Parameters.AddWithValue("@RegistrationDate", registrations.RegistrationDate);
                sqlCommand.Parameters.AddWithValue("@PaymentStatus", registrations.PaymentStatus);

                conn.Open();
                
                int i = sqlCommand.ExecuteNonQuery();
                conn.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool DeleteRegistration(int RegistrationID, int EventID)
        {
            using (SqlConnection conne = new SqlConnection(Connection.ConnectionString))
            {
                string commandText = "usp_DeteleRegistration";
                SqlCommand sqlCommand = new SqlCommand(commandText, conne);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@RegistrationID", RegistrationID);
                sqlCommand.Parameters.AddWithValue("@EventID", EventID);

                conne.Open();
                int i = sqlCommand.ExecuteNonQuery();
                conne.Close();

                if (i > 0)
                    return true;
                else
                    return false;

            }
        }







    }
}
