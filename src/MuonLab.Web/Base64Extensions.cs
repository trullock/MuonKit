using System;

namespace MuonLab.Web
{
    public static class Base64Extensions
    {
        public static byte[] FromUrlSafeBase64(this string value)
        {
            return Convert.FromBase64String(value.Replace('-', '+').Replace('_', '/').Replace('!', '='));
        }

        public static string ToUrlSafeBase64(this byte[] value)
        {
            return Convert.ToBase64String(value).Replace('+', '-').Replace('/', '_').Replace('=', '!');
        }
    }
}