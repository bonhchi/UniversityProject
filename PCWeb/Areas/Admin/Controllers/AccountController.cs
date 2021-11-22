using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PCWeb.Models.Account;

namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Staff()
        {
            var testUser = await _userManager.GetUsersInRoleAsync("Staff");
            List<User> users = new List<User>();
            foreach (var item in testUser)
            {
                User user = _userManager.Users.FirstOrDefault(p => p.Id == item.Id);
                users.Add(user);
            }
            return View(testUser.ToList());
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Customer()
        {
            var testUser = await _userManager.GetUsersInRoleAsync("Customer");
            List<User> users = new List<User>();
            foreach (var item in testUser)
            {
                User user = _userManager.Users.FirstOrDefault(p => p.Id == item.Id);
                users.Add(user);
            }
            return View(testUser.ToList());
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Add()
        {
            string[] gender = { "Nam", "Nữ" };
            ViewBag.Selected = gender.ToList();
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Add(StaffRegistation userModel,string name)
        {
            string[] gender = { "Nam", "Nữ" };
            userModel.Gender = name;
            if (!ModelState.IsValid)
                return View(userModel);
            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                ViewBag.Selected = gender.ToList();
                return View(userModel);
            }
            await _userManager.AddToRoleAsync(user, "Staff");
            return RedirectToAction(nameof(AccountController.Staff), "Account");
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            string[] gender = { "Nam", "Nữ" };
            ViewBag.Selected = gender.ToList();
            var user = await _userManager.FindByIdAsync(id);
            var model = new UserEdit
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DayOfBirth = user.DayOfBirth
            };
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserEdit userModel,string name)
        {
            string[] gender = { "Nam", "Nữ" };
            var user = await _userManager.FindByIdAsync(id);
            if (ModelState.IsValid)
            {
                user.DayOfBirth = userModel.DayOfBirth;
                user.Email = userModel.Email;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.Gender = name;
                user.PhoneNumber = userModel.PhoneNumber;
                user.Address = userModel.Address;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(user, "Staff") == true)
                        return RedirectToAction("Staff", "Account");
                    else
                        return RedirectToAction("Customer", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ViewBag.Selected = gender.ToList();
            }
            return View(userModel);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return RedirectToAction("Index","Account");
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Index","Account");
            }
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var model = new UserInfo
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DayOfBirth = user.DayOfBirth
            };
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task <IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Staff", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrlAdmin"] = returnUrl;
            if (User.Identity.IsAuthenticated)
                return Redirect("Admin/Home/Index");
            else
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
