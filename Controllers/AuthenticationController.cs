using System.Linq;
using Extensions;
using Identity_Login_and_Reg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Identity_Login_and_Reg.Persistence;
using System.Threading.Tasks;

namespace Extensions.Authentication
{
    public class AuthenticationController : Controller
    {
        private MyDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private const string DASHBOARD_PAGE = "/dashboard";
        
        public AuthenticationController(MyDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        [Route("/")]
        public IActionResult Index(){
            return View();
        }


        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                //Create a new User object, without adding a Password
                User NewUser = new User { UserName = model.UserName, Email = model.Email };
                //CreateAsync will attempt to create the User in the database, simultaneously hashing the
                //password
                IdentityResult result = await _userManager.CreateAsync(NewUser, model.Password);
                //If the User was added to the database successfully
                if(result.Succeeded)
                {
                    //Sign In the newly created User
                    //We're using the SignInManager, not the UserManager!
                    await _signInManager.SignInAsync(NewUser, isPersistent: false);
                }
                //If the creation failed, add the errors to the View Model
                foreach( var error in result.Errors )
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();//model);
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userSubmission.UserName, userSubmission.Password, isPersistent: false, lockoutOnFailure: false);
            
                if (result.Succeeded)
                {
                    return Redirect(DASHBOARD_PAGE);
                }
                ModelState.AddModelError("UserName/Password","The username or password failed.");
                return View("Index");
            }

            ModelState.AddModelError("UserName/Password","The username or password failed.");
            return View("Index");
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout(){
            await _signInManager.SignOutAsync();
            return View("Index");
        }

        
    }
}