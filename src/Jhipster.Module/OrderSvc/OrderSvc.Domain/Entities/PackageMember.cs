using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class PackageMember 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public int? Status { get; set; }
        public ProfileCustomer? Customer { get; set; }
        public string PackageName { get; set; }
        public string Decripstion { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
