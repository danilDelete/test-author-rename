using AutoMapper;
using CsStat.Domain.Entities.Demo;
using ReadFile.ReadDemo.Model;

namespace ReadFile.ReadDemo.Profiles
{
    public class DemoProfile : Profile
    {
        public DemoProfile()
        {
            CreateMap<Kill, KillLog>()
                .ForMember(dest => dest.RoundNumber, opts => opts.MapFrom(src => src.RoundNumber))
                .ForMember(dest => dest.IsHeadshot, opts => opts.MapFrom(src => src.IsHeadshot))
                .ForMember(dest => dest.Weapon, opts => opts.MapFrom(src => src.Weapon))
                .ForMember(dest => dest.Killer, opts => opts.MapFrom(src => src.Killer == null ? (long?) null : src.Killer.SteamID))
                .ForMember(dest => dest.KillerName, opts => opts.MapFrom(src => src.Killer == null ? null : src.Killer.Name))
                .ForMember(dest => dest.Assister, opts => opts.MapFrom(src => src.Assister == null ? (long?) null : src.Assister.SteamID))
                .ForMember(dest => dest.AssisterName, opts => opts.MapFrom(src => src.Assister == null ? null : src.Assister.Name))
                .ForMember(dest => dest.Victim, opts => opts.MapFrom(src => src.Victim == null ? (long?) null : src.Victim.SteamID))
                .ForMember(dest => dest.VictimName, opts => opts.MapFrom(src => src.Victim == null ? null : src.Victim.Name))
                .ForMember(dest => dest.IsSuicide, opts => opts.MapFrom(src => src.IsSuicide))
                ;
        }
    }
}