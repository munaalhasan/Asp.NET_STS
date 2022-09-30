using Microsoft.Extensions.Configuration;
using SupportTicketsSystem.Services.Logic.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SupportTicketsSystem.Services.Data.DB
{
    public class DataAccessDBTickets : ITicketsDataAccess
    {
        private readonly IConfiguration _configuration;

        public string ConnectionString { get; set; }

        public DataAccessDBTickets(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetSection("Settings")["ConnectionString"];
        }

        public DataAccessDBTickets(string connectionstring)
        {
            ConnectionString = connectionstring;
        }

        public List<Ticket> GetAllTicketsDB()
        {
            List<Ticket> ticketsList = new List<Ticket>();
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT t.ID,c.FullName,c.Email
                                            ,Description
                                            ,SerialNumber
                                            ,Confirmed
                                            FROM Tickets t
                                      Left Join Clients c on t.AssignedToClientID=c.ID
                                      WHERE t.isDeleted=0";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ticket ticket = new Ticket();
                    ticket.ID = Convert.ToInt32(reader["ID"].ToString());
                    ticket.Description = reader["Description"].ToString();
                    ticket.SerialNumber = Convert.ToInt32(reader["SerialNumber"].ToString());
                    if (DBNull.Value.Equals(reader["FullName"]) || DBNull.Value.Equals(reader["Email"]))
                    {
                        ticket.client.FullName = " ";
                        ticket.client.Email = " ";
                    }
                    else
                    {
                        ticket.client.FullName = reader["FullName"].ToString();
                        ticket.client.Email = reader["Email"].ToString();
                    }
                    ticket.Confirmed =Convert.ToBoolean(reader["Confirmed"]);
                    ticketsList.Add(ticket);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return ticketsList;
        }

        public Ticket GetTicket(int ticketId)
        {
            Ticket ticket = new Ticket();
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT t.ID,c.FullName,c.Email
                                            ,t.AssignedToClientID
                                            ,Description
                                            ,SerialNumber     
                                            FROM Tickets t
                                            Left Join Clients c on t.AssignedToClientID=c.ID
                                            WHERE t.ID=@ID";

                command.Parameters.AddWithValue("ID", ticketId);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    ticket.ID = ticketId;
                    ticket.Description = row["Description"].ToString();
                    ticket.SerialNumber = Convert.ToInt32(row["SerialNumber"].ToString());
                    if (DBNull.Value.Equals(row["AssignedToClientID"]))
                    {
                        ticket.client.ID = 0;                        
                        
                    }
                    else if (DBNull.Value.Equals(row["FullName"]))
                    {
                        ticket.client.FullName = " ";
                    }
                    else if (DBNull.Value.Equals(row["Email"]))
                    {
                        ticket.client.Email = " ";
                    }
                    else
                    {
                        ticket.client.ID = Convert.ToInt32(row["AssignedToClientID"].ToString()); 
                        ticket.client.FullName = row["FullName"].ToString();
                        ticket.client.Email = row["Email"].ToString();
                    }
                }
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ticket;
        }

        public void DeleteTicket(int ticketId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"UPDATE Tickets
                                               SET isDeleted = 1
                                               WHERE ID =@ID";

                command.Parameters.AddWithValue("ID", ticketId);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Ticket> GetAllDeletedTickets()
        {
            List<Ticket> ticketsList = new List<Ticket>();
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT ID
                                            ,Description
                                            ,SerialNumber     
                                            FROM Tickets 
                                            WHERE IsDeleted=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Ticket ticket = new Ticket();
                    ticket.ID = Convert.ToInt32(reader["ID"].ToString());
                    ticket.Description = reader["Description"].ToString();
                    ticket.SerialNumber = Convert.ToInt32(reader["SerialNumber"].ToString());
                    ticketsList.Add(ticket);
                }

                reader.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return ticketsList;
        }

        public void Restore(int ticketId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"UPDATE Tickets
                                               SET isDeleted = 0
                                               WHERE ID =@ID";

                command.Parameters.AddWithValue("ID", ticketId);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void CreateTicket(Ticket newTicket)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = @"INSERT INTO Tickets
                                               (Description
                                               ,SerialNumber)
                                         VALUES
                                               (@Description
                                               ,@SerialNumber)";

                command.Parameters.AddWithValue("Description", newTicket.Description);
                command.Parameters.AddWithValue("SerialNumber", newTicket.SerialNumber);

                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditTicket(Ticket ticket)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.Connection.Open();
                command.CommandText = @"UPDATE Tickets
                                                   SET Description = @Description
                                                      ,SerialNumber = @SerialNumber                                                     
                                                      ,Confirmed = @Confirmed
                                                      ,AssignedToClientID=@AssignedToClientID
                                                 WHERE ID =@ID";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("ID", ticket.ID);
                command.Parameters.AddWithValue("Description", ticket.Description);
                command.Parameters.AddWithValue("SerialNumber", ticket.SerialNumber);
                command.Parameters.AddWithValue("AssignedToClientID", ticket.client.ID);
                command.Parameters.AddWithValue("Confirmed", ticket.Confirmed);
                command.ExecuteNonQuery();
                command.Connection.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateRequest(TicketsRequest newRequest)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.CommandText = @"INSERT INTO TicketsRequest
                                               (ClientID
                                               ,TicketID
                                               ,StatusID)
                                         VALUES
                                               (@ClientID
                                               ,@TicketID
                                               ,@StatusID)";

                command.Parameters.AddWithValue("ClientID", newRequest.client.ID);
                command.Parameters.AddWithValue("TicketID", newRequest.ticket.ID);
                command.Parameters.AddWithValue("StatusID", (int)newRequest.status);

                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditRequest(TicketsRequest request)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            try
            {
                command.Connection.Open();
                command.CommandText = @"UPDATE TicketsRequest
                                                   SET ClientID = @ClientID
                                                      ,TicketID = @TicketID
                                                      ,StatusID = @StatusID
                                                 WHERE ID =@ID";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("ID", request.ID);
                command.Parameters.AddWithValue("ClientID", request.client.ID);
                command.Parameters.AddWithValue("TicketID", request.ticket.ID);
                command.Parameters.AddWithValue("StatusID", (int)request.status);

                command.ExecuteNonQuery();
                command.Connection.Close();

            }
            catch (Exception)
            {
                throw;
            }

        }

        public TicketsRequest GetRequest(int requestId)
        {
            TicketsRequest ticketsRequest = new TicketsRequest();
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT tr.ID
                                              ,ClientID
                                              ,TicketID
                                              ,StatusID
                                           FROM TicketsRequest tr
                                           Join Clients c on tr.ClientID = c.ID
                                           Join Tickets t on tr.TicketID = t.ID
                                           Join RequestStatus s on tr.StatusID = s.ID
                                           WHERE tr.ID=@ID";

                command.Parameters.AddWithValue("ID", requestId);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    ticketsRequest.ID = requestId;
                    ticketsRequest.client.ID = Convert.ToInt32(row["ClientID"].ToString());
                    ticketsRequest.ticket.ID = Convert.ToInt32(row["TicketID"].ToString());
                    ticketsRequest.status = (Status)Enum.Parse(typeof(Status), row["StatusID"].ToString());                   
                }
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ticketsRequest;

        }

        public int GetID(TicketsRequest ticketsRequest)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT ID
                                       ,[ClientID]
                                       ,[TicketID]
                                       ,[StatusID]
                               FROM [TicketsRequest] 
                               WHERE ClientID=@ClientID AND TicketID=@TicketID AND StatusID=@StatusID";

                command.Parameters.AddWithValue("ClientID", ticketsRequest.client.ID);
                command.Parameters.AddWithValue("TicketID", ticketsRequest.ticket.ID);
                command.Parameters.AddWithValue("StatusID", (int)ticketsRequest.status);
                
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    ticketsRequest.ID = Convert.ToInt32(row["ID"].ToString()); 
                }
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ticketsRequest.ID;

        }
        
        public List<TicketsRequest> GetAllTicketsRequests()
        {
            List<TicketsRequest> requestsList = new List<TicketsRequest>();
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT tr.ID
                                              ,ClientID
                                              ,TicketID
                                              ,StatusID
                                           FROM TicketsRequest tr
                                           Join Clients c on tr.ClientID = c.ID
                                           Join Tickets t on tr.TicketID = t.ID
                                           Join RequestStatus s on tr.StatusID = s.ID";

                               
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TicketsRequest ticketsRequest = new TicketsRequest();
                    ticketsRequest.ID = Convert.ToInt32(reader["ID"].ToString());
                    ticketsRequest.client.ID = Convert.ToInt32(reader["ClientID"].ToString());
                    ticketsRequest.ticket.ID = Convert.ToInt32(reader["StatusID"].ToString());
                    ticketsRequest.status = (Status)Enum.Parse(typeof(Status), reader["StatusID"].ToString());
                    requestsList.Add(ticketsRequest);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return requestsList;

        }

        public List<TicketsRequest> GetAllUserRequests(int clientId)
        {
            List<TicketsRequest> requestsList = new List<TicketsRequest>();
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT tr.ID
                                              ,ClientID
                                              ,TicketID
                                              ,StatusID
                                           FROM TicketsRequest tr
                                           Join Clients c on tr.ClientID = c.ID
                                           Join Tickets t on tr.TicketID = t.ID
                                           Join RequestStatus s on tr.StatusID = s.ID
                                           WHERE tr.ClientID=@ClientID";
                
                command.Parameters.AddWithValue("ClientID", clientId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TicketsRequest ticketsRequest = new TicketsRequest();                    
                    ticketsRequest.ID = Convert.ToInt32(reader["ID"].ToString()); ;
                    //ticketsRequest.client.ID = Convert.ToInt32(reader["ClientID"].ToString());
                    ticketsRequest.ticket.ID = Convert.ToInt32(reader["TicketID"].ToString());
                    ticketsRequest.status = (Status)Enum.Parse(typeof(Status), reader["StatusID"].ToString());
                    requestsList.Add(ticketsRequest);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return requestsList;

        }

    }


}
