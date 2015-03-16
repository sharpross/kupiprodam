using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;
using YaProdayu2.Y2System.Utils;

namespace YaProdayu2.Models.Views
{
    public class ProfileView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string VKontakte { get; set; }

        public string Facebook { get; set; }

        public bool IsSeller { get; set; }

        public string About { get; set; }

        public string Site { get; set; }

        public DateTime CreationTime { get; set; }

        public string Skype { get; set; }

        public string Post { get; set; }

        public string Organization { get; set; }

        public List<object> ListTenders { get; set; }

        public int ImageId { get; set; }

        public string Portfolio { get; set; }

        public List<Subsciptions> ListSubsciptions { get; set; }

        public List<Theme> ListThemes { get; set; }

        public HttpPostedFileBase[] NewImages { get; set; }

        public List<int> ListImages { get; set; }
    }
}