namespace KupiProdam.Controllers
{
    using KupiProdam.Utils;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        /// <summary>
        /// Заголовок котроллера
        /// </summary>
        public string Titile 
        { 
            get 
            { 
                return "Главная"; 
            } 
        }

        public ActionResult Index()
        {
            this.ViewBag.Titile = this.Titile;
            this.ViewBag.Pagination = this.GetPagination();

            return View();
        }

        /// <summary>
        /// Получение списка пэйджинга
        /// </summary>
        /// <returns>List<PaginationItem> - список пэйджингов</returns>
        private List<PaginationItem> GetPagination()
        {
            var result = new List<PaginationItem>();

            result.Add(new PaginationItem() 
            { 
                Controller = "Home", 
                Method = "Index",
                Titile = this.Titile
            });

            return result;
        }
    }
}