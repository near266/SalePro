using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Application.Query.CompanyQuery;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.PackageMemberQuery
{
    public class GetAllOrSearchQuery : IRequest<PagedList<ProfileRes>>
    {
       
        public string? name { get; set; }
        public int page {  get; set; }
        public int  pageSize { get; set; }
    }
    public class GetAllOrSearchQueryHandler : IRequestHandler<GetAllOrSearchQuery, PagedList<ProfileRes>>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public GetAllOrSearchQueryHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<PagedList<ProfileRes>> Handle(GetAllOrSearchQuery rq, CancellationToken cancellationToken)
        {
             var reslut =await _repo.SearchOrDetail(rq.name, rq.page, rq.pageSize);
           
        
            
            return reslut;
        }
    }
}
