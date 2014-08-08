namespace KupiProdam.Utils
{
    /// <summary>
    /// Описывает элемент пэйджинга
    /// </summary>
    public class PaginationItem
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
    }
}
