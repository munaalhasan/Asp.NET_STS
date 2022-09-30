using SupportTicketsSystem.Services.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportTicketsSystem.Services.Data
{
    public interface IClientsDataAccess
    {
        public List<Client> GetAllClientsDB();
        public Client GetClient(int clientId);
        public Client GetClientByEmail(string Email);
        public void DeleteClient(int clientId);
        public List<Client> GetAllDeletedClients();
        public void Restore(int clientId);   
        public void EditClient(Client client);       
        public void AddClient(Client newClient);

    }
}
