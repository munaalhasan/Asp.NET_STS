using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicketsSystem.Services.Logic;
using SupportTicketsSystem.Services.Logic.Models;
using SupportTicketsSystem.Website.Models.Clients;
using SupportTicketsSystem.Website.Views.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_Tickets_System.Controllers
{
    [Authorize (Roles ="Admin")]
    public class ClientsController : Controller
    {
        private readonly ClientsService _clientsService;

        public ClientsController(ClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        public IActionResult Index()
        {
            List<Client> clients = _clientsService.GetAllClients();            

            ClientViewModel model = new ClientViewModel();

            model.ClientsCount = clients.Count.ToString();
            
            foreach (var client in clients)
            {
                model.ClientsList.Add(new ClientRecord()
                {
                    ClientId = client.ID,
                    ClientName = client.FullName,
                    ClientEmail =client.Email
                });
            }

            
            return View(model);
        }

        public IActionResult Details(int clientId)
        {
            Client client = _clientsService.GetClientByID(clientId);
            ClientDetails model = new ClientDetails
            {
                ClientId = client.ID,
                ClientName = client.FullName,                  
            };
            return View(model);
        }

        public IActionResult Delete(int clientId)
        {
            _clientsService.Delete(clientId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deleted()
        {
            List<Client> clients = _clientsService.GetAllDeletedClients();
            ClientViewModel model = new ClientViewModel();

            foreach (var client in clients)
            {
                model.ClientsList.Add(new ClientRecord()
                {
                    ClientId = client.ID,
                    ClientName = client.FullName,
                });
            }
            return View(model);
        }


        public IActionResult Restore(int deletedClientId)
        {
            _clientsService.RestoreClientById(deletedClientId);
            RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Create()
        {
            CreateAndEditClientModel model = new CreateAndEditClientModel();
            model.IsNewClient = true;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateAndEditClientModel newClient)
        {
            Client client = MapClient(newClient);

            if (_clientsService.GetAllClients().Any(e => e.Email == client.Email))
            {
                ModelState.AddModelError(string.Empty, "Email is already Exist");
                return View(newClient);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(newClient);
            }
            _clientsService.AddClient(client);
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public IActionResult Edit(int clientId)
        {
            Client client = _clientsService.GetClientByID(clientId);
            CreateAndEditClientModel model = new CreateAndEditClientModel()
            {
                Id = client.ID,
                FullName = client.FullName,                
                Email = client.Email               
            };
            
            model.IsNewClient = false;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CreateAndEditClientModel editClient)
        {
            Client client = MapClient(editClient);
            client.ID = editClient.Id;
            _clientsService.Edit(client);
            return RedirectToAction(nameof(Index));
        }

        private Client MapClient(CreateAndEditClientModel clientModel)
        {
            Client dbClient=new Client();
            dbClient.FullName = clientModel.FullName;
            dbClient.Email = clientModel.Email;
            return dbClient;
        }

    }
}
