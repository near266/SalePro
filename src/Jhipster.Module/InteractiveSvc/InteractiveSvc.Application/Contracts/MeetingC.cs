using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace InteractiveSvc.Application.Contract.Contracts
{
    public class ActivityRequest
    {
        public Guid? Id { get; set; }
        public Guid? MeetingId { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public string? Banner { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? EndedDate { get; set; }
    }


    public class MeetingRequest
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public string? Fullname { get; set; }
        public string? Avatar { get; set; }
        public string? Major { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? EndedDate { get; set; }

    }

    public class MeetingJoinRequest
    {
        public Guid? Id { get; set; }
        public string UserId { get; set; }
        public Guid MeetingId { get; set; }
        public string? Type { get; set; }
    }

    public class MeetingBannerRequest
    {
        public Guid? Id { get; set; }
        public Guid MeetingId { get; set; }
        public string Value { get; set; }

    }

    public class MeetingActionRequest
    {
        public Guid Id { get; set; }
        public Guid? MeetingId { get; set; }
        public string? Title { get; set; }
        public string? Infor { get; set; }
        public List<string>? Actions { get; set; }
    }

    public class ActivityResponse
    {
        public Guid? Id { get; set; }
        public Guid? MeetingId { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? EndedDate { get; set; }
        public bool? IsUsing { get; set; }
        public string? Banner { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }

    }

    public class MeetingResponse
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public string? Fullname { get; set; }
        public string? Avatar { get; set; }
        public string? Major { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? EndedDate { get; set; }
        public int Quantity { get; set; }
        public List<MeetingBannerResponse>? Banners { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
    public class MeetingJoinResponse
    {

        public Guid? Id { get; set; }
        public string UserId { get; set; }
        public Guid MeetingId { get; set; }
        public string? Title { get; set; }
        public string? PresentorAvatar { get; set; }
        public string? PresentorName { get; set; }
        public string Type { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }

    }


    public class MeetingBannerResponse
    {
        public Guid? Id { get; set; }
        public Guid MeetingId { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class MeetingActionResponse
    {
        public Guid Id { get; set; }
        public Guid? MeetingId { get; set; }
        public string? Title { get; set; }
        public string? Infor { get; set; }
        public List<string>? Actions { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }

}
