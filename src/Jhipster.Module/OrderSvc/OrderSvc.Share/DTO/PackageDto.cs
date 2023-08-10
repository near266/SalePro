using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Share.DTO
{
    public class PackageDto
    {
        public Guid? UserId { get; set; }
        public string? Avatar { get; set; }
        public int? status { get; set; }
        public int? CurrentStatus {  get; set; }    
        public string? UserName { get; set; }
        public string? CustomerName { get; set; }
        public int? StatusCus {  get; set; }
       // public Guid? PackgeId { get; set; }
    //    public int? StatusPackage { get; set; } 


    }
}
