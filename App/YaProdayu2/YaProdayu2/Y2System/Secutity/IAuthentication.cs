using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Y2System.Security
{
    public interface IAuthentication
    {
        /// <summary>
        /// Конекст (тут мы получаем доступ к запросу и кукисам)
        /// </summary>
        HttpContext HttpContext { get; set; }

        UserSystem Login(string login, string password, bool isPersistent);

        UserSystem Login(string login);

        void LogOut();

        UserSystem CurrentUser { get; }
    }
}