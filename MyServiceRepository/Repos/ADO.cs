using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceRepository.Repos
{
    public class ADO
    {
        // ADO,
        // used to iteract with Database.


        // Architectures :
        // Connected Arch : A constant connection to DB while data is processed.
        public void ConnectedArch()
        {
            // SSMS -> Database properties -> View Connection properties -> Server Name and Database name.
            // "Data Source=(localdb)\\Local;Initial Catalog=YourDatabaseName;Integrated Security=True;" // ;User ID=YourUsername;Password=YourPassword;
            // Integrated Security if Windows auth, UserID and Passoword if SQL Server Auth.

            string conString = @"Data Source=(localdb)\Local;Initial Catalog=AJDB;Integrated Security=True;";
            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from students (nolock)", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read()) // connection is used while processing data.
                {
                    Console.WriteLine(sqlDataReader["name"]);
                }
            }
        }

        // DisConnected Arch : Data is fetched and conn is closed, processed in memory
        public void DisConnectedArch()
        {
            // SSMS -> Database properties -> View Connection properties -> Server Name and Database name.
            // "Data Source=(localdb)\\Local;Initial Catalog=YourDatabaseName;Integrated Security=True;" // ;User ID=YourUsername;Password=YourPassword;
            // Integrated Security if Windows auth, UserID and Passoword if SQL Server Auth

            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Local;Initial Catalog=AJDB;Integrated Security=True;"))
            {
                sqlConnection.Open();
                DataSet dataSet = new DataSet();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from students (nolock)", sqlConnection);
                sqlDataAdapter.Fill(dataSet); // Output filled so can beused later on.
            }
        }

        public void Pooling()
        {
            // Reusing existing connections. Performance
            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Local;Initial Catalog=AJDB;Integrated Security=True;Pooling=true;Max Pool Size=100"))
            {
                sqlConnection.Open();
                DataSet dataSet = new DataSet();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from students (nolock)", sqlConnection);
                sqlDataAdapter.Fill(dataSet); // Output filled so can beused later on.
            }

            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Local;Initial Catalog=AJDB;Integrated Security=True;Pooling=true;Max Pool Size=100"))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from students (nolock)" , sqlConnection);
                sqlCommand.Parameters.Add(new SqlParameter("@test", 1));

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();// Fast, Forward-only and read-only cursor.
                while (sqlDataReader.Read())
                {

                }
            }

            // SqlDataAdapter fills the dataset and updates the changes in DB.
            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Local;Initial Catalog=AJDB;Integrated Security=True;Pooling=true;Max Pool Size=100"))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Data Source=(localdb)\\Local;Initial Catalog=AJDB;Integrated Security=True;Pooling=true;Max Pool Size=100", sqlConnection);

                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                sqlDataAdapter.Update(dataSet, "TableName");
            }
        }

        public void Transactions()
        {
            using(SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Local;Initial Catalog=AJDB;Integrated Security=True;Pooling=true;Max Pool Size=100"))
            {
                sqlConnection.Open();

                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = new SqlCommand("select * from students" , sqlConnection , sqlTransaction);
                sqlCommand.ExecuteNonQuery(); // Non return commands 

                sqlConnection.Close();
                sqlTransaction.Commit(); //Throws error as connection is closed.
                sqlTransaction.Rollback();

            }
        }

        // Acid properties
        // Atomocity -> Transaction is completed else roll back.
        // Consistent -> DB Transit from one correct state to other
        // Isolation -> Transactions are isolated from each other.
        // Durablity -> If Transaction is committed then data is stored permanently even in case of system failure.

        public void ExecutePocs()
        {
            using (SqlConnection sqlConnection = new SqlConnection(""))
            {
                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@test", 1));

                cmd.ExecuteNonQuery();
            }
        }
    }
}
