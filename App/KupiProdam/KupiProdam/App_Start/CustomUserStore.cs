using KupiProdam.Entities.Entites;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KupiProdam.App_Start
{
    public class CustomUserStore : IUserStore<User>
    {
        static readonly List<User> Users = new List<User>();

        public void Dispose()
        {
            this.Dispose();
        }

        public Task CreateAsync(User user)
        {
            return Task.Factory.StartNew(() => Users.Add(user));
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task<User>.Factory.StartNew(() => Users.FirstOrDefault(u => u.UserName == userName));
        }
    }
}