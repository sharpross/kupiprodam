
using System.Collections.Generic;
using YaProdayu2.Models.Entities;
namespace YaProdayu2.Models
{
    public class SubCityModel
    {
        public virtual int Id { get; set; }

        public virtual int RegionId { get; set; }

        public virtual int CityId_1 { get; set; }

        public virtual int CityId_2 { get; set; }

        public virtual int CityId_3 { get; set; }

        public virtual int CityId_4 { get; set; }

        public virtual int CityId_5 { get; set; }

        public virtual string Region { get; set; }

        public virtual string City_1 { get; set; }

        public virtual string City_2 { get; set; }

        public virtual string City_3 { get; set; }

        public virtual string City_4 { get; set; }

        public virtual string City_5 { get; set; }

        public virtual List<Region> ListRegions { get; set; }

        public virtual List<City> ListCitys { get; set; }

        private RegionService RegionService { get; set; }

        private CityService CityService { get; set; }

        public SubCityModel()
        {
            this.ListCitys = new List<City>();
            this.ListRegions = new List<Region>();
        }

        public SubCityModel(int userId)
        {
 
        }
    }
}
