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
    public class UpdateProductCommand :IRequest<ProductDTO>
    {
        public Guid? Id { get; set; }
        public Guid? OrderId { get; set; }  
       
        public string? ProductName { get; set; }
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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDTO>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository repo, IMapper mapper, ILogger<UpdateProductCommandHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ProductDTO> Handle(UpdateProductCommand rq, CancellationToken cancellationToken)
        {
            var value = _mapper.Map<Product>(rq);
            var check= await _repo.Update(value);
            if(check != 0)
            {
               var res= _mapper.Map<ProductDTO>(value);
                return res;
            }
            else
            {
                return new ProductDTO();
            }
        }

    }
}
