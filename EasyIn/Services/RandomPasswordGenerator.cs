using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Services
{
    public class RandomPasswordGenerator
    {
        private static readonly Random Random = new Random();

        const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBERS = "123456789";
        const string SPECIALS = @"!@£$%^&*()#€";

        public static string Generate(int passwordSize, bool useLowercase = true, bool useUppercase = true, bool useNumbers = true, bool useSpecial = true)
        {
            var _password = new char[passwordSize];
            var charSet = "";
            
            if (useLowercase)
                charSet += LOWER_CASE;

            if (useUppercase)
                charSet += UPPER_CAES;

            if (useNumbers)
                charSet += NUMBERS;

            if (useSpecial)
                charSet += SPECIALS;

            for (int i = 0; i < passwordSize; i++)
            {
                _password[i] = charSet[Random.Next(charSet.Length - 1)];
            }

            return string.Join(null, _password);
        }
    }
}