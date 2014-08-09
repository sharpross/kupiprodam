namespace KupiProdam.Utils
{
    /// <summary>
    /// Описывает хлебной крошки
    /// </summary>
    public class BreadcrumbsItem
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
