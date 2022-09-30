using Microsoft.Extensions.Configuration;
using SupportTicketsSystem.Services.Logic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SupportTicketsSystem.Services.Data.DB
{
    public class DataAccessDB: IClientsDataAccess
    {
        private readonly IConfiguration _configuration;

        public string ConnectionString { get; set; }

        public DataAccessDB(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetSection("Settings")["ConnectionString"];
        }

        public DataAccessDB(string connectionstring)
        {
            ConnectionString = connectionstring;
        }

        public List<Client> GetAllClientsDB()
        {
            List<Client> clientsList = new List<Client>();
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT ID,FullName,Email 
                                           FROM Clients
                                           WHERE IsDeleted=0";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Client client = new Client();
                    client.ID = Convert.ToInt32(reader["ID"].ToString());
                    client.FullName = reader["FullName"].ToString();
                    client.Email = reader["Email"].ToString();
                    clientsList.Add(client);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return clientsList;
        }

        public Client GetClient(int clientId)
        {
            Client client = new Client();
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT ID,FullName,Email
                                            FROM Clients
                                            WHERE ID=@ID";

                command.Parameters.AddWithValue("ID", clientId);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    client.ID = clientId;
                    client.FullName= row["FullName"].ToString();
                    client.Email = row["Email"].ToString();
                }
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return client;
        }

        public Client GetClientByEmail(string Email)
        {
            Client client = null;
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT ID,FullName
                                            FROM Clients
                                             WHERE Email=@Email";

                command.Parameters.AddWithValue("Email", Email);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    client = new Client();
                    client.ID = Convert.ToInt32(row["ID"].ToString());
                    client.FullName = row["FullName"].ToString();                    
                }

                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return client;
        }

        public void Restore(int clientId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"UPDATE Clients
                                               SET IsDeleted = 0
                                               WHERE ID =@ID";

                command.Parameters.AddWithValue("ID", clientId);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void DeleteClient(int clientId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"UPDATE [dbo].[Clients]
                                               SET [IsDeleted] = 1
                                               WHERE ID =@ID";

                command.Parameters.AddWithValue("ID", clientId);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<Client> GetAllDeletedClients()
        {
            List<Client> clientsList = new List<Client>();
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT  ID,FullName,Email                                      
                                            FROM Clients 
                                            WHERE IsDeleted=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Client client=new Client();
                    client.ID= Convert.ToInt32(reader["ID"].ToString());
                    client.FullName = reader["FullName"].ToString();
                    client.Email = reader["Email"].ToString();
                    clientsList.Add(client);
                }

                reader.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return clientsList;
        }

        public void EditClient(Client client)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.Connection.Open();
                command.CommandText = @"UPDATE [dbo].[Clients]
                                                   SET [FullName] = @FullName
                                                      ,Email = @Email                                                     
                                                 WHERE ID =@ID";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("ID", client.ID);
                command.Parameters.AddWithValue("FullName", client.FullName);
                command.Parameters.AddWithValue("Email", client.Email);                

                command.ExecuteNonQuery();
                command.Connection.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }
              
        public void AddClient(Client newClient)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = @"INSERT INTO [dbo].[Clients]
                                               (FullName
                                               ,Email
                                               )
                                         VALUES
                                               (@FullName
                                               ,@Email
                                               )";

                command.Parameters.AddWithValue("ID", newClient.ID);
                command.Parameters.AddWithValue("FullName", newClient.FullName);
                command.Parameters.AddWithValue("Email", newClient.Email);

                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
