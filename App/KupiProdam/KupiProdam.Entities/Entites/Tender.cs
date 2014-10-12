using System;
using System.Collections.Generic;
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
        public int Theme { get; set; }

        /// <summary>
        /// Подтема
        /// </summary>
        public int SubTheme { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Messaget { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Разрешить писать мне
        /// </summary>
        public bool WriteMe { get; set; }

        /// <summary>
        /// Статус тендера
        /// </summary>
        public int Statuse { get; set; }

        /// <summary>
        /// Дата окончания тендера
        /// </summary>
        public DateTime ShowTime { get; set; }
    }
}
