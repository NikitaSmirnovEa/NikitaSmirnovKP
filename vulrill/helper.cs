using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace vulrill
{
    internal class helper
    {
        public static string connect = "Server=127.0.0.1;uid=root;pwd=root;database=vulrill";
        //public static string connect = "Server=localhost;uid=root;pwd=root;database=vulrill";

        public static string login;
        public static string surname;
        public static string name;
        public static string patronymic;
        public static string role;

        public static string CreateMD5(string input)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(input)).Select(x => x.ToString("X2")));
            }
        }

        public static string path = AppDomain.CurrentDomain.BaseDirectory + @"tattoo\";

        public static string loginEdit;
        public static string orderSketch;
        public static string orderClient;
    }
}
