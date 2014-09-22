using KupiProdam.Entities;
using KupiProdam.Entities.Entites;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KupiProdam.App_Start
{
    /*public class CustomUserStore<T> : IUserStore<T> where T : Seller
    {
        
        System.Threading.Tasks.Task IUserStore<T>.CreateAsync(T user)
        {
            //Create /Register New User 
            var repo = new RepoSeller();
            repo.Create(user);
        }

        System.Threading.Tasks.Task IUserStore<T>.DeleteAsync(T user)
        {
            //Delete User 
            var repo = new RepoSeller();
            repo.Delete(user);

            return new System.Threading.Tasks.Task()
            {
                Status = System.Threading.Tasks.TaskStatus.Created
            };
        }

        System.Threading.Tasks.Task<T> IUserStore<T>.FindByIdAsync(string userId)
        {
            var repo = new RepoSeller();
            repo.GetById(int.Parse(userId));
        }

        System.Threading.Tasks.Task<T> IUserStore<T>.FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        System.Threading.Tasks.Task IUserStore<T>.UpdateAsync(T user)
        {
            //Update User Profile 
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            // throw new NotImplementedException(); 

        }
    }*/
}