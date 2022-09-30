using SupportTicketsSystem.Services.Data;
using SupportTicketsSystem.Services.Logic.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportTicketsSystem.Services.Logic
{
    public class TicketsService
    {
        ITicketsDataAccess ticketsDataAccess;
        public TicketsService(ITicketsDataAccess data)
        {
            ticketsDataAccess = data;
        }

        public List<Ticket> GetAllTickets()
        {
            try
            {
                return ticketsDataAccess.GetAllTicketsDB();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Ticket GetTicketByID(int ticketID)
        {
            return ticketsDataAccess.GetTicket(ticketID);
        }        
        public void Delete(int ticketID)
        {
            ticketsDataAccess.DeleteTicket(ticketID);
        }
        public List<Ticket> GetAllDeletedTickets()
        {
            return ticketsDataAccess.GetAllDeletedTickets();
        }
        public void RestoreTicketById(int ticketID)
        {
            ticketsDataAccess.Restore(ticketID);
        }
        public void Edit(Ticket updatedClient)
        {
            ticketsDataAccess.EditTicket(updatedClient);
        }
        public void CreateTicket(Ticket newTicket)
        {
            ticketsDataAccess.CreateTicket(newTicket);
        }
    }
}
