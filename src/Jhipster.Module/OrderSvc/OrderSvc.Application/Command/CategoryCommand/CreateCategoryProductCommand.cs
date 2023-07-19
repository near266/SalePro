using AutoMapper;
using MediatR;
using OrderSvc.Application.Persistences;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Application.Command.CategoryCommand
{
    public class CreateCategoryProductCommand :IRequest<int>
    {
        [JsonIgnore] public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? CategoryId { get; set; }
    }
    public class CreateCategoryProductCommandHandler : IRequestHandler<CreateCategoryProductCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repo;
        public CreateCategoryProductCommandHandler(IMapper mapper, ICategoryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(CreateCategoryProductCommand rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<CategoryProduct>(rq);
            return await _repo.AddCatePro(res);
        }
    }
}
