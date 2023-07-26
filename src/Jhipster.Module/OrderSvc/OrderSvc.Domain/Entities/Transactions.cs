using Jhipster.Service.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class Transactions 
    {
        [Key]
        public string TransactionId { get; set; } = RandomUtil.GenerateKey();
        public string? TransactionName { get; set; } 
        public string TransactionType { get; set; }
        public double? TotalAmount { get; set; }
       
        public DateTime? TransactionDate { get; set; }
        public string? PaymentMethod { get; set; }


    }
    //public static class RandomUtil
    //{

    //    public static string GenerateKey()
    //    {
    //         var random = new Random();
    //        const string numbers = "0123456789";

    //        var code = "DH";

    //        for (int i = 0; i < 10; i++)
    //        {
    //            var randomNumber = numbers[random.Next(numbers.Length)];
    //            code += randomNumber;
    //        }

    //        return code;
    //    }
    //}
    public static class RandomUtil
    {
        private static readonly Random random = new Random();

        public static string GenerateKey()
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
