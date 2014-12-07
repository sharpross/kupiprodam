using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Entities
{
    public class UserMessage
    {
        public int Id { get; set; }

        public string FromId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}