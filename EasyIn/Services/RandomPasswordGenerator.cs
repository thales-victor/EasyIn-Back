using EasyIn.Models;
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
        const string SPECIALS = @"!@$%&*()#";

        public static string Generate(GeneratePasswordModel model)
        {
            return Generate(model.PasswordSize, model.UseLowercase, model.UseUppercase, model.UseNumbers, model.UseSpecial);
        }

        public static string Generate(int passwordSize, bool useLowercase = true, bool useUppercase = true, bool useNumbers = true, bool useSpecial = true)
        {
            var charSet = string.Empty;
            
            if (useLowercase)
                charSet += LOWER_CASE;

            if (useUppercase)
                charSet += UPPER_CAES;

            if (useNumbers)
                charSet += NUMBERS;

            if (useSpecial)
                charSet += SPECIALS;

            var password = string.Empty;

            for (int i = 0; i < passwordSize; i++)
            {
                password += charSet[Random.Next(charSet.Length - 1)];
            }

            return password;
        }
    }
}