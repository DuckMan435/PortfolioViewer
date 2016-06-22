using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;

namespace PortfolioViewer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            if (!db.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);

                // Add missing roles
                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    roleManager.Create(role);
                }

                role = roleManager.FindByName("User");
                if (role == null)
                {
                    role = new IdentityRole("User");
                    roleManager.Create(role);
                }

                // Create Admin user
                var user = userManager.FindByName("Admin");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "Admin",
                        Email = ""
                    };
                    userManager.Create(newUser, "Password1!");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "Admin");
                }

                // Create test users
                user = userManager.FindByName("Client 1");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "Client 1",
                        Email = "user@client1.com"
                    };
                    userManager.Create(newUser, "Password1!");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "User");
                }

                user = userManager.FindByName("Client 2");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "Client 2",
                        Email = "user@client2.com"
                    };
                    userManager.Create(newUser, "Password1!");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "User");
                }

                user = userManager.FindByName("Client 3");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "Client 3",
                        Email = "user@client3.com"
                    };
                    userManager.Create(newUser, "Password1!");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "User");
                }
            }
        }
    }
}