namespace KupiProdam.Utils.Generators
{
    using KupiProdam.Entities.Entites;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Sellers
    {
        private Random Rnd { get; set; }

        public Sellers()
        {
            this.Rnd = new Random(DateTime.Now.Millisecond);
        }

        public List<Seller> Get()
        {
            var sellers = new List<Seller>();

            for (var i = 0; i <= 30; i++)
            {
                var seller = new Seller() 
                {
                    Id = i,
                    Name = this.GetName(),
                    Photo = this.GetPhotos(),
                    Email = "mail@yandex.ru",
                    About = "Мы явно очень хорошее предприятие. У нас очень много довольных клиентов. Айдате к нам! Мы явно очень хорошее предприятие. У нас очень много довольных клиентов. Айдате к нам!",
                    Site = "www.yandex.ru",
                    Skype = "skype",
                    VKontakte = "vk.com/abv",
                    Facebook = "www.facebook.com/ukraina.ru",
                    Phones = this.GetPhones(),
                    Addresses = this.GetAddresses()
                };

                sellers.Add(seller);
            }

            return sellers;
        }

        private string GetName()
        {
            var list = new List<string>();

            list.Add("Иванов И.И.");
            list.Add("Нефтедром");
            list.Add("Карентер");
            list.Add("Верталётный завод имени \"Чекаловска\"");
            list.Add("Банк добрых пожеланий");
            list.Add("Парус-М");
            list.Add("Спичечный завод №4");

            return list[this.Rnd.Next(0, 6)];
        }

        private string GetPhotos()
        {
            var list = new List<string>();

            list.Add("~/Content/images/fish/1.jpg");
            list.Add("~/Content/images/fish/2.jpg");
            list.Add("~/Content/images/fish/3.jpg");
            list.Add("~/Content/images/fish/4.jpg");

            return list[this.Rnd.Next(0, 4)];
        }

        private List<string> GetPhones()
        {
            var list = new List<string>();

            list.Add("+7 999 99 99");
            list.Add("+7 999 99 99");
            list.Add("+7 999 99 99");
            list.Add("+7 999 99 99");

            return list;
        }

        private List<string> GetAddresses()
        {
            var list = new List<string>();

            list.Add("Казань, Проспект Победы, дом 100");
            list.Add("Казань, Перовго мая, дом 101");
            list.Add("Казань, Азинская, дом 102");

            return list;
        }
    }
}
