using KupiProdam.Entities.Entites;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KupiProdam.App_Start
{
    public class CustomUserManager : UserManager<User>
    {
        public CustomUserManager(CustomUserStore store)
            : base(store)
        {
            this.PasswordHasher = new CustomPasswordHasher();
        }

        public override Task<User> FindAsync(string userName, string password)
        {
            Task<User> taskInvoke = Task<User>.Factory.StartNew(() =>
            {
                PasswordVerificationResult result = this.PasswordHasher.VerifyHashedPassword(userName, password);
                if (result == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    return Store.FindByNameAsync(userName).Result;
                }
                return null;
            });
            return taskInvoke;
        }
    }

    public class CustomPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (true)
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}