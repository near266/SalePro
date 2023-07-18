using Jhipster.Service.Utilities;
using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Application.Persistences
{
    public interface ICategoryRepository
    {
        Task<int> AddCate(Category category);
        Task<int> UpdateCate(Category category);
        Task<int> DeleteCate(Guid id);
        Task<PagedList<Category>> SearchOrGetDetail(Guid? id, string? name, int page, int pageSize);
    }
}
