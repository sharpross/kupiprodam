namespace KupiProdam.Entities.Entites
{
    public class UserInfo
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public decimal UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Сотовый телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Скайп
        /// </summary>
        public string Skype { get; set; }

        /// <summary>
        /// Ссылка на профиль ВКонтакте
        /// </summary>
        public string VK { get; set; }

        /// <summary>
        /// Ссылка на профиль Facebook
        /// </summary>
        public string Facebook { get; set; }

        /// <summary>
        /// Идентификатор города
        /// </summary>
        public decimal CityId { get; set; }
    }
}
