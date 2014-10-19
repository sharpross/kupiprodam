using KupiProdam.Entities.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupiProdam.Utils.Generators
{
    public class TenderGenerator
    {
        private Random Rnd { get; set; }

        public TenderGenerator()
        {
            this.Rnd = new Random(DateTime.Now.Millisecond);
        }

        public List<Tender> Get()
        {
            var list = new List<Tender>();

            list.Add(new Tender() 
            {
                Messaget = "Куплю рыбу недорого",
                Title = "Ищу рыбу",
                Statuse = 0,
                WriteMe = true,
                Id = 1,
                ShowTime = "1 неделю",
                Theme = "Продукты",
                SubTheme = "Морепродукты",
                SubTheme2 = "Рыба"
            });

            list.Add(new Tender()
            {
                Messaget = "Куплю кракодила",
                Title = "Ищу рыбу",
                Statuse = 1,
                WriteMe = true,
                Id = 2,
                ShowTime = "1 неделю",
                Theme = "Продукты",
                SubTheme = "Морепродукты",
                SubTheme2 = "Нечто"
            });

            list.Add(new Tender()
            {
                Messaget = "Найму киллеров",
                Title = "Чёрная работа",
                Statuse = 3,
                WriteMe = true,
                Id = 3,
                ShowTime = "1 день",
                Theme = "Социальное",
                SubTheme = "Общество",
                SubTheme2 = "Тёмные делишки"
            });

            list.Add(new Tender()
            {
                Messaget = "Помогите собрать машину времени",
                Title = "Машина времени",
                Statuse = 0,
                WriteMe = true,
                Id = 4,
                ShowTime = "1 месяц",
                Theme = "Социальное",
                SubTheme = "Творчество",
                SubTheme2 = "Прочее"
            });

            list.Add(new Tender()
            {
                Messaget = "Куплю битые бутылки для опытов над собой",
                Title = "Битые бутылки",
                Statuse = 0,
                WriteMe = true,
                Id = 1,
                ShowTime = "1 неделю",
                Theme = "Социальное",
                SubTheme = "Творчество",
                SubTheme2 = "Садомаза"
            });

            return list;
        }
    }
}
