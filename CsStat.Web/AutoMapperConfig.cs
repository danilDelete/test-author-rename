using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CSStat.CsLogsApi.Extensions;
using CsStat.Domain.Entities;
using CsStat.Domain.Models;
using CsStat.SystemFacade.Attributes;
using CsStat.Web.Models;

namespace CsStat.Web
{
    public class AutoMapperConfig
    {
        public class PlayerStatProfile : Profile
        {
            public PlayerStatProfile()
            {
                CreateMap<PlayerStatsModel, PlayerStatsViewModel>()
                    .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Player.Id))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Player.NickName))
                    .ForMember(dest => dest.Assists, opts => opts.MapFrom(src => src.Assists))
                    .ForMember(dest => dest.AssistsPerGame, opts => opts.MapFrom(src => src.AssistsPerGame))
                    .ForMember(dest => dest.Deaths, opts => opts.MapFrom(src => src.Death))
                    .ForMember(dest => dest.DeathsPerGame, opts => opts.MapFrom(src => src.DeathPerGame))
                    .ForMember(dest => dest.DefusedBombs, opts => opts.MapFrom(src => src.Defuse))
                    .ForMember(dest => dest.ExplodedBombs, opts => opts.MapFrom(src => src.Explode))
                    .ForMember(dest => dest.FriendlyKills, opts => opts.MapFrom(src => src.FriendlyKills))
                    .ForMember(dest => dest.ImagePath, opts => opts.MapFrom(src => src.Player.ImagePath))
                    .ForMember(dest => dest.KdRatio, opts => opts.MapFrom(src => src.KdRatio))
                    .ForMember(dest => dest.Kills, opts => opts.MapFrom(src => src.Kills))
                    .ForMember(dest => dest.KillsPerGame, opts => opts.MapFrom(src => src.KillsPerGame))
                    .ForMember(dest => dest.Points, opts => opts.MapFrom(src => src.Points))
                    .ForMember(dest => dest.TotalGames, opts => opts.MapFrom(src => src.TotalGames))
                    .ForMember(dest => dest.HeadShot, opts => opts.MapFrom(src => src.HeadShot))
                    .AfterMap((s, d, context) =>
                    {
                        if (s.Guns != null && s.Guns.Any())
                        {
                            d.Guns = context.Mapper.Map<List<GunViewModel>>(s.Guns);
                        }
                        if (s.Achievements != null &&  s.Achievements.Any())
                        {
                            d.Achievements = context.Mapper.Map<List<AchievementViewModel>>(s.Achievements);
                        }

                        if (d.Victims != null && s.Victims.Any())
                        {
                            d.Victims = context.Mapper.Map<List<PlayerViewModel>>(s.Victims);
                        }

                        if (d.Killers != null && s.Killers.Any())
                        {
                            d.Killers = context.Mapper.Map<List<PlayerViewModel>>(s.Killers);
                        }
                    });

                CreateMap<GunModel, GunViewModel>()
                    .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Gun))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Gun.GetDescription()))
                    .ForMember(dest => dest.Kills, opts => opts.MapFrom(src => src.Kills));

                CreateMap<AchieveModel, AchievementViewModel>()
                    .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Achieve))
                    .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Achieve.GetDescription()))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Achieve.GetAttribute<Caption>().Value))
                    ;

                CreateMap<PlayerModel, PlayerViewModel>()
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Count, opts => opts.MapFrom(src => src.Count))
                    .ForMember(dest => dest.ImagePath, opts => opts.MapFrom(src => src.ImagePath))
                    ;
            }



        }

        public class UsefulInfoProfile : Profile
        {
            public UsefulInfoProfile()
            {
                CreateMap<InfoViewModel, UsefulInfo>()
                    .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Caption, opts => opts.MapFrom(src => src.Caption))
                    .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                    .ForMember(dest => dest.PublishDate, opts => opts.MapFrom(src => src.PublishDate))
                    .ForMember(dest => dest.Url, opts => opts.MapFrom(src => src.Url))
                    .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => string.Join(";",src.Tags)))
                    .ForMember(dest => dest.Image, opts => opts.MapFrom(src => src.ImagePath))
                    .ForAllOtherMembers(x=>x.Ignore())
                    ;

                CreateMap<UsefulInfo, InfoViewModel>()
                    .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Caption, opts => opts.MapFrom(src => src.Caption))
                    .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                    .ForMember(dest => dest.PublishDate, opts => opts.MapFrom(src => src.PublishDate))
                    .ForMember(dest => dest.Url, opts => opts.MapFrom(src => src.Url))
                    .ForMember(dest => dest.ImagePath, opts => opts.MapFrom(src => src.Image))
                    .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.Tags.Split(';')))
                    .ForAllOtherMembers(x => x.Ignore())
                    ;
            }
        }

    }
}