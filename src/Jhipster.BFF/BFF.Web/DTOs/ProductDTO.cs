﻿using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BFF.Web.DTOs
{
    public class ProductDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid? CateId {  get; set; }
        public Guid? CompanyId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public List<string>? Image { get; set; }
        public string? Decripstion { get; set; }
    }
}