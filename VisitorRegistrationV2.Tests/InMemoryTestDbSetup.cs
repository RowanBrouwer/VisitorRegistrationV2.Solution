using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Data;

namespace VisitorRegistrationV2.Tests
{
    class InMemoryTestDbSetup
    {
        public static ValueTuple<DbContextOptions, IOptions<OperationalStoreOptions>> CreateNewContextOptions()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid()
                .ToString())
                .EnableSensitiveDataLogging().Options;

            var optionsStore = Options.Create(new OperationalStoreOptions());

            var returnObject = (options, optionsStore);

            return returnObject;
        }

        public static UserManager<Registrar> CreateUsermanager(ApplicationDbContext context)
        {
            IUserStore<Registrar> userStore = new UserStore<Registrar>(context);

            IPasswordHasher<Registrar> passwordHasher = new PasswordHasher<Registrar>();

            var manager = new UserManager<Registrar>(userStore, null, passwordHasher, null, null, null, null, null, null);

            return manager;
        }

        public static ApplicationDbContext CreateContext((DbContextOptions, IOptions<OperationalStoreOptions>) options)
        {
            return new ApplicationDbContext(options.Item1, options.Item2);
        }

        public static void Seed(UserManager<Registrar> manager, ApplicationDbContext context)
        {
            ApplicationSeed.Seed(manager, context, true);
        }
    }
}
