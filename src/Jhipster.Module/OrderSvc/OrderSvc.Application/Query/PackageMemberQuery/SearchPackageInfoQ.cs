using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.AspNetCore.Http;
using OrderSvc.Application.Persistences;
using OrderSvc.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Query.PackageMemberQuery
{
    public class SearchPackageInfoQ:IRequest<PagedList<PackageDto>>
    {
        public List<int>? status { get; set; }    
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class SearchPackageInfoQHandler : IRequestHandler<SearchPackageInfoQ, PagedList<PackageDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public SearchPackageInfoQHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<PagedList<PackageDto>> Handle(SearchPackageInfoQ rq, CancellationToken cancellationToken)
        {
            var reslut = await _repo.SearchPackageInfo(rq.status,rq.page,rq.pageSize);



            return reslut;
        }
    }
}
