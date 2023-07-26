using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.DTOs
{
    public class UpdateStatusRq
    {
        public List<Guid> Id { get; set; }
        public int? Status { get; set; }
    }
}
