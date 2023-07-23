using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.ProductCommand
{
    public class AddProductCommand :IRequest<ProductDTO>
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        public Guid? CompanyId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public double? PriceNum { get; set; }

        public string? Provider { get; set; }
        public List<string>? Image { get; set; }
        public string? Decripstion { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductDTO>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<AddProductCommandHandler> _logger;

        public AddProductCommandHandler(IProductRepository repo, IMapper mapper, ILogger<AddProductCommandHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ProductDTO> Handle(AddProductCommand rq,CancellationToken cancellationToken)
        {
            var value = _mapper.Map<Product>(rq);
           var check = await _repo.Add(value,cancellationToken);
            if(check != 0)
            {
               var map= _mapper.Map<ProductDTO>(value);
                return map;

            }
            return new ProductDTO();
        }

    }
}
