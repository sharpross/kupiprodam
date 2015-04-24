using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Models.Entities;
using YaProdayu2.Y2System;

namespace YaProdayu2.Controllers
{
    public class ImageController : BaseController
    {
        [OutputCache(Duration = 10)]
        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                var obj = new Image();

                using (var session = DBHelper.OpenSession())
                {
                    obj = session.CreateCriteria<Image>().List<Image>()
                        .Where(x => x.ID == id)
                        .FirstOrDefault();
                }

                if (obj != null)
                {
                    return File(obj.Data, obj.Type);
                }
            }

            var dir = Server.MapPath("/Content/images/image_not_found.png");
            var path = Path.Combine(dir);

            return base.File(path, "image/png");
        }

        [HttpPost]
        public JsonResult Remove(int? id)
        {
            if (id.HasValue)
            {
                using (var session = DBHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var res = new Image()
                        {
                            ID = (int)id
                        };

                        session.Delete(res);
                        transaction.Commit();
                    }
                }

                return Json(new { Success = true });
            }

            return Json(new { Success = false });
        }
	}
}