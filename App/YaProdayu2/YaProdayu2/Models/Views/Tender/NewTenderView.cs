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

        public string Region { get; set; }

        public bool AllowWriteMe { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string ActiveTime { get; set; }

        public int Coste { get; set; }

        public string TenderType { get; set; }

        public List<string> ListSubThemes { get; set; }

        public List<string> ListCitys { get; set; }

        public List<string> Errors { get; set; }

        public int[] Photo { get; set; }

        public HttpPostedFileBase[] ListPhoto { get; set; }

        public NewTenderView()
        {
            this.Errors = new List<string>();
        }

        public List<string> ListRegions { get; set; }

        public bool IsValid()
        {
            var isValid = true;

            this.GetErrors();

            if (this.Errors.Count > 0)
            {
                isValid = false;
            }

            return isValid;
        }

        public void GetErrors()
        {
            this.Errors.Clear();

            if (string.IsNullOrEmpty(this.Theme)) Errors.Add("Укажите категорию");
            if (string.IsNullOrEmpty(this.SubTheme)) Errors.Add("Укажите подтему тендера");
            if (string.IsNullOrEmpty(this.City)) Errors.Add("Укажите город");
            if (string.IsNullOrEmpty(this.Title)) Errors.Add("Укажите заголовок тендера");
            if (string.IsNullOrEmpty(this.Message)) Errors.Add("Опишите подробнее тендер");
            if (string.IsNullOrEmpty(this.ActiveTime)) Errors.Add("Укажите время активности тендера");
            if (string.IsNullOrEmpty(this.TenderType)) Errors.Add("Укажите тип тендера");
            if (this.Coste == 0 || this.Coste < 0) Errors.Add("Укажите сумму тендера");
        }
    }
}