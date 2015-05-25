using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Models.Entities;
using YaProdayu2.Models.Pay;
using YaProdayu2.Y2System;

namespace YaProdayu2.Controllers
{
    public class PayController : BaseController
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
            // your registration data
            string sMrchLogin = "my-tend.com";
            string sMrchPass1 = "my-tend2015";
            // order properties
            decimal nOutSum = 10M;
            int nInvId = 0;
            string sDesc = "desc";

            string sOutSum = nOutSum.ToString("0.00", CultureInfo.InvariantCulture);
            string sCrcBase = string.Format("{0}:{1}:{2}:{3}:Shp_item={4}",
                                             sMrchLogin, sOutSum, nInvId, sMrchPass1, this.Auth.CurrentUser.Id);

            // build CRC value
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bSignature = md5.ComputeHash(Encoding.ASCII.GetBytes(sCrcBase));

            StringBuilder sbSignature = new StringBuilder();
            foreach (byte b in bSignature)
                sbSignature.AppendFormat("{0:x2}", b);

            string sCrc = sbSignature.ToString();

            // build URL
            return "<a href=\"https://auth.robokassa.ru/Merchant/Index.aspx?" +
                                                "MrchLogin=" + sMrchLogin +
                                                "&OutSum=" + sOutSum +
                                                "&InvId=" + nInvId +
                                                "&Desc=" + sDesc +
                                                "&Shp_item=" + this.Auth.CurrentUser.Id +
                                                "&SignatureValue=" + sCrc + "\">оплатить</a>";

        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Fail()
        {
            return View();
        }

        public ActionResult sjrfhgsiueh()
        {
            var payService = new PayService();
            var userService = new UserSystemService();

            var records = payService.GetAll();

            var list = new List<PayInfoModel>();

            foreach (var rec in records)
            {
                list.Add(new PayInfoModel() { 
                    UserMail = userService.GetAll().Where(x => x.Id == rec.UserId).FirstOrDefault().Login,
                    PayDay = rec.DateBegin,
                    PayEnd = rec.DateEnd
                });
            }

            return View(list);
        }

        public ActionResult Payresult()
        {
            try
            {
                if (this.IsValid())
                {
                    var userId = GetPrm("Shp_item");

                    var dateBegin = DateTime.Now;
                    var dateEnd = DateTime.Now.AddMonths(1);

                    var userService = new UserSystemService();

                    var user = userService.Get(int.Parse(userId));

                    if (user != null)
                    {
                        var payService = new PayService();

                        payService.Save(new PayInfo()
                        {
                            UserId = user.Id,
                            DateBegin = dateBegin,
                            DateEnd = dateEnd
                        });
                    }

                    ViewBag.IsTruePay = "true";


                    //return RedirectToAction("Success");
                }
                else
                {
                    ViewBag.IsTruePay = "false";
                }
            }
            catch (Exception ex)
            {
                ViewBag.IsTruePay = ex.StackTrace;
            }

            return View();

            //return RedirectToAction("Fail");
        }

        private bool IsValid()
        {
            string sMrchPass2 = "my-tend2016";

            // HTTP parameters
            string sOutSum = GetPrm("OutSum");
            string sInvId = GetPrm("InvId");
            string Shp_item = GetPrm("Shp_item");
            string sCrc = GetPrm("SignatureValue");

            string sCrcBase = string.Format("{0}:{1}:{2}:Shp_item={3}",
                                             sOutSum, sInvId, sMrchPass2, Shp_item);

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
            string sValue = string.Empty;

            if ((this.Request.Params[sName] as string) != null)
                return this.Request.Params[sName] as string;

            if ((this.Request[sName] as string) != null)
                return this.Request[sName] as string;

            if ((this.Request.QueryString[sName] as string) != null)
                return this.Request.QueryString[sName] as string;

            return sValue;
        }
    }

    public class PayInfoModel
    {
        public string UserMail { get; set; }

        public DateTime PayDay { get; set; }

        public DateTime PayEnd { get; set; }
    }
}