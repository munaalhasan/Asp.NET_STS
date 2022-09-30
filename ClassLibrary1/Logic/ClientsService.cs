using SupportTicketsSystem.Services.Data;
using SupportTicketsSystem.Services.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportTicketsSystem.Services.Logic
{
    public class ClientsService
    {
        IClientsDataAccess clientsDataAccess;
        public ClientsService(IClientsDataAccess data)
        {
            clientsDataAccess = data;
        }

        public List<Client> GetAllClients()
        {
            try
            {
                return clientsDataAccess.GetAllClientsDB();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Client GetClientByID(int clientID)
        {
            return clientsDataAccess.GetClient(clientID);
        }
        public Client GetClientByEmail(string Email)
        {
            return clientsDataAccess.GetClientByEmail(Email);
        }
        public void Delete(int clientID)
        {
            clientsDataAccess.DeleteClient(clientID);
        }
        public List<Client> GetAllDeletedClients()
        {
            return clientsDataAccess.GetAllDeletedClients();
        }
        public void RestoreClientById(int clientID)
        {
            clientsDataAccess.Restore(clientID);
        }          
        public void Edit(Client updatedClient)
        {
            clientsDataAccess.EditClient(updatedClient);
        }       
        public void AddClient(Client newClient)
        {
            clientsDataAccess.AddClient(newClient);
        }       
        
    }
}
