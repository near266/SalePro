using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.PackageMemberQuery
{
    public class GetAllOrSearchPackageQ :IRequest<PagedList<PackageMember>>
    {
        public int? status { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }   
    }
    public class GetAllOrSearchPackageQHandler : IRequestHandler<GetAllOrSearchPackageQ, PagedList<PackageMember>>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public GetAllOrSearchPackageQHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<PagedList<PackageMember>> Handle(GetAllOrSearchPackageQ rq, CancellationToken cancellationToken)
        {
            var reslut = await _repo.SearchOrDetailPackge(rq.status,rq.page, rq.pageSize);



            return reslut;
        }
    }
}
