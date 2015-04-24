using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace YaProdayu2.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay
        public ActionResult Index()
        {
            ViewBag.PayScript = GetPayString();
            ViewBag.PayHistory = new List<string>();
            ViewBag.PayEnd = null;

            return View();
        }

        private string GetPayString()
        {
            // регистрационная информация (логин, пароль #1)
            // registration info (login, password #1)
            string sMrchLogin = "alfa-tender";
            string sMrchPass1 = "my-tend201";

            // номер заказа
            // number of order
            int nInvId = 0;

            // описание заказа
            // order description
            string sDesc = "Оплата подписки на Я-Прелагаю";

            // сумма заказа
            // sum of order
            string sOutSum = "100.00";

            // тип товара
            // code of goods
            string sShpItem = "1";

            // язык
            // language
            string sCulture = "ru";

            // кодировка
            // encoding
            string sEncoding = "utf-8";

            // формирование подписи
            // generate signature
            string sCrcBase = string.Format("{0}:{1}:{2}:{3}:shpItem={4}",
                                sMrchLogin, sOutSum, nInvId, sMrchPass1, sShpItem);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bSignature = md5.ComputeHash(Encoding.UTF8.GetBytes(sCrcBase));

            StringBuilder sbSignature = new StringBuilder();
            foreach (byte b in bSignature)
                sbSignature.AppendFormat("{0:x2}", b);

            string sCrc = sbSignature.ToString();

            // HTML-страница с кассой
            // ROBOKASSA HTML-page
            // ltKassa is System.Web.UI.WebControls.Literal;

            return "<script language=JavaScript " +
                                    "src=\"https://auth.robokassa.ru/Merchant/PaymentForm/FormMS.js?" +
                                    "MerchantLogin=" + sMrchLogin +
                                    "&OutSum=" + sOutSum +
                                    "&InvoiceID=" + nInvId +
                                    "&shpItem=" + sShpItem +
                                    "&SignatureValue=" + sCrc +
                                    "&Description=" + sDesc +
                                    "&Culture=" + sCulture +
                                    "&Encoding=" + sEncoding + "\"></script>";
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Fail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payresult()
        {
            if (this.IsValid())
            {
                var user = this.Request["shpItem"].ToString();

                var dateBegin = DateTime.Now;
                var dateEnd = DateTime.Now.AddMonths(1);

                //var temdata = this.Request["shpItem"].ToString();

                return RedirectToAction("Success");
            }

            return RedirectToAction("Fail");
        }

        private bool IsValid()
        {
            string sMrchPass2 = "my-tend2016";

            // HTTP parameters
            string sOutSum = GetPrm("OutSum");
            string sInvId = GetPrm("InvId");
            string sCrc = GetPrm("SignatureValue");

            string sCrcBase = string.Format("{0}:{1}:{2}",
                                             sOutSum, sInvId, sMrchPass2);

            // build own CRC
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bSignature = md5.ComputeHash(Encoding.ASCII.GetBytes(sCrcBase));

            StringBuilder sbSignature = new StringBuilder();
            foreach (byte b in bSignature)
                sbSignature.AppendFormat("{0:x2}", b);

            string sMyCrc = sbSignature.ToString();

            if (sMyCrc.ToUpper() != sCrc.ToUpper())
            {
                return false;
            }

            return true;
            // perform some action (change order state to paid)  
        }

        private string GetPrm(string sName)
        {
            string sValue;
            sValue = this.Request[sName] as string;

            if (string.IsNullOrEmpty(sValue))
                sValue = this.Request.Params[sName] as string;

            if (string.IsNullOrEmpty(sValue))
                sValue = String.Empty;

            return sValue;
        }
    }
}