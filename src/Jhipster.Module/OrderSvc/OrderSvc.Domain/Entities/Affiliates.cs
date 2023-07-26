using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class Affiliates :BaseEntity<Guid>
    {
        public Guid? ReferrerId { get; set; }
        public Guid? ProviderId { get; set; }
        public Guid? SalerId { get; set; }
        public Guid? ParticipantsId { get; set; }
    }
}
