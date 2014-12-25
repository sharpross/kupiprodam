using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Views
{
    public class ListTenderView
    {
        public string Themne { get; set; }

        public string SubTheme { get; set; }

        public string SubSubTheme { get; set; }
        
        public bool AllowWriteMe { get; set; }

        public string Title { get; set; }

        public string ActiveTime { get; set; }

        public int Messages { get; set; }

        public bool HaveNewMessages { get; set; }
    }
}