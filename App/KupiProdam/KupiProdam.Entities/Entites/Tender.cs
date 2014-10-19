using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupiProdam.Entities.Entites
{
    public class Tender
    {
        /// <summary>
        /// Идкнтификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тема
        /// </summary>
        [Display(Name = "Тема")]
        [DataType(DataType.Text)]
        public string Theme { get; set; }

        /// <summary>
        /// Подтема
        /// </summary>
        [Display(Name = "Под тема")]
        [DataType(DataType.Text)]
        public string SubTheme { get; set; }

        /// <summary>
        /// Подтема
        /// </summary>
        [Display(Name = "Под тема №2")]
        [DataType(DataType.Text)]
        public string SubTheme2 { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [Display(Name = "Сообщение")]
        [DataType(DataType.Text)]
        public string Messaget { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        [Display(Name = "Заголовок")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        /// <summary>
        /// Разрешить писать мне
        /// </summary>
        [Display(Name = "Разрешить писать мне")]
        public bool WriteMe { get; set; }

        /// <summary>
        /// Статус тендера
        /// </summary>
        [Display(Name = "Статус заявки")]
        public int Statuse { get; set; }

        /// <summary>
        /// Дата окончания тендера
        /// </summary>
        [Display(Name = "Тема")]
        [DataType(DataType.Text)]
        public string ShowTime { get; set; }
    }
}
