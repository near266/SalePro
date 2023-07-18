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
        public string TransactionType { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? PaymentMethod { get; set; }


    }
    public static class RandomUtil
    {

        public static string GenerateKey()
        {
             var random = new Random();
            const string numbers = "0123456789";

            var code = "DH";

            for (int i = 0; i < 10; i++)
            {
                var randomNumber = numbers[random.Next(numbers.Length)];
                code += randomNumber;
            }

            return code;
        }
    }
}
