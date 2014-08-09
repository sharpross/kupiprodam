namespace KupiProdam.Utils
{
    using System.Collections.Generic;

    /// <summary>
    /// Элемент меню
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Titile { get; set; }

        /// <summary>
        /// Имя контроллера
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Имя метода котроллера
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Параметры для пункта меню
        /// </summary>
        public List<object> Params { get; set; }

        /// <summary>
        /// Подменю
        /// </summary>
        public List<MenuItem> SubMenu { get; set; }
    }
}
