using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupportTicketsSystem.Services.Logic;
using SupportTicketsSystem.Services.Logic.Models;
using SupportTicketsSystem.Website.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SupportTicketsSystem.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ClientsService _clientsService;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ClientsService clientsService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _clientsService = clientsService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {                   

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var link = Url.Action(nameof(VerifyEmail), "Account",
                    new { userId = user.Id, token },
                    Request.Scheme, Request.Host.ToString());
                    
                    string Body = $"<a href=\"{link}\">Confirm your Email</a>";                   
                    SendMessage(model.Email,Body);
                    AddClientToDB(model);
                    RedirectToAction(nameof(VerifyEmail));
                    return RedirectToAction(nameof(ConfirmEmail));
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }



        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                var roleId = 2;
                var role = await roleManager.FindByIdAsync(roleId.ToString());
                await userManager.AddToRoleAsync(user, role.Name);
                return RedirectToAction(nameof(Login));
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            //return View("Error");
            return View();            

        }

        public IActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Tickets");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

    
        public void AddClientToDB(RegisterViewModel newClient)
        {
            Client client = new Client();
            client.FullName = newClient.FullName;
            client.Email = newClient.Email;
            _clientsService.AddClient(client);
        }
        
        public void SendMessage(string emailTo, string msgBody)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("xx@gmail.com");
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = "Email Verfication";
            mailMessage.Body = msgBody;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("xx@gmail.com", "the Password");
            smtpClient.Send(mailMessage);
        }
    
    }


}
