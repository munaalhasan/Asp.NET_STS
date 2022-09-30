using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicketsSystem.Services.Logic;
using SupportTicketsSystem.Services.Logic.Models.Tickets;
using SupportTicketsSystem.Website.Models.Tickets;
using SupportTicketsSystem.Website.Views.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_Tickets_System.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly TicketsService _ticketsService;

        public TicketsController(TicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        public IActionResult Index()
        {
            List<Ticket> tickets = _ticketsService.GetAllTickets();

            TicketViewModel model = new TicketViewModel();

            foreach (var ticket in tickets)
            {
                model.TicketsList.Add(new TicketRecord()
                {
                    TicketId = ticket.ID,
                    TicketDesc = ticket.Description,
                    TicketSerialNumber = ticket.SerialNumber,
                    AssignedToClient = ticket.client.FullName,
                    AssignedToClientEmail =ticket.client.Email,
                    ConfirmedReq=ticket.Confirmed
                });
            }

            return View(model);
        }

        public IActionResult Details(int ticketId)
        {
            Ticket ticket = _ticketsService.GetTicketByID(ticketId);
            TicketDetails model = new TicketDetails
            {
                TicketId = ticket.ID,
                TicketDesc = ticket.Description,
                TicketSerialNumber = ticket.SerialNumber,
                AssignedToClient = ticket.client.FullName,
                AssignedToClientEmail = ticket.client.Email
            };
            return View(model);
        }

        public IActionResult Delete(int ticketId)
        {
            _ticketsService.Delete(ticketId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deleted()
        {
            List<Ticket> tickets = _ticketsService.GetAllDeletedTickets();
            TicketViewModel model = new TicketViewModel();

            foreach (var ticket in tickets)
            {
                model.TicketsList.Add(new TicketRecord()
                {
                    TicketId=ticket.ID,
                    TicketDesc=ticket.Description,
                    TicketSerialNumber=ticket.SerialNumber
                });
            }
            return View(model);
        }


        public IActionResult Restore(int deletedTicketId)
        {
            _ticketsService.RestoreTicketById(deletedTicketId);
            RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateAndEditTicketModel model = new CreateAndEditTicketModel();
            model.IsNewTicket = true;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateAndEditTicketModel newTicket)
        {
            Ticket ticket = MapTicket(newTicket);
           
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(newTicket);
            }
            _ticketsService.CreateTicket(ticket);
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public IActionResult Edit(int ticketId)
        {
            Ticket ticket = _ticketsService.GetTicketByID(ticketId);
            CreateAndEditTicketModel model = new CreateAndEditTicketModel()
            {
                ID=ticket.ID,
                Description=ticket.Description,
                SerialNumber=ticket.SerialNumber,
            };
            model.ClientID = ticket.client.ID;
            model.IsNewTicket = false;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CreateAndEditTicketModel editTicket)
        {
            Ticket ticket = MapTicket(editTicket);
            ticket.ID = editTicket.ID;
            ticket.client.ID = editTicket.ClientID;
            _ticketsService.Edit(ticket);
            return RedirectToAction(nameof(Index));
        }

        private Ticket MapTicket(CreateAndEditTicketModel ticketModel)
        {
            Ticket dbTicket = new Ticket();
            dbTicket.Description = ticketModel.Description;
            dbTicket.SerialNumber = ticketModel.SerialNumber;
            return dbTicket;
        }
    }
}
