using System.Text;

namespace AzinMnpWebService.Common;

public static class Extension
{
    public static string SHA1Special(this string Value)
    {
        byte[] result;
        System.Security.Cryptography.SHA1 ShaEncrp = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        Value = String.Format("{0}{1}{0}", "CSAASADM", Value);
        byte[] buffer = new byte[Value.Length];
        buffer = Encoding.UTF8.GetBytes(Value);
        result = ShaEncrp.ComputeHash(buffer);
        return Convert.ToBase64String(result);
    }
}