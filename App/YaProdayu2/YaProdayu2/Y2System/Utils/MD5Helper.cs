namespace YaProdayu2.Y2System.Utils
{
    public static class MD5Helper
    {
        public static string GetMD5String(string password)
        {
            var md5string = string.Empty;

            md5string = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

            return md5string;
        }

    }
}