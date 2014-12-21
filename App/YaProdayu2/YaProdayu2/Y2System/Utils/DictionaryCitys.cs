using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Y2System.Utils
{
    public class DictionaryCitys
    {
        public List<City> List { get; set; }

        public DictionaryCitys()
        {
            this.List = new List<City>();

            this.List.Add(new City() 
            {
                Name = "Казань",
                Region = "Северное полжье"
            });

            this.List.Add(new City()
            {
                Name = "Индия",
                Region = "Европа"
            });
        }
    }
}