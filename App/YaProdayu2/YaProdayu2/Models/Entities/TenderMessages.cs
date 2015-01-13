using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Y2System.System;

namespace YaProdayu2.Models.Entities
{
    public class TenderMessage
    {
        private UserSystem _userInfo;

        public virtual int Id { get; set; }

        public virtual int TenderId { get; set; }

        public virtual int ParentMessageId { get; set; }

        public virtual int FromUserId { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual decimal Coste { get; set; }

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
                            .Where(x => x.Id == this.FromUserId)
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

        public virtual string ShortMessage 
        {
            get
            {
                var maxWords = 10;
                var i = 0;
                var words = this.Message.Split(' ');
                var shortMessage = string.Empty;

                foreach (var word in words)
                {
                    if (i == maxWords) 
                    {
                        shortMessage += "...";
                        break;
                    }

                    shortMessage += word;
                    i++;
                }

                return shortMessage;
            }
        }
    }
}