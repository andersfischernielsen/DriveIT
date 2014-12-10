using System.Security.Claims;
using System.Threading.Tasks;
using DriveIT.Entities;
using DriveIT.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace DriveIT.Web
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

// ReSharper disable once InconsistentNaming
    public class DriveITUserManager : UserManager<DriveITUser>
    {
        public DriveITUserManager(IUserStore<DriveITUser> store)
            : base(store)
        {
        }

        public static DriveITUserManager Create(IdentityFactoryOptions<DriveITUserManager> options,
            IOwinContext context)
        {
            var manager = new DriveITUserManager(new UserStore<DriveITUser>(context.Get<DriveITContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<DriveITUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<DriveITUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
// ReSharper disable once InconsistentNaming
    public class DriveITSignInManager : SignInManager<DriveITUser, string>
    {
        public DriveITSignInManager(DriveITUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(DriveITUser user)
        {
            return user.GenerateUserIdentityAsync((DriveITUserManager)UserManager, CookieAuthenticationDefaults.AuthenticationType);
        }

        public static DriveITSignInManager Create(IdentityFactoryOptions<DriveITSignInManager> options, IOwinContext context)
        {
            return new DriveITSignInManager(context.GetUserManager<DriveITUserManager>(), context.Authentication);
        }
    }
}
