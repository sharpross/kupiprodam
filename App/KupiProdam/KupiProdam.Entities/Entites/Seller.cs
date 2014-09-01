﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KupiProdam.Entities.Entites
{
    /// <summary>
    /// Продавец
    /// </summary>
    public class Seller
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [Display(Name="Наименование")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// О нас
        /// </summary>
        [Display(Name = "О нас")]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }

        /// <summary>
        /// Фото
        /// </summary>
        [Display(Name = "Фото")]
        public string Photo { get; set; }

        /// <summary>
        /// Сайт
        /// </summary>
        [Display(Name = "Наш сайт")]
        [DataType(DataType.Url)]
        public string Site { get; set; }

        /// <summary>
        /// Скайп
        /// </summary>
        [Display(Name = "Skype")]
        [DataType(DataType.Text)]
        public string Skype { get; set; }

        /// <summary>
        /// Ссылка на страницу ВКонтакте
        /// </summary>
        [Display(Name = "ВКонтакте")]
        [DataType(DataType.Url)]
        public string VKontakte { get; set; }

        /// <summary>
        /// Ссылка на страницу Facebook
        /// </summary>
        [Display(Name = "Facebook")]
        [DataType(DataType.Url)]
        public string Facebook { get; set; }

        /// <summary>
        /// Телефоны
        /// </summary>
        [Display(Name = "Телефоны")]
        public List<string> Phones { get; set; }

        /// <summary>
        /// Адреса
        /// </summary>
        [Display(Name = "Адреса")]
        public List<string> Addresses { get; set; }

        /// <summary>
        /// Продукты
        /// </summary>
        [Display(Name = "Продукты")]
        public List<Product> Products { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Seller()
        {
            this.Phones = new List<string>();
            this.Addresses = new List<string>();
        }
    }
}