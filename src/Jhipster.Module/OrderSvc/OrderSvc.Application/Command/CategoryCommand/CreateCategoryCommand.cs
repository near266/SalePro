using AutoMapper;
using MediatR;
using OrderSvc.Application.Command.CompanyCommand;
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
    public class CreateCategoryCommand :IRequest<int>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repo;
        public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<int> Handle(CreateCategoryCommand rq, CancellationToken cancellationToken)
        {
            var res = _mapper.Map<Category>(rq);
            return await _repo.AddCate(res);
        }
    }
}
