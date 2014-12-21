using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Y2System.Utils;

namespace YaProdayu2.Models.Views
{
    public class NewTenderView
    {
        public string Theme { get; set; }

        public string IconTheme { get; set; }

        public int IconWidth { get; set; }

        public int IconHeight { get; set; }

        public string SubTheme { get; set; }

        public string City { get; set; }

        public bool AllowWriteMe { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string ActiveTime { get; set; }

        public int Coste { get; set; }

        public string TenderType { get; set; }

        public List<string> ListSubThemes { get; set; }

        public string Errors { get; set; }

        public List<City> Citys { get; set; }
    }
}