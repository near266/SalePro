using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.PackageCommand
{
    public class DeleteCustomerCommand  : IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand,int>
    {
        private readonly IMapper _mapper;
        private readonly IPackageMember _repo;
        public DeleteCustomerCommandHandler(IMapper mapper, IPackageMember repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(DeleteCustomerCommand rq, CancellationToken cancellationToken)
        {
            var check = await _repo.DeleteCus(rq.Id);
            if (check != 0)
            {
                return check;
            }
            else
            {
                throw new ArgumentException(" fail");
            }
        }
    }
}
