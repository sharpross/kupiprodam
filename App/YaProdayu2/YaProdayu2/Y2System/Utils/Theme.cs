using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Y2System.Utils
{
    public class Theme
    {
        public string Key { get; set; }

        public List<string> SubThemes { get; set; }

        public string Image { get; set; }

        public int Width { get; set; }

        public int Heigth { get; set; }

        public Theme()
        {
            this.Width = 120;
            this.Heigth = 100;
        }
    }
}