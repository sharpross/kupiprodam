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
        public string Title { get; set; }

        /// <summary>
        /// Имя контроллера
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Имя метода котроллера
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Является активной
        /// </summary>
        public bool IsActive { get; set; }
    }
}
