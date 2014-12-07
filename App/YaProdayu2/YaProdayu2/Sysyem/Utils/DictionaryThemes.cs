using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Sysyem.Utils
{
    public class DictionaryThemes
    {
        public List<Theme> List { get; set; }

        public DictionaryThemes()
        {
            this.List = new List<Theme>();

            this.List.Add(new Theme() 
            {
                Image = "1.png",
                Key = "Авто, Автоперевозки",
                SubThemes = new List<string>()
                {
                    "Водитель",
                    "Грузчик",
                    "Дальнобойщик",
                    "Картеж"
                }
            });

            this.List.Add(new Theme()
            {
                Image = "2.png",
                Key = "Недвижимость-сделки, Аренда",
                SubThemes = new List<string>()
                {
                    "Куплю квартиру",
                    "Сдам квартиру",
                    "Сдам комнату"
                }
            });

            this.List.Add(new Theme()
            {
                Image = "3.png",
                Key = "Услуги кредитования",
                SubThemes = new List<string>()
                {
                    "Возьму в долг",
                    "Дам в долг"
                }
            });

            this.List.Add(new Theme()
            {
                Image = "4.png",
                Key = "Аренда, прокат",
                SubThemes = new List<string>()
                {
                    "Сдам в аренду",
                    "Арендую",
                    "Возьму на прокат",
                    "Сдам на прокат"
                }
            });
        }
    }
}