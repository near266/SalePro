﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.DTOs
{
    public class AddInfoPackageRq
    {
        public Guid? UserId { get;set; }
        public int? Type { get; set; }
    }
}
