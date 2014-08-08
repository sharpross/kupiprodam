using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupiProdam.Entities
{
    public class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public decimal Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }
    }
}
