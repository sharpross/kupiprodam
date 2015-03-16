using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.UserMessages
{
    public class UserMessageModel
    {
        public List<UserMessage> ListMessages { get; set; }

        public int ToUserId { get; set; }

        public string Message { get; set; }

        public int UserId { get; set; }

        public UserMessageModel()
        {
 
        }

        public UserMessageModel(int userid, int toUserId)
        {
            this.ListMessages = new List<UserMessage>();

            this.UserId = userid;
            this.ToUserId = toUserId;

            var service = new UserMessageService();

            var messages = service.GetAll()
                .Where(x => (x.UserId == toUserId && x.ToUserId == userid))
                .ToList();

            messages.AddRange(service.GetAll()
                .Where(x => (x.UserId == userid && x.ToUserId == toUserId))
                .ToList());

            messages = messages.OrderBy(x => x.DateCreation).ToList();

            foreach (var msg in messages)
            {
                var newMsg = new UserMessage() { 
                    DateCreation = msg.DateCreation,
                    Id = msg.Id,
                    IsRead = msg.IsRead,
                    Message = msg.Message,
                    UserId = msg.UserId,
                    ToUserId = msg.ToUserId
                };

                newMsg.User = this.Getuser(msg.UserId);
                newMsg.ToUser = this.Getuser(msg.ToUserId);

                this.ListMessages.Add(newMsg);
            }

            foreach (var msg in messages)
            {
                if (msg.ToUserId == userid)
                {
                    msg.IsRead = true;
                    service.Upsert(msg);
                }
            }
        }

        public void Save()
        {
            var service = new UserMessageService();

            var rec = new UserMessage()
            {
                DateCreation = DateTime.Now,
                Message = this.Message,
                ToUserId = this.ToUserId,
                UserId = this.UserId,
                IsRead = false,
                User = this.Getuser(this.UserId),
                ToUser = this.Getuser(this.ToUserId)
            };

            service.Save(rec);
        }

        private UserSystem Getuser(int id)
        {
            var service = new UserSystemService();

            return service.Get(id);
        }
    }
}