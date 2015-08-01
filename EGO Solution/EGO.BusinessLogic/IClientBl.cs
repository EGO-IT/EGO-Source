using System;
using System.Collections.Generic;
using System.Linq;
using EGO.Data;
using EGO.Model;
using EGO.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EGO.BusinessLogic
{
    public class ClientBl : IClientBl
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public string UserId { get; set; }

        public ClientBl()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())),
            new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())),
            new ClientRepository())
        {
            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        public ClientBl(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IClientRepository clientService)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }


        public List<RegisterViewModel> GetAllClients()
        {
            using (var clientRep = new ClientRepository())
            {
                return clientRep.GetAll().Select(x => new RegisterViewModel
                {
                    UserProfileId = x.UserProfileId,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Gender = x.Gender,
                    LastName = x.LastName
                }).ToList();
            }
        }

        public void AddClient(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                UserProfile = new Client
                {
                    UserProfileId = model.UserProfileId,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender
                }
            };
            var result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                AssignUserToRole(user.Id, "Client");
                UserId = user.Id;
                SendEmail(model);
            }
        }
        public void AssignUserToRole(string user, string role)
        {
            if (!RoleManager.RoleExists(role))
            {
                RoleManager.Create(new IdentityRole { Name = role });
            }
            UserManager.AddToRole(user, role);
        }
        public string SendEmail(RegisterViewModel model)
        {
            return "Hello! " + model.FirstName + ". Congratulation you have registered at EGO." +
                   "<br/>The following in your user name.<br/>"
                   + "UserName: " + model.Email;
        }

        public string IsConfirmed(string email)
        {
            var user = _db.Users.ToList().Find(x => x.Email == email);
            if (user != null)
            {
                if (user.EmailConfirmed == false)
                    return "Success";
            }
            return "Error";
        }
        public void DeleteClient(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateClient(RegisterViewModel id)
        {
            throw new NotImplementedException();
        }

        public RegisterViewModel GetClientById(int id)
        {
            throw new NotImplementedException();
        }
    }
    public interface IClientBl
    {
        List<RegisterViewModel> GetAllClients();
        void AddClient(RegisterViewModel model);
        void DeleteClient(int id);
        void UpdateClient(RegisterViewModel id);
        RegisterViewModel GetClientById(int id);
        void AssignUserToRole(string user, string role);
    }
}
