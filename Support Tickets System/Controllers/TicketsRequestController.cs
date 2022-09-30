using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Support_Tickets_System.Models;
using Support_Tickets_System.Website.Models.TicketsRequest;
using SupportTicketsSystem.Services.Logic;
using SupportTicketsSystem.Services.Logic.Models;
using SupportTicketsSystem.Services.Logic.Models.Tickets;
using SupportTicketsSystem.Website.Models.TicketsRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Support_Tickets_System.Controllers
{
    [Authorize]
    public class TicketsRequestController : Controller
    {
        private readonly ClientsService _clientsService;
        private readonly TicketsService _ticketsService;
        private readonly TicketsRequestService _ticketsRequestService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public TicketsRequestController(ClientsService clientsService,
            TicketsService ticketsService,
            TicketsRequestService ticketsRequestService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> SignInManager)
        {
            _clientsService = clientsService;
            _ticketsService = ticketsService;
            _ticketsRequestService = ticketsRequestService;
            _userManager = userManager;
            _signInManager = SignInManager;
        }


        private void SendEmail(string emailTo, string message)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("comanysuppit@gmail.com");

            mailMessage.To.Add(emailTo);
            mailMessage.Subject = "Tickets Requests System";

            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            string host = mailMessage.From.Host;
            
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("comanysuppit@gmail.com", "CompanyIT123");
            smtpClient.Send(mailMessage);

        }


        [Authorize (Roles ="Admin")]
        public IActionResult RequestReceived()
        {
            List<TicketsRequest> ticketsRequests = _ticketsRequestService.GetAllTicketsRequests();

            TicketRequestViewModel model = new TicketRequestViewModel();

            foreach (var ticketsRequest in ticketsRequests)
            {
                int clientID=_ticketsRequestService.GetTicketRequest(ticketsRequest.ID).client.ID;
                model.RequestsList.Add(new RequestRecord()
                {
                    RequestId=ticketsRequest.ID,
                    RequestFromClientFullName= _clientsService.GetClientByID(clientID).FullName,
                    TicketDescription=_ticketsService.GetTicketByID(ticketsRequest.ticket.ID).Description,
                    status= ticketsRequest.status.ToString()

            });
            }
            return View(model);
        }

        public IActionResult RequestSent()
        {            
            return View();
        }
       

        //[HttpGet]
        //public IActionResult RequestTicket()
        //{
        //    return View();
        //}

       
        public IActionResult RequestTicket(int id)
        {
            TicketsRequest ticketRequest = new TicketsRequest();

            var clientEmail = _userManager.GetUserName(User);
            Client client = _clientsService.GetClientByEmail(clientEmail);

            ticketRequest.ticket.ID = id;
            ticketRequest.client.ID = client.ID;
            ticketRequest.status = (Status)Enum.Parse(typeof(Status),"Pending");

            _ticketsRequestService.CreateTicketRequest(ticketRequest);
            int reqID = _ticketsRequestService.GetRequestID(ticketRequest);
            
            string email = "comanysuppit@gmail.com";

            var link = Url.Action(nameof(RequestReceived), "TicketsRequest", new { reqID }
            , Request.Scheme, Request.Host.ToString());
            var message =  $"<a href=\"{link}\">Confirm Ticket Request</a>";
            SendEmail(email, message);
                        
            Ticket ticket = _ticketsService.GetTicketByID(id);
            ticket.Confirmed = true;
            _ticketsService.Edit(ticket);
            
            return RedirectToAction(nameof(RequestSent));

        }
        
        
        public IActionResult Approved(int id)
        {
            TicketsRequest ticketsRequest = _ticketsRequestService.GetTicketRequest(id);
            ticketsRequest.status = (Status)Enum.Parse(typeof(Status), "Approved"); 
            Ticket ticket = _ticketsService.GetTicketByID(ticketsRequest.ticket.ID);
                       
            _ticketsRequestService.EditTicketRequest(ticketsRequest);

            string email = _clientsService.GetClientByID(ticketsRequest.client.ID).Email;
            string message = "Your Request is Approved";
            SendEmail(email, message);

            ticket.client.ID = ticketsRequest.client.ID;
            ticket.Confirmed = true;
            _ticketsService.Edit(ticket);
            return RedirectToAction(nameof(RequestReceived));
        }


        [HttpGet]
        public IActionResult Rejected()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Rejected(RejectedNote model, int id)
        {
            TicketsRequest ticketsRequest = _ticketsRequestService.GetTicketRequest(id);
            ticketsRequest.status = (Status)Enum.Parse(typeof(Status), "Rejected");;
            _ticketsRequestService.EditTicketRequest(ticketsRequest);
                       
            string email =_clientsService.GetClientByID(ticketsRequest.client.ID).Email;
            string message = "Your Request is Rejected " + "note:" + model.note;
            SendEmail(email, message);

            Ticket ticket = _ticketsService.GetTicketByID(id);
            ticket.Confirmed = false;
            _ticketsService.Edit(ticket);

            return RedirectToAction(nameof(RequestReceived));
        }

    }
}
