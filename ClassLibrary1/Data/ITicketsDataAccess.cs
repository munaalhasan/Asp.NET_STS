using SupportTicketsSystem.Services.Logic.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportTicketsSystem.Services.Data
{
    public interface ITicketsDataAccess
    {
        public List<Ticket> GetAllTicketsDB();
        public Ticket GetTicket(int ticketId);
        public void DeleteTicket(int ticketId);
        public List<Ticket> GetAllDeletedTickets();
        public void Restore(int ticketId);
        public void CreateTicket(Ticket newTicket);
        public void EditTicket(Ticket ticket);
        public void CreateRequest(TicketsRequest newRequest);
        public void EditRequest(TicketsRequest request);
        public TicketsRequest GetRequest(int requestId);
        public int GetID(TicketsRequest ticketsRequest);
        public List<TicketsRequest> GetAllTicketsRequests();
        public List<TicketsRequest> GetAllUserRequests(int clientId);

    }
}
