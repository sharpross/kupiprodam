using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.UserMessages
{
    public class MessageInfo
    {
        public UserSystem User { get; set; }

        public string LastMessage { get; set; }
    }

    public class UserListMessagesView
    {
        public List<MessageInfo> Messages { get; set; }

        public UserListMessagesView(int userId)
        {
           this.Messages = new List<MessageInfo>();

            var messageService = new UserMessageService();

            var messages = messageService
                .GetAll()
                .Where(x => x.ToUserId == userId)
                .ToList()
                .GroupBy(x => x.UserId);

            foreach (var item in messages)
            {
                var user = new UserSystemService().Get(item.Key);

                var lstMsg = item.OrderByDescending(x => x.DateCreation)
                    .FirstOrDefault();

                this.Messages.Add(new MessageInfo() { 
                    User = user, 
                    LastMessage = lstMsg.Message 
                });
            }
        }
    }
}