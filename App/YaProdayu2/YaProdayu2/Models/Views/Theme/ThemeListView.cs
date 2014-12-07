using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Views.Theme
{
    public class ThemeListView
    {
        public ThemeView Theme { get; set; }

        public List<ThemeView> SubTheme { get; set; }
    }
}