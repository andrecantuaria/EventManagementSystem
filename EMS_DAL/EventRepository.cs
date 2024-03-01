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
    /// <summary>
    /// EventRepository is responsible for connecting to DB and performing and executing commands
    /// </summary>
    /// 
    public class EventRepository
    {

        public List<Event> GetEvents()
        {
            // Create a list
            List<Event> events = new List<Event>();

            // Connect to DB with 'using' block.
            // The connection will be opened and used to interact with the database, and upon exiting the using block,
            // the SqlConnection connection will be automatically closed.
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                // Comamand to execute the store procedure to get all events.
                string commandText = "usp_GetAllEvents";
                SqlCommand sqlCommand = new SqlCommand(commandText, conn); //// creating an new sql command object
                sqlCommand.CommandType = CommandType.StoredProcedure; ////it says that the command type is a store procedure

                // at this point, ADO.NET will fire query using command.
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                // at this point, query is fired and the data table has been filled with data/records
                adapter.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    events.Add(
                        new Event
                        {
                            EventID = Convert.ToInt32(dr["EventID"]),
                            EventName = Convert.ToString(dr["EventName"]),
                            EventDescription = Convert.ToString(dr["EventDescription"]),
                            StartDate = Convert.ToDateTime(dr["StartDate"]),
                            EndDate = Convert.ToDateTime(dr["EndDate"])

                        });
                }

                return events;

            }

        }


        public bool AddEvent(Event events) // bool e util pra indicar se a operacao de adicao foi bem sucedida ou nao (true or false)
        {
            //Declara uma conexão com o banco de dados dentro de um bloco using, garantindo que a conexão será fechada automaticamente ao final do bloco. 
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                // parameter to be accepeted by store procedure
                SqlCommand sqlCommand = new SqlCommand("usp_InsertEvents", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EventName", events.EventName);  // "@Destination" (parameter string), trip.Destination (value)
                sqlCommand.Parameters.AddWithValue("@EventDescription", events.EventDescription);
                sqlCommand.Parameters.AddWithValue("@StartDate", events.StartDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", events.EndDate);

                conn.Open();
                //execute non query will fire (acionar) the query/execute store procedure (para adicionar/armazenar os dados)
                // and will return 1 if the operation is successful, 0 if unsuccessful
                // PT O método ExecuteNonQuery() é um método utilizado em objetos SqlCommand em ADO.NET para executar comandos SQL
                // que não retornam dados, como comandos de inserção, exclusão ou atualização- como o nome diz nao e uma consulta.
                int i = sqlCommand.ExecuteNonQuery();
                conn.Close();

                // ExecuteNonQuery() retorna o numero de linha afetadas. Casa nao haja linha afetada ele retorna o numero 0.
                // dai essa logica abaixo.
                // We use ExecuteNonQuery() when we want to perform DML Operations (Data Manipulation Language) - INSERT, UPDATE, DELETE, RECOVER data

                if (i > 0) // lembrado que "i" representa o comando sqlCommand.ExecuteNonQuery(). 
                    return true; // sucesso - 1 ou mais linhas foram afetadas
                else
                    return false; // caso seja 0 returna "nao sucedido" porque nenhuma linha foi afetada.

            }
        }


        public bool UpdateEvent(Event events)
        {
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {

                string commandText = "usp_UpdateEvent";

                SqlCommand sqlCommand = new SqlCommand(commandText, conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EventID", events.EventID);
                sqlCommand.Parameters.AddWithValue("@EventName", events.EventName);
                sqlCommand.Parameters.AddWithValue("@EventDescription", events.EventDescription);
                sqlCommand.Parameters.AddWithValue("@StartDate", events.StartDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", events.EndDate);

                conn.Open();
                int i = sqlCommand.ExecuteNonQuery();
                conn.Close();

                if (i > 0)
                    return true;
                else
                    return false;

            }

        }

        // Repository method witch deletes the trip from db
        public bool DeleteEvent(int eventID)

        {

            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                string commandText = "usp_DeleteEvent";
                SqlCommand sqlCommand = new SqlCommand(commandText, conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@EventId", eventID);

                conn.Open();
                int i = sqlCommand.ExecuteNonQuery();
                conn.Close();

                if (i > 0)
                    return true;
                else
                    return false;


            }

        }





    }
    
}
