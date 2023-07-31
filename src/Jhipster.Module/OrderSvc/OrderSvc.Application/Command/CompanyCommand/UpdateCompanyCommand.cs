using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.CompanyCommand
{
    public class UpdateCompanyCommand :IRequest<Company>
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }

        [MaxLength(int.MaxValue)]
        public string? Avatar
        {
            get; set;
        }
    }
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Company>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyReqository _repo;
        public UpdateCompanyCommandHandler(IMapper mapper, ICompanyReqository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Company> Handle(UpdateCompanyCommand rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<Company>(rq);
            var check= await _repo.Update(res);
            if(check != 0)
            {
                return res;

            }
            else
            {
                throw new ArgumentException("fail");
            }
        }
    }
}
