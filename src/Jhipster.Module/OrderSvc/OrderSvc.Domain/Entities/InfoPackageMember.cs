using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class InfoPackageMember:BaseEntity<Guid>
    {
        public Guid? ProfileMemberId { get; set;}
        public int? status { get; set;}
        public int? CurrentStatusMember {get; set;}
        public virtual ProfileCustomer? ProfileCustomer { get; set;}
        public Guid? PackageId { get; set;}
        public virtual PackageMember? PackageMember { get; set; }

    }
}
