using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Actual;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Expected;

namespace VisitorRegistrationV2.Data
{
    public class ApplicationSeed
    {
        public static void Seed(UserManager<Registrar> userManager, ApplicationDbContext db, bool testing)
        {
            if (db.Registrars.Count() == 0)
            {
                Registrar user = new()
                {
                    Email = "Admin1@Admin1",
                    NormalizedEmail = "ADMIN1@ADMIN1",
                    EmailConfirmed = true,
                    UserName = "Admin1@Admin1",
                    NormalizedUserName = "ADMIN1@ADMIN1"
                };

                if (!testing)
                {
                    IdentityResult result = userManager.CreateAsync(user, "!Admin123").Result;
                } 
                
                db.Add(user);
                db.SaveChanges();
            }

            if (db.Visitors.Count() == 0)
            {
                var timespan1 = new TimeSpan(3, 5, 1);
                var timespan2 = new TimeSpan(4, 15, 31);
                var timespan3 = new TimeSpan(1, 0, 50);
                var timespan4 = new TimeSpan(5, 45, 9);
                var timespan5 = new TimeSpan(1, 10, 32, 19);

                List<Visitor> visitors = new List<Visitor>()
                {
                    new Visitor
                    {  
                        ActualTimesList = 
                        new List<ActualTime> 
                        {new ActualTime{ ArrivalTime = DateTime.Now.Subtract(timespan1)} },
                        ExpectedTimes =
                        new List<ExpectedTime>
                        {new ExpectedTime{ DepartureTime = DateTime.Now} }
                        , FirstName = "Rowan"
                        , LastName = "Brouwer"
                    },

                    new Visitor
                    {
                        ActualTimesList =
                        new List<ActualTime>
                        {new ActualTime{ ArrivalTime = DateTime.Now.Subtract(timespan3)} }
                        , FirstName = "Jan"
                        , LastName = "Jansen"
                    },

                    new Visitor
                    {
                        ActualTimesList =
                        new List<ActualTime>
                        {new ActualTime{ ArrivalTime = DateTime.Now.Subtract(timespan2), DepartureTime = DateTime.Now.Subtract(timespan3)} }
                        , FirstName = "Bob"
                        , MiddleName = "de"
                        , LastName = "Bouwer"
                    },

                    new Visitor
                    {
                        ActualTimesList =
                        new List<ActualTime>
                        {new ActualTime{ ArrivalTime = DateTime.Now.Subtract(timespan4)} }
                        , FirstName = "Lucky"
                        , LastName = "Luke"
                    },

                    new Visitor
                    {
                        ActualTimesList =
                        new List<ActualTime>
                        {new ActualTime{ ArrivalTime = DateTime.Now} },
                        ExpectedTimes =
                        new List<ExpectedTime>
                        {new ExpectedTime{ ArrivalTime = DateTime.Now} }
                        , FirstName = "Sponge"
                        , LastName = "Bob"
                    },

                    new Visitor
                    {
                        ActualTimesList =
                        new List<ActualTime>
                        {new ActualTime{ ArrivalTime = DateTime.Now.Subtract(timespan5), DepartureTime = DateTime.Now.Subtract(timespan1)} },
                        ExpectedTimes =
                        new List<ExpectedTime>
                        {new ExpectedTime{ DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.Subtract(timespan1)} }
                        , FirstName = "Koos"
                        , LastName = "Brouwer"
                    },
                };

                db.AddRange(visitors);
                db.SaveChanges();
            }
        }
    }
}
