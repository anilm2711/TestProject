using EBazarModels.Data.Static;
using EBazarModels.Data.ViewModels;
using EBazarModels.Models;
using EBazarWebApi.Data;
using EBazarWebApi.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EBazarWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public UserManager<ApplicationUser> _userManager { get; set; }
        private SignInManager<ApplicationUser> _signInManager { get; set; }
        private AppDbContext _context { get; set; }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public LoginVM _loginVM { get; set; } = new LoginVM();

        [HttpGet]
        [Route("GetLogin")]
        public LoginVM GetLogin( LoginVM loginVM)
        {
            if(string.IsNullOrEmpty(loginVM.EmailAddress))
            {
                _loginVM = new LoginVM() { EmailAddress = "sample@g.com", Password = "P@ssword", ErrorMessage = "SampleLogin", IsSucceed = false };
            }
            _loginVM = loginVM;
            return loginVM;
        }

        [HttpGet]
        [Route("Users")]
        public List<ApplicationUser> Users()
        {
            return _context.Users.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<LoginVM>> Login(LoginVM loginVM)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
                if (user != null)
                {
                    var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                    if (passwordCheck)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                        loginVM.IsSucceed = result.Succeeded;
                    }
                    else
                    {
                        loginVM.ErrorMessage = "Wrong credentials. Please, try again!";
                    }
                }
                else
                {
                    loginVM.ErrorMessage = "Wrong credentials. Please, try again!";
                }
            }
            catch (Exception)
            {

                throw;
            }
            return loginVM;
        }

        [HttpPost]
        public async Task<ActionResult<RegisterVM>> Register(RegisterVM registerVM)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
                if (user != null)
                {
                    registerVM.ErrorMessage = "This email address is already in use";
                }
                else
                {
                    var newUser = new ApplicationUser()
                    {
                        FullName = registerVM.FullName,
                        Email = registerVM.EmailAddress,
                        UserName = registerVM.EmailAddress
                    };
                    var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
                    if (newUserResponse.Succeeded)
                        await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                    else
                    {
                        List<IdentityError> errorsmsg = newUserResponse.Errors.ToList();
                        registerVM.ErrorMessage = errorsmsg[0].Description;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return registerVM;
        }

        [HttpPost]
        public async Task<string> Logout()
        {
            await _signInManager.SignOutAsync();
            return "/";
        }
    }
}
