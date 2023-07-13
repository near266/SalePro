using AutoMapper;
using InteractiveSvc.Application.Contract.Contracts;
using InteractiveSvc.Domain.Entities;
using InteractiveSvc.Application.DTO;
using InteractiveSvc.Domain.Entities.Events;
using InteractiveSvc.Domain.Entities.Meetings;
using InteractiveSvc.Application.Commands.Events;
using InteractiveSvc.Application.Commands.UserProfiles;

namespace InteractiveSvc.Application.Configurations.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProfile, UserUpdateDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<UserProfile, UserProfile>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            
            CreateMap<UserUpdateDTO, UserUpdateCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<UserProfile, UserCreateCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            
            CreateMap<Meeting, MeetingUpdateDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            
            CreateMap<Event, EventUpdateDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Event, Event>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Event, EventDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<EventJoin, EventJoinDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            
            CreateMap<Event, EventCreateCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<EventUpdateDTO, EventUpdateCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

            CreateMap<EventJoin, EventJoin>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<EventJoin, EventJoinCreateCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            


            CreateMap<UserProfile, UserProfileResponse>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));


        }
    }
}
