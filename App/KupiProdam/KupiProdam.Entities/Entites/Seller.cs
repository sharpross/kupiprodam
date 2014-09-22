using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KupiProdam.Entities.Entites
{
    /// <summary>
    /// Продавец
    /// </summary>
    public class Seller : IdentityUser
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [Display(Name="Наименование")]
        [DataType(DataType.Text)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        /// <summary>
        /// Основной телефон
        /// </summary>
        [Required]
        [Display(Name = "Телефон")]
        [DataType(DataType.Text)]
        [RegularExpression("\\+\\d{10}", ErrorMessage="Телефон должен быть представлен так +9876543210")]
        public virtual string MainPhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        /// <summary>
        /// О нас
        /// </summary>
        [Display(Name = "О нас")]
        [DataType(DataType.MultilineText)]
        public virtual string About { get; set; }

        /// <summary>
        /// Фото
        /// </summary>
        [Display(Name = "Фото")]
        public virtual byte[] Photo { get; set; }

        /// <summary>
        /// Сайт
        /// </summary>
        [Display(Name = "Наш сайт")]
        [DataType(DataType.Url)]
        public virtual string Site { get; set; }

        /// <summary>
        /// Скайп
        /// </summary>
        [Display(Name = "Skype")]
        [DataType(DataType.Text)]
        public virtual string Skype { get; set; }

        /// <summary>
        /// Ссылка на страницу ВКонтакте
        /// </summary>
        [Display(Name = "ВКонтакте")]
        [DataType(DataType.Url)]
        public virtual string VKontakte { get; set; }

        /// <summary>
        /// Ссылка на страницу Facebook
        /// </summary>
        [Display(Name = "Facebook")]
        [DataType(DataType.Url)]
        public virtual string Facebook { get; set; }

        /// <summary>
        /// Телефоны
        /// </summary>
        [Display(Name = "Телефоны")]
        public virtual List<string> Phones { get; set; }

        /// <summary>
        /// Адреса
        /// </summary>
        [Display(Name = "Адреса")]
        public virtual List<string> Addresses { get; set; }

        /// <summary>
        /// Продукты
        /// </summary>
        [Display(Name = "Продукты")]
        public virtual List<Product> Products { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Seller()
        {
            this.Id = new int();
            this.Email = string.Empty;
            this.About = string.Empty;
            this.Name = string.Empty;
            this.Password = string.Empty;
            this.Site = string.Empty;
            this.Skype = string.Empty;
            this.VKontakte = string.Empty;
            this.Facebook = string.Empty;
            this.MainPhone = string.Empty;
            this.Products = new List<Product>();
            this.Addresses = new List<string>();
            this.Phones = new List<string>();
        }
    }
}
