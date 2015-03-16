using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Y2System;

namespace YaProdayu2.Models.Entities
{
    public class Tender
    {
        private UserSystem _userInfo;

        public virtual int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime DateCreation { get; set; }

        public virtual int Status { get; set; }

        public virtual string Theme { get; set; }

        public virtual string SubTheme { get; set; }

        public virtual string City { get; set; }

        public virtual string Region { get; set; }

        public virtual string ActiviteTime { get; set; }

        public virtual bool AllowWriteMe { get; set; }

        public virtual string TypeTender { get; set; }

        public virtual int Cost { get; set; }

        public virtual bool IsClose { get; set; }

        public virtual int[] Photos { get; set; }

        public virtual int Winner { get; set; }

        public virtual UserSystem UserInfo
        {
            get
            {
                if (_userInfo == null)
                {
                    using (var session = DBHelper.OpenSession())
                    {
                        _userInfo = session.CreateCriteria<UserSystem>()
                            .List<UserSystem>()
                            .Where(x => x.Id == this.UserId)
                            .FirstOrDefault();
                    }

                    return _userInfo;
                }
                else
                {
                    return _userInfo;
                }
            }
        }

        public Tender()
        {
            this.IsClose = false;
        }
    }
}