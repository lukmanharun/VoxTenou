using Microsoft.AspNetCore.Mvc;
using VoxTenouApp.Interfaces;
using VoxTenouApp.Models.User;

namespace VoxTenouApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpServices httpServices;
        public UserController(IHttpServices httpServices)
        {
            this.httpServices = httpServices;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("SignIn")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInDTO userModel)
        {
            if (ModelState.IsValid)
            {
                var r = await httpServices.SignIn(userModel);
                if (r.success)
                {
                    this.HttpContext.Session.SetString("Id", $"{r.auth.id}");
                    this.HttpContext.Session.SetString("Token", $"{r.auth.token}");
                    this.HttpContext.Session.SetString("Email", $"{r.auth.email}");
                    return Redirect("/Organizer/Index");
                }
                return View(userModel);
            }
            else
            {
                return View(userModel);
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateUserDTO userModel)
        {
            if (ModelState.IsValid)
            {
                var r = await httpServices.CreateUser(userModel);
                if (r.success)
                {
                    return Redirect("/User/Login");
                }
                return View(userModel);
            }
            else
            {
                return View(userModel);
            }
        }
    }
}
