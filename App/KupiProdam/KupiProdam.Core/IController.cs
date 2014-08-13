namespace KupiProdam.Core
{
    using KupiProdam.Utils;
    using System.Collections.Generic;

    public interface IBaseController
    {
        /// <summary>
        /// Заголовок котроллер
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Получение списка хлебных крошек
        /// </summary>
        /// <returns>List<PaginationItem> - хлебных крошек</returns>
        List<BreadcrumbsItem> GetBreadcrumbs(string pageBane);
    }
}
