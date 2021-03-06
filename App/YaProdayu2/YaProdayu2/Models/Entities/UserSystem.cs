﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Entities
{
    public class UserSystem
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string Phone { get; set; }

        public virtual string VKontakte { get; set; }

        public virtual string Facebook { get; set; }

        public virtual bool IsSeller { get; set; }

        public virtual string About { get; set; }

        public virtual string Site { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual string Skype { get; set; }

        public virtual string Post { get; set; }

        public virtual string Organization { get; set; }

        public virtual int ImageId { get; set; }

        public virtual string Login { get; set; }

        public virtual string Portfolio { get; set; }

        public virtual List<int> ListImages { get; set; }
    }
}