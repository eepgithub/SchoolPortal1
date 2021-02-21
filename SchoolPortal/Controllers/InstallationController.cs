using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Data;
using SchoolPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal.Controllers
{
    public class InstallationController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public InstallationController(
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)

        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            if(!_userManager.Users.Any())
            return View();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Index(InstallationViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@local",
                    Email = "admin@local",
                    FirstName = "Administrator",
                    LastName = "Account"
                };

            var result = await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("ProgramManager"));
                    await _roleManager.CreateAsync(new IdentityRole("Teacher"));
                    await _roleManager.CreateAsync(new IdentityRole("Student"));

                    await _userManager.AddToRoleAsync(user, "Admin");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

        }

            return View();
        }


    }
}
