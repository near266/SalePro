using System.Runtime.Serialization;

namespace InteractiveSvc.Application.Contract.Contracts
{

    public class BaseListRequest
    {
        public Guid? Id { get; set; }
        public string? Value { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class BaseRequest
    {
        public Guid? Id { get; set; }
        public string? Value { get; set; }
    }
}
