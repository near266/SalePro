using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Application.Query.ProductQuery;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.CompanyCommand
{
    public class CreateCompanyCommand : IRequest<int>
    {
     [JsonIgnore]   public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<ProfileCustomer>? Customer { get; set; }
        public virtual ICollection<Product>? Products { get; }
        [MaxLength(int.MaxValue)]
        public string? Avatar
        {
            get; set;
        }
    }
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly  ICompanyReqository _repo;
       public CreateCompanyCommandHandler(IMapper mapper,ICompanyReqository repo) 
        { 
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(CreateCompanyCommand rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<Company>(rq);
            return await _repo.Add(res);
        }
    }
}
