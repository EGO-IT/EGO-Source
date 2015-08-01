using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EGO.BusinessLogic;
using EGO.Model;
using Microsoft.AspNet.Identity.Owin;

namespace EGO.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ClientBl _clientBl = new ClientBl();
        private readonly Email _email = new Email();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ClientsController() { }

        public ClientsController(ClientBl client, Email email, ApplicationUserManager userManager)
        {
            _clientBl = client;
            _email = email;
            UserManager = userManager;
        }
        public ActionResult GetAllClients()
        {
            return View(_clientBl.GetAllClients());
        }

        [HttpGet]
        public ActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddClient(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _clientBl.AddClient(model);
                 string code = await UserManager.GenerateEmailConfirmationTokenAsync(_clientBl.UserId);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = _clientBl.UserId, code }, Request.Url.Scheme);
                _email.SendEmail(_clientBl.SendEmail(model),model,callbackUrl);
                return RedirectToAction("GetAllClients");
            }
            return View(model);
        }
    }
}