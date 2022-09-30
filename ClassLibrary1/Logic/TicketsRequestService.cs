using SupportTicketsSystem.Services.Data;
using SupportTicketsSystem.Services.Logic.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportTicketsSystem.Services.Logic
{
    public class TicketsRequestService
    {
        ITicketsDataAccess ticketsDataAccess;
        public TicketsRequestService(ITicketsDataAccess data)
        {
            ticketsDataAccess = data;
        }
        public List<TicketsRequest> GetAllTicketsRequests()
        {
            return ticketsDataAccess.GetAllTicketsRequests();
        }
        public List<TicketsRequest> GetAllUserRequests(int clientId)
        {
            return ticketsDataAccess.GetAllUserRequests(clientId);
        }
        public TicketsRequest GetTicketRequest(int requestId)
        {
            return ticketsDataAccess.GetRequest(requestId);
        }
        public int GetRequestID(TicketsRequest ticketsRequest)
        {
            return ticketsDataAccess.GetID(ticketsRequest);
        }
        public void CreateTicketRequest(TicketsRequest newTicketRequest)
        {
            ticketsDataAccess.CreateRequest(newTicketRequest);
        }
        public void EditTicketRequest(TicketsRequest newTicketRequest)
        {
            ticketsDataAccess.EditRequest(newTicketRequest);
        }
        
    }
}
